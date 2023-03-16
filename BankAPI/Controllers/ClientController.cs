using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Models;

namespace BankAPI.Controllers;



[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly ClientService clientService;
    public ClientController(ClientService clientService)
    {
        this.clientService = clientService;
    }

    [HttpGet]
    public async Task<IEnumerable<Client>> Get()
    {
        return await clientService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetById(string id)
    {
        var client = await clientService.GetById(id);
        if(client is null)
        {
            return ClientNotFound(id);
        }
        return client;
    }

    [HttpPost]
    public async Task<ActionResult<Client>> Create(Client client)
    {
        if(await clientService.GetById(client.docNumber) is null)
        {
            var newClient = await clientService.Create(client);
            return CreatedAtAction(nameof(GetById), new { id = newClient.docNumber}, newClient);
        }
        return BadRequest(new { message = $"El cliente con numero de documento ({client.docNumber}) ya existe en la base de datos!"});
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Client>> Update(String id, Client client)
    {
        if(id != client.docNumber)
        {
            return BadRequest(new { message = $"El nro de documento({id}) de la URL no coincide con el nro. de documento({client.docNumber}) del cuerpo de la solicitud."});
        }

        var clientToUpdate = await clientService.GetById(id);
        if(clientToUpdate is not null)
        {
            await clientService.Update(id, client);
            return NoContent();
        }
        else
        {
            return ClientNotFound(id);
        }
        
    }

    [HttpDelete]
    public async Task<ActionResult<Client>> Delete(String id, Client client)
    {
        var clientToDelete = await clientService.GetById(id);
        if(clientToDelete is not null)
        {
            await clientService.Delete(id);
            return Ok();
        }
        else
        {
            return ClientNotFound(id);
        }
    }

    private NotFoundObjectResult ClientNotFound(String id)
    {
        return NotFound(new { message = $"El cliente con ID = {id} no existe."});
    }
}