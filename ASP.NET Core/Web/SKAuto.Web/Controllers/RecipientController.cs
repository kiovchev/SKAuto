namespace SKAuto.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

    public class RecipientController : BaseController
    {
        public IActionResult Index(PartByCategoryAndModelViewModel partModel)
        {
            return this.View(partModel);
        }

        public IActionResult Create(PartByCategoryAndModelViewModel partModel)
        {
            return this.View(partModel);
        }
    }
}
