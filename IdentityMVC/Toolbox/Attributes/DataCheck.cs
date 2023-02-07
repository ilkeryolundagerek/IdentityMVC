using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityMVC.Toolbox.Attributes
{
    public class DataCheck : ActionFilterAttribute
    {
        private string myRole;

        public DataCheck(string role)
        {
            myRole = role;
        }

        //ActionFilter: Bir aksiyona bağlı olarak, öncesinde, çalışma anında veya sonrasında işlemler yapmamızı sağlar.
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string data = string.Join(", ", context.RouteData.Values.Values);
            context.RouteData.Values.Add("myRole", myRole);
            context.RouteData.Values["id"] = "Çok karışık";
            base.OnActionExecuting(context);
        }
    }
}
