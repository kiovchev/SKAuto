namespace SKAuto.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Services.Data;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;
    using SKAuto.Web.ViewModels.ViewModels.RecipientViewModels;

    public class RecipientController : BaseController
    {
        private readonly IRecipientService recipientService;

        public RecipientController(IRecipientService recipientService)
        {
            this.recipientService = recipientService;
        }

        public IActionResult Index(PartByCategoryAndModelViewModel partModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Brand/Details");
            }

            return this.View(partModel);
        }

        [HttpGet]
        public IActionResult Create(PartByCategoryAndModelViewModel partModel)
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
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Index", "Recipient", partModel);
            }

            bool ifExists = await this.recipientService.IfRecipientExistsAsync(paramsRecipient.Phone);

            if (ifExists)
            {
                RecipientFindAndPartModel recipientFindAndPart = new RecipientFindAndPartModel()
                {
                    PartModel = partModel,
                    ParamsRecipient = paramsRecipient,
                };
                return this.RedirectToAction("Find", "Recipient", recipientFindAndPart);
            }
            else
            {
                var recipientId = await this.recipientService.CreateRecipientAndOrderAsync(paramsRecipient);

                RecipientOrderIpnutModel recipientOrderIpnutModel = new RecipientOrderIpnutModel()
                {
                    PartModel = partModel,
                    RecipientId = recipientId,
                };

                return this.RedirectToAction("CreateOrAdd", "Order", recipientOrderIpnutModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Find(PartByCategoryAndModelViewModel partModel, RecipientParamsViewModel paramsRecipient)
        {
            if (this.ModelState.IsValid)
            {
                RecipientFindInputModel recipientFindModel = new RecipientFindInputModel()
                {
                    FirstName = paramsRecipient.FirstName,
                    LastName = paramsRecipient.LastName,
                    Phone = paramsRecipient.Phone,
                };

                var recipientOrderIpnutModel = await this.recipientService.TakeRecipientIdAsync(partModel, recipientFindModel);

                return this.RedirectToAction("CreateOrAdd", "Order", recipientOrderIpnutModel);
            }
            else
            {
                return this.View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Find(PartByCategoryAndModelViewModel partModel, RecipientFindInputModel findInputModel)
        {
            var recipientOrderIpnutModel = await this.recipientService.TakeRecipientIdAsync(partModel, findInputModel);

            return this.RedirectToAction("Create", "Order", recipientOrderIpnutModel);
        }
    }
}
