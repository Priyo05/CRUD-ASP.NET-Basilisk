using Basilisk.Business.Interfaces;
using Basilisk.Business.Repositories;
using Basilisk.DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basilisk.Presentation.API.Suppliers;
public class SupplierService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }


    public List<SupplierDto> GetAllSuppliers(string? name)
    {
        List<SupplierDto> result;

        result = _supplierRepository.AllSuppliers(name)
            .Select(c => new SupplierDto
            {
                Id = c.Id,
                CompanyName = c.CompanyName,
                ContactPerson = c.ContactPerson,
                JobTitle = c.JobTitle,
                Address = c.Address,
                City = c.City,
                Phone = c.Phone,
                Email = c.Email,
                DeleteDate = c.DeleteDate,

            }).ToList();

        return result;
    }


    public SupplierDto GetById(long id)
    {
        var supplier = _supplierRepository.GetSupplierById(id);

        return new SupplierDto
        {
            Id = supplier.Id,
            CompanyName = supplier.CompanyName,
            ContactPerson = supplier.ContactPerson,
            JobTitle = supplier.JobTitle,
            Address = supplier.Address,
            City = supplier.City,
            Phone = supplier.Phone,
            Email = supplier.Email,
            DeleteDate = supplier.DeleteDate
        };

    }

    public Supplier InsertSupplier(SupplierDto supplier)
    {


        Supplier result = new Supplier
        {
            CompanyName = supplier.CompanyName,
            ContactPerson = supplier.ContactPerson,
            JobTitle = supplier.JobTitle,
            Address = supplier.Address,
            City = supplier.City,
            Phone = supplier.Phone,
            Email = supplier.Email,
            DeleteDate = supplier.DeleteDate
        };

        var resulthasil =_supplierRepository.InsertSupplier(result);
        return resulthasil;
    }

    public SupplierDto UpdateSupplier(long id, SupplierDto dto)
    {
        var get = _supplierRepository.GetSupplierById(id);

        get.CompanyName = dto.CompanyName;
        get.ContactPerson = dto.ContactPerson;
        get.JobTitle = dto.JobTitle;
        get.Address = dto.Address;
        get.City = dto.City;
        get.Phone = dto.Phone;
        get.Email = dto.Email;
        get.DeleteDate = dto.DeleteDate;

        var updated = _supplierRepository.UpdateSupplier(get);

        return new SupplierDto
        {
            CompanyName = updated.CompanyName,
            ContactPerson = updated.ContactPerson,
            JobTitle = updated.JobTitle,
            Address = updated.Address,
            City = updated.City,
            Phone = updated.Phone,
            Email = updated.Email,
            DeleteDate = updated.DeleteDate
        };
    }

    public Supplier SoftDelete(long id)
    {
        var get = _supplierRepository.GetSupplierById(id);


        var deleted = _supplierRepository.SoftDelete(get);

        return deleted;

    }






}


