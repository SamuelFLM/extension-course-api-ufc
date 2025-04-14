namespace api_rest.Domain.Repositories
{
    public interface IUnityOfWork
    {
        Task CompleteAsync();
    }
}
