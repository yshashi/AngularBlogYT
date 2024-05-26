namespace Domain;

public interface IUnitOfWork
{
    void Commit();
    Task CommitAsync();
}
