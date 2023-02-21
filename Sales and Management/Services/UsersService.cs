using MongoDB.Driver;
using Sales_and_Management.Models;

namespace Sales_and_Management.Services
{
    public class UsersService : IUsersService
    {
        private readonly IMongoCollection<Users> _usersCollection;
        public UsersService(IMongoDatabase mongoDatabase)
        {
            _usersCollection = mongoDatabase.GetCollection<Users>("Users");
        }
        public async Task<List<Users>> GetAllAsync()
        {
            return await _usersCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Users> GetUserByIdAsync(string id)
        {
            return await _usersCollection.Find(_ => _.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateNewUserAsync(Users user)
        {
            await _usersCollection.InsertOneAsync(user);
        }

        public async Task UpdateUserAsync(Users userToUpdate)
        {
            await _usersCollection.ReplaceOneAsync(_ => _.Id == userToUpdate.Id, userToUpdate);
        }

        public async Task DeleteUserAsync(string id)
        {
            await _usersCollection.DeleteOneAsync(_ => _.Id == id );
        }

        public async Task<Users> GetUserLogin(string userName, string password)
        {
            return await _usersCollection.Find(_ => _.userName == userName && _.password == password).FirstOrDefaultAsync();
        }

    }
}
