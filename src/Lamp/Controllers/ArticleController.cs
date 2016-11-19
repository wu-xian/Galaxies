using DAL.MySql;
using Dapper;
using Galaxies.Model.LogicModel;
using IDAL;
using Lamp.BIZ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Model.EntityModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lamp.Controllers
{
    [AllowAnonymous]
    public class ArticleController : BaseController
    {
        private ArticleBIZ articleBIZ;

        public ArticleController(ArticleBIZ _articleBIZ)
        {
            articleBIZ = _articleBIZ;
        }

        public IActionResult GetPaging(PagingRequestModel requestModel)
        {
            int count = 0;
            var resultData = articleBIZ.Paging(requestModel.Offset, requestModel.Limit, ref count);
            return Json(new PagingResponseModel()
            {
                Rows = resultData,
                Total = count
            });
        }

        #region MyRegion
        //public IActionResult GetList()
        //{
        //    var list = dbsession.DbConnection.Query<Article>("select * from article");
        //    return Json(list);
        //}

        //public IActionResult GetArticleContent(Guid articleId)
        //{
        //    var list = dbsession.DbConnection.Query("select a.Title,a.Content,r.DataType,r.Number,r.DataLength,r.DataName from article a left join repertory r on a.Id=r.ArticleId where a.Id=@Id ",
        //        new
        //        {
        //            Id = articleId
        //        });
        //    if (list.Count() != 0)
        //    {
        //        dynamic responseData = new
        //        {
        //            Title = list.FirstOrDefault().Title,
        //            Content = list.FirstOrDefault().Content,
        //            Files = new List<dynamic>()
        //        };
        //        foreach (var item in list)
        //        {
        //            responseData.Files.Add(new
        //            {
        //                FileType = item.DataType,
        //                Number = item.Number,
        //                Length = item.DataLength,
        //                Name = item.DataName
        //            });
        //        }
        //        return Json(responseData);
        //    }
        //    else
        //    {
        //        return Json(new
        //        {
        //            msg = "no such file"
        //        });
        //    }
        //}

        //public IActionResult GetTitleList(int index)
        //{
        //    var response = dbsession.DbConnection.Query("select id,Title,Content,InTime,Creator,LoveTimes,HateTimes from article order by InTime limit @index,10",
        //        new
        //        {
        //            index = index * 10
        //        }).ToList();
        //    response.ForEach(d => d.InTime = d.InTime.ToString("MM-dd HH:mm:ss"));
        //    return Json(response);
        //}

        //public IActionResult GetFileList(Guid articleId)
        //{
        //    var response = dbsession.DbConnection.Query<Repertory>("select * from repertory where ArticleId=@ArticleId",
        //        new
        //        {
        //            ArticleId = articleId
        //        });
        //    return Json(response);
        //}

        //public IActionResult DownloadFile(Guid articleId, int number)
        //{
        //    var response = dbsession.DbConnection.Query<Repertory>("select DataValue,DataType,DataName from repertory where ArticleId=@ArticleId and Number=@Number",
        //                    new
        //                    {
        //                        ArticleId = articleId,
        //                        Number = number
        //                    }).FirstOrDefault(); ;
        //    return File(response.DataValue, response.DataType, response.DataName);
        //}

        ////public IActionResult AddNew(IList<IFormFile> files, string json)
        ////{
        ////    Guid articleId = Guid.NewGuid();
        ////    var contentResult = AddContent(json, articleId);
        ////    var fileResult = SaveFile(files, articleId);
        ////    if ((contentResult == 1) && (fileResult == 1))
        ////    {
        ////        return Json(ResultStatu.Success, "normal", "添加成功");
        ////    }
        ////    return Json(ResultStatu.Faild, "normal", $"添加失败,contentResult:{contentResult},fileResult:{fileResult}");

        ////}

        //public IActionResult AddContent(string json)
        //{
        //    var articleId = Guid.NewGuid();
        //    var articleData = JsonConvert.DeserializeObject<Article>(json);
        //    var articleModel = new Article()
        //    {
        //        Id = articleId,
        //        Creator = articleId,
        //        InTime = DateTime.Now,
        //        Title = articleData.Title,
        //        Content = articleData.Content
        //    };
        //    dbsession.DbConnection.Execute("insert into article(Id,Title,Content,InTime,Creator) values(@Id,@Title,@Content,@InTime,@Creator)",
        //        articleModel);
        //    return Json(new
        //    {
        //        Id = articleId
        //    });
        //}

        //public IActionResult SaveFile(IFormFile file, int index, Guid guid)
        //{
        //    List<Repertory> modelList = new List<Repertory>();
        //    var stream = file.OpenReadStream();
        //    long fileSize = stream.Length;
        //    byte[] buffer = new byte[fileSize];
        //    stream.Read(buffer, 0, Convert.ToInt32(fileSize));

        //    Repertory model = new Repertory()
        //    {
        //        ArticleId = guid,
        //        Number = index,
        //        InTime = DateTime.Now,
        //        Creator = guid,//operator id
        //        DataLength = fileSize,
        //        DataType = file.ContentType,
        //        DataName = file.FileName,
        //        DataValue = buffer
        //    };
        //    modelList.Add(model);
        //    return Json(dbsession.DbConnection.Execute("insert into repertory values(@ArticleId,@Number,@DataValue,@DataLength,@DataType,@InTime,@Creator,@DataName)"
        //        , modelList));
        //}

        //public IActionResult SaveFiles(List<IFormFile> files, Guid guid)
        //{
        //    List<Repertory> modelList = new List<Repertory>();
        //    for (int i = 0; i < files.Count; i++)
        //    {

        //        var stream = files[i].OpenReadStream();
        //        long fileSize = stream.Length;
        //        byte[] buffer = new byte[fileSize];
        //        stream.Read(buffer, 0, Convert.ToInt32(fileSize));

        //        Repertory model = new Repertory()
        //        {
        //            ArticleId = guid,
        //            Number = i,
        //            InTime = DateTime.Now,
        //            Creator = guid,//operator id
        //            DataLength = fileSize,
        //            DataType = files[i].ContentType,
        //            DataName = files[i].FileName,
        //            DataValue = buffer
        //        };
        //        modelList.Add(model);
        //    }
        //    return Json(dbsession.DbConnection.Execute("insert into repertory values(@ArticleId,@Number,@DataValue,@DataLength,@DataType,@InTime,@Creator,@DataName)"
        //        , modelList));
        //} 
        #endregion
    }
}
