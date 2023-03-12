using MyAspDotNet6App.Pages.MasterMaintenance;

namespace MyAspDotNet6App.Domain
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetUsers(UserSearchCondition? searchCondition)
        {
            return _userRepository.GetUsers(searchCondition);
        }
    }
}
