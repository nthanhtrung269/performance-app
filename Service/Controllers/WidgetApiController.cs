﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using Core.Domain.TileWidgets;
using Core.Interfaces;
using Core.Interfaces.Repositories.Business;
using Infrastructure.AutoMapper;
using Service.Dtos.Widget;

namespace Service.Controllers
{
    public class WidgetApiController : ApiController
    {
        private readonly ITileWidgetRepository<TileWidget> _repository;
        private readonly IComplete _unitOfWork;

        public WidgetApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (IComplete)unitOfWork;
            _repository = unitOfWork.TileWidgets;
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> CreateAsync(CreateWidgetDto createWidgetDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newWidget = createWidgetDto.Map<TileWidget>();

            newWidget.UserId = Guid.NewGuid().ToString();
            _repository.Add(newWidget);

            await _unitOfWork.CompleteAsync();

            return Created(new Uri(Request.RequestUri + "/" + newWidget.Id), newWidget);
        }
    }
}
