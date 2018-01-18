using System.Web;

namespace FireSafetyStore.Web.Client.Infrastructure.Common
{
    public static class SessionManager<T>
    {
        public static void SetValue(string key, T value)
        {
            HttpContext.Current.Session.Add(key,value);
        }

        public static T GetValue(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

    }
}