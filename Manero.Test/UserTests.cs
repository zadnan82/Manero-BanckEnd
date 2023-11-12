using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Test;

public class UserTests
{
    //Arrange
    private readonly UserCreateRequest  userCreateRequest =   new UserCreateRequest
        {
             FirstName = "Zainab",
              LastName  = "Adnan",
              Email = "zainab@hotmail.com",
               Password = "Bytmig123!",
        };
     

    [Fact]
    public void UserCreateRequest_Should_Convert_ToUserEntity()
    {
        //Act 
        UserEntity userEntity = userCreateRequest;

        //Assert
        Assert.NotNull(userEntity);
        Assert.IsType<UserEntity>(userEntity);
        Assert.Equal(userCreateRequest.FirstName, userEntity.FirstName);
        Assert.Equal(userCreateRequest.LastName, userEntity.LastName);
        Assert.Equal(userCreateRequest.Email, userEntity.Email);
    }

    [Fact]
    public void GenerateSecurePassword_Should_Return_PasswordAndSecurityKey()
    {
        //Arrange 
        var password = "Bytmig123!";
        var userEntity = new UserEntity();

        //Act 
        userEntity.GenerateSecurPassword(password);

        //Assert 
        Assert.NotNull(userEntity);
        Assert.True(userEntity.Password.Length > 0);
        Assert.True(!string.IsNullOrEmpty(userEntity.Password.ToString()));
        Assert.NotEqual(password, userEntity.Password.ToString());
    }
     

}
