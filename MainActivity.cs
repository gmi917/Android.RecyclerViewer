using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using travelAppRecyclerViewer.Model;
using System.Collections.Generic;
using Android.Views;
using System;
using Android.Content;
using Xamarin.Essentials;
using Android.Support.V4.Widget;
using Android.Graphics;
using System.ComponentModel;
using Java.Lang;

namespace travelAppRecyclerViewer
{
    [Activity(Theme = "@style/AppTheme", Label = "travelAppRecyclerViewer", MainLauncher = true)]
    public class MainActivity : Activity
    {
        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        public static MainActivity ContextActivity;
        AwardAdapter mAdapter;
        TextView emptyTextView;
        ProgressBar pbLoading;
        Button btnLogout;
        Button btnRegister;
        Button btnLogin;
        ImageView imgLogin;
        TextView textName;        
        DateTime? firstTime;
        SwipeRefreshLayout refreshLayout;

        public static MainActivity mActivity;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            mActivity = this;
            emptyTextView = FindViewById<TextView>(Resource.Id.emptyTextView);
            imgLogin = FindViewById<ImageView>(Resource.Id.imgLogin);
            textName = FindViewById<TextView>(Resource.Id.textName);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnLogout = FindViewById<Button>(Resource.Id.btnLogout);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            pbLoading = FindViewById<ProgressBar>(Resource.Id.pbLoading);

            if (((AppValue)this.Application).account != "")
            {
                imgLogin.Visibility = ViewStates.Visible;
                textName.Visibility = ViewStates.Visible;
                textName.Text = ((AppValue)this.Application).account;
                btnLogin.Visibility = ViewStates.Invisible;
                btnLogout.Visibility = ViewStates.Visible;
                btnRegister.Visibility = ViewStates.Invisible;
            }

            if (NetworkCheck.IsInternet())
            {
                //check app version
                OpenAppInStore();

                // Get RecyclerView layout:
                refreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout1);
                refreshLayout.SetColorSchemeColors(Color.Red);
                mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
                refreshLayout.Refresh += RefreshLayout_Refresh;
                SetUpRecyclerView(mRecyclerView);
            }
            else
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("訊息");
                alert.SetMessage("請先開啟網路");
                alert.SetButton("OK", (c, ev) =>
                {
                    // Ok button click task  
                });
                alert.Show();
            }
            FindViewById<Button>(Resource.Id.btnLogin).Click += delegate
            {
                this.StartActivity(typeof(LoginActivity));
                this.Finish();
            };
            FindViewById<Button>(Resource.Id.btnLogout).Click += delegate
            {
                ((AppValue)this.Application).account = "";
                this.StartActivity(typeof(MainActivity));
                this.Finish();
            };
            FindViewById<Button>(Resource.Id.btnRegister).Click += delegate
            {
                this.StartActivity(typeof(RegisterActivity));
            };

        }

        private void RefreshLayout_Refresh(object sender, EventArgs e)
        {
            //Data Refresh Place
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += Work_DoWork;
            work.RunWorkerCompleted += Work_RunWorkerCompleted;
            work.RunWorkerAsync();
        }

        private void Work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                SetUpRecyclerView(mRecyclerView);

            });
            refreshLayout.Refreshing = false;
        }

        private void Work_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
        }

        private async void SetUpRecyclerView(RecyclerView recyclerView)
        {
            pbLoading.Visibility = ViewStates.Visible;
            AwardCollection data = new AwardCollection();
            List<Award.RootObject> awardDataList = new List<Award.RootObject>();
            awardDataList = await data.getData();
            pbLoading.Visibility = ViewStates.Gone;
            if (awardDataList != null && awardDataList.Count > 0)
            {
                mLayoutManager = new LinearLayoutManager(this);
                ContextActivity = this;
                mRecyclerView.SetLayoutManager(mLayoutManager);
                mAdapter = new AwardAdapter(awardDataList);
                mRecyclerView.SetAdapter(mAdapter);
            }
            else
            {
                emptyTextView.Visibility = ViewStates.Visible;
            }           
        }

        public async void OpenAppInStore()
        {
            bool isUsingLatestVersion = await VersionCheck.IsUsingLatestVersion();
            if (!isUsingLatestVersion)
            {

                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("訊息");
                alert.SetMessage("有最新版程式,請更新程式版本");
                alert.SetButton("OK", (c, ev) =>
                {
                    try
                    {
                        var intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse($"market://details?id=" + AppInfo.PackageName));
                        intent.SetPackage("com.android.vending");
                        intent.SetFlags(ActivityFlags.NewTask);
                        Application.Context.StartActivity(intent);
                    }
                    catch (ActivityNotFoundException)
                    {
                        var intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse($"https://play.google.com/store/apps/details?id=" + AppInfo.PackageName + "&hl=zh_TW"));
                        Application.Context.StartActivity(intent);
                    }

                });
                alert.SetButton2("cancel", (c, ev) =>
                {

                });
                alert.Show();
            }
        }
        
        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back && e.Action == KeyEventActions.Down)
            {
                if (!firstTime.HasValue || DateTime.Now.Second - firstTime.Value.Second > 2)
                {
                    Toast.MakeText(this, "再按一次返回鍵退出程式", ToastLength.Short).Show();
                    firstTime = DateTime.Now;
                }
                else
                {
                    ((AppValue)this.Application).account = "";
                    this.Finish();
                }
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }

        public string getAccount()
        {
            return ((AppValue)this.Application).account;
        }
    }
}

