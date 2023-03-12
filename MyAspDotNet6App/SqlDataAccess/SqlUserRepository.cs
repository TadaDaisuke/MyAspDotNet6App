using MyAspDotNet6App.Common;
using MyAspDotNet6App.Domain;
using MyAspDotNet6App.Pages.MasterMaintenance;
using MyAspDotNet6App.SqlDataAccess.Common;
using System.Data;
using System.Text;

namespace MyAspDotNet6App.SqlDataAccess
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly MyAppContext _context;

        public SqlUserRepository(MyAppContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers(UserSearchCondition? searchCondition)
        {
            var cmd = new StringBuilder()
                .AppendLine("SELECT [user_id]")
                .AppendLine("    ,[user_name]")
                .AppendLine("    ,mail_address")
                .AppendLine("FROM [user]")
                .AppendLine("WHERE (")
                .AppendLine("        @user_name_part IS NULL")
                .AppendLine("        OR [user_name] LIKE N'%' + @user_name_part + N'%'")
                .AppendLine("        )")
                .ToSqlCommand()
                .AddParameter("@user_name_part", SqlDbType.NVarChar, searchCondition?.UserNamePart);
            return _context.GetRowList(cmd)
                .Select(row =>
                    new User()
                    {
                        UserId = row["user_id"].ToInt(),
                        UserName = row["user_name"] ?? string.Empty,
                        MailAddress = row["mail_address"]
                    })
                .ToList();
        }
    }
}
