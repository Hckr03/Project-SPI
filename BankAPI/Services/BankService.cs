using BankAPI.Data;
using BankAPI.Models;
using BankAPI.Models.Dtos;
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
        // .where(a => a.bankCode == bankDbContext.Transfers.bankCode)
    }
    
    public async Task<Bank> GetByCode(String code)
    {
        return await bankDbContext.Banks
        .FirstOrDefaultAsync(b => b.BankCode.ToLower() == code.ToLower());
    }

     public async Task<Bank> Create(BankDtoIn bank)
    {
        var newBank = new Bank(
            bank.BankCode,
            bank.Fullname,
            bank.Adress
        );

        bankDbContext.Banks.Add(newBank);
        await bankDbContext.SaveChangesAsync();
        return newBank;
    }

     public async Task Update(string code, BankDtoIn bank)
    {
        var existingBank = await GetByCode(code);

        if (existingBank is not null)
        {
            existingBank.Fullname = bank.Fullname;
            existingBank.Adress = bank.Adress;

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
            .Where(b => b.BankCode.ToLower() == code.ToLower())
            .FirstOrDefaultAsync();

            return existByCode;
    }
        
}