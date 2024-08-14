using Microservice.Register.Function.Helpers;

namespace Microservice.Register.Function.Data.Contexts;

public class DefaultData
{
    public List<Microservice.Register.Function.Domain.User> GetUserDefaultData()
    {
        return new List<Microservice.Register.Function.Domain.User>()
        {
            CreateUser(new Guid("6c84d0a3-0c0c-435f-9ae0-4de09247ee15"), "intergration-test-user@example.com", "$2a$11$K7TSYHDJaepUjxZPiE4dY.tuzpiL2JoEItsb3CVqwNkNELXIX2Ywy", Enums.Role.User, "-HpwWGVP5WXtxdvIH7VMNlJUyTS9_z9O7ef1BgPjhLcsjrOWxyyQNw44", Convert.ToDateTime("2023-08-18 15:21:38.8758226"), Convert.ToDateTime("2024-03-27 15:31:21.0703642")),
            CreateUser(new Guid("929eaf82-e4fd-4efe-9cae-ce4d7e32d159"), "intergration-test-user2@example.com", "$2a$11$1hPEhBElDwFfKDstC5j7EeGebkAKHyKEdVguvu2GOREdm8qNpbNOi", Enums.Role.User, "-AbTPQWp3vTaExY6q3SF9nAqsVAulzTmTkuj-gfmv_5-XDabkYa1EQ44", Convert.ToDateTime("2023-08-18 15:26:46.2930065"), Convert.ToDateTime("2024-03-27 15:31:45.9512476"))
        };
    }
      
    private Domain.User CreateUser(Guid id, string email, string passwordHash, Enums.Role role, string verificationToken, DateTime verified, DateTime created)
    {
        return new Domain.User { Id = id, Email = email, PasswordHash = passwordHash, Role = role, VerificationToken = verificationToken, Verified = verified, Created = created };
    }
}