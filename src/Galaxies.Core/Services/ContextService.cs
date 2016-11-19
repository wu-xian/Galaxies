using Galaxies.Logic.BIZ;
using Galaxies.Model.EntityModel;
using Galaxies.Model.LogicModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Galaxies.Core.Services
{
    public class ContextService
    {
        #region -Private
        private HttpContext context;
        private SecurityService security;
        private SessionService session;
        private UserBIZ userBIZ;
        private UserRoleClaimBIZ userroleclaimBIZ;

        private const string SECURITY_COOKIE_KEY = "_GALAXIES";
        private const string USERSTORE_SESSION_KEY = "_USER_STORE_SESSION_KEY";
        private const string PERMISSION = "_PERMISSION";
        public const string USER = "_USER";
        public const string USER_STORE = "_USER_SOTRE";

        public ContextService(IHttpContextAccessor accessor
            , SessionService _session
            , SecurityService _security
            , UserBIZ _userBIZ
            , UserRoleClaimBIZ _userroleclaimBIZ
            )
        {
            session = _session;
            context = accessor.HttpContext;
            security = _security;
            userBIZ = _userBIZ;
            userroleclaimBIZ = _userroleclaimBIZ;
        }

        private void SetItem<T>(string key, T t)
        {
            session.Obj2Session(key, t);
            //context.Items.Add(key, t);
        }

        private T GetItem<T>(string key) where T : class
        {
            return session.GetObj<T>(key);
            //object value;
            //return context.Items.TryGetValue(key, out value) ? value as T : null;
        }

        private T GetValueItem<T>(string key)
        {
            return (T)context.Items[key];
        }

        public User User
        {
            get
            {
                return GetItem<User>(USER);
            }
            set
            {
                SetItem<User>(USER, value);
            }
        }
        public UserStore UserStore
        {
            get
            {
                return GetItem<UserStore>(USER_STORE);
            }
            set
            {
                SetItem<UserStore>(USER_STORE, value);
            }
        }
        #endregion

        #region Login&Logout
        public bool Login(string userName, string password)
        {
            var aa = User;
            var dbResult = userBIZ.GetUserByUserName(userName);
            if (dbResult == null)
            {
                return false;
            }
            if (!dbResult.Password.Equals(password))
            {
                return false;
            }
            User = dbResult;
            LoadToContext();
            //TODO: set cookie
            return true;
        }

        public bool IsLogin()
        {
            if (User != null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool GetCookie()
        {
            var requestCookies = context.Request.Cookies;
            if (requestCookies != null)
            {
                foreach (var item in requestCookies)
                {
                    if (item.Key == SECURITY_COOKIE_KEY && string.IsNullOrEmpty(item.Value))
                    {
                        Guid cookieUserGuid;
                        try
                        {
                            var decryptionUserId = security.Decryption(item.Value);
                            cookieUserGuid = JsonConvert.DeserializeObject<Guid>(decryptionUserId);
                            if (cookieUserGuid == Guid.Empty)
                            {
                                return false;
                            }
                        }
                        catch
                        {
                            return false;
                        }
                        var cookieUser = userBIZ.GetUserByUserId(cookieUserGuid);
                        if (cookieUser != null)
                        {
                            if (Login(cookieUser.UserName, cookieUser.Password))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void SetCookie()
        {
            if (IsLogin())
            {
                Guid cookieUserid = User.Id;
                if (cookieUserid != Guid.Empty)
                {
                    var jsonUserId = JsonConvert.SerializeObject(cookieUserid);
                    var encrptionUserId = security.Encryption(jsonUserId);
                    context.Response.Cookies.Append(SECURITY_COOKIE_KEY, encrptionUserId);
                }
            }
        }

        public bool Logout()
        {
            User = null;
            UserStore = null;
            //TODO: clear cookie
            return true;
        }
        #endregion

        #region UserStore
        private void LoadToContext()
        {
            UserStore _userStore = new UserStore();
            var userId = User.Id;
            _userStore.Roles = userroleclaimBIZ.GetRolesByUserId(userId);
            _userStore.ProgramForWeb = userBIZ.GetWebProgramByUserId(userId);
            _userStore.UserId = userId;

            SetItem<UserStore>(USER_STORE, _userStore);
        }

        #endregion

        public bool HasPermission(string areaName, string controllerName, string actionName)
        {
            foreach (var item in UserStore.ProgramForWeb)
            {
                if (string.Equals(item.AreaName, areaName, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(item.ControllerName, controllerName, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(item.ActionName, actionName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
