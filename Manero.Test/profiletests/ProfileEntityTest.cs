
using Manero_BanckEnd.Entities;

namespace Manero.Test.profiletests;

public class ProfileEntityTest
{
    [Fact]
    public void ProfileEntity_Creation_Successful()
    {
        // Arrange
        var userId = "1";
        var imageUrl = "https://example.com/image.jpg";
        var location = "japan";
        var phoneNumber = "987654321";

        // Act
        var profileEntity = new ProfileEntity
        {
            UserId = userId,
            ImageUrl = imageUrl,
            Location = location,
            PhoneNumber = phoneNumber
        };

        // Assert
        Assert.NotNull(profileEntity);
        Assert.Equal(userId, profileEntity.UserId);
        Assert.Equal(imageUrl, profileEntity.ImageUrl);
        Assert.Equal(location, profileEntity.Location);
        Assert.Equal(phoneNumber, profileEntity.PhoneNumber);
    }
}
