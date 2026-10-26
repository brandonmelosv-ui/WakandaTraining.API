
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using WakandaTraining.API.Data;
using WakandaTraining.API.Models;

namespace WakandaTraining.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<Usuario> _hasher = new();

        public UsuariosController(
            AppDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> Cadastrar(
            CadastroUsuarioRequest request)
        {
            string email = request.Email.Trim().ToLowerInvariant();

            bool existe = await _context.Usuarios
                .AnyAsync(u => u.Email == email);

            if (existe)
            {
                return Conflict(new
                {
                    mensagem = "Este e-mail já está cadastrado."
                });
            }

            var usuario = new Usuario
            {
                Nome = request.Nome.Trim(),
                Email = email
            };

            usuario.SenhaHash = _hasher.HashPassword(
                usuario,
                request.Senha
            );

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensagem = "Usuário cadastrado com sucesso.",
                usuario.Id,
                usuario.Nome,
                usuario.Email
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginUsuarioRequest request)
        {
            string email = request.Email.Trim().ToLowerInvariant();

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null ||
                _hasher.VerifyHashedPassword(
                    usuario,
                    usuario.SenhaHash,
                    request.Senha
                ) == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new
                {
                    mensagem = "E-mail ou senha inválidos."
                });
            }

            var chaveJwt = _configuration["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(chaveJwt))
            {
                return StatusCode(500, new
                {
                    mensagem = "A chave JWT não está configurada."
                });
            }

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    usuario.Id.ToString()
                ),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(chaveJwt)
            );

            var credenciais = new SigningCredentials(
                chave,
                SecurityAlgorithms.HmacSha256
            );

            var descritor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = credenciais
            };

            var token = new JsonWebTokenHandler()
                .CreateToken(descritor);

            return Ok(new
            {
                mensagem = "Login realizado com sucesso.",
                token = token,
                usuario.Id,
                usuario.Nome,
                usuario.Email
            });
        }

        // Rota protegida: exige um JWT válido.
        [Authorize]
        [HttpGet("me")]
        public IActionResult MinhaConta()
        {
            return Ok(new
            {
                mensagem = "Token válido. Acesso autorizado.",
                id = User.FindFirst(
                    ClaimTypes.NameIdentifier)?.Value,
                nome = User.Identity?.Name,
                email = User.FindFirst(
                    ClaimTypes.Email)?.Value
            });
        }
    }

    public class CadastroUsuarioRequest
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(8)]
        public string Senha { get; set; } = string.Empty;
    }

    public class LoginUsuarioRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;
    }
}
