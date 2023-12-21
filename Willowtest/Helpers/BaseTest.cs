using Willownet.BL.Auth;
using Willownet.BL.General;
using Willownet.BL.Profile;
using Willownet.DAL;

namespace Willowtest.Helpers
{
    // DI helper tool
    public class BaseTest
    {
        protected IAuthDAL authDAL = new AuthDAL();
        protected IEncrypt encrypt = new Encrypt();        
        protected IDbSessionDAL sessionDAL = new DbSessionDAL();
        protected IUserTokenDAL userTokenDAL = new UserTokenDAL();
        protected IProfileDAL profileDAL = new ProfileDAL();
        protected ISkillDAL skillDAL = new SkillDAL();

        protected IAuth authBL;
        protected IDbSession sessionBL;
        protected IWebCookie webCookie;
        protected ICurrentUser currentUser;
        protected IProfile profile;

        public BaseTest()
        {
            webCookie = new TestCookie();
            sessionBL = new DbSession(sessionDAL, webCookie);
            authBL = new Auth(authDAL, encrypt, webCookie, sessionBL, userTokenDAL);
            currentUser = new CurrentUser(sessionBL, webCookie, userTokenDAL, profileDAL);
            profile = new Profile(profileDAL, skillDAL);
        }  
    }
}
