using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LetsCook.Droid
{
    [Activity(Label = "Lets Cook!!")]
    public class ShowRecipe : Activity
    {
        GridView gridView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "Show Recipes" layout resource
            SetContentView(Resource.Layout.ShowRecipes);

            gridView = FindViewById<GridView>(Resource.Id.gridView1);

            List<VideoData> imageItem = new List<VideoData>();

            imageItem = DBResources.image_VideoURLMap;
            
            gridView.Adapter = new GridViewAdapter(this.ApplicationContext);
            gridView.ItemClick += GridView_ItemClick;
        }

        private void GridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var activity2 = new Intent(this, typeof(ShowVideo));
            activity2.PutExtra("videoURL", DBResources.image_VideoURLMap[e.Position].VideoUrl);
            StartActivity(activity2);
        }
        
        List<Bitmap> GetBitmapImages(List<String> bitmapUris)
        {
            List<Bitmap> images = new List<Bitmap>();
            Bitmap image;
            foreach (var uri in bitmapUris)
            {
                image = GetImageBitmapFromUri(uri);
                images.Add(image);
            }
            return images;
        }

        private Bitmap GetImageBitmapFromUri(string uri)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(uri);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            StartActivity(typeof(ChooseVegActivity));
        }
    }

    internal class GridViewAdapter : BaseAdapter<VideoData>
    {
        Context context;

        public override int Count
        {
            get { return DBResources.image_VideoURLMap.Count; }
        }

        public override VideoData this[int position]
        {
            get { return DBResources.image_VideoURLMap[position]; }
        }

        public GridViewAdapter(Context context)
        {
            this.context = context;
        }
        
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = DBResources.image_VideoURLMap[position];
            LinearLayout ll;
            //ImageView imageView;

            if (convertView == null)
            {  // if it's not recycled, initialize some attributes

                ll = new LinearLayout(context);
                ll.Orientation = Orientation.Vertical;
                ImageView imageView;

                TextView txtView = new TextView(context);
                txtView.Text = item.Name;
                ///txtView.SetWidth(100);
                //txtView.SetHeight(50);
                //txtView.SetTextSize(Android.Util.ComplexUnitType.Dip, 24);
                imageView = new ImageView(context);
                imageView.LayoutParameters = new GridView.LayoutParams(500, 500);
                imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                imageView.SetPadding(50, 0, 50, 50);

                ll.AddView(txtView);
                ll.AddView(imageView);
            }
            else
            {
                //imageView = (ImageView)convertView;
                ll = (LinearLayout)convertView;
            }
            var imageBitmap = GetImageBitmapFromUrl(item.ImageUrl);
            (ll.GetChildAt(1) as ImageView).SetImageBitmap(imageBitmap);

            return ll;
        }

        public Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}