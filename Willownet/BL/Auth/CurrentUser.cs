using Willownet.BL.General;
using Willownet.BL.Profile;
using Willownet.DAL;
using Willownet.DAL.Models;

namespace Willownet.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IDbSession dbSession;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;
        private readonly IProfileDAL profileDAL;

        public CurrentUser(
            IDbSession dbSession,
            IWebCookie webCookie,
            IUserTokenDAL userTokenDAL,
            IProfileDAL profileDAL) 
        { 
            this.dbSession = dbSession;
            this.webCookie = webCookie;
            this.userTokenDAL = userTokenDAL;
            this.profileDAL = profileDAL;
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

        public async Task<IEnumerable<ProfileModel>> GetProfiles()
        {
            int? userId = await GetCurrentUserId();
            if (userId == null)
                throw new Exception("User does not exist");

            return await profileDAL.GetByUserId((int)userId);
        }
    }
}
