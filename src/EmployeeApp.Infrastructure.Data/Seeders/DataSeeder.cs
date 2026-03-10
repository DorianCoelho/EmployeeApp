using Microsoft.AspNetCore.Identity;
using NHibernate;

namespace EmployeeApp.Infrastructure.Data.Seeders;

public static class DataSeeder
{
    public static async Task SeedAdminUser(ISessionFactory sessionFactory)
    {
        using var session = sessionFactory.OpenSession();
        using var transaction = session.BeginTransaction();

        // 1. Verificar si ya existe
        var existingUser = session.Query<User>()
            .FirstOrDefault(u => u.Email == "User@dualimind.com");

        if (existingUser == null)
        {
            var admin = new User
            {
                Email = "User@dualimind.com",
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            // 2. Hashear la contraseña de forma segura
            var hasher = new PasswordHasher<User>();
            admin.PasswordHash = hasher.HashPassword(admin, "Login123@");

            await session.SaveAsync(admin);
            await transaction.CommitAsync();
        }
    }
}