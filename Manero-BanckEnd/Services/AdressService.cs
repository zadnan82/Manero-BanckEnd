using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using System.Diagnostics;
using Manero_BanckEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace Manero_BanckEnd.Services
{
    public class AddressService
    {
        private readonly DataContext _dbContext;
        private readonly ProfileRepo _profileRepo;
        private readonly UserRepo _userRepo;
        private readonly AddressRepo _addressRepo;
        private readonly AddressTypeRepo _addressTypeRepo;

        public AddressService(DataContext dbContext, ProfileRepo profileRepo, UserRepo userRepo, AddressRepo addressRepo, AddressTypeRepo addressTypeRepo)
        {
            _dbContext = dbContext;
            _profileRepo = profileRepo;
            _userRepo = userRepo;
            _addressRepo = addressRepo;
            _addressTypeRepo = addressTypeRepo;
        }

        public async Task<ServiceResponse> CreateAddress(string userEmail, string streetName, AddressCreateRequest request)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(a => a.Email == userEmail);

                if (user == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "user not found" };
                }

                var existingAddress = await _dbContext.AddressTypes.FirstOrDefaultAsync(a => a.Address.StreetName == streetName);

                if (existingAddress != null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Address already exists." };
                }

                var addressEntity = new AddressEntity
                {
                    StreetName = request.StreetName,
                    City = request.City,
                    Zipcode = request.Zipcode,


                };

                var newAddress = await _addressRepo.CreateAsync(addressEntity);



                if (newAddress != null)
                {
                    existingAddress.Address.StreetName = request.StreetName;
                    existingAddress.Address.City = request.City;
                    existingAddress.Address.Zipcode = request.Zipcode;
                    existingAddress.User.FirstName = request.FirstName;
                    existingAddress.User.LastName = request.LastName;
                    existingAddress.Title = request.Title;
                    existingAddress.AddressId = request.AddressId;

                    await _addressTypeRepo.UpdateAsync(existingAddress);
                    return new ServiceResponse
                    {
                        Status = ResponseStatusCode.CREATED,
                        Message = "Created Successfully",
                    };
                }


                return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Error creating address" };


            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating address: {ex}");
                return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Error creating address" };
            }
        }

        public async Task<ServiceResponse> GetAddress(string streetName)
        {
            try
            {
                var address = await _dbContext.Addresses.FirstOrDefaultAsync(u => u.StreetName == streetName);

                if (address == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "Address not found" };
                }

                var addressEntity = await _dbContext.Addresses.FirstOrDefaultAsync(p => p.Id == address.Id);

                if (addressEntity == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "Address not found" };
                }

                

                return new ServiceResponse { Status = ResponseStatusCode.OK, Result = addressEntity };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting address: {ex}");
                return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Error getting address" };
            }
        }

        public async Task<ServiceResponse> UpdateAddress(string userEmail, AddressUpdateRequest request)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
                
                if (user == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "User not found" };
                }

                var existingAddress = await _dbContext.AddressTypes.FirstOrDefaultAsync(p => p.UserId == user.Id);

                if (existingAddress == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "Address not found" };
                }

                existingAddress.Address.StreetName = request.StreetName;
                existingAddress.Address.City = request.City;
                existingAddress.Address.Zipcode = request.Zipcode;
                existingAddress.User.FirstName = request.FirstName;
                existingAddress.User.LastName = request.LastName;
                existingAddress.AddressId = request.AddressId;
                existingAddress.Title = request.Title;

                
                _dbContext.AddressTypes.Update(existingAddress);
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();

                return new ServiceResponse { Status = ResponseStatusCode.OK, Message = "Addresses updated successfully" };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating profile: {ex}");
                return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Error updating profile" };
            }
        }
    }
}
