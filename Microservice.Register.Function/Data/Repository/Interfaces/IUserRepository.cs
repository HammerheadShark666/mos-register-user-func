using Microservice.Register.Function.Domain;

namespace Microservice.Register.Function.Data.Repository.Interfaces;

public interface IUserRepository
{   
    Task<bool> UserExistsAsync(string email); 
    Task<Domain.User> AddAsync(Domain.User user);
}
