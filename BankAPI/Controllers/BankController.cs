using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Models;
using BankAPI.Models.Dtos;

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

    [HttpGet("{code}")]
    public async Task<ActionResult<Bank>> GetByCode(String code)
    {
        var bank = await bankService.GetByCode(code);
        if(bank is null)
        {
            return BankNotFound(code);
        }
        return bank;
    }

    [HttpPost]
    public async Task<ActionResult<Bank>> Create(BankDtoIn bank)
    {
        if(await bankService.GetByCode(bank.BankCode) is not null)
        {
            return BadRequest(new {message = $"El codigo de banco ({bank.BankCode}) ya existe!"});
        }
        var newBank = await bankService.Create(bank);
        return  CreatedAtAction(nameof(GetByCode), new { code = newBank.BankCode}, newBank);
    }

    [HttpPut("{code}")]
    public async Task<ActionResult<Bank>> Update(String code, BankDtoIn bank)
    {
        if(code != bank.BankCode)
        {
            return BadRequest(new { message = $"El nro del Banco({code}) de la URL no coincide con el nro. del Banco({bank.BankCode}) del cuerpo de la solicitud."});
        }

        var bankToUpdate = await bankService.GetByCode(code);
        if(bankToUpdate is not null)
        {
            await bankService.Update(code, bank);
            return NoContent();
        }
        else
        {
            return BankNotFound(code);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<Bank>> Delete(String code)
    {
        var bankToDelete = await bankService.GetByCode(code);
        if(bankToDelete is not null)
        {
            await bankService.Delete(code);
            return Ok();
        }
        else
        {
            return BankNotFound(code);
        }
    }

    private NotFoundObjectResult BankNotFound(String code)
    {
        return NotFound(new { message = $"El Banco con ID = {code} no existe."});
    }
}