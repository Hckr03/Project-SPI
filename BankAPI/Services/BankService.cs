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
    
    public async Task<Bank?> GetByCode(String code)
    {
        return await bankDbContext.Banks
        .FirstOrDefaultAsync(b => b.bankCode.ToLower() == code.ToLower());
    }

     public async Task<Bank> Create(Bank newBank)
    {
            bankDbContext.Banks.Add(newBank);
            await bankDbContext.SaveChangesAsync();
        return newBank;
    }

     public async Task Update(String code, Bank bank)
    {
        var existingBank = await GetByCode(code);

        if (existingBank is not null)
        {
            existingBank.name = bank.name;
            existingBank.adress = bank.adress;

            await bankDbContext.SaveChangesAsync();
        }
    }

    public async Task Delete(String code)
    {
        var bankToDelete = await GetByCode(code);

        if(bankToDelete is not null)
        {
            bankDbContext.Banks.Remove(bankToDelete);
            await bankDbContext.SaveChangesAsync();
        }
    }

    public async Task<Bank> ExistByCode(String code)
    {
            var existByCode = await bankDbContext.Banks
            .Where(b => b.bankCode.ToLower() == code.ToLower())
            .FirstOrDefaultAsync();

            return existByCode;
    }
        
}