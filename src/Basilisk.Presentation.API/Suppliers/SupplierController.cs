using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.API.Suppliers;

// nambah {id?}, artinya mau ada,atau tidak ada gpp

[Authorize(Roles = "Administrator,Finance")]
[Route("api/v1/suppliers")]
[ApiController]
public class SupplierController : ControllerBase
{
    private readonly SupplierService _service;

    public SupplierController(SupplierService service)
    {
        _service = service;
    }
    [Authorize(Roles = "Administrator,Finance")]
    [HttpGet]
    public IActionResult GetAll(string? name)
    {
        var dto = _service.GetAllSuppliers(name);
        return Ok(dto);
    }
    [Authorize(Roles = "Administrator")]
    [HttpGet("{id}")]
    public IActionResult GetById(long id)
    {
        if (id < 0)
        {
            return BadRequest();
        }
        var dto = _service.GetById(id);
        return Ok(dto);
    }


    [Authorize(Roles = "Administrator")]
    [HttpPost("{id}")]
    public IActionResult  Insert (SupplierDto dto)
    {
        var insert = _service.InsertSupplier(dto);
        return Created("",insert);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id}")]
    public IActionResult Update( long id, SupplierDto dto)
    {
        var updated = _service.UpdateSupplier(id,dto);
        return Ok(updated);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id}")]
    public IActionResult SoftDelete(long id)
    {
        var deleted = _service.SoftDelete(id);
        return Ok(deleted);
    }

}

