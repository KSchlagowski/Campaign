using System;

namespace CampaignApi.Core.Models
{
    public class UserModel
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Username { get; protected set; }
        public string FullName { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected UserModel()
        {
        }

        public UserModel(string email, string password, 
            string salt, string username)
            {
                Id = Guid.NewGuid();
                Email = email.ToLowerInvariant();
                Username = username;
                Password = password;
                Salt = salt;
                CreatedAt = DateTime.UtcNow;
            }
    }
}