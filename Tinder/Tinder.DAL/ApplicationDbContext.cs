
using Microsoft.EntityFrameworkCore;
using Tinder.DAL.Entities;

namespace Tinder.DAL;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : 
        base(options)
    {
        if (Database.IsRelational())
        {
            Database.Migrate();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LikeEntity>()
            .HasOne(l => l.SenderUser)
            .WithMany(u => u.SentLikes)
            .HasForeignKey(l => l.SenderId)
            .OnDelete(DeleteBehavior.NoAction);
            
        modelBuilder.Entity<LikeEntity>()
            .HasOne(l => l.ReceiverUser)
            .WithMany(u => u.ReceivedLikes)
            .HasForeignKey(l => l.ReceiverId)
            .OnDelete(DeleteBehavior.NoAction);

        base.OnModelCreating(modelBuilder); 
    }

    public DbSet<UserEntity> Users { get; set; }    
    public DbSet<PhotoEntity> Photos { get; set; }
    public DbSet<ChatEntity> Chats { get; set; }
    public DbSet<LikeEntity> Likes { get; set; }
    public DbSet<MessageEntity> Messages { get; set; }

}