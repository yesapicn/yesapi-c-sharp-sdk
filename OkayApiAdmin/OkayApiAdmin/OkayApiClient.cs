using PhalApiClientSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OkayApiAdmin
{
    class OkayApiClient
    {
        protected static OkayApiClient client;

        public String host;
        public String app_key;
        public String app_secrect;

        protected OkayApiClient()
        {
            loadConfig();
        }

        public static OkayApiClient instance()
        {
            if (OkayApiClient.client == null)
            {
                OkayApiClient.client = new OkayApiClient();
            }
            return OkayApiClient.client;
        }

        public void loadConfig()
        {
            // 读取配置
            IniFileHelper iniFileHelper = new IniFileHelper();
            StringBuilder sb = new StringBuilder(200);
            iniFileHelper.GetIniString("Product", "host", "", sb, sb.Capacity);
            host = sb.ToString();
            if (host.Length == 0)
            {
                host = "http://api.okayapi.com/";
            }

            iniFileHelper.GetIniString("Product", "app_key", "", sb, sb.Capacity);
            app_key = sb.ToString();
            if (app_key.Length == 0)
            {
                app_key = "16BD4337FB1D355902E0502AFCBFD4DF";
            }

            iniFileHelper.GetIniString("Product", "app_secrect", "", sb, sb.Capacity);
            app_secrect = sb.ToString();
            if (app_secrect.Length == 0)
            {
                app_secrect = "4c1402596e4cd017eeaO670df6f8B6783475b4ac8A32B4900f20abP2159711ad";
            }
        }

        public void updateConfig(String hh, String a_k, String a_s)
        {
            host = hh;
            app_key = a_k;
            app_secrect = a_s;

            IniFileHelper iniFileHelper = new IniFileHelper();
            iniFileHelper.WriteIniString("Product", "host", host);
            iniFileHelper.WriteIniString("Product", "app_key", app_key);
            iniFileHelper.WriteIniString("Product", "app_secrect", app_secrect);
        }

        public PhalApiClientResponse go(String service, Dictionary<String, String> paramsDict)
        {
            // 增加必要的公共参数
            paramsDict.Add("app_key", app_key);
            paramsDict.Add("service", service);

            // 重新生成签名
            paramsDict.Remove("sign");
            String sign = encryptAppKey(paramsDict);
            paramsDict.Add("sign", sign);

            PhalApiClient client = PhalApiClient.create()
               .withHost(host)
               .withTimeout(3000);

            // 追加参数
            foreach (KeyValuePair<String, String> it in paramsDict)
            {
                client.withParams(it.Key, Uri.EscapeUriString(it.Value));
            }

            PhalApiClientResponse response = client.request();

            return response;
        }

        public String encryptAppKey(Dictionary<String, String> dict)
        {
            List<KeyValuePair<String, String>> lst = new List<KeyValuePair<String, String>>(dict);

            //倒叙排列：只需要把变量s2 和 s1 互换就行了 例： return s1.Value.CompareTo(s2.Value);
            //进行排序 目前是顺序

            lst.Sort(delegate (KeyValuePair<String, String> s1, KeyValuePair<String, String> s2)
            {
                return s1.Key.CompareTo(s2.Key);
            });

            String tmpStr = "";
            foreach (KeyValuePair < String, String > it in lst)
            {
                tmpStr += it.Value;
            }


            String strMd5ForUtf8 = UserMd5(tmpStr + app_secrect);

            return strMd5ForUtf8.ToUpper();
        }

        public static string UserMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x2");

            }
            return pwd;
        }
    }
}
