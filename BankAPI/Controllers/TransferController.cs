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

    [HttpPost]
    public async Task<ActionResult<Transfer>> Send(TransferDtoIn transfer)
    {
        if(valNullEmpty(transfer))
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

        balanceUpdate(transfer);
        var newTransfer = await createTransfer(transfer);
        await transferService.Send(newTransfer);
        return CreatedAtAction(nameof(GetById), new { id = newTransfer.Id}, newTransfer);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Transfer>> UpdateState(Guid id, Transfer transfer)
    {
        if(id != transfer.Id)
        {
            return BadRequest(new { message = $"El nro de transferencia({id}) de la URL no coincide con el nro de transferencia({transfer.Id}) del cuerpo de la solicitud."});
        }

        var transferToUpdate = await transferService.GetById(id);
        if(transferToUpdate is not null)
        {
            await transferService.UpdateState(id, transfer);
            return NoContent();
        }
        else
        {
            return TransferNotFound(id);
        }
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
        Account fromAccount =  accountService.GetByNum(transfer.FromAccountNum);
        Client fromClient =  await clientService.GetByNum(transfer.FromClientDocNumber);

        Account toAccount =   accountService.GetByNum(transfer.ToAccountNum);
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

    private bool valNullEmpty(TransferDtoIn transfer)
    {
        if(String.IsNullOrEmpty(transfer.ToString()))
        {
            return true;
        }
        return false;
    }

    private async Task<bool> insufficientBalance(TransferDtoIn transfer)
    {
        Account fromBalance =  accountService.GetByNum(transfer.FromAccountNum);
        if(fromBalance.Balance < transfer.Amount){
            return true;
        }
        return false;
    }

    private async Task<bool> banksEquals(TransferDtoIn transfer)
    {
        Account fromBank = accountService.GetByNum(transfer.FromAccountNum);
        Account toBank =  accountService.GetByNum(transfer.ToAccountNum);

        if(fromBank.Bank.BankCode.Equals(toBank.Bank.BankCode))
        {
            return true;
        }
        return false;
    }

    private void balanceUpdate(TransferDtoIn transfer)
    {
        Account fromAccount = accountService.GetByNum(transfer.FromAccountNum);
        Account toAccount =  accountService.GetByNum(transfer.ToAccountNum);
        
        accountService.UpdateBalanceOut(fromAccount, transfer.Amount);
        accountService.UpdateBalanceIn(toAccount, transfer.Amount);
    }
}