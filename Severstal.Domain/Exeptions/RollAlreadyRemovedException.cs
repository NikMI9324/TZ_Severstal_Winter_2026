namespace Severstal.Domain.Exeptions
{
    public class RollAlreadyRemovedException : Exception
    {
        public int Id { get; }
        public bool IsRemoved { get; }
        public RollAlreadyRemovedException(int id, bool isRemoved) : 
            base($"Рулон с Id: {id} уже удален") => (Id, IsRemoved) = (id, isRemoved);
    }
}
