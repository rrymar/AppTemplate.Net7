using System.Linq;
using AppTemplate.Users.Database;
using Core.Web.Crud;
using Microsoft.EntityFrameworkCore;

namespace AppTemplate.Users.UserManagement.Search
{
    public class SearchUsersService
    {
        private readonly UsersDataContext dataContext;

        private readonly UserMapper mapper;

        public SearchUsersService(UsersDataContext dataContext, UserMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public ResultsList<UserModel> Search(SearchQuery query)
        {
            var queryable = dataContext.Users.Where(e => e.IsActive);
            if (!string.IsNullOrWhiteSpace(query.Keyword))
                queryable.Where(e => EF.Functions.Like(e.FullName, query.Keyword + "%"));

            var totalCount = queryable.Count();
            var items = queryable.ApplyPagingAndSorting(query)
                .Select(mapper.ToModel)
                .ToList();
            return new ResultsList<UserModel>(items, totalCount);
        }
    }
}
