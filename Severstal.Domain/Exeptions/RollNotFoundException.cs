namespace Severstal.Domain.Exeptions
{
    public class RollNotFoundException : Exception
    {
        public int Id { get; }
        public RollNotFoundException(int id) : base($"Рулон с ID : {id} не найден") => Id = id;
    }
}
