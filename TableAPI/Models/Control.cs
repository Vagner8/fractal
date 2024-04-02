using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixAPI.Models
{
    public enum Operation
    {
        None,
        Add,
        Update,
        Remove
    }

    public class Control
    {
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Value { get; set; }

        [ConcurrencyCheck]
        public Guid Version { get; set; }

        [ForeignKey("MatrixId")]
        public Guid? MatrixId { get; set; }
        public Matrix Matrix { get; set; } = null!;

        [ForeignKey("LineId")]
        public Guid? LineId { get; set; }
        public Line Line { get; set; } = null!;

        [ForeignKey("SortId")]
        public Guid? SortId { get; set; }
        public Sort Sort { get; set; } = null!;
    }

    public class ControlDto
    {
        public Guid? Id { get; set; }
        public Operation Operation { get; set; } = Operation.None;
        public required string Name { get; set; }
        public required string Value { get; set; }
    }
}
