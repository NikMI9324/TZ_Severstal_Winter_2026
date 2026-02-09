using Microsoft.EntityFrameworkCore;
using Severstal.Domain.Entities;
using Severstal.Domain.Exeptions;
using Severstal.Domain.Interfaces;
using Severstal.Infrastructure.Data;
using Severstal.Infrastructure.Extensions;

namespace Severstal.Infrastructure.Repositories
{
    public class RollCrudRepository : IRollCrudRepository
    {
        private readonly AppDbContext _context;

        public RollCrudRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Roll> AddRollAsync(Roll roll, CancellationToken ct = default)
        {
            await _context.Rolls.AddAsync(roll, ct);
            await _context.SaveChangesAsync(ct);
            return roll;
        }

        public async Task<Roll> RemoveRollAsync(int id, CancellationToken ct = default)
        {
            var roll = await _context.Rolls.FirstOrDefaultAsync(r => r.Id == id, ct);
            if (roll == null)
                throw new RollNotFoundException(id);

            if (roll.IsRemoved)
                throw new InvalidOperationException($"Рулон с Id : {id} уже удален");

            var removedDate = DateTime.Today;
            await _context.Rolls.Where(r => r.Id == id)
                .ExecuteUpdateAsync(setters => setters.SetProperty(r => r.RemovedDate, removedDate), ct);

            roll.RemovedDate = removedDate;
            return roll;
        }

        public async Task<IEnumerable<Roll>> GetFilteredDataAsync(
            int? idFrom, int? idTo,
            double? weightFrom, double? weightTo,
            double? lengthFrom, double? lengthTo,
            DateTime? dateAddFrom, DateTime? dateAddTo,
            DateTime? dateRemoveFrom, DateTime? dateRemoveTo,
            CancellationToken ct = default)
        {
            var query = _context.Rolls.AsNoTracking();

            query = query
                .ApplyRangeIfExists(idFrom, idTo, r => r.Id)
                .ApplyRangeIfExists(weightFrom, weightTo, r => r.Weight)
                .ApplyRangeIfExists(lengthFrom, lengthTo, r => r.Length)
                .ApplyRangeIfExists(dateAddFrom, dateAddTo, r => r.AddedDate)
                .ApplyRangeIfExists(dateRemoveFrom, dateRemoveTo, r => r.RemovedDate);

            return await query.ToListAsync(ct);
        }

        
    }
}