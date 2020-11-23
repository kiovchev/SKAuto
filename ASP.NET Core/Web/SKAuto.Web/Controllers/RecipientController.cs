namespace SKAuto.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Common;
    using SKAuto.Services.Data;
    using SKAuto.Web.ViewModels.ViewModels.RecipientViewModels;

    public class RecipientController : BaseController
    {
        private readonly IRecipientService recipientService;

        public RecipientController(IRecipientService recipientService)
        {
            this.recipientService = recipientService;
        }

        public IActionResult Home()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecipientParamsViewModel paramsRecipient)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Brand/Details");
            }

            bool ifExists = await this.recipientService.IfRecipientExistsAsync(paramsRecipient.Phone);

            if (ifExists)
            {
                var error = new RecipientError();
                error.ErrorMessage = GlobalConstants.RecipientCreateErrorMessage;
                return this.RedirectToAction("Error", "Recipient", error);
            }

            var recipientId = await this.recipientService.CreateRecipientAsync(paramsRecipient);
            return this.RedirectToAction("Create", "Order", new { recipientId = recipientId });
        }

        public IActionResult Find()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Find(RecipientFindInputViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Recipient/Find");
            }

            var recipientId = await this.recipientService.FindRecipientAsync(model.Phone);

            if (recipientId == 0)
            {
                var error = new RecipientError();
                error.ErrorMessage = GlobalConstants.RecipientFindErrorMessage;
                return this.RedirectToAction("Error", "Recipient", error);
            }

            return this.RedirectToAction("Create", "Order", new { recipientId = recipientId });
        }

        public IActionResult Error(RecipientError recipientError)
        {
            return this.View(recipientError);
        }
    }
}
