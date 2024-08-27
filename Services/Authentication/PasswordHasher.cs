using BCrypt.Net;

namespace OdaMeClone.Services
    {
    public class PasswordHasher : IPasswordHasher
        {
        // Configuration options for BCrypt
        private const int WorkFactor = 12;

        // Hashes the password with a strong salt
        public string HashPassword(string password)
            {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: WorkFactor);
            }

        // Verifies the password against the hashed password
        public bool VerifyPassword(string password, string hashedPassword)
            {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
        }
    }
