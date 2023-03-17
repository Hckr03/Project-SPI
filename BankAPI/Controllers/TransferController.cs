using BankAPI.Models;
using BankAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TransferController : ControllerBase
{
    private readonly TransferService transferService;

    public TransferController(TransferService transferService)
    {
        this.transferService = transferService;
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
    public async Task<ActionResult<Transfer>> Create(Transfer transfer)
    {
        var newTransfer = await transferService.Create(transfer);
        return CreatedAtAction(nameof(GetById), new { id = newTransfer.Id}, newTransfer);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Transfer>> Update(Guid id, Transfer transfer)
    {
        if(id != transfer.Id)
        {
            return BadRequest(new { message = $"El nro de transferencia({id}) de la URL no coincide con el nro de transferencia({transfer.Id}) del cuerpo de la solicitud."});
        }

        var transferToUpdate = await transferService.GetById(id);
        if(transferToUpdate is not null)
        {
            await transferService.Update(id, transfer);
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