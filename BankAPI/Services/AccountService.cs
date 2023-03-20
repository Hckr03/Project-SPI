

using BankAPI.Data;
using BankAPI.Models;
using BankAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;


namespace BankAPI.Services;

public class AccountService
{
    private readonly BankDbContext bankDbContext;
    private readonly ClientService clientService;

    public AccountService(BankDbContext bankDbContext, ClientService clientService){
        this.bankDbContext = bankDbContext;
        this.clientService = clientService;
    }

    public async Task<IEnumerable<Account>> GetAll()
    {
        return await bankDbContext.Accounts
            .Include(a => a.Client)
            .Include(a => a.Bank)
            .ToListAsync();
    }

    public async Task<Account?> GetById(Guid id)
    {
        return await bankDbContext.Accounts
        .Where(a => a.Id == id)
        .Include(a => a.Client)
        .Include(a => a.Bank)
        .SingleOrDefaultAsync();
    }

    public async Task<Account?> GetByNum(string accountNum)
    {
        // return await bankDbContext.Accounts
        // .FirstOrDefaultAsync(b => b.AccountNum.ToLower() == accountNum.ToLower());
        return await bankDbContext.Accounts
        .Where(a => a.AccountNum == accountNum)
        .Include(a => a.Client)
        .Include(a => a.Bank)
        .SingleOrDefaultAsync();
    }

    public async Task<Bank?> GetByCode(string code)
    {
        return await bankDbContext.Banks
        .FirstOrDefaultAsync(b => b.BankCode.ToLower() == code.ToLower());
    }

    public async Task<Account> Create(Account account)
    {
        bankDbContext.Accounts.Add(account);
        await bankDbContext.SaveChangesAsync();
        return account;
    }

    public async Task Update(Guid id, Account account)
    {
        var existingAccount = await GetById(id);
        if(existingAccount is not null)
        {
            existingAccount.AccountNum = account.AccountNum;
            existingAccount.Balance = account.Balance;
            existingAccount.Currency = account.Currency;
            existingAccount.Client = account.Client;
            existingAccount.Bank = account.Bank;

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