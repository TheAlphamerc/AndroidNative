
using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V4.App;

namespace NativeAndroid.Utility
{
    [BroadcastReceiver]
    internal class NotificationAlarmHandler : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            // Pull out the parameters from the alarm.
            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");

            // Create the notification.
            var builder = new NotificationCompat.Builder(Application.Context)
             .SetContentTitle(title)
             .SetContentText(message)
             .SetDefaults(1)
             .SetSmallIcon(Resource.Drawable.abc_textfield_search_material)
             .SetAutoCancel(true);
            builder.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.All));

            // Set this application to open when the notification is clicked. If the application
            // is already open it will reuse the same activity.
            var resultIntent = Application.Context.PackageManager.GetLaunchIntentForPackage(Application.Context.PackageName);
            resultIntent.SetAction(intent.Action);
            resultIntent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);

            var resultPendingIntent = PendingIntent.GetActivity(Application.Context, 0, resultIntent, PendingIntentFlags.UpdateCurrent);
            builder.SetContentIntent(resultPendingIntent);

            // Show the notification.
            var notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.Notify(0, builder.Build());
        }
   
    }
}