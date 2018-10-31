using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System.Collections.Generic;
using Android.Content;
using System;
using NativeAndroid.Utility;
using Android.Util;

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
    }
}