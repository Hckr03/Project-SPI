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
    private readonly ClientService clientService;
    private readonly BankService bankService;

    public AccountController(AccountService accountService, ClientService clientService)
    {
        this.accountService = accountService;
        this.clientService = clientService;
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
        Client newClient = await clientService.GetByNum(account.ClientDocNumber);
        Bank newBank = await accountService.GetByCode(account.BankCode);

        var newAccount = new Account();
            newAccount.AccountNum = account.AccountNum;
            newAccount.Currency = account.Currency;
            newAccount.Balance = account.Balance;
            newAccount.Client = newClient;
            newAccount.Bank = newBank;

        await accountService.Create(newAccount);
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
            Client newClient = await clientService.GetByNum(account.ClientDocNumber);
            Bank newBank = await bankService.GetByCode(account.BankCode);

            var newAccount = new Account();
                newAccount.AccountNum = account.AccountNum;
                newAccount.Currency = account.Currency;
                newAccount.Balance = account.Balance;
                newAccount.Client = newClient;
                newAccount.Bank = newBank;

            await accountService.Update(id, newAccount);
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