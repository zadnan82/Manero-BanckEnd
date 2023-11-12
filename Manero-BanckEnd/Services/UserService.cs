using Azure.Core;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Models;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace Manero_BanckEnd.Services
{
    public class UserService
    {
        private readonly UserRepo _userRepo;
        private readonly ApiKeyRepo _apiKeyRepo;

        public UserService(UserRepo userRepo, ApiKeyRepo apiKeyRepo)
        {
            _userRepo = userRepo;
            _apiKeyRepo = apiKeyRepo;
        }

        public async Task<ServiceResponse> CreateUserAsync(UserCreateRequest request)
        {
            try
            {
                if (await _userRepo.ExistAsync(x => x.Email == request.Email))
                {
                    return new ServiceResponse
                    {
                        Status = ResponseStatusCode.EXIST,
                        Message = "Already Exist",
                        Result = null!,
                    };
                }

                User user = await _userRepo.CreateAsync(request);

                if (user != null)
                {
                    var apiKey = await _apiKeyRepo.CreateAsync(new KeyEntity
                    {
                        UserId = user.Id,
                        Key = Guid.NewGuid().ToString(),
                    });

                    return new ServiceResponse
                    {
                        Status = ResponseStatusCode.CREATED,
                        Message = "Created Successfully",
                        Result = new UserCreateResult
                        {
                            User = user,
                            ApiKey = apiKey.Key,
                        },
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Debug.WriteLine(ex.Message);
            }

            return new ServiceResponse
            {
                Status = ResponseStatusCode.ERROR,
                Message = "Something went wrong",
            };
        }

        public async Task<ServiceResponse> LoginUserAsync(UserLoginRequest request)
        {

            try
            {
                var user = await _userRepo.GetAsync(x=> x.Email == request.Email);
                if (user != null)
                {
                    if (user.ValidatePassword(request.Password))
                    {
                        var apiKey = await _apiKeyRepo.GetAsync(x => x.UserId == user.Id);
                        return new ServiceResponse
                        {
                            Status = ResponseStatusCode.OK,
                            Message = "successfully Authorized",
                            Result = new UserLoginResult
                            {
                                User = user,
                                ApiKey = apiKey.Key

                            }
                        };
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new ServiceResponse
            {
                Status = ResponseStatusCode.ERROR,
                Message = "Something went wrong"
            };
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _userRepo.GetAsync(x => x.Email == email);
                if (user != null)
                { 
                    return user;

                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!; 

        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            try
            {
                var user = await _userRepo.GetAsync(x => x.Id == id);
                    return user; 
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public async Task<bool> UpdateUserAsync(User user)
        { 
            try
            {
                UserEntity entity = await _userRepo.GetAsync(x=> x.Id == user.Id);
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;    
                user = await _userRepo.UpdateAsync(entity);
                if (user != null) 
                    return true;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return false;
        }
    }
}