using GESTION_DES_STOCKS.Data;
using GESTION_DES_STOCKS.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GESTION_DES_STOCKS.Services
{
    public interface IAuthService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<bool> RegisterAsync(User user, string password);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
        Task<bool> ResetPasswordAsync(string email);
        string GenerateJwtToken(User user);
        ClaimsPrincipal? ValidateToken(string token);
    }

    public class AuthService : IAuthService
    {
        private readonly StockDbContext _context;
        private readonly string _jwtSecret = "votre_secret_jwt_tres_securise_ici_2024";

        public AuthService(StockDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

                if (user == null)
                    return null;

                if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return user;
                }

                return null;
            }
            catch (Exception ex)
            {
                // Log l'erreur
                System.Diagnostics.Debug.WriteLine($"Erreur d'authentification: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> RegisterAsync(User user, string password)
        {
            try
            {
                // Vérifier si l'utilisateur existe déjà
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == user.Username || u.Email == user.Email);

                if (existingUser != null)
                    return false;

                // Hasher le mot de passe
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                user.CreatedAt = DateTime.Now;
                user.IsActive = true;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur d'enregistrement: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                    return false;

                if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                    return false;

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur de changement de mot de passe: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ResetPasswordAsync(string email)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);

                if (user == null)
                    return false;

                // Générer un nouveau mot de passe temporaire
                var tempPassword = GenerateTemporaryPassword();
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(tempPassword);

                await _context.SaveChangesAsync();

                // Ici, vous pourriez envoyer un email avec le mot de passe temporaire
                // Pour l'instant, on affiche juste dans la console
                System.Diagnostics.Debug.WriteLine($"Nouveau mot de passe temporaire pour {email}: {tempPassword}");

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur de réinitialisation: {ex.Message}");
                return false;
            }
        }

        public string GenerateJwtToken(User user)
        {
            // Implémentation simplifiée - dans un vrai projet, utilisez JWT.NET
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            // Pour l'instant, retournons un token simple
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{user.Username}:{user.Role}"));
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            try
            {
                // Implémentation simplifiée
                var decoded = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var parts = decoded.Split(':');
                
                if (parts.Length == 2)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, parts[0]),
                        new Claim(ClaimTypes.Role, parts[1])
                    };

                    var identity = new ClaimsIdentity(claims, "Custom");
                    return new ClaimsPrincipal(identity);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private string GenerateTemporaryPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
} 