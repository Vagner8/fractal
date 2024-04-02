using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace MatrixAPI.Models
{
    public class Line
    {
        [Key]
        public Guid Id { get; set; }
        public ICollection<Control> Controls { get; set; } = [];

        [ForeignKey("MatrixId")]
        public Guid? MatrixId { get; set; }
        public Matrix Matrix { get; set; } = null!;
    }

    public class LineDto
    {
        public Guid? Id { get; set; }
        public List<ControlDto> Controls { get; set; } = [];
    }
}
