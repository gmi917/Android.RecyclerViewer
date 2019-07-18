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
using Android.Support.V7.Widget;
using travelAppRecyclerViewer.Model;

namespace travelAppRecyclerViewer
{
    class AwardAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public List<Award.RootObject> mawards;

        public AwardAdapter(List<Award.RootObject> awards)
        {
            mawards = awards;
        }
        public override int ItemCount
        {
            get
            {
                return mawards.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            AwardHolder vh = holder as AwardHolder;

            var mAdapter = new SubPrizeAdapter(mawards[position].prize);
            vh.SubRecyclerView.SetAdapter(mAdapter);
            vh.textCategoryName.Text = mawards[position].categoryName;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            View itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.PrizeCardView, parent, false);

            AwardHolder vh = new AwardHolder(itemView, OnClick);
            return vh;
        }
        private void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }
}