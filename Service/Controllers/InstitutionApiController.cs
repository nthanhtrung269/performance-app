﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Core.Domain.Institutions;
using Core.Interfaces;
using Core.Interfaces.Repositories.Business;
using Infrastructure.AutoMapper;
using Service.Dtos.Institution;

namespace Service.Controllers
{
    [RoutePrefix("api/institutions")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InstitutionApiController : ApiController
    {
        private readonly IComplete _unitOfWork;
        private readonly IInstitutionRepository<Institution> _repository;

        public InstitutionApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (IComplete) unitOfWork;
            _repository = unitOfWork.Institutions;
        }

        [ResponseType(typeof(ICollection<InstitutionDto>))]
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> GetAsync()
        {
            var institutions = await _repository.GetAll().ToListAsync();

            if (institutions == null)
            {
                return NotFound();
            }

            return Ok(institutions.Map<ICollection<Institution>>());
        }

        [ResponseType(typeof(InstitutionDto))]
        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            var institution = await _repository.GetAsync(id);

            if (institution == null)
            {
                return NotFound();
            }

            return Ok(institution.Map<InstitutionDto>());
        }

        [HttpPut, Route("{id}")]
        public async Task<IHttpActionResult> UpdateAsync(int id, InstitutionDto institution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var institutionInDb = await _repository.GetAsync(id);

            if (institutionInDb == null)
            {
                return NotFound();
            }

            _repository.Add(institution.Map<Institution>());

            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> CreateAsync(InstitutionDto institution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _repository.Add(institution.Map<Institution>());

            await _unitOfWork.CompleteAsync();

            return Created(new Uri(Request.RequestUri + "/" + institution.Id), institution);
        }

        [HttpDelete, Route("{id}/delete")]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            var institution = _repository.GetAsync(id);

            if (institution == null)
            {
                return NotFound();
            }

            _repository.Remove(institution.Map<Institution>());

            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
