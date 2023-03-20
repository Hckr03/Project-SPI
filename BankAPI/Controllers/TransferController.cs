using BankAPI.Models;
using BankAPI.Models.Dtos;
using BankAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TransferController : ControllerBase
{
    private readonly TransferService transferService;
    private readonly AccountService accountService;
    private readonly ClientService clientService;
    private readonly BankService bankService;

    public TransferController(TransferService transferService, 
        AccountService accountService, 
        ClientService clientService,
        BankService bankService)
    {
        this.transferService = transferService;
        this.accountService = accountService;
        this.clientService = clientService;
        this.bankService = bankService;
    }

    [HttpGet]
    public async Task<IEnumerable<Transfer>> GetAll()
    {
        return await transferService.GetALL();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Transfer?>> GetById(Guid id)
    {
        var transfer = await transferService.GetById(id);
        if(transfer is null)
        {
            return TransferNotFound(id);
        }
        return transfer;
    }

    [HttpGet("state/{state}")]
    public async Task<ActionResult<Transfer?>> GetByState(string state)
    {
        var transfer = await transferService.GetByState(state);
        if(transfer.Any() != true)
        {
            return BadRequest(new { message = $"No existen transferencias con el estado de $({state})"});
        }
        return Ok(transfer);
    }

    [HttpGet("account/{accountNum}")]
    public async Task<ActionResult<Transfer?>> GetByAccount(string accountNum)
    {
        var transfer = await transferService.GetByAccount(accountNum);
        if(transfer.Any() != true) return BadRequest(new { message = $"El numero de cuenta $({accountNum}) ingresado no existe"});
        return Ok(transfer);
    }

    [HttpGet("client/{docNum}")]
    public async Task<ActionResult<Transfer?>> GetByClient(string docNum)
    {
        var transfer = await transferService.GetByClient(docNum);
        if(transfer.Any() != true) return BadRequest(new { message = $"El numero de Cliente $({docNum}) ingresado no existe"});
        return Ok(transfer);
    }

    [HttpPost]
    public async Task<ActionResult<Transfer>> Send(TransferDtoIn transfer)
    {
        if(await valNullEmpty(transfer))
        {
            return BadRequest(new { message = "El formulario de transferencias posee campos vacios o nulos"});
        }

        if(await insufficientBalance(transfer))
        {
            return BadRequest(new { message = "El saldo es insuficiente para realizar esta operación"});
        }
        
        if(await banksEquals(transfer))
        {
            return BadRequest(new { message = "No se puede realizar esta operacion debido a que ambas cuentas son del mismo Banco"});
        }

        await updateBalance(transfer);
        var newTransfer = await createTransfer(transfer);
        await transferService.Send(newTransfer);
        return CreatedAtAction(nameof(GetById), new { id = newTransfer.Id}, newTransfer);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Transfer>> UpdateState(Guid id, StateDotIn state)
    {
        Transfer transfer = await transferService.GetById(id);
        if(transfer is null)
        {
            return BadRequest(new { message = $"El nro de transferencia ({id}) no existe!"});
        }
        await transferService.UpdateState(transfer.Id, state);
        return Ok(new { message = $"Se actualizo el estado de la transferencia con id:({transfer.Id})"});
    }
    

    [HttpDelete]
    public async Task<ActionResult<Transfer>> Delete(Guid id)
    {
        var transferToDelete = transferService.GetById(id);
        if(transferToDelete is not null)
        {
            await transferService.Delete(id);
            return Ok();
        }
        else
        {
            return TransferNotFound(id);
        }
    }

    private NotFoundObjectResult TransferNotFound(Guid id)
    {
        return NotFound(new { message = $"La cuenta con ID = ({id}) no existe."});
    }

    private async Task<Transfer> createTransfer(TransferDtoIn transfer)
    {
        Account fromAccount = await accountService.GetByNum(transfer.FromAccountNum);
        Client fromClient =  await clientService.GetByNum(transfer.FromClientDocNumber);

        Account toAccount = await accountService.GetByNum(transfer.ToAccountNum);
        Client toClient =  await clientService.GetByNum(transfer.ToClientDocNumber);

        Transfer newTransfer = new Transfer();
        newTransfer.FromAccount = fromAccount;
        newTransfer.FromClient = fromClient;
        newTransfer.Amount = transfer.Amount;
        newTransfer.ToAccount = toAccount;
        newTransfer.ToClient = toClient;
        newTransfer.State = transfer.State;

        return newTransfer;
    }

    private async Task<bool> valNullEmpty(TransferDtoIn transfer)
    {
        if(String.IsNullOrEmpty(transfer.ToString()))
        {
            return true;
        }
        return false;
    }

    private async Task<bool> insufficientBalance(TransferDtoIn transfer)
    {
        Account? fromBalance =  await accountService.GetByNum(transfer.FromAccountNum);
        if(fromBalance.Balance < transfer.Amount){
            return true;
        }
        return false;
    }

    private async Task<bool> banksEquals(TransferDtoIn transfer)
    {
        Account? fromBank = await accountService.GetByNum(transfer.FromAccountNum);
        Account? toBank = await accountService.GetByNum(transfer.ToAccountNum);

        if(fromBank.Bank.BankCode.Equals(toBank.Bank.BankCode))
        {
            return true;
        }
        return false;
    }

    private async Task updateBalance(TransferDtoIn transfer)
    {
        Account? fromAccount = await accountService.GetByNum(transfer.FromAccountNum);
        Account? toAccount = await accountService.GetByNum(transfer.ToAccountNum);

        fromAccount.Balance = Decimal.Subtract(fromAccount.Balance, transfer.Amount);
        toAccount.Balance = Decimal.Add(toAccount.Balance, transfer.Amount);
    }
}