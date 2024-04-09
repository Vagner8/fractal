using MatrixAPI.Models;
using MatrixAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace MatrixAPI.Services
{
    public class MatrixService(
        AppDbContext db,
        IMapService map,
        IControlService control,
        IResponseService response) : IMatrixService
    {
        private readonly AppDbContext _db = db;
        private readonly IMapService _map = map;
        private readonly IControlService _control = control;
        private readonly IResponseService _response = response;

        public async Task<ResponseDto> Add(MatrixDto matrixDto)
        {
            var matrix = _map.ToMatrix(matrixDto);
            var entry = await _db.Matrices.AddAsync(matrix);
            await SaveChangesAsync();
            return _response.Data(_map.ToMatrixDto(entry.Entity));
        }

        public async Task<ResponseDto> Update(MatrixDto matrixDto)
        {
            var matrix = await GetMatrix(matrixDto.Id);
            UpdateMatrixControls(matrix.Controls, matrixDto.Controls);
            UpdateLineControls(matrix.Lines, matrixDto.Lines);
            await SaveChangesAsync();
            return _response.Data(_map.ToMatrixDto(matrix));
        }
        public async Task<Matrix> GetMatrix(Guid? id)
        {
            return await _db.Matrices
                .Include(m => m.Controls)
                .Include(m => m.Lines)
                .ThenInclude(l => l.Controls)
                .FirstOrDefaultAsync(m => m.Id == id) ?? throw new Exception($"Find matrix: {id}");
        }

        public async Task<List<Matrix>> GetMatrices()
        {
            return await _db.Matrices
                .Include(m => m.Controls)
                .Include(m => m.Lines)
                .ThenInclude(l => l.Controls)
                .ToListAsync() ?? throw new Exception("Find matrixs");
        }

        private void UpdateMatrixControls(ICollection<Control> controls, List<ControlDto> controlsDto)
        {
            UpdateControls(controls, controlsDto);
        }

        private void UpdateLineControls(ICollection<Line> lines, List<LineDto> linesDto)
        {
            foreach (var lineDto in linesDto)
            {
                var line = GetLine(lines, lineDto.Id);
                UpdateControls(line.Controls, lineDto.Controls);
            }
        }

        private void UpdateControls(ICollection<Control> controls, List<ControlDto> controlsDto)
        {
            foreach (var controlDto in controlsDto)
            {
                switch (controlDto.Operation)
                {
                    case Operation.Add:
                        _control.Add(controls, controlDto);
                        break;
                    case Operation.Update:
                        var controlToUpdate = _control.GetControl(controls, controlDto.Id);
                        _control.Update(controlToUpdate, controlDto);
                        break;
                    case Operation.Remove:
                        var controlToDelete = _control.GetControl(controls, controlDto.Id);
                        _control.Remove(controls, controlToDelete);
                        break;
                }
            }
        }

        private static Line GetLine(ICollection<Line> lines, Guid? id)
        {
            return lines.FirstOrDefault(l => l.Id == id) ?? throw new Exception($"Find line: {id}");
        }

        private async Task SaveChangesAsync()
        {
            var maxAttempts = 5;
            var attempts = 0;
            var saved = false;
            while (!saved)
            {
                try
                {
                    await _db.SaveChangesAsync();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    attempts++;
                    if (attempts > maxAttempts)
                    {
                        throw new Exception("Maximum retry attempts exceeded");
                    }
                    foreach (var entry in ex.Entries)
                    {
                        var name = entry.Metadata.Name;
                        if (entry.Entity is Control)
                        {
                            var databaseValues = entry.GetDatabaseValues()
                                ?? throw new Exception($"Entity {name} was deleted by another user");
                            entry.OriginalValues.SetValues(databaseValues);
                            await _db.SaveChangesAsync();
                        }
                        else
                        {
                            throw new NotSupportedException($"Can't handle concurrency conflicts for {name}");
                        }
                    }
                }
            }
        }
    }

    public interface IMatrixService
    {
        public Task<ResponseDto> Add(MatrixDto matrixDto);
        public Task<ResponseDto> Update(MatrixDto matrixDto);
        public Task<Matrix> GetMatrix(Guid? id);
        public Task<List<Matrix>> GetMatrices();
    }
}
