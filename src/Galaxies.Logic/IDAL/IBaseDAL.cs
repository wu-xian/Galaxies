using Galaxies.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Galaxies.Logic.IDAL
{
    public interface IBaseDAL<T> where T : class, new()
    {
        //添加
        int Add(T t);
        //修改
        int Modify(T t);
        /// <summary>
        /// 查询后批量修改
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        int Modify(Expression<Func<T, bool>> whereLambda, Action<T> setter);
        //删除
        int Delete(T t);
        /// <summary>
        /// 根据lambda表达式查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IList<T> Query(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int AddList(IList<T> t);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int ModifyList(IList<T> t);
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int DeleteList(IList<T> t);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        IList<T> PagingList(Expression<Func<T, bool>> whereLambda, int pageIndex, int pageSize);
        /// <summary>
        /// 分页排序查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        IList<T> PagingList<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy, int pageIndex, int pageSize, ref int rowCount, bool isAsc = true);
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IList<T> All();
        /// <summary>
        /// 获取第一个
        /// </summary>
        /// <returns></returns>
        T First();

        /// <summary>
        /// 获取原始DB上下文
        /// </summary>
        GalaxiesDbContext Raw { get; }
    }
}
