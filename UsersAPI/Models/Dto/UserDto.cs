﻿namespace UsersAPI.Models.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime Updated { get; set; }
    }
}
