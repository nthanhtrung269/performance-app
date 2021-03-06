﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Core.Domain.Partners;
using Core.Interfaces;
using Core.Interfaces.Repositories.Business;
using Infrastructure.Extensions;
using Ninject.Extensions.Logging;
using Web.Controllers.Templates;
using Web.ViewModels.PartnerViewModels;

namespace Web.Controllers
{
    [Authorize]
    [RoutePrefix("partners")]
    public class PartnerController : BaseController
    {
        private readonly IPartnerRepository _partnerRepository;

        public PartnerController(IUnitOfWork unitOfWork, 
            IPartnerRepository partnerRepository,
            ILogger logger)
            : base(logger, unitOfWork)
        {
            _partnerRepository = partnerRepository;
        }

        [Route("")]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        /// <summary>
        /// List all Partners
        /// </summary>
        /// <returns></returns>
        [Route("list")]
        public ActionResult List()
        {
            return View();
        }
        
        /// <summary>
        /// Create new Partner
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            var partnerVm = new PartnerViewModel
            {
                PartnerTypeSelection = GetPartnerTypeSelection()
            };
            return View(partnerVm);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> Create(PartnerViewModel partnerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(partnerVm);
            }

            _partnerRepository.Add(partnerVm.Map<Partner>());

            await UnitOfWork.CompleteAsync();

            return View("List");
        }

        [HttpGet]
        [Route("update/{id}")]
        public async Task<ActionResult> Update(int id)
        {
            var partnerInDb = await _partnerRepository.GetAsync(id);

            if (partnerInDb == null)
            {
                return View("List");
            }

            var partnerVm = partnerInDb.Map<PartnerViewModel>();
            partnerVm.PartnerTypeSelection = GetPartnerTypeSelection();

            return View(partnerVm);
        }

        /// <summary>
        /// Update the Partner 
        /// </summary>
        /// <param name="id">The Id of the partner to be updated</param>
        /// <param name="partnerVm">The viewmodel of the partner to be updated</param>
        /// <returns></returns>
        [HttpPost]
        [Route("update/{id}")]
        public async Task<ActionResult> Update(int id, PartnerViewModel partnerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(partnerVm);
            }

            var partnerInDb = await _partnerRepository.GetAsync(id);

            if (partnerInDb == null)
            {
                return View(partnerVm);
            }

            partnerInDb = partnerVm.Map<Partner>();

            _partnerRepository.Add(partnerInDb);

            await UnitOfWork.CompleteAsync();

            return RedirectToAction("List");
        }

        /// <summary>
        /// Returns a list of partners available to be linked to the contact
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetPartnerTypeSelection()
        {
            var partnerTypes = _partnerRepository.GetTypesAsQueryable()
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                });

            return new SelectList(partnerTypes, "Value", "Text");
        }
    }
}