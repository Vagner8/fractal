using MatrixAPI.Models;

namespace MatrixAPI.Services
{
    public class MapService : IMapService
    {
        public Matrix ToMatrix(MatrixDto matrixDto)
        {
            return new Matrix
            {
                Lines = matrixDto.Lines.Select(ToLine).ToList(),
                Controls = matrixDto.Controls.Select(ToControl).ToList(),
            };
        }

        public MatrixDto ToMatrixDto(Matrix matrix)
        {
            return new MatrixDto
            {
                Id = matrix.Id,
                Lines = matrix.Lines.Select(ToLineDto).ToList(),
                Controls = matrix.Controls.Select(ToControlDto).ToList(),
            };
        }

        public Line ToLine(LineDto rowDto)
        {
            return new Line
            {
                Controls = rowDto.Controls.Select(ToControl).ToList(),
            };
        }

        public LineDto ToLineDto(Line unit)
        {
            return new LineDto
            {
                Id = unit.Id,
                Controls = unit.Controls.Select(ToControlDto).ToList(),
            };
        }

        public Control ToControl(ControlDto controlDto)
        {
            return new Control
            {
                Name = controlDto.Name,
                Value = controlDto.Value,
            };
        }

        public ControlDto ToControlDto(Control control)
        {
            return new ControlDto
            {
                Id = control.Id,
                Name = control.Name,
                Value = control.Value,
                Operation = Operation.None,
            };
        }

        public Sort ToSort(SortDto sortDto)
        {
            return new Sort
            {
                Controls = sortDto.Controls.Select(ToControl).ToList(),
            };
        }

        public SortDto ToSortDto(Sort sort)
        {
            return new SortDto
            {
                Controls = sort.Controls.Select(ToControlDto).ToList(),
            };
        }
    }

    public interface IMapService
    {
        public Matrix ToMatrix(MatrixDto matrixDto);
        public MatrixDto ToMatrixDto(Matrix matrix);
        public Line ToLine(LineDto rowDto);
        public LineDto ToLineDto(Line unit);
        public Control ToControl(ControlDto controlDto);
        public ControlDto ToControlDto(Control control);
        public Sort ToSort(SortDto sortDto);
        public SortDto ToSortDto(Sort sort);
    }
}
