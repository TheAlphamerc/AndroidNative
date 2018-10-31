using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System.Collections.Generic;
using Android.Content;
using System;
using NativeAndroid.Utility;
using Android.Util;
using Android.Graphics;
using NativeAndroid.SampleActivity;

namespace NativeAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        static readonly List<string> phoneNumbers = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

          

            TextView translatedPhoneWord = FindViewById<TextView>(Resource.Id.Label); // Label
            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.textfield); // Entry feild
            Button translateButton = FindViewById<Button>(Resource.Id.button);        // Translate button
            Button translationHistoryButton = FindViewById<Button>(Resource.Id.call); // History button
            Button NotificationButton = FindViewById<Button>(Resource.Id.StartNotification); // Notification Button
            NotificationButton.Click += OpenNotification;

            Button UpdateNotificationButton = FindViewById<Button>(Resource.Id.UpdateNotification); // Notification Button
            UpdateNotificationButton.Click += UpdateNotification;

            Button StartActivityButton = FindViewById<Button>(Resource.Id.StartActivity_2); // Notification Button
            StartActivityButton.Click += StartActivity_2;


            string translatedNumber = string.Empty;
            translateButton.Click += (sender, e) =>
            {
                 translatedNumber = MainClass.ToNumber(phoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(translatedNumber))
                {
                    translatedPhoneWord.Text = string.Empty;
                }
                else
                {
                    translatedPhoneWord.Text = translatedNumber;
                    phoneNumbers.Add(translatedNumber);
                    translationHistoryButton.Enabled = true;
                }
            };
            translationHistoryButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(TranslationHistoryActivity));
                intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
                StartActivity(intent);
            };



         
          
           
       
     }

        private void StartActivity_2(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Activity_2));
            StartActivity(intent);
        }

        #region Notification
        private void OpenNotification(object sender, EventArgs e)
        {
            try
            {
                Intent NotificationIntent = new Intent(this, typeof(NotificatonService));
                StartService(NotificationIntent);
            }
            catch (Exception ex)
            {

                Log.Debug("Message[]", "Error in Notification Service started " + ex.Message);
            }
        }
        // Update Notification
        void UpdateNotification(object sender, EventArgs e)
        {
         
  
            Intent NotificationIntent = new Intent(this, typeof(NotificatonService));
            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, NotificationIntent, PendingIntentFlags.UpdateCurrent);

            var resultIntent = Application.Context.PackageManager.GetLaunchIntentForPackage(Application.Context.PackageName);

            resultIntent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);

            var notification = GetNotification(pendingIntent);

            NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.Notify(9000, notification);
        }

        Notification GetNotification(PendingIntent intent)
        {
            return new Notification.Builder(this)
                    .SetContentTitle(Resources.GetString(Resource.String.notification_content_title_2))
                    .SetContentText(Resources.GetString(Resource.String.notification_content_text_2))
                    .SetSmallIcon(Resource.Drawable.abc_seekbar_tick_mark_material)
                    .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.abc_switch_thumb_material))
                    .SetContentIntent(intent).Build();
        }
        #endregion

        #region Override Methods
        protected override void OnResume()
        {
            Log.Debug("Activity[]", "On Resume ");
            base.OnResume();
        }
        protected override void OnStart()
        {
            Log.Debug("Activity[]", "On start ");
            base.OnStart();
        }
        protected override void OnPause()
        {
            Log.Debug("Activity[]", "On Pause ");
            base.OnPause();
        }
        protected override void OnStop()
        {
            Log.Debug("Activity[]", "On Stop ");
            base.OnStop();
        }
        protected override void OnDestroy()
        {
            Log.Debug("Activity[]", "On Destroy ");
            base.OnDestroy();
        }
        protected override void OnRestart()
        {
            Log.Debug("Activity[]", "On Restart ");
            base.OnRestart();
        } 
        #endregion

    }
}