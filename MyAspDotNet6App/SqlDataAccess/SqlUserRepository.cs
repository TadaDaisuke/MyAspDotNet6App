using Microsoft.Data.SqlClient;
using MyAspDotNet6App.Common;
using MyAspDotNet6App.Domain;
using MyAspDotNet6App.Pages.MasterMaintenance;
using MyAspDotNet6App.SqlDataAccess.Common;
using System.Data;

namespace MyAspDotNet6App.SqlDataAccess
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly MyAppContext _context;

        public SqlUserRepository(MyAppContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers(SearchCondition searchCondition)
        {
            var cmd = new SqlCommand("SELECT * FROM [user] WHERE user_name LIKE N'%' + @user_name_part + N'%'")
                .AddParameter("@user_name_part", SqlDbType.NVarChar, searchCondition.UserNamePart);
            return _context.GetRowList(cmd)
                .Select(row =>
                    new User()
                    {
                        UserId = row["user_id"].ToInt(),
                        UserName = row["user_name"],
                        MailAddress = row["mail_address"]
                    })
                .ToList();
        }
    }
}
