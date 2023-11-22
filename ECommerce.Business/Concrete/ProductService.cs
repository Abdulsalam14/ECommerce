using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class ProductService : IProductService
    {
        private IProductDal _productDal;

        public ProductService(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task Add(Product product)
        {
           await _productDal.Add(product);
        }

        public async Task Delete(int id)
        {
            var item=await _productDal.Get(p=>p.ProductId == id);
            await _productDal.Delete(item);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _productDal.GetList();
        }

        public async Task<List<Product>> GetAllByCategory(int categoryId)
        {
            return await _productDal.GetList(p => p.CategoryId == categoryId || categoryId == 0);
        }

        public async Task<List<Product>> GetAllByCategoryAndFilter(int categoryId, string filterAz, string filterHl)
        {
            var products = await GetAllByCategory(categoryId);

            if (filterAz == "a-z")
            {
                products = products.OrderBy(p => p.ProductName).ToList();
                filterAz = "z-a";
            }
            else if (filterAz == "z-a")
            {
                products = products.OrderByDescending(p => p.ProductName).ToList();
                filterAz = "a-z";
            }

            if (filterHl == "l-h") products = products.OrderBy(p => p.UnitPrice).ToList();
            else if(filterHl=="h-l")products=products.OrderByDescending(p => p.UnitPrice).ToList();

            return products;
        }

        public async Task<Product> GetById(int id)
        {
            return await _productDal.Get(p=>p.ProductId==id);
        }

        public async Task Update(Product product)
        {
            await _productDal.Update(product);
        }
    }
}
