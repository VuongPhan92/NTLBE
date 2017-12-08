using System;
using System.IO;
using System.Net;
namespace Domain.ViewModels
{
    public class SpeedSMSAPI
    {
        public const int TYPE_QC = 1;
        public const int TYPE_CSKH = 2;
        public const int TYPE_BRANDNAME = 3;
        public const int TYPE_BRANDNAME_NOTIFY = 4; // Gửi sms sử dụng brandname Notify
        public const int TYPE_GATEWAY = 5; // Gửi sms sử dụng app android từ số di động cá nhân, download app tại đây: https://play.google.com/store/apps/details?id=com.speedsms.gateway

        const String rootURL = "http://api.speedsms.vn/index.php";
        private String accessToken = "Your access token";

        public SpeedSMSAPI()
        {

        }

        public SpeedSMSAPI(String token)
        {
            this.accessToken = token;
        }

        public String getUserInfo()
        {
            String url = rootURL + "/user/info";
            NetworkCredential myCreds = new NetworkCredential(accessToken, ":x");
            WebClient client = new WebClient();
            client.Credentials = myCreds;
            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);
            return reader.ReadToEnd();
        }

        public String sendSMS(String[] phones, String content, int type, String sender)
        {
            String url = rootURL + "/sms/send";
            if (phones.Length <= 0)
                return "";
            if (content.Equals(""))
                return "";
            if (type < TYPE_QC || type > TYPE_BRANDNAME)
                return "";
            if (type == TYPE_BRANDNAME && sender.Equals(""))
                return "";
            if (!sender.Equals("") && sender.Length > 11)
                return "";

            NetworkCredential myCreds = new NetworkCredential(accessToken, ":x");
            WebClient client = new WebClient();
            client.Credentials = myCreds;
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            string builder = "{\"to\":[";

            for (int i = 0; i < phones.Length; i++)
            {
                builder += "\"" + phones[i] + "\"";
                if (i < phones.Length - 1)
                {
                    builder += ",";
                }
            }
            builder += "], \"content\": \"" + content + "\", \"type\":" + type + ", \"sender\": \"" + sender + "\"}";

            String json = builder.ToString();
            return client.UploadString(url, json);
        }
    }
}
