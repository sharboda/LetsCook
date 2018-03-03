using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Provider;
using Android.Graphics;
using System.Collections.Generic;
using Android.Content.PM;
using System;

namespace LetsCook.Droid
{
    public static class App {
    public static Java.IO.File _file;
    public static Java.IO.File _dir;
    public static Bitmap bitmap;
}
    
    [Activity(Label = "Lets Cook!!", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button btnScan = FindViewById<Button>(Resource.Id.cameraBtn);
            btnScan.Click += TakeAPicture;

            Button btnGetRecipe = FindViewById<Button>(Resource.Id.btnGetRecipes);
            btnGetRecipe.Click += delegate {
                StartActivity(typeof(ShowRecipe));
            };
        }
        
        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 1);
        }
        
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            
            // Dispose of the Java side bitmap.
            GC.Collect();

            StartActivity(typeof(ChooseVegActivity));
        }

        private void BtnScan_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            //App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            //intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);
        }
        public override void OnBackPressed()
        {
            base.OnBackPressed();
            Finish();
        }
    }
}