using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

// DbContext da aplicação: representa a ligação entre modelos C# e a BD
public class AppDBContext : DbContext
{
    // Construtor padrão que recebe opções (connection string, provider, etc.)
    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    // Cada DbSet representa uma tabela na base de dados
    public DbSet<Account> Accounts => Set<Account>();

    public DbSet<Transaction> Transactions => Set<Transaction>();

    // Configurações do modelo (colunas, relacionamentos, conversões)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Guarda o Account.Type (enum) como string na base de dados
        modelBuilder.Entity<Account>()
            .Property(account => account.Type)
            .HasConversion<string>();

        // Define a relação 1 Account -> N Transactions
        // Transaction.AccountId é a foreign key para Account.Id
        modelBuilder.Entity<Transaction>()
            .HasOne(transaction => transaction.BankAccount)
            .WithMany(account => account.Transactions)
            .HasForeignKey(transaction => transaction.AccountId);
    }
}
