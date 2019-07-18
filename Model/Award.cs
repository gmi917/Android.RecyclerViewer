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

namespace travelAppRecyclerViewer.Model
{
    class Award
    {
        public class Prize
        {
            public string image { get; set; }
            public string prizeName { get; set; }
            public string id { get; set; }
        }

        public class RootObject
        {
            public string categoryName { get; set; }
            public List<Prize> prize { get; set; }
        }
    }
}