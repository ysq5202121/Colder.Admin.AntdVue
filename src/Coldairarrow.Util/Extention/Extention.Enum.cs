using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 拓展类
    /// </summary>
    public static partial class Extention
    {
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            try
            {
                if (value == null) return null;
                DescriptionAttribute attribute = value.GetType()
                    .GetField(value.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .SingleOrDefault() as DescriptionAttribute;
                return attribute == null ? value.ToString() : attribute.Description;
            }
            catch (Exception e)
            {
                return value.ToString();
            }
            
        }

        /// <summary>
        /// 获取枚举DisplayName
        /// </summary>
        public static string ToDisplayName(this Enum obj)
        {
            try
            {
                if (obj == null) return null;
                var field = obj.GetType().GetField(obj.ToString());//通过这个类型获取到值
                if (field == null) return null;
                return ((DisplayAttribute)field.GetCustomAttribute(typeof(DisplayAttribute))).Name;//得到特性
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
