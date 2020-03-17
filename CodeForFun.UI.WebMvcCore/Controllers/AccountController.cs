﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using CodeForFun.Repository.Business.Abstract.Services;
using CodeForFun.Repository.DataAccess.DbContexts;
using CodeForFun.UI.WebMvcCore.Models;
using CodeForFun.UI.WebMvcCore.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace CodeForFun.UI.WebMvcCore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private IAuth _auth;

		public AccountController(IAuth auth)
		{
			_auth = auth;

		}

		// POST: api/Account
		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Post([FromBody]UserForRegisterViewModel userForRegister)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			var user = new User
			{
				Email = userForRegister.Email,
				Password = userForRegister.Password,
				Name = userForRegister.Name,
				Surname = userForRegister.Surname
			};

			try
			{
				var isReg = _auth.Register(user, user.Password);

				if (isReg == "")
					throw new Exception();
			}
			catch (Exception ex)
			{
				return StatusCode(500);
			}

			var token = _auth.Login(user.Email, user.Password);

			return Ok(new
			{
				token = tokenHandler.WriteToken(token)
			});

		}

		[HttpPost]
		[Route("login")]
		public IActionResult Login(UserForLoginViewModel model)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = _auth.Login(model.Email, model.Password);

			if (token == null)
				return Unauthorized();

			return Ok(new
			{
				token = tokenHandler.WriteToken(token)
			});
		}



	}
}
