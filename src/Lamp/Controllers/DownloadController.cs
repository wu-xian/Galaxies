using Dapper;
using IDAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lamp.Controllers
{
    [AllowAnonymous]
    [Route("/[controller]/{articleId?}/{number?}")]
    public class DownloadController : Controller
    {
        public DownloadController()
        {
        }

        
        //public IActionResult Invoke(Guid articleId,int number)
        //{
        //    var response = dbSession.DbConnection.Query<Repertory>("select DataValue,DataType,DataName from repertory where ArticleId=@ArticleId and Number=@Number",
        //                    new
        //                    {
        //                        ArticleId = articleId,
        //                        Number = number
        //                    }).FirstOrDefault(); ;
        //    return File(response.DataValue, response.DataType, response.DataName);
        //}
    }
}
