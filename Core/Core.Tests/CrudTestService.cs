using Core.Web.WebClient;

namespace Core.Tests
{
    public abstract class CrudTestService<TModel> 
        : CrudTestService<TModel, int>
    {
        public CrudTestService(RestClient client) 
            : base(client)
        {
        }
    }

    public abstract class CrudTestService<TModel, TId> 
        : ICrudTestService<TModel, TId>
    {
        protected readonly RestClient client;

        public abstract string Url { get; }

        public CrudTestService(RestClient client)
        {
            this.client = client;
        }

        private RestRequest CreateRequest(TId id)
        {
            return new RestRequest(Url + "/" + id);
        }

        public virtual TModel Get(TId id)
        {
            var request = CreateRequest(id);
            return client.Get<TModel>(request);
        }

        public virtual TModel Create(TModel model)
        {
            var request = new RestRequest(Url);
            request.AddJsonContent(model);

            return client.Post<TModel>(request);
        }

        public virtual TModel Update(TId id, TModel model)
        {
            var request = CreateRequest(id);
            request.AddJsonContent(model);

            return client.Put<TModel>(request);
        }

        public virtual void Delete(TId id)
        {
            var request = CreateRequest(id);
            client.Delete(request);
        }
    }
}
