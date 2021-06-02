using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using Mingle.Entinies;
using Mingle.Controllers;
using Mingle.Services;
using Mingle.Exceptions;
using System.Web.Http.Results;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
namespace MingleUnitTests
{
    public class AvatarControllerTests
    {
        [Fact]
        public void GetAllAvatars_Returns_Correct_Number_Of_Avatars()
        {
            //Arrange
            var avatarRepositoryMock = new Mock<IAvatarsRepository>();
            avatarRepositoryMock.Setup(rep => rep.GetAllAvatars())
                .Returns(GetTestAvatars());
            var controller = new AvatarsController(avatarRepositoryMock.Object);
   
            //Act

            var actionResult = controller.GetAllAvatars();
            var contentResult = actionResult as OkObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            contentResult.Value.Should().BeEquivalentTo(GetTestAvatars());
        }

        [Fact]
        public void GetAllAvatars_Returns_Wrong_Number_Of_Avatars()
        {
            //Arrange
            var ListPlusOne = GetTestAvatars();
            ListPlusOne.Add(new Avatar() { Id = "123", Name = "Mr. Wrong", Shoesize = 23.4f, CanMineUnobtainium = false, Color = "Blue" });
            var avatarRepositoryMock = new Mock<IAvatarsRepository>();
            avatarRepositoryMock.Setup(rep => rep.GetAllAvatars())
                .Returns(ListPlusOne);
            var controller = new AvatarsController(avatarRepositoryMock.Object);

            //Act

            var actionResult = controller.GetAllAvatars();
            var contentResult = actionResult as OkObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            contentResult.Value.Should().NotBeEquivalentTo(GetTestAvatars());
        }

        [Fact]
        public void GetById_Returns_The_Correct_Avatar()
        {
            //Arrange
            var ListPlusOneAvatar = GetTestAvatars();
            var newAvatar = new Avatar() { Id = "testAvatar", Name = "Mr. Wrong", Shoesize = 23.4f, CanMineUnobtainium = false, Color = "Blue" };
            ListPlusOneAvatar.Add(newAvatar);
            var avatarRepositoryMock = new Mock<IAvatarsRepository>();
            avatarRepositoryMock.Setup(rep => rep.GetAvatarById("testAvatar"))
                .Returns(newAvatar);
            var controller = new AvatarsController(avatarRepositoryMock.Object);

            //Act

            var actionResult = controller.GetById("testAvatar");
            var contentResult = actionResult as OkObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            contentResult.Value.Should().BeEquivalentTo(newAvatar);
        }
        [Fact]
        public void GetById_Returns_NotFoundResult()
        {
            //Arrange
            var ListPlusOneAvatar = GetTestAvatars();
            var newAvatar = new Avatar() { Id = "testAvatar", Name = "Mr. Wrong", Shoesize = 23.4f, CanMineUnobtainium = false, Color = "Blue" };
            ListPlusOneAvatar.Add(newAvatar);
            var avatarRepositoryMock = new Mock<IAvatarsRepository>();
            avatarRepositoryMock.Setup(rep => rep.GetAvatarById("testAvatar"))
                .Returns(newAvatar);
            var controller = new AvatarsController(avatarRepositoryMock.Object);

            //Act

            var actionResult = controller.GetById("test_Avatar");
            //var contentResult = actionResult as NotFoundObjectResult;

            //Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundResult>(actionResult);
        }

        private List<Avatar> GetTestAvatars()
        {
            List<Avatar> testProducts = new List<Avatar>();
            testProducts.Add(new Avatar { Id = "MingleSPORTS", Name = "Mingle", Color = "Blue" , CanMineUnobtainium= true, Shoesize = 80});
            testProducts.Add(new Avatar { Id = "J00s7", Name = "Joost", Color = "Blue", CanMineUnobtainium = true, Shoesize = 44.5f });
            testProducts.Add(new Avatar { Id = "Au_ke", Name = "Auke", Color = "Red", CanMineUnobtainium = true, Shoesize = 43 });
            testProducts.Add(new Avatar { Id = "Lyudmil_15", Name = "Lyudmil", Color = "Red", CanMineUnobtainium = true , Shoesize= 45.5f});

            return testProducts;
        }
    }
}
