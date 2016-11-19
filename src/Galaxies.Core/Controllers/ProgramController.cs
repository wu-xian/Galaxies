using Galaxies.Logic.BIZ;
using Galaxies.Model.LogicModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Core.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class ProgramController : ControllerBase
    {
        private ProgramForWebBIZ programForWebBIZ;

        public ProgramController(ProgramForWebBIZ _programForWebBIZ)
        {
            programForWebBIZ = _programForWebBIZ;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPagging(PagingRequestModel pagingModel)
        {
            int count = 0;
            var dbResult = programForWebBIZ.GetPaging(d => true, pagingModel.Offset, pagingModel.Limit, ref count);
            return TableJson(dbResult, count);
        }
    }
}
