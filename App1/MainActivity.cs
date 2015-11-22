using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Net;

namespace heavykick
{
    [Activity(Label = "MilkVRLauncher", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected AutoCompleteTextView Edit = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            Edit = FindViewById<AutoCompleteTextView>(Resource.Id.Edit);

            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate {
                string EditText = Edit.Text;

                Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse("http://test.mp4"));
                intent.SetDataAndType(Android.Net.Uri.Parse(EditText), "video/mp4");
                StartActivity(intent);
            };
        }
    }
}

