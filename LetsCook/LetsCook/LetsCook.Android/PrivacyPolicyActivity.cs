
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;

namespace LetsCook.Droid
{
    [Activity(Label = "PrivacyPolicyActivity")]
    public class PrivacyPolicyActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PrivacyPolicy);

            string privacyPolicyHtml = File.ReadAllText("Resources/PrivacyPolicy.html");

            var browser = (WebView)FindViewById(Resource.Id.privacyPolicy);
            //var browser = new WebView()

            browser.LoadData(privacyPolicyHtml, "text/html", null);
        }
    }
}
