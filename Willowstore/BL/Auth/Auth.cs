using Willowstore.DAL.Models;
using Willowstore.DAL;
using Willowstore.BL.Exceptions;
using System.Transactions;
using Willowstore.BL.General;

namespace Willowstore.BL.Auth
{
    public class Auth : IAuth
    {
        private readonly IAuthDAL authDAL;
        private readonly IEncrypt encrypt;
        private readonly IDbSession dbSession;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;

        public Auth(IAuthDAL authDAL, 
                IEncrypt encrypt,
                IWebCookie webCookie, 
                IDbSession dbSession,
                IUserTokenDAL userTokenDAL) 
        { 
            this.authDAL = authDAL;
            this.encrypt = encrypt;
            this.webCookie = webCookie;
            this.dbSession = dbSession;
            this.userTokenDAL = userTokenDAL;
        }

        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);
            int id = await authDAL.CreateUser(user);
            await Login(id);
            return id;
        }

        public async Task Login(int id)
        {
            await dbSession.SetUserId(id);

            var roles = await authDAL.GetRoles(id);
            if (roles.Any(m => m.Abbreviation == AuthConstants.AdminRoleAbbr))
            {
                dbSession.AddValue(AuthConstants.AdminRoleKey, AuthConstants.AdminRoleAbbr);
                await dbSession.UpdateSessionData();
            }
        }

        public async Task<int> Authenticate(string email, string password, bool rememberMe)
        {
            var user = await authDAL.GetUser(email);

            if(user.UserId != null && user.Password == encrypt.HashPassword(password, user.Salt))
            {
                await Login(user.UserId ?? 0);

                if (rememberMe)
                {
                    Guid tokenId = await userTokenDAL.Create(user.UserId ?? 0);
                    webCookie.AddSecure(AuthConstants.RememberMeCookieName, tokenId.ToString(), AuthConstants.RememberMeDays);
                }

                return user.UserId ?? 0;
            }

            throw new AuthorizationException();
        }

        public async Task ValidateEmail (string email)
        {
            var user = await authDAL.GetUser(email);

            if (user.UserId != null)
                throw new DuplecateEmailException();
        }

        public async Task RegisterUser(UserModel user)
        {
            using (TransactionScope scope = Helpers.CreateTransactionScope())
            {
                await dbSession.Lock();
                await ValidateEmail(user.Email);
                await CreateUser(user);
                scope.Complete();
            }
        }
    }
}
