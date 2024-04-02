using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixAPI.Models
{
    public class Matrix
    {
        [Key]
        public Guid Id { get; set; }
        public ICollection<Line> Lines { get; set; } = [];
        public ICollection<Control> Controls { get; set; } = [];

        [ForeignKey("SortId")]
        public Guid? SortId { get; set; }
        public Sort Sort { get; set; } = null!;
    }

    public class MatrixDto
    {
        public Guid? Id { get; set; }
        public List<LineDto> Lines { get; set; } = [];
        public List<ControlDto> Controls { get; set; } = [];
        public List<SortDto> Sorts { get; set; } = [];
    }
}
