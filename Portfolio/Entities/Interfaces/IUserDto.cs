﻿namespace Portfolio.Entities.Interfaces
{
    public interface IUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
