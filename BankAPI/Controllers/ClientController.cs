using Microsoft.AspNetCore.Mvc;
using BankAPI.Data;
using BankAPI.Models;

namespace BankAPI.Controllers;



[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly BankContext bankContext;
    public ClientController(BankContext bankContext)
    {
        this.bankContext = bankContext;
    }

    [HttpGet]
    public IEnumerable<Client> Get()
    {
        return bankContext.Clients.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Client> GetById(int id)
    {
        var client = bankContext.Clients.Find(id);
        if(client is null)
        {
            return NotFound();
        }
        return client;
    }

        [HttpPost]
    public ActionResult<Client> Save(Client client)
    {
        bankContext.Clients.Add(client);
        bankContext.SaveChanges();
        return client;
    }
}