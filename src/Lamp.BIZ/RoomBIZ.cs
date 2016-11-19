using DAL.MySql;
using IDAL;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lamp.BIZ
{
    public class RoomBIZ
    {
        private LampDbContext db;
        public RoomBIZ(LampDbContext _db)
        {
            db = _db;
        }
        public List<Room> GetRoomsByPaging(Expression<Func<Room, bool>> whereLambda, int index, int pageSize)
        {
            return db.Set<Room>().Where(whereLambda).OrderBy(d => d.InTime).Skip((index - 1) * pageSize).Take(pageSize).ToList();
        }

        public bool Exist(string roomName)
        {
            var dbResult = db.Set<Room>().Where(d => d.Name == roomName).ToList();
            return dbResult != null;
        }

        public int Add(Room room)
        {
            db.Set<Room>().Attach(room);
            db.Set<Room>().Add(room);
            return db.SaveChanges();
        }

        public Room GetRoomById(int roomId)
        {
            return db.Room.Where(d => d.Id == roomId).FirstOrDefault();
        }
    }
}
