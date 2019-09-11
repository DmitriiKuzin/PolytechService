using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore2.Models
{
    public class AuthDbContext : IdentityDbContext
    {

        public AuthDbContext(DbContextOptions options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            var user = new AppUser
            {
                UserName = "Admin",
                EmailConfirmed = true

            };
            var passHash = new PasswordHasher<AppUser>();
            user.PasswordHash = passHash.HashPassword(user, "1234");

            base.OnModelCreating(builder);
            builder.Entity<AppUser>()
                            .HasData(user);

            var identRoles = new List<IdentRole>()
            {

              new IdentRole  {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    rusName = "Администратор"
                },

               new IdentRole {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "USER",
                    rusName = "Пользователь"
                }
            };
            builder.Entity<IdentRole>()
                .HasData(identRoles);

            var userRoles = new UserRoles
            {
                RoleId = "1",
                UserId = user.Id
            };
            builder.Entity<UserRoles>()
                .HasData(userRoles);

            Dorm dorm = new Dorm
            {
                Id = 1,
                Address = "ул. Примерная д28"
            };
            builder.Entity<Dorm>()
                .HasData(dorm);

            var categories = new List<Category>()
            {
                new Category
                {
                    Id = 1,
                    Name = "Сантехника"
                },
                new Category
                {
                    Id = 2,
                    Name = "Электрика"
                },
                new Category
                {
                    Id = 3,
                    Name = "Мебель"
                }
            };

            builder.Entity<Category>()
                .HasData(categories);

            var statuses = new List<Status>
            {
                new Status{   Id = 1, Name = "Открыта" },
                new Status{   Id= 2, Name= "Закрыта" },
                new Status{   Id= 3, Name= "Закрыта неуспешно" },
                new Status{   Id= 4, Name= "Пауза" }
            };
            builder.Entity<Status>()
                .HasData(statuses);

            var priorities = new List<Priority>
            {
                 new Priority { Id= 1, Name= "Высокий" },
                 new Priority { Id= 2, Name= "Средний" },
                 new Priority { Id= 3, Name= "Низкий" },
            };
            builder.Entity<Priority>()
                .HasData(priorities);

        }



        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<IdentRole> IdentRoles { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Dorm> Dorm { get; set; }
        public DbSet<Lifecycle> Lifecycle { get; set; }

    }
}
