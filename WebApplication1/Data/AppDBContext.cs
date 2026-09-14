using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class AppDBContext : IdentityDbContext<ApplicationUser>
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>()
            .Property(account => account.Type)
            .HasConversion<string>();

        modelBuilder.Entity<Transaction>()
            .HasOne(transaction => transaction.BankAccount)
            .WithMany(account => account.Transactions)
            .HasForeignKey(transaction => transaction.AccountId);
    }
}
