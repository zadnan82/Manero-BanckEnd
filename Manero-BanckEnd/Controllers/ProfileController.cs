﻿using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Models;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Manero_BanckEnd.Controllers
{
    [Route("api/Profile")]
    [ApiController]
    [UseApiKey]
    [Authorize(Policy = "JwtPolicy")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _profileService;

        public ProfileController(ProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProfile(ProfileCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var userEmail = User.FindFirstValue(ClaimTypes.Email); 
                var result = await _profileService.CreateProfile(userEmail, request);

                return result.Status switch
                {
                    ResponseStatusCode.CREATED => Created("", result),
                    ResponseStatusCode.ERROR => Conflict(result.Message),
                    ResponseStatusCode.NOTFOUND => NotFound(result.Message),
                    _ => Problem(result.Message),
                }; 
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Problem();
            }
        }


        [HttpGet("Get")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                
                var userEmail = User.FindFirstValue(ClaimTypes.Email); 
                var result = await _profileService.GetProfile(userEmail);

                return result.Status switch
                {
                    ResponseStatusCode.OK => Ok(result.Result),
                    ResponseStatusCode.NOTFOUND => NotFound(result.Message),
                    _ => Problem(result.Message),
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Problem();
            }
        }


        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProfile(ProfileUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                var result = await _profileService.UpdateProfile(userEmail, request);

                return result.Status switch
                {
                    ResponseStatusCode.OK => Ok(result),
                    ResponseStatusCode.ERROR => Conflict(result.Message),
                    ResponseStatusCode.NOTFOUND => NotFound(result.Message),
                    _ => Problem(result.Message),
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Problem();
            }
        }
    }
}
 
