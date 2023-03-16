

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
            .Include(a => a.client)
            .Include(a => a.bank)
            .ToListAsync();
    }

    public async Task<Account?> GetById(Guid id)
    {
        return await bankDbContext.Accounts.FindAsync(id);
    }

    public async Task<Account> Create(Account newAccount)
    {
        // var newAccount = new Account();
        // newAccount.accountNum = account.accountNum;
        // newAccount.currency = account.currency;
        // newAccount.balance = account.balance;
        // newAccount.docNumber = account.docNumber;
        // newAccount.bankCode = account.bankCode;

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