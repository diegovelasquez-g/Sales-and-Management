using Sales_and_Management.Models;

namespace Sales_and_Management.Services
{
    public interface IUsersService
    {
        //Get All Users
        Task<List<Users>> GetAllAsync();

        //Get user by Id
        Task<Users> GetUserByIdAsync(string id);

        //Add an user into the collection
        Task CreateNewUserAsync(Users user);

        //Update an user
        Task UpdateUserAsync(Users userToUpdate);

        //Delete an user
        Task DeleteUserAsync(string id);

        // Login
        Task<Users> GetUserLogin(string userName, string password);
    }
}
