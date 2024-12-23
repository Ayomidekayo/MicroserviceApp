using AutoMapper;
using Mango.Servicces.ProductAPI.Data;
using Mango.Servicces.ProductAPI.Migrations;
using Mango.Servicces.ProductAPI.Model;
using Mango.Servicces.ProductAPI.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Servicces.ProductAPI.Controllers
{
     [Route("api/product")]
    [ApiController]
    ///[Authorize]
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;

        public ProductController(AppDbContext appDbContext,IMapper mapper)
        {
            this._appDbContext = appDbContext;
            this._mapper = mapper;
            _responseDto= new();
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> products = _appDbContext.Products.ToList();
                _responseDto.Result=(_mapper.Map<List<ProductDto>>(products));
            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess=false;
                _responseDto.Message=ex.Message;
            }
           return _responseDto;
        }
        [HttpGet("{id:int}")]
        public ResponseDto Product(int id)
        {
            try
            {
                Product prodct = _appDbContext.Products.Find(id);
                _responseDto.Result = _mapper.Map<ProductDto>(prodct);
            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
      
        [HttpPost]
        public ResponseDto AddProduct([FromBody] ProductDto dto)
        {
            try
            {
                Product prodct =  _mapper.Map<Product>(dto);
                _appDbContext.Products.Add(prodct);
                _appDbContext.SaveChanges();
                _responseDto.Result= prodct;

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpPut]
        public ResponseDto update(ProductDto dto)
        {
            try
            {
                Product product=_mapper.Map<Product>(dto);
                _appDbContext.Products.Update(product);
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpDelete("{id:int}")]
        
        public ResponseDto Delete(int id)
        {
            try
            {
                Product product=_appDbContext.Products.Find(id);
                _appDbContext.Products.Remove(product);
                _appDbContext.SaveChanges() ;
            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
