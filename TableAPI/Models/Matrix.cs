using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixAPI.Models
{
    public class Matrix
    {
        [Key]
        public Guid Id { get; set; }
        public ICollection<Control> Controls { get; set; } = [];
        public ICollection<Line> Lines { get; set; } = [];
    }

    public class MatrixDto
    {
        public Guid? Id { get; set; }
        public List<LineDto> Lines { get; set; } = [];
        public List<ControlDto> Controls { get; set; } = [];
    }
}
