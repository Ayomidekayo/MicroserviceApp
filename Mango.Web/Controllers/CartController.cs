﻿using Mango.Web.Models;
using Mango.Web.Models.DTO;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Mango.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            this._cartService = cartService;
        }
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadrCartDtoBaseOnUserLogin());
        }
       
        private async Task<CartDto> LoadrCartDtoBaseOnUserLogin()
        {
            var userId=User.Claims.Where(u=>u.Type==JwtRegisteredClaimNames.Sub)?.
                FirstOrDefault()?.Value;
            ResponseDto? response = await _cartService.GetCartByUserAsnc(userId);
            if (response!=null & response.IsSuccess)
            {
                CartDto cartDto =JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto; 
            }
            return new CartDto();
        }
    }
}
