using Android.App;
using Android.Widget;
using Android.OS;
using Android.Telephony;
using Android.Net;
using GatewaySMS.Model;
using RestSharp;
using Newtonsoft.Json;
using Timer = System.Timers.Timer;

namespace GatewaySMS
{
    [Activity(Label = "GatewaySMS", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public static Timer timer;
        public static int interval;

        public static string name;
        public static string imei;
        public static string status;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var sBtn = FindViewById<ToggleButton>(Resource.Id.sBtn);
            var phoneNumber = FindViewById<EditText>(Resource.Id.phoneNumber);

            var telephonyManager = (TelephonyManager)GetSystemService(TelephonyService);
            var connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;

            phoneNumber.Text = telephonyManager.Line1Number;
            name = Build.Manufacturer + " " + Build.Model;
            imei = telephonyManager.DeviceId;

            sBtn.CheckedChange += delegate
            {
                if (sBtn.Checked)
                {
                    sBtn.SetBackgroundResource(Resource.Drawable.onbtn);
                    var deviceInfo = new SendDeviceInfo();

                    var client = new RestClient();
                    client.BaseUrl = new System.Uri("http://seweryn-malecki.pl/api/DevicesAPI");
                    var request = new RestRequest("", Method.POST);
                    request.AddBody(GetDeviceInfo(deviceInfo, name, imei, phoneNumber.Text, "on"));
                    var responseStatus = client.Execute(request);
                    var content = responseStatus.Content;

                    GetResponse getResponse = JsonConvert.DeserializeObject<GetResponse>(content);
                    interval = getResponse.Interval;

                    if(interval > 0)
                    {
                        Toast.MakeText(ApplicationContext, "Usluga wlaczona", ToastLength.Long).Show();
                    }
                }
                else
                {
                    sBtn.SetBackgroundResource(Resource.Drawable.offbtn);
                    Toast.MakeText(ApplicationContext, "Usluga wylaczona", ToastLength.Long).Show();
                }
            };
        }

        private SendDeviceInfo GetDeviceInfo(SendDeviceInfo deviceInfo, string name, string imei, string phone, string status)
        {
            deviceInfo.DeviceName = name;
            deviceInfo.DeviceImei = imei;
            deviceInfo.DevicePhoneNumber = phone;
            deviceInfo.DeviceStatus = status;

            return deviceInfo;
        }
    }
}

