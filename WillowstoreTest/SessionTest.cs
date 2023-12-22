using System.Text.Json;
using System.Transactions;
using WillowstoreTest.Helpers;

namespace WillowstoreTest
{
    public class SessionTest : BaseTest
    {
        [Test]
        [NonParallelizable]
        public async Task CreateSessionTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.sessionBL.ResetSessionCache();
                var session = await this.sessionBL.GetSession();

                var dbSession = await this.sessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSession, "Session should not be null");

                Assert.That(dbSession.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.sessionBL.GetSession();
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task CreateAuthorizedSessionTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.sessionBL.ResetSessionCache();
                var session = await this.sessionBL.GetSession();

                await this.sessionBL.SetUserId(10);
                var dbSession = await this.sessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSession, "Session should not be null");

                Assert.That(dbSession.UserId, Is.EqualTo(10));

                Assert.That(dbSession.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.sessionBL.GetSession();
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));

                int? userId = await this.currentUser.GetCurrentUserId();
                Assert.That(userId, Is.EqualTo(10));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task AddValue()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.sessionBL.ResetSessionCache();
                var session = await this.sessionBL.GetSession();

                this.sessionBL.AddValue("Test", "TestValue");
                await this.sessionBL.UpdateSessionData();

                Assert.That(this.sessionBL.GetValueDef("Test", ""), Is.EqualTo("TestValue"));

                await this.sessionBL.SetUserId(10);

                var dbSession = await this.sessionDAL.Get(session.DbSessionId);
                var SessionData = JsonSerializer.Deserialize<Dictionary<string, object>>(dbSession?.SessionData ?? "");

                Assert.IsTrue(SessionData?.ContainsKey("Test"));
                Assert.That(SessionData?["Test"].ToString(), Is.EqualTo("TestValue"));

                this.sessionBL.ResetSessionCache();
                session = await this.sessionBL.GetSession();
                Assert.That(this.sessionBL.GetValueDef("Test", "").ToString(), Is.EqualTo("TestValue"));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task RemoveValue()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)webCookie).Clear();
                this.sessionBL.ResetSessionCache();
                var session = await this.sessionBL.GetSession();

                this.sessionBL.AddValue("Test", "TestValue");
                await this.sessionBL.UpdateSessionData();

                this.sessionBL.RemoveValue("Test");
                await this.sessionBL.UpdateSessionData();

                this.sessionBL.ResetSessionCache();
                session = await this.sessionBL.GetSession();
                Assert.That(this.sessionBL.GetValueDef("Test", "").ToString(), Is.EqualTo(""));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task UpdateValue()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.sessionBL.ResetSessionCache();
                var session = await this.sessionBL.GetSession();

                this.sessionBL.AddValue("Test", "TestValue");
                Assert.That(this.sessionBL.GetValueDef("Test", "").ToString(), Is.EqualTo("TestValue"));

                this.sessionBL.AddValue("Test", "UpdatedValue");
                Assert.That(this.sessionBL.GetValueDef("Test", "").ToString(), Is.EqualTo("UpdatedValue"));

                await this.sessionBL.UpdateSessionData();

                this.sessionBL.ResetSessionCache();
                session = await this.sessionBL.GetSession();
                Assert.That(this.sessionBL.GetValueDef("Test", "").ToString(), Is.EqualTo("UpdatedValue"));
            }
        }
    }
}
