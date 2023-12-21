using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Willownet.BL.Exceptions;
using Willowtest.Helpers;

namespace Willowtest
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
                    new Willownet.DAL.Models.UserModel()
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
            }
        }
    }
}
