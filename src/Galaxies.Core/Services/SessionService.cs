using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Galaxies.Core.Services
{
    public class SessionService
    {
        private ISession session;
        public Guid id = Guid.NewGuid();

        public SessionService(IHttpContextAccessor accessor)
        {
            session = accessor.HttpContext?.Session;
        }

        /// <summary>
        /// 暂时解决 中间件中 生成单例IHttpContextAccessor
        /// </summary>
        /// <param name="_session"></param>
        public void Set(ISession _session)
        {
            session = _session;
        }

        public void Obj2Session(string key, object value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            if (null == value) throw new ArgumentNullException(nameof(value));
            string jsonString = JsonConvert.SerializeObject(value);
            if (string.IsNullOrEmpty(jsonString)) throw new FormatException(nameof(value));
            session.SetString(key, jsonString);
        }

        public void Int2Session(string key, int value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            session.SetInt32(key, value);
        }

        public void String2Session(string key, string value)
        {
            if (null == key) throw new ArgumentNullException(nameof(key));
            session.SetString(key, value);
        }

        public T GetObj<T>(string key) where T : class
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            string objString = session.GetString(key);
            if (string.IsNullOrEmpty(objString)) return null;
            return JsonConvert.DeserializeObject<T>(objString);
        }

        public static T GetObj2<T>(ISession _session, string key) where T : class
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            string objString = _session.GetString(key);
            if (string.IsNullOrEmpty(objString)) return null;
            return JsonConvert.DeserializeObject<T>(objString);
        }

        public int? GetInt(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            return session.GetInt32(key);
        }

        public string GetString(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            return session.GetString(key);
        }

        public void Clear()
        {
            session.Clear();
        }
    }
}
