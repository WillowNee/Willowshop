using System.Transactions;
using Willownet.BL.Auth;
using Willownet.BL.Exceptions;
using Willowtest.Helpers;

namespace Willowtest
{
    public class RegisterTests : BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task BaseRegistrationTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                string email = new Guid().ToString() + "@test.com";

                // validate: should not be in DB
                await authBL.ValidateEmail(email);

                // create user
                int userId = await authBL.CreateUser(
                    new Willownet.DAL.Models.UserModel()
                    {
                        Email = email,
                        Password = "Qwerty4&qwerty"
                    });

                Assert.Greater(userId, 0);

                var userdalresult = await authDAL.GetUser(userId);
                Assert.That(userdalresult.Email, Is.EqualTo(email));
                Assert.NotNull(userdalresult.Salt);

                var userbyemaildalresult = await authDAL.GetUser(email);
                Assert.That(userdalresult.Email, Is.EqualTo(email));

                // validate: should be in DB
                Assert.Throws<DuplecateEmailException>(delegate { authBL.ValidateEmail(email).GetAwaiter().GetResult(); });

                string encpassword = encrypt.HashPassword("Qwerty4&qwerty", userbyemaildalresult.Salt);
                Assert.That(userbyemaildalresult.Password, Is.EqualTo(encpassword));
            } 
        }
    }
}