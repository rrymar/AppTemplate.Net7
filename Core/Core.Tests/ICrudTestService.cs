namespace Core.Tests
{
    public interface ICrudTestService<TModel>
        : ICrudTestService<TModel, int>
    {
    }

    public interface ICrudTestService<TModel, TId>
    {
        string Url { get; }

        TModel Create(TModel model);

        TModel Get(TId id);

        TModel Update(TId id, TModel model);

        void Delete(TId id);
    }
}