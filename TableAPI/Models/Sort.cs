using System.ComponentModel.DataAnnotations;

namespace MatrixAPI.Models
{
    public class Sort
    {
        [Key]
        public Guid Id { get; set; }
        public ICollection<Control> Controls { get; set; } = [];
        public ICollection<Matrix> Matrices { get; set; } = [];
    }

    public class SortDto
    {
        public Guid? Id { get; set; }
        public List<ControlDto> Controls { get; set; } = [];
    }
}
