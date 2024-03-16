﻿namespace EntityAPI.Models
{
    public class FieldDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
