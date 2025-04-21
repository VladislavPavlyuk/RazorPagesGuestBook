using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RazorPagesGuestBook.Data
{
    public class AppDbContext : IdentityDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка сущности Message
            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Messages"); // Задаём имя таблицы

                entity.HasKey(e => e.Id); // Устанавливаем первичный ключ

                entity.Property(e => e.Content)
                      .IsRequired()
                      .HasMaxLength(1000); // Устанавливаем ограничения на длину сообщения

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()"); // По умолчанию используем текущую дату/время

                // Настройка связи с AspNetUsers
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade); // При удалении пользователя удаляются и его сообщения
            });
        }

        public DbSet<Message> Messages { get; set; }      
        public List<IdentityRole> UserRoles { get; private set; }
        public DbSet<IdentityUser> Users { get; set; } 

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            if (Database.EnsureCreated())
            {
                UserRoles = new List<IdentityRole>
                    {
                        new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                        new IdentityRole { Name = "User", NormalizedName = "USER" }
                    };
                foreach (var role in UserRoles)
                {
                    if (!Roles.Any(r => r.Name == role.Name))
                    {
                        Roles.Add(role);
                    }
                }

                SaveChanges();
            }
        }
    }
}
