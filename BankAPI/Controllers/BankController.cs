using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Models;

namespace BankAPI.Controllers;



[ApiController]
[Route("[controller]")]
public class BankController : ControllerBase
{
    private readonly BankService bankService;
    public BankController(BankService bankService)
    {
        this.bankService = bankService;
    }

    [HttpGet]
    public async Task<IEnumerable<Bank>> Get()
    {
        return await bankService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Bank>> GetById(Guid id)
    {
        var client = await bankService.GetById(id);
        if(client is null)
        {
            return BankNotFound(id);
        }
        return client;
    }

    [HttpPost]
    public async Task<ActionResult<Bank>> Create(Bank bank)
    {
        var newBank = await bankService.Create(bank);
        return  CreatedAtAction(nameof(GetById), new { id = newBank.bankCode}, newBank);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Bank>> Update(Guid id, Bank bank)
    {
        if(id != bank.bankCode)
        {
            return BadRequest(new { message = $"El nro del Banco({id}) de la URL no coincide con el nro. del Banco({bank.bankCode}) del cuerpo de la solicitud."});
        }

        var bankToUpdate = await bankService.GetById(id);
        if(bankToUpdate is not null)
        {
            await bankService.Update(id, bank);
            return NoContent();
        }
        else
        {
            return BankNotFound(id);
        }
        
    }

    [HttpDelete]
    public async Task<ActionResult<Bank>> Delete(Guid id)
    {
        var bankToDelete = await bankService.GetById(id);
        if(bankToDelete is not null)
        {
            await bankService.Delete(id);
            return Ok();
        }
        else
        {
            return BankNotFound(id);
        }
    }

    private NotFoundObjectResult BankNotFound(Guid id)
    {
        return NotFound(new { message = $"El Banco con ID = {id} no existe."});
    }
}