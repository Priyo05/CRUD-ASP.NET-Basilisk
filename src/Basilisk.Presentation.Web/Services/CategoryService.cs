using Basilisk.Business.Interfaces;
using Basilisk.Business.Repositories;
using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Basilisk.Presentation.Web.Service;
public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    /*    public List<CategoryViewModel> GetAll(string name, string description)
        {
            List<CategoryViewModel> result; //*

            //cara klasik (imperative approach)
            //List<CategoryViewModel> result = new List<CategoryViewModel>(); //*
            //foreach (var category in _categoryRepository.GetAllCategories(minPrice)) {
            //    result.Add(new CategoryViewModel { Id = category.Id, Name = category.Name, Description = category.Description });
            //}


            //cara baru (Linq) (declarative approach)
            result = _categoryRepository.GetAllCategories(name, description)
                                .Select(c => new CategoryViewModel
                                {
                                    Id = c.Id,
                                    Name = c.Name,
                                    Description = c.Description
                                })
                        .ToList();

            return result;
        }*/

    public CategoryIndexViewModel GetAll(int pageNumber, int pageSize, string? name)
    {
        List<CategoryViewModel> result;

        result = _categoryRepository.GetAllCategories(pageNumber, pageSize, name)
                    .Select(c => new CategoryViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description
                    })
                    .ToList();

        int totalCategories = _categoryRepository.CountCategories(name);


        return new CategoryIndexViewModel
        {
            Categories = result,
            Name = name,
            pagenationInfoViewModel = new PaginationInfoViewModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalCategories
            }

        };
    }
    public CategoryViewModel GetByID(long id)
    {

        var result = _categoryRepository.GetCategoryByID(id);

        var resultHasil = new CategoryViewModel()
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description,
        };


        return resultHasil;
    }

    public void InsertCategory(CategoryViewModel categoryView)
    {

        Category result = new Category()
        {
            Name = categoryView.Name,
            Description = categoryView.Description
        };

        _categoryRepository.InsertCategory(result);
    }
    public void UpdateCategory(CategoryViewModel categoryView)
    {
        Category result = new Category()
        {
            Id = categoryView.Id,
            Description = categoryView.Description,
            Name = categoryView.Name
        };

        _categoryRepository.UpdateCategory(result);
    }

    /*    public void UpdateCategory(long id)
        {
            Category result = new Category()
            {
                Id = categoryView.Id,
                Description = categoryView.Description,
                Name = categoryView.Name
            };

            _categoryRepository.UpdateCategory(result);
        }*/

    public void DeleteCategory(CategoryViewModel categoryView)
    {
        Category result = new Category()
        {
            Id = categoryView.Id,
            Description = categoryView.Description,
            Name = categoryView.Name
        };

        _categoryRepository.DeleteCategory(result);
    }


    public int DeleteCategory(long id)
    {
        int totalProductbyCategory = _categoryRepository.CountProductsByCategoryId(id);

        if (totalProductbyCategory > 0)
        {
            return totalProductbyCategory;
        }
        else
        {
            _categoryRepository.DeleteCategories(id);
            return 0;
        }
    }

    public CategoryIndexViewModel GetAllProduct(long categoryId)
    {
        List<Product> products = _categoryRepository.GetProductsByCategoryId(categoryId);
        Category category = _categoryRepository.GetCategoryByID(categoryId);

        List<ProductInfoViewModel> result = products.Select(
            p => new ProductInfoViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Supplier = p.Supplier.CompanyName,
                    Price = p.Price,
                }).ToList();

        CategoryIndexViewModel result2 = new CategoryIndexViewModel
        {
            Name = category.Name,
            Description = category.Description,
            pagenationInfoViewModel = new PaginationInfoViewModel
            {
                PageNumber = 1,
                PageSize = 1,
                TotalItems = result.Count,
            },
            Products = result,
        };

        return result2;
    }




}

