using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basilisk.Business.Repositories.ProductRepository;

namespace Basilisk.Business.Interfaces
{
    public interface IProductRepository
    {

        public List<Product> GetAllProduct(int pageNumber, int pageSize, string? name, long? supplierID, long? categoryID);
        public List<Supplier> GetAllSupplier();
        public int CountProduct(string? name);
        public List<Product> GetAllProduct(decimal minPrice);
        public void InsertProduct(Product product);
        public void UpdateProduct(Product product);
        public void DeleteProduct(long id);
        public Product GetProductByID(long id);
        public List<ProductDetailInfo> GetProductMonthly(long Id);
        public List<ProductDetailInfo> GetProductYearly(long Id);
/*        public List<Product> GetProductDetail(long id);*/

        /*public Category GetProductsByCategoryId(long categoryId);*/
        /*        public int CountProductsByCategoryId(long id);*/
    }
}
