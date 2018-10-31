using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
#pragma warning disable
namespace NativeAndroid.Utility
{
    [Service]
    public class NotificatonService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        // A notification requires an id that is unique to the application.
        const int NOTIFICATION_ID = 9000;
        // Create Notification
        public override  StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {

            Log.Debug("Message[]", "Notification Service started");

            Notification.Builder notificationBuilder = new Notification.Builder(this)
                .SetSmallIcon(Resource.Drawable.abc_ic_voice_search_api_material)
                .SetContentTitle(Resources.GetString(Resource.String.notification_content_title))
                .SetContentText(Resources.GetString(Resource.String.notification_content_text));

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Notify(NOTIFICATION_ID, notificationBuilder.Build());
            return StartCommandResult.Sticky;
        }
       
    }
}