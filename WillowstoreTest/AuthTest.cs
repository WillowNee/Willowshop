using System.Transactions;
using Willowstore.BL.Auth;
using Willowstore.BL.Exceptions;
using WillowstoreTest.Helpers;

namespace WillowstoreTest
{
    public class AuthTest : BaseTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task AuthRegistrationTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                string email = new Guid().ToString() + "@test.com";

                // create user
                int userId = await authBL.CreateUser(
                    new Willowstore.DAL.Models.UserModel()
                    {
                        Email = email,
                        Password = "Qwerty4&qwerty"
                    });

                // wrong email wrong pass
                Assert.Throws<AuthorizationException>(
                    delegate { authBL.Authenticate("ggg", "111", false).GetAwaiter().GetResult(); });

                // right email wrong pass
                Assert.Throws<AuthorizationException>(
                    delegate { authBL.Authenticate(email, "111", false).GetAwaiter().GetResult(); });

                // wrong email right pass
                Assert.Throws<AuthorizationException>(
                    delegate { authBL.Authenticate("ggg", "Qwerty4&qwerty", false).GetAwaiter().GetResult(); });

                // right email right pass
                await authBL.Authenticate(email, "Qwerty4&qwerty", false);

                string? authCookie = webCookie.Get(AuthConstants.SessionCookieName);
                Assert.NotNull(authCookie);

                string? rememberMeCookie = webCookie.Get(AuthConstants.RememberMeCookieName);
                Assert.Null(rememberMeCookie);
            }
        }

        [Test]
        public async Task RememberMeTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@test.com";

                int userId = await authBL.CreateUser(
                    new Willowstore.DAL.Models.UserModel()
                    {
                        Email = email,
                        Password = "Qwerty4&qwerty"
                    });

                await authBL.Authenticate(email, "Qwerty4&qwerty", true);

                string? authCookie = this.webCookie.Get(AuthConstants.SessionCookieName);
                Assert.NotNull(authCookie);

                string? rememberMeCookie = this.webCookie.Get(AuthConstants.RememberMeCookieName);
                Assert.NotNull(rememberMeCookie);
            }
        }
    }
}
