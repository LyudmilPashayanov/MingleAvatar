using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using Mingle.Model;
using Mingle.Controllers;
using Mingle.Repository;
using Mingle.Exceptions;
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
                .Returns(Moq.It.IsAny<List<Avatar>>);
            var controller = new AvatarsController(avatarRepositoryMock.Object);

            //Act
            var actionResult = controller.GetAllAvatars();

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(actionResult);
        }

        [Fact]
        public void GetById_Returns_The_Correct_Avatar()
        {
            //Arrange
            var newAvatar = new Avatar() { Id = "testAvatar", Name = "Mr. Wrong", Shoesize = 23.4f, CanMineUnobtainium = false, Color = "Blue" };
            var avatarRepositoryMock = new Mock<IAvatarsRepository>();
            avatarRepositoryMock.Setup(rep => rep.GetAvatarById("testAvatar"))
                .Returns(newAvatar);
            var controller = new AvatarsController(avatarRepositoryMock.Object);

            //Act
            var actionResult = controller.GetById("testAvatar");
            var contentResult = actionResult as OkObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(actionResult);
            contentResult.Value.Should().BeEquivalentTo(newAvatar);
        }
        [Fact]
        public void GetById_Returns_NotFoundResult()
        {
            //Arrange
            var avatarRepositoryMock = new Mock<IAvatarsRepository>();
            avatarRepositoryMock.Setup(rep => rep.GetAvatarById(It.IsAny<string>()))
                .Throws(new AvatarNotFoundException(It.IsAny<string>()));
            var controller = new AvatarsController(avatarRepositoryMock.Object);

            //Act
            var actionResult = controller.GetById("test_Avatar");

            //Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundObjectResult>(actionResult);
        }

        [Fact]
        public void AddAvatar_Correct_Creates_And_Returns_New_Avatar()
        {
            //Arrange
            var newAvatar = new Avatar() { Id = "testAvatar", Name = "Mr. Wrong", Shoesize = 23.4f, CanMineUnobtainium = false, Color = "Blue" };
            var avatarRepositoryMock = new Mock<IAvatarsRepository>();
            avatarRepositoryMock.Setup(rep => rep.CreateAvatar(newAvatar))
                .Returns(newAvatar);
            var controller = new AvatarsController(avatarRepositoryMock.Object);

            //Act
            var actionResult = controller.AddAvatar(newAvatar);
            var contentResult = actionResult as CreatedAtActionResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<Microsoft.AspNetCore.Mvc.CreatedAtActionResult>(actionResult);
            contentResult.Value.Should().BeEquivalentTo(newAvatar);
        }

        [Fact]
        public void AddAvatar_Returns_BadRequest_When_Empty_Id_Given()
        {
            //Arrange
            var newAvatar = new Avatar() { Name = "Mr. Wrong", Shoesize = 23.4f, CanMineUnobtainium = false, Color = "Blue" };
            var avatarRepositoryMock = new Mock<IAvatarsRepository>();
            avatarRepositoryMock.Setup(rep => rep.CreateAvatar(newAvatar))
                .Returns(newAvatar);
            var controller = new AvatarsController(avatarRepositoryMock.Object);

            //Act
            var actionResult = controller.AddAvatar(newAvatar);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public void AddAvatar_Returns_BadRequestResult_Because_AvatarAlreadyExistedException()
        {
            //Arrange
            var newAvatar = new Avatar() { Id = "testAvatar", Name = "Mr. Wrong", Shoesize = 23.4f, CanMineUnobtainium = false, Color = "Blue" };
            var avatarRepositoryMock = new Mock<IAvatarsRepository>();  
            avatarRepositoryMock.Setup(rep => rep.CreateAvatar(Moq.It.IsAny<Avatar>()))
                .Throws(new AvatarAlreadyExistsException(It.IsAny<string>())); 
            var controller = new AvatarsController(avatarRepositoryMock.Object);

            //Act
             var actionResult = controller.AddAvatar(newAvatar);
           
            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public void DeleteAvatar_Successfully_Returns_NoContentResult()
        {
            //Arrange
            var avatarRepositoryMock = new Mock<IAvatarsRepository>();
            var controller = new AvatarsController(avatarRepositoryMock.Object);

            //Act
            var actionResult = controller.DeleteAvatar("testAvatar");

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<Microsoft.AspNetCore.Mvc.NoContentResult>(actionResult);
        }

        [Fact]
        public void DeleteAvatar_Returns_NotFoundResult_Because_AvatarNotFoundException()
        {
            //Arrange
            var newAvatar = new Avatar() { Id = "testAvatar", Name = "Mr. Wrong", Shoesize = 23.4f, CanMineUnobtainium = false, Color = "Blue" };
            var avatarRepositoryMock = new Mock<IAvatarsRepository>();
            avatarRepositoryMock.Setup(rep => rep.DeleteAvatar("testAvatar"))
                .Throws(new AvatarNotFoundException(It.IsAny<string>()));
            var controller = new AvatarsController(avatarRepositoryMock.Object);

            //Act
            var actionResult = controller.DeleteAvatar("testAvatar");

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundObjectResult>(actionResult);
        }
    }
}
