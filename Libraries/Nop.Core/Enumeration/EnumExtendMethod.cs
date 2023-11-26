using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Core.Enumeration
{
    public static class EnumExtendMethod
    {
        public static string ToDescription<T>(this Enum en)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])en.GetType().GetField(en.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Value : string.Empty;
        }

        public static string ToValue<T>(this Enum en)
        {
            ValueAttribute[] attributes = (ValueAttribute[])en.GetType().GetField(en.ToString()).GetCustomAttributes(typeof(ValueAttribute), false);
            return attributes.Length > 0 ? attributes[0].Value : string.Empty;
        }

        public static T GetEnumFromValue<T>(string value)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(ValueAttribute)) is ValueAttribute attribute)
                {
                    if (attribute.Value == value)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "Value");
        }

        public static bool IsEnumValueExist<T>(string value)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(ValueAttribute)) is ValueAttribute attribute)
                {
                    if (attribute.Value == value)
                        return true;
                }
            }
            return false;
        }

        public static T GetEnumFromDescription<T>(string value)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Value == value)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "Description");
        }

        public static IList<SelectListItem> ToSelectListItems<T>(bool includeSelectAll = false)
        {
            IList<SelectListItem> selectListItems = new List<SelectListItem>();
            IEnumerable<Enum> enums = Enum.GetValues(typeof(T)).Cast<Enum>();
            if (enums.Count() > 0)
            {
                if (includeSelectAll)
                {
                    SelectListItem item = new SelectListItem
                    {
                        Text = "ALL",
                        Value = "ALL"
                    };
                    selectListItems.Add(item);
                }
                foreach (var val in enums)
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = val.ToDescription<T>();
                    item.Value = val.ToValue<T>();
                    selectListItems.Add(item);
                }
            }
            return selectListItems;
        }

        public static IEnumerable<Enum> ToEnumList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<Enum>();
        }
    }
}
