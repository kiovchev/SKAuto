namespace SKAuto.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Services.Data;
    using SKAuto.Web.HandMappers.RecipientMappers;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;
    using SKAuto.Web.ViewModels.ViewModels.RecipientViewModels;

    public class RecipientController : BaseController
    {
        private readonly IRecipientService recipientService;

        public RecipientController(IRecipientService recipientService)
        {
            this.recipientService = recipientService;
        }

        public IActionResult Home(RecipientHomeViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Brand/Details");
            }

            return this.View(model);
        }

        public IActionResult Create(RecipientHomeViewModel partModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Brand/Details");
            }

            return this.View(partModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartByCategoryAndModelViewModel partModel, RecipientParamsViewModel paramsRecipient)
        {
            bool checkModel = this.CheckIsValidModel(partModel);
            bool paramsModel = this.CheckIsValidParamsModel(paramsRecipient);

            if (!checkModel || !paramsModel)
            {
                return this.Redirect("/Brand/Details");
            }

            bool ifExists = await this.recipientService.IfRecipientExistsAsync(paramsRecipient.Phone);

            if (ifExists)
            {
                return this.Redirect("/Recipient/Index");
            }
            else
            {
                // TODO Create Order Controller
                var currentRecipientId = await this.recipientService.CreateRecipientAndOrderAsync(partModel, paramsRecipient);
                return this.Redirect("/Order/GetAll?currentRecipientIdq");
            }
        }

        public async Task<IActionResult> Find(int partId)
        {
            var dtoModel = await this.recipientService.GetPartParams(partId);
            var model = RecipientFindOutputMapper.Map(dtoModel);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Find(RecipientFindInputViewModel model)
        {
            // look at input model - need only partId, quantity and phone
            if (this.ModelState.IsValid)
            {
                return this.Redirect("/Brand/All");
            }

            // find or create order and redirect to this recipient order
            return null;
        }

        private bool CheckIsValidParamsModel(RecipientParamsViewModel paramsRecipient)
        {
            if (paramsRecipient.Address != null && paramsRecipient.FirstName != null
                && paramsRecipient.LastName != null && paramsRecipient.Phone != null && paramsRecipient.Town != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckIsValidModel(PartByCategoryAndModelViewModel partModel)
        {
            if (partModel.BrandAndModelName != null && partModel.CategoryName != null
                && partModel.PartName != null && partModel.SellPrice != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
