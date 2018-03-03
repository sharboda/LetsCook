using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ChooseVegActivity : Activity
    {
        List<int> selectedItems = new List<int>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Set our view from the "ChooseVeg" layout resource
            SetContentView(Resource.Layout.ChooseVeg);

            ListView listView = FindViewById<ListView>(Resource.Id.listViewVeg);
            var items = new string[] { "Potato", "Tomato", "Pepper", "Lemon", "Lime", "Spinach", "Cabbage" };
            listView.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
            listView.ItemClick += ListView_ItemClick;

            Button addMore = FindViewById<Button>(Resource.Id.btnAddMoreVeg);
            addMore.Click += AddMore_Click;

            Button getRecipe = FindViewById<Button>(Resource.Id.btnGetRecipesFromVeg);
            getRecipe.Click += GetRecipe_Click;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Color c;
            if(selectedItems.Contains(e.Position))
            {
                c = Color.White;
                selectedItems.Remove(e.Position);
            }
            else
            {
                c = Color.Aqua;
                selectedItems.Add(e.Position);
            }
            e.View.SetBackgroundColor(c);
        }

        private void GetRecipe_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ShowRecipe));
        }

        private void AddMore_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }


        public override void OnBackPressed()
        {
            base.OnBackPressed();
            StartActivity(typeof(MainActivity));
        }

    }
}