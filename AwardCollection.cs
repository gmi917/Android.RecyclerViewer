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
using Newtonsoft.Json;
using travelAppRecyclerViewer.Model;
using static travelAppRecyclerViewer.Model.Award;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;

namespace travelAppRecyclerViewer
{
    class AwardCollection
    {
        private List<Award.RootObject> mItems;
        public List<Award.Prize> GetItems(int i)
        {
            return mItems[i].prize;
        }

        public async Task<List<RootObject>> getData()
        {
            List<Award.RootObject> datas = new List<Award.RootObject>(); ;
            List<Prize> subdatas = new List<Prize>();            
            RootObject root;
            Prize subdata = new Prize();
            AppValue app = new AppValue();
            using (var client = new HttpClient())
            {
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;
                var uri =  app.url+"/AR_admin/UserfindallPrizeList";
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        
                        var posts = JsonConvert.DeserializeObject<List<Award.RootObject>>(content);
                        if (posts.Count > 0)
                        {                           
                            foreach (var postData in posts)
                            {
                                root = new RootObject();
                                root.categoryName = postData.categoryName;
                                root.prize = postData.prize;                                
                                datas.Add(root);                               
                            }
                        }
                    }

                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine(e.StackTrace);
                }

            }
            return datas;
        }
    }
}