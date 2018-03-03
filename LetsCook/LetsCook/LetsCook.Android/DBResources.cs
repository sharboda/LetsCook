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

namespace LetsCook.Droid
{
    internal static class DBResources
    {
        public static List<VideoData> image_VideoURLMap;

        static DBResources()
        {
            image_VideoURLMap = new List<VideoData>();
            image_VideoURLMap.Add(new VideoData("Chicken Cacciatore", "https://food.fnr.sndimg.com/content/dam/images/food/fullset/2010/12/10/0/EI1D10_chicken-cacciatore_s4x3.jpg.rend.hgtvcom.616.462.suffix/1382539594229.jpeg", "https://www.foodnetwork.com/recipes/giada-de-laurentiis/chicken-cacciatore-recipe-1943042"));
            image_VideoURLMap.Add(new VideoData("Shepherds pie potato bowl","https://food.fnr.sndimg.com/content/dam/images/food/fullset/2017/12/19/0/FNK_SHEPHERDS_PIE_POTATO_BOWLS_H_s4x3.jpg.rend.hgtvcom.966.725.suffix/1513725228587.jpeg", "https://www.foodnetwork.com/recipes/food-network-kitchen/shepherds-pie-potato-bowls-4538897"));
        }
    }

    internal class VideoData
    {
        internal string Name {
            private set;
            get;
        }
        internal string ImageUrl
        {
            private set;
            get;
        }
        internal string VideoUrl
        {
            private set;
            get;
        }

        internal VideoData(string _name, string _imageUrl, string _videoURL)
        {
            Name = _name;
            ImageUrl = _imageUrl;
            VideoUrl = _videoURL;
        }
    }
}