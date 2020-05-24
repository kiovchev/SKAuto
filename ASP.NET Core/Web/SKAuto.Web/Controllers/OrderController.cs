namespace SKAuto.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class OrderController : BaseController
    {
        public IActionResult GetAll(int id)
        {
            return this.View();
        }
    }
}
