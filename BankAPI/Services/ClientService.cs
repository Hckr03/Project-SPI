using BankAPI.Data;
using BankAPI.Models;
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
            .Include(a => a.accounts)
            .ToListAsync();
    }
    
    public async Task<Client?> GetById(string id)
    {
        return await bankDbContext.Clients.FindAsync(id);
    }

     public async Task<Client> Create(Client newClient)
    {
        bankDbContext.Clients.Add(newClient);
        await bankDbContext.SaveChangesAsync();

        return newClient;
    }

     public async Task Update(String id, Client client)
    {
        var existingClient = await GetById(id);

        if (existingClient is not null)
        {
            existingClient.name = client.name;
            existingClient.docType = client.docType;
            existingClient.docNumber = client.docNumber;

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