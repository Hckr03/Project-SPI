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
            .Include(a => a.Accounts)
            .Include(t => t.Transfers)
            .ToListAsync();

        // return await bankDbContext.Clients.Select( c => new ClientDtoOut {
        //     DocNumber = c.DocNumber,
        //     DocType = c.DocType,
        //     Fullname = c.Fullname,
        //     Accounts = c.Accounts,
        //     Transfers = c.Transfers
        // }).ToListAsync();
    }
    
    public async Task<Client?> GetById(string id)
    {
        return await bankDbContext.Clients.FindAsync(id);
    }

     public async Task<Client> Create(ClientDtoIn client)
    {
        var newClient = new Client(
            client.DocNumber,
            client.DocType,
            client.Fullname
        );

        bankDbContext.Clients.Add(newClient);
        await bankDbContext.SaveChangesAsync();

        return newClient;
    }

     public async Task Update(String id, ClientDtoIn client)
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