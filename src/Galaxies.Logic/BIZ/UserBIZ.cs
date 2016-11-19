using Galaxies.Logic.IDAL;
using Galaxies.Model.Context;
using Galaxies.Model.EntityModel;
using Galaxies.Model.LogicModel;
using Galaxies.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Galaxies.Logic.BIZ
{
    public class UserBIZ
    {
        #region ctor&CRUD
        private IUserDAL userDAL;
        private IUserRoleClaimDAL userroleclaimDAL;
        private GalaxiesDbContext db;
        public UserBIZ(IUserDAL _userDAL
            , IUserRoleClaimDAL _userroleclaimDAL
            , GalaxiesDbContext _db)
        {
            userDAL = _userDAL;
            userroleclaimDAL = _userroleclaimDAL;
            db = _db;
        }

        public int Add(User model)
        {
            return userDAL.Add(model);
        }

        public int Modify(User model)
        {
            return userDAL.Modify(model);
        }

        public int Delete(User model)
        {
            return userDAL.Delete(model);
        }

        public IList<User> GetPaging(Expression<Func<User, bool>> whereLambda, int index, int pageSize)
        {
            return userDAL.PagingList(whereLambda, index, pageSize);
        }

        public IList<User> Query(Expression<Func<User, bool>> whereLambda)
        {
            return userDAL.Query(whereLambda);
        }
        #endregion

        public User GetUserByUserName(string userName)
        {
            return userDAL.Query(d => d.UserName == userName).FirstOrDefault();
        }

        public User GetUserByUserId(Guid guid)
        {
            return userDAL.Query(d => d.Id == guid).FirstOrDefault();
        }

        /// <summary>
        /// 判断用户是否存在
        ///     存在返回true
        ///     不存在返回false
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsUserExist(string userName)
        {
            var dbResult = userDAL.Query(d => d.UserName == userName);
            if (dbResult == null || dbResult.Count() == 0)
            {
                return false;
            }
            return true;
        }

        public List<ProgramForWeb> GetWebProgramByUserId(Guid userId)
        {
            return db.UserRoleClaim.Where(d => d.UserId == userId)
                .Join(db.Role, urc => urc.RoleId, r => r.Id, (urc, r) => r)//result roles
                .Join(db.ProgramRoleClaim, r => r.Id, prc => prc.RoleId, (r, prc) => prc)//result programroleclaim
                .Join(db.ProgramForWeb, prc => prc.ProgramId, pfw => pfw.Id, (prc, pfw) => pfw).ToList();//result programForWeb
        }

        public List<UserWithRole> GetUserWithRole(Expression<Func<User, bool>> whereUser
            , int index, int size, ref int count)
        {
            var dbResult =
                (
                    from user in db.User.Where(whereUser).Where(d => d.InUse == true).OrderByDescending(d => d.InTime)
                    join roleclaim in db.UserRoleClaim on user.Id equals roleclaim.UserId
                    join role in db.Role on roleclaim.RoleId equals role.Id
                    select new UserWithRole
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        PhoneNo = user.PhoneNo,
                        Email = user.Email,
                        LoginTimes = user.LoginTimes,
                        RealName = user.RealName,
                        InTime = user.InTime,
                        RoleId = role.Id,
                        RoleName = role.Name,
                        RoleDescription = role.Description
                    }
                           );
            count = dbResult.Count();
            return dbResult.Skip(index).Take(size).ToList();
        }

        public List<UserWithRole> GetUserWithRoleByUserAndRole(Func<User, Role, bool> funcUserAndRole
            , int index, int size, ref int count)
        {
            var dbResult = (from user in db.User
                            join roleclaim in db.UserRoleClaim on user.Id equals roleclaim.UserId
                            join role in db.Role on roleclaim.RoleId equals role.Id
                            where funcUserAndRole(user, role) & roleclaim.InUse == true
                            select new UserWithRole
                            {
                                UserId = user.Id,
                                UserName = user.UserName,
                                PhoneNo = user.PhoneNo,
                                Email = user.Email,
                                LoginTimes = user.LoginTimes,
                                RealName = user.RealName,
                                InTime = user.InTime,
                                RoleId = role.Id,
                                RoleName = role.Name,
                                RoleDescription = role.Description
                            }
            );
            count = dbResult.Count();
            return dbResult.Skip(index).Take(size).ToList();
        }

        public int ChangeUserRole(Guid userId, int roleId, Guid operatorId)
        {
            var queryResult = userroleclaimDAL.Query(d => d.UserId == userId).ToList();
            queryResult.ForEach(d =>
            {
                d.InUse = false;
                d.ModifyTime = DateTime.Now;
                d.ModifyUser = operatorId;
            });
            userroleclaimDAL.ModifyList(queryResult);
            return userroleclaimDAL.Add(new UserRoleClaim()
            {
                Id = 0,
                InUse = true,
                RoleId = roleId,
                UserId = userId,
                InTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                ModifyUser = operatorId
            });
        }

        public int Create(User user, Guid operatorId)
        {
            if (null != userDAL.Query(d => d.UserName == user.UserName).ToList().FirstOrDefault())
            {
                user.Id = Guid.NewGuid();
                user.InTime = DateTime.Now;
                user.InUse = true;
                user.ModifyUser = operatorId;
                user.ModifyTime = DateTime.Now;
                user.LoginTimes = 0;
                return userDAL.Add(user);
            }
            return 0;
        }

        public int Delete(Guid userId, Guid operatorId)
        {
            return userDAL.Modify(d => d.Id == userId, user => user.InUse = false);
        }

        public int ModifyPassword(Guid userId, string password, Guid operatorId)
        {
            return userDAL.Modify(d => d.Id == userId, user =>
            {
                user.Password = password;
                user.ModifyTime = DateTime.Now;
                user.ModifyUser = operatorId;
            });
        }

        public int ModifyUser(ModifyUser user, Guid operatorId)
        {
            var userResult = userDAL.Modify(d => d.Id.ToString() == user.UserId, u =>
             {
                 u.RealName = user.RealName;
             });
            var roleResult = userroleclaimDAL.Modify(d => d.UserId.ToString() == user.UserId, u =>
             {
                 u.RoleId = user.RoleId;
                 u.InUse = true;
                 u.ModifyTime = DateTime.Now;
                 u.ModifyUser = operatorId;
             });
            return userResult & roleResult;
        }
    }
}
