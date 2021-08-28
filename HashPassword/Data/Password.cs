using static BCrypt.Net.BCrypt;

namespace CHLabs.Functions.Data
{
    public class Password
    {
        public string PlainTextPassword { get; set; }
        public string HashedPassword { get; set; }

        public void Hash(string salt)
            => HashedPassword = HashPassword(PlainTextPassword, salt);
    }
}