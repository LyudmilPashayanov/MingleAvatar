using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using Mingle.Model;
using Mingle.Controllers;
using Mingle.Repository;
using Mingle.Exceptions;
using Mingle;
using System.Web.Http.Results;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Threading;

namespace MingleUnitTests
{
    public class MingleSerializerTests
    {
        [Fact]
        public void Serialize_And_Deserialize_Work_Correct() 
        {
            //Arrange
            AvatarsRepository repo = new AvatarsRepository();
            List<Avatar> testProducts = new List<Avatar>();
            testProducts.Add(new Avatar { Id = "MingleSPORTS", Name = "Mingle", Color = "Blue", CanMineUnobtainium = true, Shoesize = 80 });
            testProducts.Add(new Avatar { Id = "J00s7", Name = "Joost", Color = "Blue", CanMineUnobtainium = true, Shoesize = 44.5f });
            repo.avatars = testProducts;

            //Act
            Thread.Sleep(200); // this is added as it interferes with the other tests and it returns IOException if all are run simultaneously.
            MingleSerializer.SerializeAvatars(repo);
            AvatarsRepository actedRepo = MingleSerializer.DeserializeAvatars();

            //Assert
            actedRepo.Should().BeEquivalentTo(repo);
        }
    }
}
