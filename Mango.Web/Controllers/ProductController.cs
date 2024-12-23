using Azure;
using Mango.Web.Models;
using Mango.Web.Service;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        public async Task< IActionResult> ProductIndex()
        {
           List< ProductDto>? productDto = new ();
            ResponseDto? responseDto = await _productService.GetProductsAsync(); 
            if(responseDto !=null && responseDto.IsSuccess)
            {
               productDto=JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString (responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto?.Message;

            }
            return View(productDto);
        }
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            if(ModelState.IsValid)
            {
                ResponseDto?responseDto=await _productService.CreateProductAsync(productDto);
                if(responseDto!=null && responseDto.IsSuccess)
                {
                    TempData["success"] = "Product successfully created";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
                }
            }
            return View(productDto);
        }
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
           
                ResponseDto? responseDto = await _productService.GetProductAsync(id);
                if(responseDto != null && responseDto.IsSuccess)
                {
                    ProductDto?  productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Result));
                    return View(productDto);
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
                }
                return NotFound();
          
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? responseDto=await _productService.UpdateProductAsync(productDto);
                if(responseDto!= null && responseDto.IsSuccess)
                {
                    TempData["success"] = "Product successfully Edited";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
                }  
            }
            return View(productDto);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
             ResponseDto? responseDto=await _productService.GetProductAsync(id);
                if(responseDto!=null && responseDto.IsSuccess)
                {
                    ProductDto? productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Result));
                    return View(productDto);
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
                }
         
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductDto product)
        {
            ResponseDto? responseDto = await _productService.DeleteProductAsync(product.ProductId);
            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = "Product successfully Deleted";
                // RedirectToAction(nameof(CouponIndex));
                return RedirectToAction("CouponIndex", "Coupon");
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return View(product);
        }

    }
    
}
