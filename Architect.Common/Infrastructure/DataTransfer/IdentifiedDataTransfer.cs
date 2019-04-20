namespace Architect.Common.Infrastructure.DataTransfer
{
    public abstract class IdentifiedDataTransfer
    {
        public IdentifiedDataTransfer(int id)
        {
            if (id <= 0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(id));
            }

            Id = id;
        }

        public virtual int Id { get; set; }
    }
}
