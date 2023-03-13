using Microsoft.AspNetCore.Mvc;
using BankAPI.Data;
using BankAPI.Models;

namespace BankAPI.Controllers;



[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly BankDbContext bankDbContext;
    public ClientController(BankDbContext bankDbContext)
    {
        this.bankDbContext = bankDbContext;
    }

    [HttpGet]
    public IEnumerable<Client> Get()
    {
        return bankDbContext.Clients.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Client> GetById(string id)
    {
        var client = bankDbContext.Clients.Find(id);
        if(client is null)
        {
            return NotFound();
        }
        return client;
    }

    [HttpPost]
    public ActionResult<Client> Create(Client client)
    {
        bankDbContext.Clients.Add(client);
        bankDbContext.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = client.docNumber}, client);
    }

    [HttpPut("{id}")]
    public ActionResult<Client> Update(String id, Client client)
    {
        if(id != client.docNumber)
        {
            return BadRequest();
        }

        var existingClient = bankDbContext.Clients.Find(id);
        if(existingClient is null)
        {
            return NotFound();
        }

        existingClient.name = client.name;
        existingClient.docType = client.docType;
        existingClient.docNumber = client.docNumber;

        bankDbContext.SaveChanges();

        return NoContent();
    }

    [HttpDelete]
    public ActionResult<Client> Delete(String id, Client client)
    {
        var existingClient = bankDbContext.Clients.Find(id);
        if(existingClient is null)
        {
            return NotFound();
        }

        bankDbContext.Clients.Remove(existingClient);
        bankDbContext.SaveChanges();
        return Ok();
    }
}