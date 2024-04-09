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

        public Line ToLine(LineDto lineDto)
        {
            return new Line
            {
                Controls = lineDto.Controls.Select(ToControl).ToList(),
            };
        }

        public LineDto ToLineDto(Line line)
        {
            return new LineDto
            {
                Id = line.Id,
                Controls = line.Controls.Select(ToControlDto).ToList(),
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
    }

    public interface IMapService
    {
        public Matrix ToMatrix(MatrixDto matrixDto);
        public MatrixDto ToMatrixDto(Matrix matrix);
        public Line ToLine(LineDto lineDto);
        public LineDto ToLineDto(Line line);
        public Control ToControl(ControlDto controlDto);
        public ControlDto ToControlDto(Control control);
    }
}
