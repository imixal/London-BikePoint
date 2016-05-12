using System.Web;
using System.Web.Mvc;
using ComicsMore.Resources;
namespace ComicsMore
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
