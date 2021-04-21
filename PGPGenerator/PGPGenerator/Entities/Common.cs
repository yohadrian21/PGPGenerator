using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PGPGenerator.Entities
{
    public class CommonJson
    {
        public JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
        };

        public string GetJsonRespon(string URL)
        {
            String ret = string.Empty;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = 180000;
            httpWebRequest.ReadWriteTimeout = 1800000;
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();//to send or to get reponse
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    ret = streamReader.ReadToEnd();//ret is return value that we get from service
                    ret = ret.Substring(1, ret.Length - 2).Replace("\\", "");
                }
            }
            catch (WebException)
            {
                ret = "";
                throw new Exception(string.Format("{0}", "Get data failed!!!"));
            }

            return ret;
        }

        public T GetJsonRespon<T>(string URL)
        {
            String ret = string.Empty;
            //T data = new <T>();
            T data = default(T);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = 180000;
            httpWebRequest.ReadWriteTimeout = 1800000;
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();//to send or to get reponse
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    ret = streamReader.ReadToEnd();//ret is return value that we get from service
                    data = JsonConvert.DeserializeObject<T>(ret);
                }
            }
            catch (WebException)
            {
                ret = "";
                //throw new Exception(string.Format("{0}", M10UserFrameWork.GetConfigMsgGetDataFailed()));
            }

            return data;
        }

        //public T GetJsonRespon<T>(string URL, ModelSMS.Model.Token Data)
        //{
        //    String ret = string.Empty;
        //    //T data = new <T>();
        //    T data = default(T);
        //    var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
        //    httpWebRequest.ContentType = "application/json";
        //    httpWebRequest.Method = "GET";
        //    httpWebRequest.Headers.Add("Authorization", Data.TokenType + " " + Data.TokenValue);
        //    httpWebRequest.Timeout = 180000;
        //    httpWebRequest.ReadWriteTimeout = 1800000;
        //    try
        //    {
        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();//to send or to get reponse
        //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //        {
        //            ret = streamReader.ReadToEnd();//ret is return value that we get from service
        //            data = JsonConvert.DeserializeObject<T>(ret);
        //        }
        //    }
        //    catch (WebException)
        //    {
        //        ret = "";
        //        //throw new Exception(string.Format("{0}", M10UserFrameWork.GetConfigMsgGetDataFailed()));
        //    }

        //    return data;
        //}

        //public string GetJsonResponList(string URL, ModelSMS.Model.Token Data)
        //{
        //    String ret = string.Empty;
        //    //List<T> data = new List<T>();
        //    var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
        //    httpWebRequest.ContentType = "application/json";
        //    httpWebRequest.Method = "GET";
        //    httpWebRequest.Headers.Add("Authorization", Data.TokenType + " " + Data.TokenValue);
        //    httpWebRequest.Timeout = 180000;
        //    httpWebRequest.ReadWriteTimeout = 1800000;
        //    try
        //    {
        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();//to send or to get reponse
        //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //        {
        //            ret = streamReader.ReadToEnd();//ret is return value that we get from service

        //        }
        //    }
        //    catch (WebException)
        //    {
        //        ret = "";
        //        //throw new Exception(string.Format("{0}", M10UserFrameWork.GetConfigMsgGetDataFailed()));
        //    }

        //    return ret;
        //}

        public List<T> GetJsonResponList<T>(string URL)
        {
            String ret = string.Empty;
            List<T> data = new List<T>();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = 180000;
            httpWebRequest.ReadWriteTimeout = 1800000;
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();//to send or to get reponse
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    ret = streamReader.ReadToEnd();//ret is return value that we get from service

                    if (ret.Substring(0, 1) != "[")
                    {
                        ret = "[" + ret + "]";
                    }

                    data = JsonConvert.DeserializeObject<List<T>>(ret);
                }
            }
            catch (WebException)
            {
                ret = "";
                //throw new Exception(string.Format("{0}", M10UserFrameWork.GetConfigMsgGetDataFailed()));
            }

            return data;
        }

        //public List<T> GetJsonResponList<T>(string URL, ModelSMS.Model.Token Data)
        //{
        //    String ret = string.Empty;
        //    List<T> data = new List<T>();
        //    var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
        //    httpWebRequest.ContentType = "application/json";
        //    httpWebRequest.Method = "GET";
        //    httpWebRequest.Headers.Add("Authorization", Data.TokenType + " " + Data.TokenValue);
        //    httpWebRequest.Timeout = 180000;
        //    httpWebRequest.ReadWriteTimeout = 1800000;
        //    try
        //    {
        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();//to send or to get reponse
        //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //        {
        //            ret = streamReader.ReadToEnd();//ret is return value that we get from service

        //            if (ret.Substring(0, 1) != "[")
        //            {
        //                ret = "[" + ret + "]";
        //            }

        //            data = JsonConvert.DeserializeObject<List<T>>(ret);
        //        }
        //    }
        //    catch (WebException)
        //    {
        //        ret = "";
        //        //throw new Exception(string.Format("{0}", M10UserFrameWork.GetConfigMsgGetDataFailed()));
        //    }

        //    return data;
        //}

        public string PostJsonRespon(string URL, string JSON)
        {
            String ret = string.Empty;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 180000;
            httpWebRequest.ReadWriteTimeout = 1800000;
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JSON);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            catch (Exception)
            {
                throw new Exception(string.Format("{0}", "Cannot connect to server!!!"));
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();//to send or to get reponse
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    ret = streamReader.ReadToEnd();//ret is return value that we get from service
                    if (ret.Length > 3)
                        ret = ret.Substring(1, ret.Length - 2).Replace("\\", "");
                }
            }
            catch (Exception)
            {
                ret = "";
                throw new Exception(string.Format("{0}", "Get Data Failed!!!"));
            }

            return ret;
        }

        public T PostJsonRespon<T>(string URL, string JSON)
        {
            String ret = string.Empty;
            //T data = new <T>();
            T data = default(T);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 1800000;
            httpWebRequest.ReadWriteTimeout = 1800000;
            
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JSON);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            catch (Exception es)
            {
                //throw new Exception(string.Format("{0}.\n{1}.", M10UserFrameWork.GetConfigMsgServerNotConnected().Split('|')[0], M10UserFrameWork.GetConfigMsgServerNotConnected().Split('|')[1]));
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();//to send or to get reponse
                
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    ret = streamReader.ReadToEnd();//ret is return value that we get from service
                    data = JsonConvert.DeserializeObject<T>(ret);
                }
            }
            catch (WebException)
            {
                ret = "";
                //throw new Exception(string.Format("{0}", M10UserFrameWork.GetConfigMsgGetDataFailed()));
            }

            return data;
        }

        //public T PostJsonRespon<T>(string URL, string JSON, ModelSMS.Model.Token Data)
        //{
        //    String ret = string.Empty;
        //    //T data = new <T>();
        //    T data = default(T);
        //    var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
        //    httpWebRequest.ContentType = "application/json";
        //    httpWebRequest.Method = "POST";
        //    httpWebRequest.Headers.Add("Authorization", Data.TokenType + " " + Data.TokenValue);
        //    httpWebRequest.Timeout = 180000;
        //    httpWebRequest.ReadWriteTimeout = 1800000;
        //    try
        //    {
        //        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //        {
        //            streamWriter.Write(JSON);
        //            streamWriter.Flush();
        //            streamWriter.Close();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //throw new Exception(string.Format("{0}.\n{1}.", M10UserFrameWork.GetConfigMsgServerNotConnected().Split('|')[0], M10UserFrameWork.GetConfigMsgServerNotConnected().Split('|')[1]));
        //    }

        //    try
        //    {
        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();//to send or to get reponse
        //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //        {
        //            ret = streamReader.ReadToEnd();//ret is return value that we get from service
        //            data = JsonConvert.DeserializeObject<T>(ret);
        //        }
        //    }
        //    catch (WebException)
        //    {
        //        ret = "";
        //        //throw new Exception(string.Format("{0}", M10UserFrameWork.GetConfigMsgGetDataFailed()));
        //    }

        //    return data;
        //}

        public List<T> PostJsonResponList<T>(string URL, string JSON)
        {
            String ret = string.Empty;

            List<T> data = new List<T>();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 180000;
            httpWebRequest.ReadWriteTimeout = 1800000;
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JSON);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            catch (Exception)
            {
                //throw new Exception(string.Format("{0}.\n{1}.", M10UserFrameWork.GetConfigMsgServerNotConnected().Split('|')[0], M10UserFrameWork.GetConfigMsgServerNotConnected().Split('|')[1]));
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();//to send or to get reponse
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    ret = streamReader.ReadToEnd();//ret is return value that we get from service
                    data = JsonConvert.DeserializeObject<List<T>>(ret);

                    //if (ret.Length > 3)
                    //    ret = ret.Substring(1, ret.Length - 2).Replace("\\", "");
                }
            }
            catch (Exception)
            {
                ret = "";
                //throw new Exception(string.Format("{0}", M10UserFrameWork.GetConfigMsgGetDataFailed()));
            }

            return data;
        }

        //public List<T> PostJsonResponList<T>(string URL, string JSON, ModelSMS.Model.Token Data)
        //{
        //    String ret = string.Empty;

        //    List<T> data = new List<T>();

        //    var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
        //    httpWebRequest.ContentType = "application/json";
        //    httpWebRequest.Method = "POST";
        //    httpWebRequest.Headers.Add("Authorization", Data.TokenType + " " + Data.TokenValue);
        //    httpWebRequest.Timeout = 180000;
        //    httpWebRequest.ReadWriteTimeout = 1800000;
        //    try
        //    {
        //        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //        {
        //            streamWriter.Write(JSON);
        //            streamWriter.Flush();
        //            streamWriter.Close();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //throw new Exception(string.Format("{0}.\n{1}.", M10UserFrameWork.GetConfigMsgServerNotConnected().Split('|')[0], M10UserFrameWork.GetConfigMsgServerNotConnected().Split('|')[1]));
        //    }

        //    try
        //    {
        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();//to send or to get reponse
        //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //        {
        //            ret = streamReader.ReadToEnd();//ret is return value that we get from service
        //            data = JsonConvert.DeserializeObject<List<T>>(ret);

        //            //if (ret.Length > 3)
        //            //    ret = ret.Substring(1, ret.Length - 2).Replace("\\", "");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ret = "";
        //        //throw new Exception(string.Format("{0}", M10UserFrameWork.GetConfigMsgGetDataFailed()));
        //    }

        //    return data;
        //}
    }

    public static class Common
    {
        public enum INFOHARDWARE
        {
            IPCLIENT = 0,
            MACADDRESS
        }
        public enum StatusForm
        {
            EDIT = 0,
            CREATE
        }
        public static int zIndexUser = 0;
        public static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes
                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue
                  = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;
        }
        public static string[] CompressData(DataTable dt)
        {
            try
            {
                byte[] bt = null;
                byte[] bt2 = null;
                MemoryStream ms = new MemoryStream();
                MemoryStream ms1 = new MemoryStream();
                GZipStream zip = new GZipStream(ms, CompressionMode.Compress);
                GZipStream zip1 = new GZipStream(ms1, CompressionMode.Compress);
                dt.WriteXmlSchema(zip1, false);
                dt.WriteXml(zip, false);
                zip.Close();
                zip1.Close();
                bt = ms.ToArray();
                bt2 = ms1.ToArray();
                ms.Close();
                ms1.Close();
                string[] xdata = { Convert.ToBase64String(bt), Convert.ToBase64String(bt2) };
                return xdata;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable DecompressData(string[] str)
        {
            try
            {
                byte[] btdata = Convert.FromBase64String(str[0]);
                byte[] btschema = Convert.FromBase64String(str[1]);
                MemoryStream ms = new System.IO.MemoryStream(btdata);
                MemoryStream msschema = new System.IO.MemoryStream(btschema);
                GZipStream unzip = new GZipStream(ms, CompressionMode.Decompress);
                GZipStream unzipsch = new GZipStream(msschema, CompressionMode.Decompress);
                DataTable dt = new DataTable("dataTable");
                dt.ReadXmlSchema(unzipsch);
                dt.ReadXml(unzip);
                unzip.Close();
                unzipsch.Close();
                ms.Close();
                msschema.Close();
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        static public string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes
                = System.Convert.FromBase64String(encodedData);
            string returnValue =
               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public static bool ValidUrl(string url)
        {
            return Regex.IsMatch(url, @"^(http[s]?://)?([w]{3}[.])?([a-z0-9]+[.])+com?$");
        }

        public static Dictionary<INFOHARDWARE, string> InfoLAN()
        {
            Dictionary<INFOHARDWARE, string> xinfo = new Dictionary<INFOHARDWARE, string>();
            bool r_bool = false;
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    IPInterfaceProperties properties = nic.GetIPProperties();
                    string MacAddress = nic.GetPhysicalAddress().ToString();
                    foreach (IPAddressInformation unicast in properties.UnicastAddresses)
                    {
                        if (unicast.IsDnsEligible)
                        {
                            xinfo.Add(INFOHARDWARE.IPCLIENT, unicast.Address.ToString());
                            xinfo.Add(INFOHARDWARE.MACADDRESS, MacAddress);
                            r_bool = true;
                            break;
                        }
                    }
                }
                if (r_bool)
                    break;
            }
            return xinfo;
        }

    }

    class ConvertQuery
    {

        private Regex MatchNested = new Regex(
            @"\{ (?>([^{}]+)|\{ (?<D>)| \} (?<-D>))*(?(D)(?!))\}",
                 RegexOptions.IgnorePatternWhitespace
               | RegexOptions.Compiled
               | RegexOptions.Singleline);


        public string Replace(string input, Dictionary<string, string> vars)
        {
            Matcher matcher = new Matcher(vars);
            return MatchNested.Replace(input, matcher.Replace);
        }

        private class Matcher
        {

            private Dictionary<string, string> Vars;

            public Matcher(Dictionary<string, string> vars)
            {
                Vars = vars;
            }

            public string Replace(Match m)
            {
                string name = m.Groups[1].Value;
                //int length = (m.Groups[0].Length - name.Length) / 2;
                return Vars[name];
                //return MakeString(inner, length / 2);
            }

            private string MakeString(string inner, int braceCount)
            {
                StringBuilder sb = new StringBuilder(inner.Length + (braceCount * 2));
                sb.Append('{', braceCount);
                sb.Append(inner);
                sb.Append('}', braceCount);
                return sb.ToString();
            }

        }

    }

}
