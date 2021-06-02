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
    public class AvatarsRepositoryTests
    {
        [Fact]
        public void CreateAvatar_Adds_Avatar_Successfully()
        {
            //Arrange
            var repository = new AvatarsRepository();
            repository.avatars = GetTestAvatars();
            var newAvatar = new Avatar() { Id = "testAvatar", Name = "Mr. Wrong", Shoesize = 23.4f, CanMineUnobtainium = false, Color = "Blue" };

            //Act
            repository.CreateAvatar(newAvatar);

            //Assert
            repository.avatars.Should().NotBeEquivalentTo(GetTestAvatars());
        }

        [Fact]
        public void CreateAvatar_Throws_Exception_Successfully()
        {
            //Arrange
            var repository = new AvatarsRepository();
            repository.avatars = GetTestAvatars();
            var newAvatar = new Avatar() { Id = "MingleSPORTS", Name = "Mr. Wrong", Shoesize = 23.4f, CanMineUnobtainium = false, Color = "Blue" };

            //Act           
            Action act = () => repository.CreateAvatar(newAvatar);

            //assert
            Assert.Throws<AvatarAlreadyExistsException>(act);
        }
        
             [Fact]
        public void GetAvatarById_Returns_Correct_Avatar()
        {
            //Arrange
            var repository = new AvatarsRepository();
            repository.avatars = GetTestAvatars();
            var newAvatar = new Avatar() { Id = "Mingle", Name = "Mr. Wrong", Shoesize = 23.4f, CanMineUnobtainium = false, Color = "Blue" };
            repository.avatars.Add(newAvatar);
            //Act           
            var result = repository.GetAvatarById("Mingle");

            //assert
            Assert.Equal(newAvatar, result);
        }

        [Fact]
        public void GetAvatarById_Returns_Null()
        {
            //Arrange
            var repository = new AvatarsRepository();
            repository.avatars = GetTestAvatars();

            //Act           
            Action act = () => { repository.GetAvatarById("MingleTest"); };

            //assert
            Assert.Throws<AvatarNotFoundException>(act);
        }

        [Fact]
        public void DeleteAvatar_Successfully()
        {
            //Arrange
            var repository = new AvatarsRepository();
            repository.avatars = GetTestAvatars();
            var newAvatar = new Avatar() { Id = "Mingle", Name = "Mr. Wrong", Shoesize = 23.4f, CanMineUnobtainium = false, Color = "Blue" };
            repository.avatars.Add(newAvatar);

            //Act           
            repository.DeleteAvatar("Mingle");

            //assert
            repository.avatars.Should().BeEquivalentTo(GetTestAvatars());
        }

        [Fact]
        public void DeleteAvatar_Throws_AvatarNotFoundException()
        {
            //Arrange
            var repository = new AvatarsRepository();
            repository.avatars = GetTestAvatars();

            //Act           
            Action act = () => { repository.DeleteAvatar("Mingle"); };

            //assert
            Assert.Throws<AvatarNotFoundException>(act);
        }

        private List<Avatar> GetTestAvatars()
        {
            List<Avatar> testProducts = new List<Avatar>();
            testProducts.Add(new Avatar { Id = "MingleSPORTS", Name = "Mingle", Color = "Blue", CanMineUnobtainium = true, Shoesize = 80 });
            testProducts.Add(new Avatar { Id = "J00s7", Name = "Joost", Color = "Blue", CanMineUnobtainium = true, Shoesize = 44.5f });
            testProducts.Add(new Avatar { Id = "Au_ke", Name = "Auke", Color = "Red", CanMineUnobtainium = true, Shoesize = 43 });
            testProducts.Add(new Avatar { Id = "Lyudmil_15", Name = "Lyudmil", Color = "Red", CanMineUnobtainium = true, Shoesize = 45.5f });

            return testProducts;
        }
    }
}
