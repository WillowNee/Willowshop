using Willowstore.BL.General;
using Willowstore.DAL;

namespace Willowstore.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IDbSession dbSession;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;

        public CurrentUser(
            IDbSession dbSession,
            IWebCookie webCookie,
            IUserTokenDAL userTokenDAL) 
        { 
            this.dbSession = dbSession;
            this.webCookie = webCookie;
            this.userTokenDAL = userTokenDAL;
        }

        public async Task<int?> GetUserIdByToken()
        {
            string? tokenCookie = webCookie.Get(AuthConstants.RememberMeCookieName);

            if (tokenCookie == null)
                return null;

            Guid? tokenGuid = Helpers.StringToGuidDef(tokenCookie ?? "");

            if (tokenGuid == null) 
                return null;

            int? userId = await userTokenDAL.Get((Guid)tokenGuid);

            return userId;
        }

        public async Task<bool> IsLoggedIn()
        {
            bool isLoggedIn = await dbSession.IsLoggedIn();

            if (!isLoggedIn)
            {
                int? userId = await GetUserIdByToken();

                if (userId != null)
                {
                    await dbSession.SetUserId((int)userId);
                    isLoggedIn = true;
                }
            }

            return isLoggedIn;
        }

        public async Task<int?> GetCurrentUserId()
        {
            return await dbSession.GetUserId();
        }

        public bool IsAdmin()
		{
			if (dbSession.GetValueDef(AuthConstants.AdminRoleKey, "").ToString() == AuthConstants.AdminRoleAbbr)
				return true;
			return false;
        }
    }
}
