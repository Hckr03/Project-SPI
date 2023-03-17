

using BankAPI.Data;
using BankAPI.Models;
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
        return await bankDbContext.Transfers.ToListAsync();
    }

    public async Task<Transfer?> GetById(Guid id)
    {
        return await bankDbContext.Transfers.FindAsync();
    }

    public async Task<Transfer> Create(Transfer newTransfer)
    {
        bankDbContext.Transfers.Add(newTransfer);
        await bankDbContext.SaveChangesAsync();

        return newTransfer;
    }

    public async Task Update(Guid id, Transfer transfer)
    {
        var existingTransfer = await GetById(id);
        if(existingTransfer is not null)
        {
            existingTransfer.ClientDocNumber = transfer.ClientDocNumber;
            existingTransfer.Account = transfer.Account;
            existingTransfer.AccountNum = transfer.AccountNum;
            existingTransfer.Amount = transfer.Amount;
            
            await bankDbContext.SaveChangesAsync();
        }
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