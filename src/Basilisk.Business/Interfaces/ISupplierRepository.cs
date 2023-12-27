using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Business.Interfaces;
public interface ISupplierRepository
{
    public List<Supplier> AllSuppliers(string? name);
    public Supplier GetSupplierById(long id);
    public List<Supplier> GetAllSuppliers();
    Supplier InsertSupplier(Supplier supplier);
    Supplier UpdateSupplier(Supplier supplier);
    Supplier SoftDelete(Supplier supplier);
}

