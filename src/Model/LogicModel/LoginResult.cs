using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Model.LogicModel
{
    public class LoginResult
    {
        public string Result { set; get; }
        public object Data { set; get; }

        public static string GetDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                  (DescriptionAttribute[])fi.GetCustomAttributes(
                  typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
    }

    public enum LoginResultType
    {
        [Description("Success")]
        Success,
        [Description("Fail")]
        Fail
    }

}
