using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace NativeAndroid.SampleActivity
{
    [Activity(Label = "Activity_2")]
    public class Activity_2 : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LayoutPage_2);

            //Set Navigation bar title
            #region Toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "My Toolbar";
            #endregion

            try
            {
                var editToolbar = FindViewById<Toolbar>(Resource.Id.edit_toolbar);
                editToolbar.Title = "Editing";
                editToolbar.InflateMenu(Resource.Layout.edit_menus);
                editToolbar.MenuItemClick += (sender, e) =>
                {
                    Toast.MakeText(this, "Bottom toolbar tapped: " + e.Item.TitleFormatted, ToastLength.Short).Show();
                };
                editToolbar.Visibility = ViewStates.Invisible;
            }
            catch (Exception ex)
            {

                Toast.MakeText(this, "Error[]  " + ex.Message, ToastLength.Short).Show();
            }
         



        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId.ToString().Equals(Resource.Id.menu_edit.ToString()))
            {
                var editToolbar = FindViewById<Toolbar>(Resource.Id.edit_toolbar);
                var Item2 = FindViewById<View>(Resource.Id.menu_save);
                Item2.Visibility = ViewStates.Visible;

                var Item = FindViewById<View>(Resource.Id.menu_edit);
                Item.Visibility = ViewStates.Invisible;
                editToolbar.Visibility = ViewStates.Visible;


            }
            else if (item.ItemId.ToString().Equals(Resource.Id.menu_save.ToString()))
            {
                var editToolbar = FindViewById<Toolbar>(Resource.Id.edit_toolbar);
                var Item2 = FindViewById<View>(Resource.Id.menu_save);
                Item2.Visibility = ViewStates.Invisible;

                var Item = FindViewById<View>(Resource.Id.menu_edit);
                Item.Visibility = ViewStates.Visible;
                editToolbar.Visibility = ViewStates.Invisible;

            }
            Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
                ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }
     

    }
}