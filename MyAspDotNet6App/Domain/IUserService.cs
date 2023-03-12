using MyAspDotNet6App.Pages.MasterMaintenance;

namespace MyAspDotNet6App.Domain
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers(UserSearchCondition? searchCondition);
    }
}
