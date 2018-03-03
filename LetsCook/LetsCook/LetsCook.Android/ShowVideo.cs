using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace LetsCook.Droid
{
    [Activity(Label = "Lets Cook!!")]
    public class ShowVideo : Activity
    {
        WebView webView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "ChooseVeg" layout resource
            SetContentView(Resource.Layout.ShowVideos);

            webView = FindViewById<WebView>(Resource.Id.webView1);
            WebSettings webSettings = webView.Settings;
            webSettings.JavaScriptEnabled = true;
            webView.SetWebChromeClient(new WebChromeClient());

            string url = Intent.GetStringExtra("videoURL") ?? "https://www.youtube.com/watch?v=qeo3buGdQhE";
            webView.LoadUrl(url);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            StartActivity(typeof(ShowRecipe));
        }

        public override bool OnKeyDown(Android.Views.Keycode keyCode, Android.Views.KeyEvent e)
        {
            if (keyCode == Keycode.Back && webView.CanGoBack())
            {
                webView.GoBack();
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }
    }
}