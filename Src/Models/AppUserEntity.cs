﻿namespace Models
{
    public class AppUserEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}