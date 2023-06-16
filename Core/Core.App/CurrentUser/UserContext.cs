namespace Core.App.CurrentUser
{
    public class UserContext : IUserContext, ICurrentUserLocator
    {
        public int UserId => 1;//system
    }
}
