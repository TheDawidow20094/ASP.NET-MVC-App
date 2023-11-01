using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Reflection;
using static CarWorkshopMVC.Extensions.ControllerExtensions;

namespace CarWorkshopMVC.Extensions
{
    public static class BaseExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>(false);
            return attribute != null ? attribute.Description : value.ToString();
        }
    }
}
