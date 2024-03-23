using System.ComponentModel.DataAnnotations;

namespace EntityAPI.Models
{
    public class Sort
    {
        [Key] public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Position { get; set; }
        public Guid Entityid { get; set; }
    }
    public class SortDto
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public int Position { get; set; }
    }
}
