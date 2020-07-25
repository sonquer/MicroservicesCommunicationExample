using System;

namespace Users.Api.Application.Models
{
    public class User
    {
        public long Id { get; protected set; }

        public string Email { get; protected set; }

        public string Name { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public User(long id, string email, string name, DateTime createdAt)
        {
            Id = id;
            Email = email;
            Name = name;
            CreatedAt = createdAt;
        }
    }
}
