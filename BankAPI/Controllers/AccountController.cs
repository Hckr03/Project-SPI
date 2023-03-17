using BankAPI.Models;
using BankAPI.Models.Dtos;
using BankAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService accountService;

    public AccountController(AccountService accountService)
    {
        this.accountService = accountService;
    }

    [HttpGet]
    public async Task<IEnumerable<Account>> GetAll()
    {
        return await accountService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account?>> GetById(Guid id)
    {
        var account = await accountService.GetById(id);
        if(account is null)
        {
            return AccountNotFound(id);
        }
        return account;
    }

    [HttpPost]
    public async Task<ActionResult<Account>> Create(AccountDtoIn account)
    {
        var newAccount = await accountService.Create(account);
        return CreatedAtAction(nameof(GetById), new { id = newAccount.Id}, newAccount);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Account>> Update(Guid id, AccountDtoIn account)
    {
        if(id != account.Id)
        {
            return BadRequest(new { message = $"El nro de cuenta({id}) de la URL no coincide con el nro de cuenta({account.Id}) del cuerpo de la solicitud."});
        }

        var accountToUpdate = await accountService.GetById(id);
        if(accountToUpdate is not null)
        {
            await accountService.Update(id, account);
            return NoContent();
        }
        else
        {
            return AccountNotFound(id);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<Account>> Delete(Guid id)
    {
        var accountToDelete = accountService.GetById(id);
        if(accountToDelete is not null)
        {
            await accountService.Delete(id);
            return Ok();
        }
        else
        {
            return AccountNotFound(id);
        }
    }

    private NotFoundObjectResult AccountNotFound(Guid id)
    {
        return NotFound(new { message = $"La cuenta con ID = ({id}) no existe."});
    }
}