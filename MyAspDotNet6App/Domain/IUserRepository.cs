using MyAspDotNet6App.Pages.MasterMaintenance;

namespace MyAspDotNet6App.Domain
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetUsers(SearchCondition searchCondition);
    }
}
