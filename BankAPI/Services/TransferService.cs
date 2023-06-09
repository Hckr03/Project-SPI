

using BankAPI.Data;
using BankAPI.Models;
using BankAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;

public class TransferService
{
    private readonly BankDbContext bankDbContext;
    
    public TransferService(BankDbContext bankDbContext)
    {
        this.bankDbContext = bankDbContext;
    }

    public async Task<IEnumerable<Transfer>> GetALL()
    {
        return await bankDbContext.Transfers
            .Include(a => a.FromAccount)
            .Include(c => c.FromClient)
            .Include(c => c.FromAccount.Bank)
            .Include(a => a.ToAccount)
            .Include(c => c.ToClient)
            .Include(c => c.ToAccount.Bank)
            .ToListAsync();
    }

    public async Task<Transfer?> GetById(Guid id)
    {
        return await bankDbContext.Transfers
        .Where(t => t.Id == id)
        .Include(t => t.FromAccount)
        .Include(c => c.FromClient)
        .Include(c => c.FromAccount.Bank)
        .Include(a => a.ToAccount)
        .Include(c => c.ToClient)
        .Include(c => c.ToAccount.Bank)
        .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Transfer?>> GetByState(string state)
    {
        return await bankDbContext.Transfers
        .Where(t => t.State.ToLower() == state.ToLower())
        .Include(t => t.FromAccount)
        .Include(c => c.FromClient)
        .Include(c => c.FromAccount.Bank)
        .Include(a => a.ToAccount)
        .Include(c => c.ToClient)
        .Include(c => c.ToAccount.Bank)
        .ToListAsync();
    }

    public async Task<IEnumerable<Transfer?>> GetByAccount(string accountNum)
    {
        return await bankDbContext.Transfers
        .Where(t => t.FromAccount.AccountNum == accountNum)
        .Include(t => t.FromAccount)
        .Include(c => c.FromClient)
        .Include(c => c.FromAccount.Bank)
        .Include(a => a.ToAccount)
        .Include(c => c.ToClient)
        .Include(c => c.ToAccount.Bank)
        .ToListAsync();
    }

        public async Task<IEnumerable<Transfer?>> GetByClient(string clientDocNum)
    {
        return await bankDbContext.Transfers
        .Where(t => t.FromClient.DocNumber == clientDocNum)
        .Include(t => t.FromAccount)
        .Include(c => c.FromClient)
        .Include(c => c.FromAccount.Bank)
        .Include(a => a.ToAccount)
        .Include(c => c.ToClient)
        .Include(c => c.ToAccount.Bank)
        .ToListAsync();
    }

    public async Task<Account?> GetByNum(string accountNum)
    {
        return await bankDbContext.Accounts
        .FirstOrDefaultAsync(a => a.AccountNum.ToLower() == accountNum.ToLower());
    }

    public async Task<Transfer> Send(Transfer transfer)
    {
        bankDbContext.Transfers.Add(transfer);
        await bankDbContext.SaveChangesAsync();
        return transfer;
    }

    public async Task UpdateState(Guid id, StateDotIn state)
    {
        var updateState = await GetById(id);
        updateState.State = state.State;
        await bankDbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var transferToDelete = await GetById(id);
        if(transferToDelete is not null)
        {
            bankDbContext.Transfers.Remove(transferToDelete);
            await bankDbContext.SaveChangesAsync();
        }
    }
}