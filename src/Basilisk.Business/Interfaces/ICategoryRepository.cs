using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Business.Interfaces;
public interface ICategoryRepository
{

    public List<Category> GetAllCategories();
    public List<Category> GetAllCategories(decimal minPrice);
    public List<Category> GetAllCategories(int pageNumber, int pageSize, string? name);
    public int CountCategories(string? name);
    public void InsertCategory(Category category);
    public void UpdateCategory(Category category);
    public void DeleteCategories(long id);
    public void DeleteCategory(Category category);
    public Category GetCategoryByID(long id);
    public List<Product> GetProductsByCategoryId(long categoryId);
    public int CountProductsByCategoryId(long id);


}

