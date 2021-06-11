using DapperASPNetCore.Contracts;
using DapperASPNetCore.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Controllers
{
	[Route("api/companies")]
	[ApiController]
	public class CompaniesController : ControllerBase
	{
		private readonly ICompanyRepository _companyRepo;

		public CompaniesController(ICompanyRepository companyRepo)
		{
			_companyRepo = companyRepo;
		}

		[HttpGet]
		public async Task<IActionResult> GetCompanies()
		{
			try
			{
				var companies = await _companyRepo.GetCompanies();
				return Ok(companies);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "CompanyById")]
		public async Task<IActionResult> GetCompany(int id)
		{
			try
			{
				var company = await _companyRepo.GetCompany(id);
				if (company == null)
					return NotFound();

				return Ok(company);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateCompany(CompanyForCreationDto company)
		{
			try
			{
				var createdCompany = await _companyRepo.CreateCompany(company);
				return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCompany(int id, CompanyForUpdateDto company)
		{
			try
			{
				var dbCompany = await _companyRepo.GetCompany(id);
				if (dbCompany == null)
					return NotFound();

				await _companyRepo.UpdateCompany(id, company);
				return NoContent();
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCompany(int id)
		{
			try
			{
				var dbCompany = await _companyRepo.GetCompany(id);
				if (dbCompany == null)
					return NotFound();

				await _companyRepo.DeleteCompany(id);
				return NoContent();
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("ByEmployeeId/{id}")]
		public async Task<IActionResult> GetCompanyForEmployee(int id)
		{
			try
			{
				var company = await _companyRepo.GetCompanyByEmployeeId(id);
				if (company == null)
					return NotFound();

				return Ok(company);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}/MultipleResult")]
		public async Task<IActionResult> GetCompanyEmployeesMultipleResult(int id)
		{
			try
			{
				var company = await _companyRepo.GetCompanyEmployeesMultipleResults(id);
				if (company == null)
					return NotFound();

				return Ok(company);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("MultipleMapping")]
		public async Task<IActionResult> GetCompaniesEmployeesMultipleMapping()
		{
			try
			{
				var company = await _companyRepo.GetCompaniesEmployeesMultipleMapping();

				return Ok(company);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("multiple")]
		public async Task<IActionResult> CreateCompany(List<CompanyForCreationDto> companies)
		{
			try
			{
				await _companyRepo.CreateMultipleCompanies(companies);
				return StatusCode(201);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}
	}
}
