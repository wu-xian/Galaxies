using Galaxies.Model.LogicModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Core.Controllers
{
    public class ControllerBase : Controller
    {
        public JsonResult TableJson(object data, int totalCount)
        {
            return Json(new PagingResponseModel()
            {
                Rows = data,
                Total = totalCount
            });
        }

        public JsonResult OperationJson(RequestResultType responseType, object data, string msg)
        {
            return Json(new OperationResponseModel()
            {
                Result = DescriptionHelper.GetDescription(responseType),
                Data = data,
                Message = msg
            });
        }
    }
}
