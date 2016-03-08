using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinderhuisStageOpdracht.Extensions
{
    /*                                                                      *
     *      This extension was derive from Brad Christie's answer           *
     *      on StackOverflow.                                               *
     *                                                                      *
     *      The original code can be found at:                              *
     *      http://stackoverflow.com/a/18338264/998328                      *
     *                                                                      */

    public static class NotificationExtensions
    {
        private static IDictionary<String, String> NotificationKey = new Dictionary<String, String>
        {
            { "Fout",      "App.Notifications.Error" }, 
            { "Waarschuwing",    "App.Notifications.Warning" },
            { "Gelukt",    "App.Notifications.Success" },
            { "Info",       "App.Notifications.Info" }
        };


        public static void AddNotification(this ControllerBase controller, String message, String notificationType)
        {
            string NotificationKey = getNotificationKeyByType(notificationType);
            ICollection<String> messages = controller.TempData[NotificationKey] as ICollection<String>;

            if (messages == null)
            {
                controller.TempData[NotificationKey] = (messages = new HashSet<String>());
            }

            messages.Add(message);
        }

        public static IEnumerable<String> GetNotifications(this HtmlHelper htmlHelper, String notificationType)
        {
            string NotificationKey = getNotificationKeyByType(notificationType);
            return htmlHelper.ViewContext.Controller.TempData[NotificationKey] as ICollection<String> ?? null;
        }

        private static string getNotificationKeyByType(string notificationType)
        {
            try
            {
                return NotificationKey[notificationType];
            }
            catch (IndexOutOfRangeException e)
            {
                ArgumentException exception = new ArgumentException("Key is invalid", "notificationType", e);
                throw exception;
            }
        }
    }

    public static class NotificationType
    {
        public const string ERROR = "Fout";
        public const string WARNING = "Waarschuwing";
        public const string SUCCESS = "Gelukt";
        public const string INFO = "Info";

    }
}