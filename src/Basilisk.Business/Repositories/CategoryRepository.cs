using Basilisk.Business.Interfaces;
using Basilisk.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Business.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly BasiliskTfContext context;

    public CategoryRepository(BasiliskTfContext dbcontext)
    {
        context = dbcontext;
    }

    public List<Category> GetAllCategories()
    {
        return context.Categories.ToList();
    }
    public List<Category> GetAllCategories(decimal minPrice)
    {
        var query = from category in context.Categories.Include(c => c.Products)
                    where category.Products.Any(p => p.Price > minPrice)
                    select category;
        return query.ToList();
    }
    /*    public List<Category> GetAllCategories(string name, string description)
        {
            using var context = new BasiliskTfContext();
            var query = from category in context.Categories
                        where category.Name.Contains(name)
                            && category.Description.Contains(description)
                        select category;

            return query.ToList();
        }*/
    public  List<Category>GetAllCategories(int pageNumber, int pageSize, string? name)
    {
        var query = from category in context.Categories
                    where category.Name.Contains(name) || name == null
                    select category;

        return query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

    }
    public int CountCategories(string? name)
    {
        var query = from category in context.Categories
                    where category.Name.Contains(name) || name == null
                    select category;

        return query.Count();
    }
    public void InsertCategory(Category category)
    {
        context.Categories.Add(category);
        // savechanges bisa diartikan sebagai excute atau commit
        context.SaveChanges();
    }
    public void UpdateCategory(Category category)
    {
        if (category.Id == 0)
        {
            throw new ArgumentNullException("Kalo mau update mohon masukan IDnya");
        }
        context.Categories.Update(category);
        context.SaveChanges();
    }
    public void DeleteCategories(long id)
    {
        context.Categories.Remove(GetCategoryByID(id));
        context.SaveChanges();
    }
    public void DeleteCategory(Category category) //Category
    {
        context.Categories.Remove(category);
        context.SaveChanges();
    }
    public Category GetCategoryByID(long id)
    {
        // find dia akan mengambil 
        /*context.Categories.Find(id);
        *single kalau banyak dia akan throw exeption
        context.Categories.SingleOrDefaultAsync(c => c.Id == id);*/
        // first dia akan mengambil yang pertama
        return context.Categories.FirstOrDefault(c => c.Id == id) ??  // karena kemungkina null
            throw new NullReferenceException($"catogry not found in id {id}");  // maka menambahkan throw null
    }

    public List<Product> GetProductsByCategoryId(long categoryId)
    {
        var query = context.Categories
            .Include(c => c.Products)
            .ThenInclude(p => p.Supplier)
            .Where(c => c.Id == categoryId).SelectMany(c => c.Products.Select(product => new Product
            {
                Id = product.Id,
                Name = product.Name,
                Supplier = new Supplier
                {
                    CompanyName = product.Supplier.CompanyName
                },
                Price = product.Price
            }));

        return query.ToList();
    }

    public int CountProductsByCategoryId(long id)
    {
        var count = context.Products
                .Include(c => c.Category)
                .Include(p => p.Supplier)
                .Where(p => p.CategoryId == id);
        return count.Count();
    }



}




