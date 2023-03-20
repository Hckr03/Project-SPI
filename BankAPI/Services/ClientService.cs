using BankAPI.Data;
using BankAPI.Models;
using BankAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;



public class ClientService
{
    private readonly BankDbContext bankDbContext;

    public ClientService(BankDbContext bankDbContext)
    {
        this.bankDbContext = bankDbContext;
    }

    public async Task<IEnumerable<Client>> GetAll()
    {
        return await bankDbContext.Clients
            // .Include(a => a.Accounts)
            // .Include(t => t.Transfers)
            .ToListAsync();
    }
    
    public async Task<Client?> GetById(string id)
    {
        return await bankDbContext.Clients.FindAsync(id);
    }

        public async Task<Client> GetByNum(string docNum)
    {
        return await bankDbContext.Clients
        .FirstOrDefaultAsync(b => b.DocNumber.ToLower() == docNum.ToLower());
    }

     public async Task<Client> Create(Client client)
    {
        bankDbContext.Clients.Add(client);
        await bankDbContext.SaveChangesAsync();

        return client;
    }

     public async Task Update(String id, Client client)
    {
        var existingClient = await GetById(id);

        if (existingClient is not null)
        {
            existingClient.Fullname = client.Fullname;
            existingClient.DocNumber = client.DocNumber;
            existingClient.DocType = client.DocType;

            await bankDbContext.SaveChangesAsync();
        }
    }

    public async Task Delete(string id)
    {
        var clientToDelete = await GetById(id);

        if(clientToDelete is not null)
        {
            bankDbContext.Clients.Remove(clientToDelete);
            await bankDbContext.SaveChangesAsync();
        }
    }
        
}