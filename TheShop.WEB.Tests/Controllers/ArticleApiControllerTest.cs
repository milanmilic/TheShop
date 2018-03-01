using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheShop.WEB.Controllers;
using Moq;
using TheShop.BL.IRepositories;
using System.Collections.Generic;
using TheShop.DAL.DTO;
using System.Web.Http;
using System.Web.Http.Results;

namespace TheShop.WEB.Tests.Controllers
{
    [TestClass]
    public class ArticleApiControllerTest
    {
        [TestMethod]
        public void GetArticleByIdTest()
        {
            //Arrange
            var articleRepository = new Mock<IArticleRepository>();
            articleRepository.Setup(x => x.GetArticleById(1)).Returns(new ArticleDTO { ArticleId = 1 });

            var controller = new ArticleApiController(articleRepository.Object);

            //Act
            IHttpActionResult actionResult = controller.GetArticleById(1);
            var contentResult = actionResult as OkNegotiatedContentResult<ArticleDTO>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.ArticleId);
        }

        [TestMethod]
        public void GetArticleReturnsNotFound()
        {
            // Arrange
            var articleRepository = new Mock<IArticleRepository>();
            var controller = new ArticleApiController(articleRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetArticleById(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<ArticleDTO>));
        }

        [TestMethod]
        public void DeleteArticleReturnsOk()
        {
            // Arrange
            var articleRepository = new Mock<IArticleRepository>();
            var controller = new ArticleApiController(articleRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.RemoveArticle(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<bool>));
        }
    }
}
