using Android.App;
using Android.Content;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using travelApp.Model;

namespace travelAppRecyclerViewer
{
    class SubPrizeHolder : RecyclerView.ViewHolder
    {
        public ImageView PrizeImage { get; private set; }
        public TextView textPrizeId { get; private set; }
        
        public SubPrizeHolder(View itemView, Action<int> listener)
                : base(itemView)
            {
            // Locate and cache view references:
            PrizeImage = itemView.FindViewById<ImageView>(Resource.Id.imgeCellViewPrize);
            textPrizeId = itemView.FindViewById<TextView>(Resource.Id.textCellViewPrizeId);
            PrizeImage.Click += PrizeImage_Click;
            // Detect user clicks on the item view and report which item
            // was clicked (by position) to the listener:
            itemView.Click += (sender, e) => listener(base.Position);
        }

        private async void PrizeImage_Click(object osender, EventArgs e)
        {            
            AppValue app = new AppValue();
            if (NetworkCheck.IsInternet())
            {
                using (var client = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback +=
                        (sender, cert, chain, sslPolicyErrors) => true;
                    var uri = app.url + "/AR_admin/UsergetPrizeDetailbyId/" + textPrizeId.Text;                    
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        //handling the answer  
                        var posts = JsonConvert.DeserializeObject<List<PrizeDetail>>(content);
                        if (posts.Count > 0)
                        {
                            Intent prizeDetailIntent = new Intent(Application.Context, typeof(PrizeDetailActivity));
                            prizeDetailIntent.PutExtra("PrizeID", textPrizeId.Text);
                            Application.Context.StartActivity(prizeDetailIntent);
                            if (!string.IsNullOrEmpty(MainActivity.mActivity.getAccount()))
                            {                                
                                MainActivity.mActivity.Finish();
                            }
                        }
                        else
                        {                            
                            Toast.MakeText(Application.Context, "查無該兌換商品詳細資料", ToastLength.Short).Show();                            
                        }
                    }
                }
            }
            else
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(Application.Context);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("訊息");
                alert.SetMessage("請先開啟網路");
                alert.SetButton("OK", (c, ev) =>
                {
                    // Ok button click task  
                });
                alert.Show();                             
            }
        }
    }
}