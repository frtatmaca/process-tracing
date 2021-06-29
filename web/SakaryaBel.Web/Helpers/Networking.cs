using System.Web;

namespace Helpers
{
    public class Networking
    {
        public static string GetIPAddress()
        {
            if (HttpContext.Current == null || HttpContext.Current.Request == null) return "";
            return GetIPAddress(new HttpRequestWrapper(HttpContext.Current.Request));
        }
        public static string GetIPAddress(HttpRequestBase request)
        {
            string ip = "";

            if (request == null && HttpContext.Current != null) request = new HttpRequestWrapper(HttpContext.Current.Request);
            if (request != null)
            {
                ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(ip))
                    ip = request.ServerVariables["REMOTE_ADDR"];
                else
                {
                    string[] ipArray = ip.Split(',');
                    ip = ipArray[0];
                }
            }
            return ip;
        }
        //public static string GetIPAddress(HttpRequestMessage request)
        //{
        //    if (request.Properties.ContainsKey("MS_HttpContext"))
        //    {
        //        return GetIPAddress(((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request);
        //    }
        //    else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
        //    {
        //        RemoteEndpointMessageProperty prop;
        //        prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
        //        return prop.Address;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
