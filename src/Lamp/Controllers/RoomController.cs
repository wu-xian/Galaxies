using Lamp.BIZ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lamp.Controllers
{
    [AllowAnonymous]
    public class RoomController : Controller
    {
        private RoomBIZ roomBIZ;
        public RoomController(RoomBIZ _roomBIZ)
        {
            roomBIZ = _roomBIZ;
        }

        public IActionResult GetPaging(int index, int pageSize)
        {
            return Json(roomBIZ.GetRoomsByPaging(d => true, index, pageSize));
        }

        public IActionResult GetList()
        {
            return Json(roomBIZ.GetRoomsByPaging(d => true, 1, 50));
        }

        public IActionResult Add(string roomName)
        {
            return Json(roomBIZ.Add(new Model.EntityModel.Room()
            {
                InTime = DateTime.Now,
                InUse = true,
                Name = roomName
            }));
        }
    }
}
