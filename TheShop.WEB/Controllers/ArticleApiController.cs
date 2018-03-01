using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheShop.BL.IRepositories;
using TheShop.DAL.DTO;

namespace TheShop.WEB.Controllers
{
    [RoutePrefix("api/ArticleApi")]
    public class ArticleApiController : ApiController
    {
        private IArticleRepository articleRepository;

        public ArticleApiController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        [HttpGet]
        [Route("GetArticles")]
        public IHttpActionResult GetArticles()
        {
            try
            {
                return Ok(articleRepository.GetArticles());
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error getting articles list data. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }

        [HttpGet]
        [Route("GetArticleById")]
        public IHttpActionResult GetArticleById(int articleId)
        {
            try
            {
                return Ok(articleRepository.GetArticleById(articleId));
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error getting article by Id. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }

        [HttpPost]
        [Route("InsertArticle")]
        public IHttpActionResult InsertArticle([FromBody]ArticleDTO article)
        {
            try
            {
                return Ok(articleRepository.InsertArticle(article));
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error inserting article in db. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }

        [HttpPost]
        [Route("UpdateArticle")]
        public IHttpActionResult UpdateArticle([FromBody]ArticleDTO article)
        {
            try
            {
                return Ok(articleRepository.UpdateArticle(article));
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error updating article in db. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }

        [HttpGet]
        [Route("RemoveArticle")]
        public IHttpActionResult RemoveArticle(int articleId)
        {
            try
            {
                return Ok(articleRepository.RemoveArticle(articleId));
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error removing article from db. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }
    }
}
