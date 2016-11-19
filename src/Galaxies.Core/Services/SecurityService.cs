using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Services
{
    public class SecurityService
    {
        private const string X_KEY = "MY_NAME_IS_WUXIAN";
        private IDataProtector protector;
        public SecurityService(IDataProtectionProvider _protector)
        {
            protector = _protector.CreateProtector(X_KEY);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Encryption(string value)
        {
            return protector.Protect(value);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Devalue"></param>
        /// <returns></returns>
        public string Decryption(string Devalue)
        {
            return protector.Unprotect(Devalue);
        }
    }
}
