using Android.Support.V7.Widget;
using System.Collections.Generic;
using travelAppRecyclerViewer.Model;
using Android.Views;
using System;
using System.Net;
using UrlImageViewHelper;

namespace travelAppRecyclerViewer
{
    class SubPrizeAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public List<Award.Prize> mprizes;
        public SubPrizeAdapter(List<Award.Prize> prizes)
        {
            mprizes = prizes;
        }

        public override int ItemCount
        {
            get
            {
                return mprizes.Count;
            }
        }

        //BIND DATA TO VIEWS
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            SubPrizeHolder vh = holder as SubPrizeHolder;
            AppValue app =new AppValue();
            if (vh != null)
            {
                ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
                vh.PrizeImage.SetUrlDrawable(app.url + mprizes[position].image, Resource.Drawable.ic_notfound);
                vh.textPrizeId.Text = mprizes[position].id;                
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the prize:
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ImageCellView, parent, false);

            // Create a ViewHolder to find and hold these view references, and 
            // register OnClick with the view holder:
            SubPrizeHolder vh = new SubPrizeHolder(itemView, OnClick);
            return vh;
        }
        void OnClick(int position)
        {
            if (ItemClick != null)
            {
                ItemClick(this, position);
            }
        }
    }
}