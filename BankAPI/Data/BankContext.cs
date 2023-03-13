using Microsoft.EntityFrameworkCore;
using BankAPI.Models;

namespace BankAPI.Data;

public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options) : base(options){
        
    }
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transfer> Transfers => Set<Transfer>();
    public DbSet<Bank> Banks => Set<Bank>();
}