using Severstal.Domain.Entities;

namespace Severstal.Domain.Interfaces
{
    public interface IRollCrudRepository
    {
        Task<Roll> AddRollAsync(Roll roll, CancellationToken cancellationToken = default);
        Task<Roll> RemoveRollAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Roll>> GetFilteredDataAsync(
            int? idFrom,
            int? idTo,
            double? weightFrom,
            double? weightTo,
            double? lengthFrom,
            double? lengthTo,
            DateTime? dateAddFrom,
            DateTime? dateAddTo,
            DateTime? dateRemoveFrom,
            DateTime? dateRemoveTo,
            CancellationToken cancellationToken = default
            );
    }
}
