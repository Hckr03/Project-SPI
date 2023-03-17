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
        Account fromAccount = await accountService.GetByNum(transfer.FromAccountNum);
        Client fromClient = await clientService.GetByNum(transfer.FromClientDocNumber);
        Bank fromBank = await bankService.GetByCode(transfer.FromBank);


        Account toAccount = await accountService.GetByNum(transfer.ToAccountNum);
        Client toClient = await clientService.GetByNum(transfer.ToClientDocNumber);
        Bank toBank = await bankService.GetByCode(transfer.FromBank);


        Transfer newTransfer = new Transfer();
        newTransfer.FromAccount = fromAccount;
        newTransfer.FromClient = fromClient;
        newTransfer.FromBank = fromBank;  
        newTransfer.Amount = transfer.Amount;
        newTransfer.ToAccount = toAccount;
        newTransfer.ToClient = toClient;
        newTransfer.ToBank = toBank;
        newTransfer.State = transfer.State;

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
}