using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Willowtest.Helpers;

namespace Willowtest
{
    public class ProfileTest : BaseTest
    {
        [Test]
        public async Task AddTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                await profile.AddOrUpdate(
                    new Willownet.DAL.Models.ProfileModel()
                    {
                        UserId = 44,
                        FirstName = "First",
                        LastName = "Last",
                        ProfileName = "Test"
                    });

                // return IEnumerable -> count must be 1
                var results = await profile.Get(44);
                Assert.That(results.Count(), Is.EqualTo(1));

                var result = results.First();
                Assert.That(result.UserId, Is.EqualTo(44));
                Assert.That(result.FirstName, Is.EqualTo("First"));
                Assert.That(result.LastName, Is.EqualTo("Last"));
                Assert.That(result.ProfileName, Is.EqualTo("Test"));
            }
        }

        [Test]
        public async Task UpdateTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                var profileModel = new Willownet.DAL.Models.ProfileModel()
                {
                    UserId = 44,
                    FirstName = "First",
                    LastName = "Last",
                    ProfileName = "Test"
                };

                await profile.AddOrUpdate(profileModel);

                profileModel.FirstName = "First4";

                await profile.AddOrUpdate(profileModel);

                var results = await profile.Get(44);
                Assert.That(results.Count(), Is.EqualTo(1));

                var result = results.First();
                Assert.That(result.UserId, Is.EqualTo(44));
                Assert.That(result.FirstName, Is.EqualTo("First4"));
            }
        }
    }
}
