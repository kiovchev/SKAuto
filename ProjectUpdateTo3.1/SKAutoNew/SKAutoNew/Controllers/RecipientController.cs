namespace SKAutoNew.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SKAutoNew.Common;
    using SKAutoNew.HandMappers.RecipientMappers;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Web.ViewModels.RecipientViewModels;
    using System.Threading.Tasks;

    public class RecipientController : BaseController
    {
        private readonly IRecipientService recipientService;

        public RecipientController(IRecipientService recipientService)
        {
            this.recipientService = recipientService;
        }

        public async Task<IActionResult> Index()
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            var recipientsForIndexDto = await this.recipientService.GetAllRecipientsAsync();
            var viewModels = RecipientIndexViewMapper.Map(recipientsForIndexDto);

            return this.View(viewModels);
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
                return this.Redirect("/Recipient/Home");
            }

            bool ifExists = await this.recipientService.IfRecipientExistsAsync(paramsRecipient.Phone);

            if (ifExists)
            {
                var error = new RecipientError();
                error.ErrorMessage = GlobalConstants.RecipientCreateErrorMessage;
                return this.RedirectToAction("Error", "Recipient", error);
            }

            var recipientId = await this.recipientService.CreateRecipientAsync(paramsRecipient.FirstName,
                                                                               paramsRecipient.LastName,
                                                                               paramsRecipient.Town,
                                                                               paramsRecipient.Address,
                                                                               paramsRecipient.Phone);
            return this.RedirectToAction("Create", "Order", new { recipientId = recipientId });
        }

        public IActionResult Find()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Find(RecipientFindInputViewModel model)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

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

        public async Task<IActionResult> Delete(RecipientDeleteViewModel deleteViewModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                var error = new RecipientError();
                error.ErrorMessage = GlobalConstants.RecipientDeleteModelValidationMessege;
                return this.RedirectToAction("Error", "Recipient", error);
            }

            var dtoModel = RecipientDeleteViewMapper.Map(deleteViewModel);
            var isDelete = await this.recipientService.DeleteRecipientAsync(dtoModel);

            if (!isDelete)
            {
                var error = new RecipientError();
                error.ErrorMessage = GlobalConstants.RecipientInvalidDeleteModelMessege;
                return this.RedirectToAction("Error", "Recipient", error);
            }

            return this.Redirect("/Recipient/Index");
        }

        public async Task<IActionResult> Update(RecipientUpdateOutputViewModel outputViewModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                var error = new RecipientError();
                error.ErrorMessage = GlobalConstants.RecipientUpdateModelValidationMessege;
                return this.RedirectToAction("Error", "Recipient", error);
            }

            var dtoModel = RecipientUpdateOutputViewMapper.Map(outputViewModel);
            var paramsDtoModel = await this.recipientService.GetRecipientParamsForUpdateAsync(dtoModel);
            var paramsViewModel = RecipientUpdateParamsMapper.Map(paramsDtoModel);

            return this.View(paramsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RecipientUpdateInputViewModel inputViewModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                var error = new RecipientError();
                error.ErrorMessage = GlobalConstants.RecipientUpdateModelValidationMessege;
                return this.RedirectToAction("Error", "Recipient", error);
            }

            var dtoModel = RecipientUpdateInputMapper.Map(inputViewModel);
            var isSame =  await this.recipientService.UpdateRecipientAsync(dtoModel);

            if (!isSame)
            {
                var error = new RecipientError();
                error.ErrorMessage = GlobalConstants.RecipientUpdateSameModelMessege;
                return this.RedirectToAction("Error", "Recipient", error);
            }

            return this.Redirect("/Recipient/Index");
        }

        public IActionResult Error(RecipientError recipientError)
        {
            return this.View(recipientError);
        }
    }
}
