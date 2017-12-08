namespace Infrastructure.Repository
{
    public interface IDBContext
    {      
        IUnitOfWork Create(string contextType);
    }
}
