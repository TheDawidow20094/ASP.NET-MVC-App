using CarWorkshopMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CarWorkshopMVC.Extensions
{
    public static class ControllerExtensions
    {
        public enum NotyficationTypes
        {
            [Description ("success")]
            Success = 0,
            [Description("info")]
            Info = 1,
            [Description("warning")]
            Warning = 2,
            [Description("error")]
            Error = 3,
        }

        public static void SetNotification(this Controller controler, NotyficationTypes type, string message)
        {
            string stringType = type.GetEnumDescription();
            var notyfication = new Notification(stringType, message);
            controler.TempData["Notyfication"] = JsonConvert.SerializeObject(notyfication);
        }
    }
}
