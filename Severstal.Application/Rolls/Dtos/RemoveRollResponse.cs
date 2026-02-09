namespace Severstal.Application.Rolls.Dtos
{
    public record RemoveRollResponse
    {
        public int Id { get; init; }
        public string Message { get; init; }
        public double Length { get; init; }
        public double Weight { get; init; }
        public DateTime AddedDate { get; init; }
        public DateTime? RemovedDate { get; init; }
    }
}
