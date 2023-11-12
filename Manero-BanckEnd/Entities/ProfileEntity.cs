using Manero_BanckEnd.Models;
using Manero_BanckEnd.Schemas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Manero_BanckEnd.Entities;

public class ProfileEntity
{
    [Key] 
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string UserId { get; set; } = null!;
    public UserEntity User { get; set; } = null!;

    public string? ImageUrl { get; set; } 

    public string? Location { get; set; } 

    public string? PhoneNumber { get; set; }



    //public static implicit operator ProfileEntity(ProfileConvert request)
    //{
    //    try
    //    {
    //        var profileEntity = new ProfileEntity()
    //        { 
    //              ImageUrl = request.ImageUrl,
    //              Location = request.Location,  
    //              PhoneNumber = request.PhoneNumber,
    //              UserId = request.UserId, 
                  
                  
                     
    //        }; 
    //        return profileEntity;
    //    }
    //    catch (Exception ex) { Debug.WriteLine(ex.Message); }
    //    return null!;
    //}

}
