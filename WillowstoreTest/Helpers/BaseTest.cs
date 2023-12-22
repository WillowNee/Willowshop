using Willowstore.BL.Auth;
using Willowstore.BL.General;
using Willowstore.DAL;

namespace WillowstoreTest.Helpers
{
    // DI helper tool
    public class BaseTest
    {
        protected IAuthDAL authDAL = new AuthDAL();
        protected IEncrypt encrypt = new Encrypt();        
        protected IDbSessionDAL sessionDAL = new DbSessionDAL();
        protected IUserTokenDAL userTokenDAL = new UserTokenDAL();

        protected IAuth authBL;
        protected IDbSession sessionBL;
        protected IWebCookie webCookie;
        protected ICurrentUser currentUser;

        public BaseTest()
        {
            DbHelper.ConnString = "User ID=postgres;Password=password;Host=localhost;Port=5433;Database=store";

            webCookie = new TestCookie();
            sessionBL = new DbSession(sessionDAL, webCookie);
            authBL = new Auth(authDAL, encrypt, webCookie, sessionBL, userTokenDAL);
            currentUser = new CurrentUser(sessionBL, webCookie, userTokenDAL);
        }  
    }
}
