using Basilisk.Business.Interfaces;
using Basilisk.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Business.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly BasiliskTfContext context;

    public ProductRepository(BasiliskTfContext dbcontext)
    {
        context = dbcontext;
    }

    public List<Product>    GetAllProduct(int pageNumber, int pageSize, string? name, long? supplierID, long? categoryID)
    {
        var query = from product in context.Products.Include(c => c.Category).Include(s => s.Supplier)
                    where (product.Name.Contains(name) || name == null) && 
                    (product.SupplierId.Equals(supplierID) || supplierID == null) &&
                    (product.CategoryId.Equals(categoryID) || categoryID == null)
                    select product;

        return query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

    }

    public List<Supplier> GetAllSupplier()
    {
        return context.Suppliers.ToList();
    }

    public int CountProduct(string? name)
    {
        var query = from product in context.Products
                    where product.Name.Contains(name) || name == null
                    select product;

        return query.Count();
    }

    public List<Product> GetAllProduct(decimal minPrice)
    {
        var query = from product in context.Products
                    where product.Price.Equals(minPrice)
                    select product;
        return query.ToList();
    }

    public void InsertProduct(Product product)
    {
        context.Products.Add(product);
        // savechanges bisa diartikan sebagai excute atau commit
        context.SaveChanges();
    }
    public void UpdateProduct(Product product)
    {
        if (product.Id == 0)
        {
            throw new ArgumentNullException("Kalo mau update mohon masukan IDnya");
        }
        context.Products.Update(product);
        context.SaveChanges();
    }
    public void DeleteProduct(long id)
    {
        context.Products.Remove(GetProductByID(id));
        context.SaveChanges();
    }
    public Product GetProductByID(long id)
    {
        // find dia akan mengambil 
        /*context.Categories.Find(id);
        *single kalau banyak dia akan throw exeption
        context.Categories.SingleOrDefaultAsync(c => c.Id == id);*/
        // first dia akan mengambil yang pertama
        return context.Products.FirstOrDefault(c => c.Id == id) ??  // karena kemungkina null
            throw new NullReferenceException($"catogry not found in id {id}");  // maka menambahkan throw null
    }


    public class ProductDetailInfo {
        public int Year { get; set; }
        public string? Month { get; set; }
        public int sold { get; set; }
        public decimal Total { get;set; }
    }

    public List<ProductDetailInfo> GetProductMonthly(long Id)
    {
        var query = from orderDetail in context.OrderDetails
                    join order in context.Orders on orderDetail.InvoiceNumber equals order.InvoiceNumber
                    where orderDetail.ProductId == Id
                    group new { orderDetail, order } by new { Year = order.OrderDate.Year, Month = order.OrderDate } into grouped
                    select new ProductDetailInfo
                    {
                        Year = grouped.Key.Year,
                        Month = grouped.Key.Month.ToString("MMMM",new CultureInfo("id-ID")),
                        sold = grouped.Sum(c => c.orderDetail.Quantity),
                        Total = grouped.Sum(c => c.orderDetail.Quantity * c.orderDetail.UnitPrice)
                    };

        return query.ToList();
                    
                    
    }

    public List<ProductDetailInfo> GetProductYearly(long Id)
    {
        var query = from orderDetail in context.OrderDetails
                    join order in context.Orders on orderDetail.InvoiceNumber equals order.InvoiceNumber
                    where orderDetail.ProductId == Id
                    group new { orderDetail, order } by new { Year = order.OrderDate.Year } into grouped
                    select new ProductDetailInfo
                    {
                        Year = grouped.Key.Year,
                        sold = grouped.Sum(c => c.orderDetail.Quantity),
                        Total = grouped.Sum(c => c.orderDetail.Quantity * c.orderDetail.UnitPrice)
                    };

        return query.ToList();

    }





}

