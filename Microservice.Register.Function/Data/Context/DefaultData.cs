using Microservice.Register.Function.Helpers;

namespace Microservice.Register.Function.Data.Contexts;

public class DefaultData
{
    public List<Microservice.Register.Function.Domain.User> GetUserDefaultData()
    {
        return new List<Microservice.Register.Function.Domain.User>()
        {
            CreateUser(new Guid("6c84d0a3-0c0c-435f-9ae0-4de09247ee15"), "intergration-test-user@example.com", "$2a$11$K7TSYHDJaepUjxZPiE4dY.tuzpiL2JoEItsb3CVqwNkNELXIX2Ywy", Enums.Role.User, "-HpwWGVP5WXtxdvIH7VMNlJUyTS9_z9O7ef1BgPjhLcsjrOWxyyQNw44", Convert.ToDateTime("2023-08-18 15:21:38.8758226"), Convert.ToDateTime("2024-03-27 15:31:21.0703642")),
            CreateUser(new Guid("929eaf82-e4fd-4efe-9cae-ce4d7e32d159"), "intergration-test-user2@example.com", "$2a$11$1hPEhBElDwFfKDstC5j7EeGebkAKHyKEdVguvu2GOREdm8qNpbNOi", Enums.Role.User, "-AbTPQWp3vTaExY6q3SF9nAqsVAulzTmTkuj-gfmv_5-XDabkYa1EQ44", Convert.ToDateTime("2023-08-18 15:26:46.2930065"), Convert.ToDateTime("2024-03-27 15:31:45.9512476")),


            //CreateUser(new Guid("aa1dc96f-3be5-41cd-8a1b-207284af3fdd"), "corday10@hotmail.com", "$2a$11$sUclSCnqTUlmXReP68uXxuqlf1TW0RjG9lovnv21/Z6R4BYoH7XeC", Enums.Role.User, "lGoeUM53kxNLZlkznM4rpDjWTJa9rNcyjCAKonHeeX08TtM0C8wpbQ44", Convert.ToDateTime("2023-08-18 15:21:38.8758226"), Convert.ToDateTime("2023-08-18 15:21:38.8757677")),
            //CreateUser(new Guid("af95fb7e-8d97-4892-8da3-5e6e51c54044"), "corday11@hotmail.com", "$2a$11$NWAxlt6tssER3IVzNA7Xw.b.ZCim3T3z58JDz/ilPmz/JhHutjDkS", Enums.Role.User, "5rGMUNyYN85wyB9o7Hn2ylEIo8w3Tlt3LtB8cSm4ks6wfF0TiMz9eQ44", Convert.ToDateTime("2023-08-18 15:26:46.2930065"), Convert.ToDateTime("2023-08-18 15:26:46.2929450")),
            //CreateUser(new Guid("55b431ff-693e-4664-8f65-cfd8d0b14b1b"), "corday12@hotmail.com", "$2a$11$R26bWvkESNmNSeLOcpNDOebwu2vUQP6RaOCB18ujskdd/Gctpzy3m", Enums.Role.User, "JikIR425mjxRk2vRKHdYgYfscjsMx-nPqs8xzk5V99420MxFFXQDdg44", Convert.ToDateTime("2023-08-18 15:30:59.2321080"), Convert.ToDateTime("2023-08-18 15:30:59.2319879")),
            //CreateUser(new Guid("2385de72-2302-4ced-866a-fa199116ca6e"), "corday13@hotmail.com", "$2a$11$/k9/nqKpHgXlr6CTMVoZbu8HBLCebf1NdrP71LcgQWrVmFl/FP7.S", Enums.Role.User, "NYCscmP1Q3z76pQY_Y01Cdqwe_khc_YpwxkLwqGukgrMVDOEBitxQg44", Convert.ToDateTime("2023-08-18 15:40:30.8014311"), Convert.ToDateTime("2023-08-18 15:40:30.8013615")),
            //CreateUser(new Guid("47417642-87d9-4047-ae13-4c721d99ab48"), "corday14@hotmail.com", "$2a$11$wdkVIkHA1Cr0EZeE3Jk1seCSl5OT/hDlXjR6LpSY3XItN7F/gQp4S", Enums.Role.User, "d4fWNZNRF98DltZ3SKo3ggp2N-P3lG-1LOUiyv2mfPprdCJFxtZXdQ44", Convert.ToDateTime("2023-08-18 15:42:07.6332373"), Convert.ToDateTime("2023-08-18 15:42:07.6331653")),
            //CreateUser(new Guid("ff4d5a80-81e3-42e3-8052-92cf5c51e797"), "corday15@hotmail.com", "$2a$11$H8BS6j19gUmelCX7F1pUeul0.27ARe7H9Ux3O9s.LBOfXyPgKGLDi", Enums.Role.User, "TC9IVqrD-n6nWkfJH0i4OT8rHM7g8xo08WrnWfAehrRy8xSgG_zgRQ44", Convert.ToDateTime("2023-08-18 15:45:55.7602347"), Convert.ToDateTime("2023-08-18 15:45:55.7601533")),
            //CreateUser(new Guid("5ff79dfe-c1fa-4dd9-996f-bc96649d6dfc"), "corday16@hotmail.com", "$2a$11$op7y2GsDxvl7cZqqRrBpRuNf85luc.bVdWqRGSkh9XWbPnKNO9dXm", Enums.Role.User, "l-K_Il3aA25ZDAETMzhfqXMflCE0C_8vfG20pNd0isnTomQ1YN2fpw44", Convert.ToDateTime("2023-08-18 15:48:31.2707367"), Convert.ToDateTime("2023-08-18 15:48:31.2706793")),
            //CreateUser(new Guid("ae55b0d1-ba02-41e1-9efa-9b4d4ac15eec"), "corday17@hotmail.com", "$2a$11$kbeO4QR4kdl/uxLRSmGdJOSEs2PRNQFFPKFxthqICX0maXxEfNZ5C", Enums.Role.User, "mEId4vbifX3S5LfPd0eEhQ7BgNs6xz6cDwLPTBuPuwc6OFf8Yw2hBw44", Convert.ToDateTime("2023-08-18 15:53:01.7154754"), Convert.ToDateTime("2023-08-18 15:53:01.7154228")),
            //CreateUser(new Guid("c95ba8ff-06a1-49d0-bc45-83f89b3ce820"), "corday18@hotmail.com", "$2a$11$jBIzZuggUZu52NOWAjlv6e2rVpIARa87JqSxdKUOe1RZ2KVZdSmDa", Enums.Role.User, "B8eXsXrt4NqdYwjiJWpVYVUGPdnetlF-Gt8pbBJvqjp8hH6CsentXQ44", Convert.ToDateTime("2023-08-18 15:55:44.9463214"), Convert.ToDateTime("2023-08-18 15:55:44.9462678")),
            //CreateUser(new Guid("f07e88ac-53b2-4def-af07-957cbb18523c"), "corday19@hotmail.com", "$2a$11$7Gb7oXNl0fMzRK3S6.2s1OUJ5tPTvlG44MgyIiguYZCsOYFCS98de", Enums.Role.User, "FwqSTqfEtdJanlaWArkYn5M4YH9lAHWBVq3HHtqK-Htw5pUlPy7R4w44", Convert.ToDateTime("2023-08-18 16:03:37.0866301"), Convert.ToDateTime("2023-08-18 16:03:37.0865647")) 
        };
    }
      
    private Microservice.Register.Function.Domain.User CreateUser(Guid id, string email, string passwordHash, Enums.Role role, string verificationToken, DateTime verified, DateTime created)
    {
        return new Microservice.Register.Function.Domain.User { Id = id, Email = email, PasswordHash = passwordHash, Role = role, VerificationToken = verificationToken, Verified = verified, Created = created };
    }
}