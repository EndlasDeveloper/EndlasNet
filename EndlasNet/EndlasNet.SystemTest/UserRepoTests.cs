using NUnit.Framework;
using EndlasNet.Data;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EndlasNet.UnitTest;
using System.Threading.Tasks;

namespace EndlasNet.SystemTest
{
    public class UserRepoTests
    {
        private EndlasNetDbContext _db;

        [SetUp]
        public void Setup()
        {
            _db = SingletonTestSetup.Instance().Get();
        }

        [Test]
        public async Task UserAddTestAsync()
        {
            /// ARRANGE

            // setup PilotRepo to be tested
            var repo = new UserRepo(_db);
            // setup pilot with name, email, aircraft and a password
            var user = new User { UserId = Guid.NewGuid(), FirstName = UnitTestUtil.getRandomString(8),
                LastName = UnitTestUtil.getRandomString(8), EndlasEmail = UnitTestUtil.getRandomString(8)+"@endlas.com",
                AuthString = UnitTestUtil.getRandomString(8) };

            /// ACT
            // call the method to be tested
            await repo.Add(user);
            var result = await(_db.Users.Where(p => p.EndlasEmail == user.EndlasEmail).FirstOrDefaultAsync());

            /// ASSERT
            Assert.IsNotNull(result);
            Assert.AreEqual(result.EndlasEmail, user.EndlasEmail);
        }

        [Test]
        public async Task UserDeleteTestAsync()
        {
            /// ARANGE
            var repo = new UserRepo(_db);
            var user = CreateUser();

            /// ACT
            await AddForTest(user);
            var preresult = await (_db.Users.Where(p => p.EndlasEmail == user.EndlasEmail).FirstOrDefaultAsync());

            await repo.Delete(user.EndlasEmail);
            var result = await(_db.Users.Where(p => p.EndlasEmail == user.EndlasEmail).FirstOrDefaultAsync());

            /// ASSERT
            Assert.IsNotNull(preresult);
            Assert.IsNull(result);
        }

        public async Task AddForTest(User user)
        {
            await _db.Users.AddAsync(user);
            _db.Entry(user).State = EntityState.Added;
            await _db.SaveChangesAsync();
        }

        private object await(object p)
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task UserGetAllTestAsync()
        {
            var repo = new UserRepo(_db);
            var user1 = CreateUser();
            var user2 = CreateUser();
            var user3 = CreateUser();

            var list = await repo.GetAll();
            Assert.AreEqual(0, list.Count());
            await AddForTest(user1);
            list = await repo.GetAll();
            Assert.AreEqual(1, list.Count());
            await AddForTest(user2);
            await AddForTest(user3);
            list = await repo.GetAll();
            Assert.AreEqual(3, list.Count());
        }

        [Test]
        public async Task GetUserViaEmailTestAsync()
        {
            var repo = new UserRepo(_db);
            User user = CreateUser();
            await AddForTest(user);
            var result = await repo.GetUser(user.EndlasEmail);
            Assert.AreEqual(user.EndlasEmail, result.EndlasEmail);
        }


        [Test]
        public async Task GetUserViaUserIdTestAsync() 
        {
            /// ARRANGE
            var repo = new UserRepo(_db);
            User user = CreateUser();
            await AddForTest(user);
            /// ACT
            var result = await repo.GetUser(user.UserId);
            /// ASSERT
            Assert.AreEqual(user.UserId.ToString(), result.UserId.ToString());
        }

        [Test]
        public async Task UserUpdateTestAsync()
        {
            var repo = new UserRepo(_db);
            User user = CreateUser();
            await AddForTest(user);
            var originalEmail = user.EndlasEmail;
            User usercopy = user;
            user.FirstName = UnitTestUtil.getRandomString(8);
            user.LastName = UnitTestUtil.getRandomString(8);
            user.EndlasEmail = UnitTestUtil.getRandomString(9) + "@endlas.com";
            await repo.Update(user);
            var result = await (_db.Users.Where(p => p.EndlasEmail == user.EndlasEmail).FirstOrDefaultAsync());
            var badresult = await (_db.Users.Where(p => p.EndlasEmail == originalEmail).FirstOrDefaultAsync());
            Assert.AreEqual(user.EndlasEmail, result.EndlasEmail);
            Assert.IsNull(badresult);

        }

        private User CreateUser()
        {
            return new User
            {
                UserId = Guid.NewGuid(),
                FirstName = UnitTestUtil.getRandomString(8),
                LastName = UnitTestUtil.getRandomString(8),
                AuthString = UnitTestUtil.getRandomString(12),
                EndlasEmail = UnitTestUtil.getRandomString(8) + "@endlas.com"
            };
        }
        [TearDown]
        public async Task CleanUpAdd()
        {
            // get all pilots in the db
            var users = await _db.Users.ToListAsync();
            // walk through and remove them
            foreach (User user in users)
            {
                _db.Remove(user);
                _db.Entry(user).State = EntityState.Deleted;
            }
            // tell the db all the pilots were removed
            await _db.SaveChangesAsync();
        }
    }
}