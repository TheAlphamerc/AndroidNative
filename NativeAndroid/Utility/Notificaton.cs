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
        private const string ActionSuffix = "NOTIFICATION";
        public const string TitleExtrasKey = "title";

        /// <summary>
        ///     Key used to store the message part of the notification
        ///     in the intent.
        /// </summary>
        public const string MessageExtrasKey = "message";
        // Create Notification
        public override  StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {

            Log.Debug("Message[]", "Notification Service started");

            Notification.Builder notificationBuilder = new Notification.Builder(this)
                .SetSmallIcon(Resource.Drawable.abc_ic_voice_search_api_material)
                .SetAutoCancel(true)
                .SetContentTitle(Resources.GetString(Resource.String.notification_content_title))
                .SetContentText(Resources.GetString(Resource.String.notification_content_text));

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Notify(NOTIFICATION_ID, notificationBuilder.Build());

            var id = Create("Message","Alarm triggers", DateTime.Now.AddSeconds(10), null);
            return StartCommandResult.Sticky;
        }
        public string Create(string title, string message, DateTime scheduleDate, Dictionary<string, string> extraInfo)
        {
            // Create the unique identifier for this notifications.
            var notificationId = Guid.NewGuid().ToString();


            // Create the alarm intent to be called when the alarm triggers. Make sure
            // to add the id so we can find it later if the user wants to update or
            // cancel.

            var alarmIntent = new Intent(Application.Context, typeof(NotificationAlarmHandler));
            alarmIntent.SetAction(BuildActionName(notificationId));
            alarmIntent.PutExtra(TitleExtrasKey, title);
            alarmIntent.PutExtra(MessageExtrasKey, message);


            // Add the alarm intent to the pending intent.
            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);


            // Figure out the alaram in milliseconds.
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(scheduleDate);
            var epochDif = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
            var notifyTimeInInMilliseconds = utcTime.AddSeconds(-epochDif).Ticks / 10000;


            // Set the notification.
            var alarmManager = Application.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            alarmManager?.Set(AlarmType.RtcWakeup, notifyTimeInInMilliseconds, pendingIntent);

            // All done.
            return notificationId;
        }
        internal static string BuildActionName(string notificationId)
        {
            return Application.Context.PackageName + "." + ActionSuffix + "-" + notificationId;
        }

    }
   
  }
