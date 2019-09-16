using System.Web.Mvc;

using WEB.AppInfrastructure.Filters;

namespace WEB {

    public static class FilterConfig {

        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {

            filters.Add(new HandleErrorCustom());

            filters.Add(new FilterSecurity());

        }
    }

}