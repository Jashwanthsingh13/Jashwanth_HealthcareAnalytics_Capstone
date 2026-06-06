using HealthcareAnalytics.Domain.Entities;
using HealthcareAnalytics.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HealthcareAnalytics.MVC.Data;

public static class AuthSeeder
{
    public static void Seed(HealthcareDbContext dbContext)
    {
        if (dbContext.Roles.Any() || dbContext.Users.Any())
        {
            return;
        }

        var adminRole = new Role { RoleName = "Admin" };
        var doctorRole = new Role { RoleName = "Doctor" };
        var patientRole = new Role { RoleName = "Patient" };

        dbContext.Roles.AddRange(adminRole, doctorRole, patientRole);
        dbContext.SaveChanges();

        var passwordHasher = new PasswordHasher<User>();

        var adminUser = new User
        {
            Username = "admin",
            Email = "admin@healthcare.local",
            RoleId = adminRole.RoleId
        };
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");

        var doctorUser = new User
        {
            Username = "doctor",
            Email = "doctor@healthcare.local",
            RoleId = doctorRole.RoleId
        };
        doctorUser.PasswordHash = passwordHasher.HashPassword(doctorUser, "Doctor@123");

        var patientUser = new User
        {
            Username = "patient",
            Email = "patient@healthcare.local",
            RoleId = patientRole.RoleId
        };
        patientUser.PasswordHash = passwordHasher.HashPassword(patientUser, "Patient@123");

        dbContext.Users.AddRange(adminUser, doctorUser, patientUser);
        dbContext.SaveChanges();
    }
}