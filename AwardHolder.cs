using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;

namespace travelAppRecyclerViewer
{
    class AwardHolder : RecyclerView.ViewHolder
    {
        public TextView textCategoryName { get; private set; }
        public RecyclerView SubRecyclerView { get; private set; }
        public AwardHolder(View itemView, Action<int> listener)
                : base(itemView)
            {
            // Locate and cache view references:
            SubRecyclerView = itemView.FindViewById<RecyclerView>(Resource.Id.subRecyclerView);

            var mLayoutManager = new LinearLayoutManager(itemView.Context)
            {
                Orientation = (int)Orientation.Horizontal
            };

            SubRecyclerView.SetLayoutManager(mLayoutManager);
            textCategoryName = itemView.FindViewById<TextView>(Resource.Id.textCategoryName);
            
            // Detect user clicks on the item view and report which item
            // was clicked (by position) to the listener:
            itemView.Click += (sender, e) => listener(base.Position);
        }
    }
}