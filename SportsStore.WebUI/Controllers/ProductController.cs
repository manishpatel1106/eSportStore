using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model;
            if (category == null)
            {
                model = new ProductsListViewModel
                {
                    Products = repository.Products
                     .OrderBy(p => p.ProductID)
                     .Skip((page - 1) * PageSize)
                     .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
                    },
                    CurrentCategory = category
                };
            }
            else
            {
                model = new ProductsListViewModel
                {
                    Products = repository.Products
                     .Where(p => p.Category == null || p.Category == category)
                     .OrderBy(p => p.ProductID)
                     .Skip((page - 1) * PageSize)
                     .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
                    },
                    CurrentCategory = category
                };
            }

            return View(model);
        }
        public FileContentResult GetImage(int ProductID)
        {
            Product prod = repository.Products
                .FirstOrDefault(p => p.ProductID == ProductID);
            if(prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else{
                return null;
            }
        }
    }
}