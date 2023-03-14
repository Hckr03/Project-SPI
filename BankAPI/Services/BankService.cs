using BankAPI.Data;
using BankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;



public class BankService
{
    private readonly BankDbContext bankDbContext;

    public BankService(BankDbContext bankDbContext)
    {
        this.bankDbContext = bankDbContext;
    }

    public async Task<IEnumerable<Bank>> GetAll()
    {
        return await bankDbContext.Banks.ToListAsync();
    }
    
    public async Task<Bank?> GetById(Guid id)
    {
        return await bankDbContext.Banks.FindAsync(id);
    }

     public async Task<Bank> Create(Bank newBank)
    {
        // var existBankByName = bankDbContext.Banks.FirstOrDefault(n => n.name == bank.name);
        // var findByName = bankDbContext.Banks
        //     .Where(b => b.name == newBank.name)
        //     .FirstOrDefault();

        // if(findByName is null)
        // {
            bankDbContext.Banks.Add(newBank);
            await bankDbContext.SaveChangesAsync();
        // }
        return newBank;
    }

     public async Task Update(Guid id, Bank bank)
    {
        var existingBank = await GetById(id);

        if (existingBank is not null)
        {
            existingBank.name = bank.name;
            existingBank.adress = bank.adress;

            await bankDbContext.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id)
    {
        var bankToDelete = await GetById(id);

        if(bankToDelete is not null)
        {
            bankDbContext.Banks.Remove(bankToDelete);
            await bankDbContext.SaveChangesAsync();
        }
    }
        
}