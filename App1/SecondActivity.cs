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

namespace heavykick
{
    [Activity]
    [IntentFilter(new[] { Intent.ActionView, Intent.ActionSend },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataMimeType = "video/*",
        DataScheme = "http")]
    [IntentFilter(new[] { Intent.ActionView, Intent.ActionSend },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataMimeType = "video/*",
        DataScheme = "https")]
//    [IntentFilter(new[] { Intent.ActionView, Intent.ActionSend },
//        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
//        DataScheme="http",  DataHost = "alain-pc", DataPort = "8096")]  //DataPathPrefix = @"/web/itemdetails.html?id=",
    public class SecondActivity : Activity
    {
        private string FilePath = "/mnt/sdcard/MilkVR/";
        private string Url = "";

        public RadioGroup MilkVROptions = null;
        public Button button1 = null;
        public AutoCompleteTextView LinkPreview = null;

        private string[,] LaunchExtensions =
             {
                { "no params",""},
                { "2D video", "_2dp"},
                { "3D top bottom video", "_3dpv"},
                { "3D side by side video", "_3dph" },
                { "Monoscopic 180", "180x180"},
                { "Monoscopic 180 16:9", "180x101" },
                { "Top bottom stereoscopic 360", "3dv" },
                { "Left right stereoscopic 360", "3dh" },
                { "Top bottom stereoscopic 3D 180", "180x180_3dv" },
                { "Left right stereoscopic 3D 180", "180x180_3dh" },
                { "Top bottom stereoscopic 3D 180x160", "180x160_3dv" }, 
                { "Two monoscopic 180 hemispheres", "180hemispheres" },
                { "Top bottom stereoscopic 3D sliced off cylindrical 2.25:1 ratio", "cylinder_slice_2x25_3dv" }, 
                { "Top bottom stereoscopic 3D sliced off cylindrical 16:9", "cylinder_slice_16x9_3dv" }, 
                { "Top Bottom stereoscopic 3D 360 sphere with no bottom", "sib3d" }, 
                { "180 Planetarium a.k.a full dome","_planetarium" },
                { "V360 camera", "_v360"}
            };

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Second);

            MilkVROptions = FindViewById<RadioGroup>(Resource.Id.MilkVROptions);
            LinkPreview = FindViewById<AutoCompleteTextView>(Resource.Id.LinkPreview);
            LinkPreview.Text = "MilkVRLauncher";

            button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += delegate
            {
                CreateMVRL(FilePath + LinkPreview.Text + ".mvrl", Url, LaunchExtensions[getSelectedElement(MilkVROptions),1]);
                //StartMilkVR();
                //LinkPreview.Text = convertMilkVRSideloadURL(Url, LaunchExtensions[getSelectedElement(MilkVROptions), 1]);
                ExecUri(convertMilkVRSideloadURL(Url, LaunchExtensions[getSelectedElement(MilkVROptions), 1]));
            };


            Url = CheckForEmby(Intent.DataString);
            button1.Text =  Resources.GetText(Resource.String.ButtonStartCaption);
            //LinkPreview.Text = convertMilkVRSideloadURL(Url, "");

            InitRadioButtons(LaunchExtensions);
        }

        protected void InitRadioButtons(string[,] aLaunchExtensions)
        {

            MilkVROptions.RemoveAllViews();

            for(int i = 0; i < aLaunchExtensions.GetLength(0); i++)
            {
                string _curS = aLaunchExtensions[i,0];

                RadioButton _button = new RadioButton(this);
                _button.Text =_curS;
                _button.Tag = i;
                MilkVROptions.AddView(_button);
            };
        }
        protected void StartMilkVR()
        {
            Intent launchMilk = PackageManager.GetLaunchIntentForPackage("com.samsung.vrvideo");
            StartActivity(launchMilk);
        }
        protected void CreateMVRL(string aFileName, string aUrl, string aFormat)
        {
            if (! System.IO.Directory.Exists("/mnt/sdcard/"))
            {
                System.IO.Directory.CreateDirectory("/mnt/sdcard/");
            }

            if (System.IO.File.Exists(aFileName))
            {
                System.IO.File.Delete(aFileName);
            }

            System.IO.StreamWriter writer = new System.IO.StreamWriter(aFileName, false);
            writer.WriteLine(aUrl);
            writer.WriteLine(aFormat);

            writer.Close();
        }
        protected int getSelectedElement(RadioGroup aRadioGroup)
        {
            int radioButtonID = aRadioGroup.CheckedRadioButtonId;
            View radioButton = aRadioGroup.FindViewById(radioButtonID);
            if (radioButton != null)
            {
                return (int)radioButton.Tag;
            }
            else { return 0; };
        }
        protected string convertMilkVRSideloadURL(string aURL, string aFormat)
        {
            // [milkvr://sideload/?url=https%3A%2F%2Fmilkvr.com%2Fcdn%2Fassets%2FCarBee_5.1.mp4&audio_type=_5.1]
            string _videoType = "";
            if (aFormat != "")
            { _videoType = @"&video_type=" + aFormat; };
            return @"milkvr://sideload/?url=" + aURL.Replace(@"/", "%2F").Replace(":", "%3A") + _videoType;
        }
        protected void ExecUri(string aURI)
        {
            Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(aURI));
            //intent.SetData(Android.Net.Uri.Parse(aURI));
            intent.PutExtra("dummy", "dummy");
            //intent.SetDataAndType(Android.Net.Uri.Parse("http://test.mp4"), "video/mp4");
            StartActivity(intent);
        }
        protected void copyToClipboard(string aText)
        {
            ClipboardManager clipboard = (ClipboardManager)GetSystemService("CLIPBOARD_SERVICE");
            clipboard.Text = aText;
        }
        protected string CheckForEmby(string aUrl)
        {
            if (aUrl.IndexOf(@"/web/itemdetails.html?id=") >= 0)
            {
                string _ID = aUrl.Substring(aUrl.IndexOf("=") + 1);
                string serverAdress = aUrl.Substring(0, aUrl.IndexOf("/web"));

                return serverAdress + "/videos/" + _ID + "/stream.mp4?static=true";
            }
            else
            {
                return aUrl;
            };

        }
    }
}