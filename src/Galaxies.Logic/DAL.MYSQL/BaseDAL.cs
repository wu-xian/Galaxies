using Galaxies.Logic.IDAL;
using Galaxies.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Galaxies.Logic.DAL.MYSQL
{
    public class BaseDAL<T> : IBaseDAL<T> where T : class, new()
    {
        #region Field & Property
        protected GalaxiesDbContext db;
        public GalaxiesDbContext Raw
        {
            get
            {
                return db;
            }
        }
        #endregion

        #region Add
        public int Add(T t)
        {
            db.Set<T>().Attach(t);
            db.Set<T>().Add(t);
            return db.SaveChanges();
        }

        public int AddList(IList<T> t)
        {
            foreach (var item in t)
            {
                db.Set<T>().Attach(item);
                db.Set<T>().Add(item);
            }
            return db.SaveChanges();
        }
        #endregion

        #region Delete
        public int Delete(T t)
        {
            db.Set<T>().Attach(t);
            db.Set<T>().Remove(t);
            return db.SaveChanges();
        }

        public int DeleteList(IList<T> t)
        {
            foreach (var item in t)
            {
                db.Set<T>().Attach(item);
                db.Set<T>().Remove(item);
            }
            return db.SaveChanges();
        }
        #endregion

        #region Modify
        public int Modify(T t)
        {
            EntityEntry entry = db.Entry<T>(t);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges();
        }

        public int ModifyList(IList<T> t)
        {
            foreach (var item in t)
            {
                EntityEntry entry = db.Entry<T>(item);
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            return db.SaveChanges();
        }
        public int Modify(Expression<Func<T, bool>> whereLambda, Action<T> setter)
        {
            var dbQuery = db.Set<T>().Where(whereLambda).ToList();
            foreach (var item in dbQuery)
            {
                setter(item);
            }
            return ModifyList(dbQuery);
        }

        #endregion

        #region Read List
        public IList<T> PagingList(Expression<Func<T, bool>> whereLambda, int pageIndex, int pageSize)
        {
            return db.Set<T>().Where(whereLambda)
                .Skip(pageIndex)
                .Take(pageSize)
                .ToList();
        }

        public IList<T> PagingList<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy, int pageIndex, int pageSize, ref int rowCount, bool isAsc = true)
        {
            rowCount = db.Set<T>().Where(whereLambda).Count();
            if (isAsc)
            {
                return db.Set<T>().OrderBy(orderBy).Where(whereLambda).Skip(pageIndex).Take(pageSize).ToList();
            }
            else
            {
                return db.Set<T>().OrderByDescending(orderBy).Where(whereLambda).Skip(pageIndex).Take(pageSize).ToList();
            }
        }

        public IList<T> Query(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).ToList();
        }
        public IList<T> All()
        {
            return db.Set<T>().ToList();
        }

        public T First()
        {
            return db.Set<T>().FirstOrDefault();
        }

        #endregion
    }
}
