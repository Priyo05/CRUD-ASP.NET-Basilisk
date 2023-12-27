using Basilisk.Business.Interfaces;
using Basilisk.DataAccess.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Business.Repositories;
public class SupplierRepository : ISupplierRepository
{
    private readonly BasiliskTfContext _dbContext;

    public SupplierRepository(BasiliskTfContext dbContext)
    {
        _dbContext = dbContext;
    }



    public List<Supplier> AllSuppliers(string? name)
    {
        var query = from supplier in _dbContext.Suppliers
                    where supplier.DeleteDate == null &&
                    (supplier.CompanyName.Contains(name) || name == null)
                    select supplier;

        return query.ToList();
    }

    public Supplier GetSupplierById(long id)
    {
        return _dbContext.Suppliers.FirstOrDefault(s => s.Id == id && s.DeleteDate == null)
            ?? throw new KeyNotFoundException("No supplier found by ID : " + id);
    }

    public List<Supplier> GetAllSuppliers()
    {
        return _dbContext.Suppliers.ToList();
    }

    public Supplier InsertSupplier(Supplier supplier)
    {
        _dbContext.Suppliers.Add(supplier);
        _dbContext.SaveChanges();
        return supplier;
    }

    public Supplier UpdateSupplier(Supplier supplier)
    {
        _dbContext.Suppliers.Update(supplier);
        _dbContext.SaveChanges();
        return supplier;
    }

    public Supplier SoftDelete(Supplier supplier)
    {
        supplier.DeleteDate = DateTime.Now;
        _dbContext.Suppliers.Update(supplier);
        _dbContext.SaveChanges();
        return supplier;
    }




    
}

