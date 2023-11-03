using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Models;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
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
                    return new ServiceResponse
                    {
                        Status = ResponseStatusCode.EXIST,
                        Message = "Already Exist",
                        Result = null!,
                    };

                User user = await _userRepo.CreateAsync(request);
                if (user != null)
                {
                    var apiKey = await _apiKeyRepo.CreateAsync(new Entities.KeyEntity
                    {
                        UserId = user.Id,
                        Key = Guid.NewGuid().ToString(),

                    });

                    return new ServiceResponse
                    {
                        Status = ResponseStatusCode.CREATED,
                        Message = "Created Succssefully",
                        Result = new UserCreateResult
                        {
                            User = user,
                            ApiKey = apiKey.Key
                        }
                    };
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new ServiceResponse
            {
                Status = ResponseStatusCode.ERROR,
                Message = "Something went wrong"
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

    }
}
