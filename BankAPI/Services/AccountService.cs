

using BankAPI.Data;
using BankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;

public class AccountService
{
    private readonly BankDbContext bankDbContext;

    public AccountService(BankDbContext bankDbContext){
        this.bankDbContext = bankDbContext;
    }

    public async Task<IEnumerable<Account>> GetAll()
    {
        return await bankDbContext.Accounts
            .Include(c => c.client)
            .Include(b => b.bank)
            .ToListAsync();
    }

    public async Task<Account?> GetById(Guid id)
    {
        return await bankDbContext.Accounts.FindAsync(id);
    }

    public async Task<Account> Create(Account newAccount)
    {
            bankDbContext.Accounts.Add(newAccount);
            await bankDbContext.SaveChangesAsync();

            return newAccount;
    }

    public async Task Update(Guid id, Account account)
    {
        var existingAccount = await GetById(id);
        if(existingAccount is not null)
        {
            existingAccount.accountNum = account.accountNum;
            existingAccount.balance = account.balance;
            existingAccount.bankCode = account.bankCode;
            existingAccount.docNumber = account.docNumber;
            existingAccount.currency = account.currency;

            await bankDbContext.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id)
    {
        var bankToDelete = await GetById(id);
        if(bankToDelete is not null)
        {
            bankDbContext.Accounts.Remove(bankToDelete);
            await bankDbContext.SaveChangesAsync();
        }
    }
}