using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            this._couponService = couponService;
        }
        public async Task< IActionResult> CouponIndex()
        {  
            List<CouponDto>?list= new ();
            ResponseDto? responseDto = await _couponService.GetCouponsAsync();
            if(responseDto != null && responseDto.IsSuccess)
            {
               
                list = JsonConvert.DeserializeObject < List<CouponDto>>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return View(list);
        }
        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto coupon)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(coupon);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Coupon successfully created";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(coupon);
        }
       
        
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            ResponseDto? responseDto = await _couponService.GetCouponByIdAsync(id);
            if (responseDto != null && responseDto.IsSuccess)
            {
              
                CouponDto? coupon = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result));

                return View(coupon);
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCoupon(CouponDto coupon)
        {
            ResponseDto? responseDto = await _couponService.DeleteCouponAsync(coupon.CouponId);
            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = "Coupon successfully Deleted";
                // RedirectToAction(nameof(CouponIndex));
              return  RedirectToAction("CouponIndex", "Coupon");
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return View(coupon);
        }
    }
}
