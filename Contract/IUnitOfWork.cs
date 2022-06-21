namespace Contract
{
    public interface IUnitOfWork
    {
        void SaveChange();
        void SaveChangeAsync();
    }
}
