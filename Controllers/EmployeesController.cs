﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NorthwindBasedWebApplication.API.Models;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using NorthwindBasedWebAPI.Repositories.IRepository;
using NorthwindBasedWebAPI.Models.Common;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Models.Dtos.EmployeeDtos;
using NorthwindBasedWebAPI.Models.Dtos.OrderDtos;
using NorthwindBasedWebAPI.Models.Dtos.TerritoryDtos;

namespace NorthwindBasedWebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private ApiResponse _response;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeesController> _logger;
        private readonly LoggingModelBuilder _loggingModelBuilder;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper,
            ILogger<EmployeesController> logger)
        {
            _employeeRepository = employeeRepository;
            _response = new();
            _mapper = mapper;
            _logger = logger;
            _loggingModelBuilder = new();
        }




        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetEmployees()
        {
            List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role.ToString()).ToList();
            ClaimsPrincipal user = this.User;

            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The model state is invalid!");
                _response.IsSuccess = false;

                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{GetEmployees}")
                       .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                       .SetMethodType("GET")
                       .SetErrorMessage("The model state is invalid!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();

                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return BadRequest(_response);
            }

            var employees = await _employeeRepository.GetAllAsync(tracked: false);

            if (employees.IsNullOrEmpty())
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("No Employees records was found!");

                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(GetEmployees)}")
                       .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                       .SetMethodType("GET")
                       .SetErrorMessage("No Employees records was found!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();

                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return BadRequest(_response);
            }

            if (employees == null)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Something went wrong while getting the employees records");

                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(GetEmployees)}")
                       .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                       .SetMethodType("GET")
                       .SetErrorMessage("Something went wrong while getting the employees records")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();

                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return BadRequest(_response);
            }

            var employeesResponse = _mapper.Map<List<ReadEmployeeDto>>(employees);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.data = employeesResponse;

            _loggingModelBuilder
                       .SetSuccess()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(GetEmployees)}")
                       .SetStatusCode(HttpStatusCode.OK.ToString())
                       .SetMethodType("GET")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();

            _logger.LogInformation("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

            return Ok(_response);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ApiResponse>> GetEmployee(int id)
        {
            List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role.ToString()).ToList();
            ClaimsPrincipal user = this.User;

            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The model state is invalid!");
                _response.IsSuccess = false;

                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(GetEmployee)}")
                       .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                       .SetMethodType("GET")
                       .SetErrorMessage("The model state is invalid!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return BadRequest(_response);
            }

            if (id <= 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The given id is invalid");
                _response.IsSuccess = false;


                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(GetEmployee)}")
                       .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                       .SetMethodType("GET")
                       .SetErrorMessage("The given id is invalid!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);


                return BadRequest(_response);
            }

            if (!await _employeeRepository.IsExistAsync(i => i.Id == id))
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("No employee found with given id!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(GetEmployee)}")
                       .SetStatusCode(HttpStatusCode.NotFound.ToString())
                       .SetMethodType("GET")
                       .SetErrorMessage("No employee found with given id!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return NotFound(_response);   
            }

            var employeeModel = await _employeeRepository.GetAsync(i => i.Id == id, tracked: false);

            if (employeeModel == null)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while getting the employee!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(GetEmployee)}")
                       .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                       .SetMethodType("GET")
                       .SetErrorMessage("Something went wrong while getting the employee!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);


                return BadRequest(_response);
            }

            var employeeResponse = _mapper.Map<ReadEmployeeDto>(employeeModel);

            _response.StatusCode = HttpStatusCode.OK;
            _response.data = employeeResponse;
            _response.IsSuccess = true;

            _loggingModelBuilder
                       .SetSuccess()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(GetEmployee)}")
                       .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                       .SetMethodType("GET")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


            _logger.LogInformation("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);


            return Ok(_response);
        }




        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {

            List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role.ToString()).ToList();
            ClaimsPrincipal user = this.User;

            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The model state is invalid!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(CreateEmployee)}")
                       .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                       .SetMethodType("POST")
                       .SetErrorMessage("The model state is invalid!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return BadRequest(_response);
            }

            if (createEmployeeDto == null)
            {
                _response.ErrorMessages.Add("No content sent it!");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;


                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(CreateEmployee)}")
                       .SetStatusCode(HttpStatusCode.NoContent.ToString())
                       .SetMethodType("POST")
                       .SetErrorMessage("No content sent it!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return BadRequest(_response);
            }

            if (await _employeeRepository.IsExistAsync(cn => cn.HomePhone.Trim() == createEmployeeDto.HomePhone.Trim()))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The customer's Home phone is exists, please choose another!");
                _response.IsSuccess = false;

                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(CreateEmployee)}")
                       .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                       .SetMethodType("POST")
                       .SetErrorMessage("The customer's Home phone is exists, please choose another!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return BadRequest(_response);
            }

            var employeeModel = _mapper.Map<Employee>(createEmployeeDto);


            var createdEmployee = await _employeeRepository.CreateAsync(employeeModel);


            if (!createdEmployee)
            {
                _response.ErrorMessages.Add("Something went error while creating employee");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;

                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(CreateEmployee)}")
                       .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                       .SetMethodType("POST")
                       .SetErrorMessage("Something went wrong while creating the employee")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;

            _loggingModelBuilder
                .SetSuccess()
                .SetDetails($"{nameof(EmployeesController)}/{nameof(CreateEmployee)}")
                .SetStatusCode(HttpStatusCode.OK.ToString())
                .SetMethodType("POST")
                .SetRole(roleClaims.First().Value.ToString())
                .SetUser(user.Identity.Name.ToString())
                .Build();


            _logger.LogInformation("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

            return Ok(_response);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateEmployee(int id,
            [FromBody]UpdateEmployeeDto updateEmployeeDto)
        {
            List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role.ToString()).ToList();
            ClaimsPrincipal user = this.User;

            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The model state is invalid!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(UpdateEmployee)}")
                       .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                       .SetMethodType("PUT")
                       .SetErrorMessage("The model state is invalid!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return BadRequest(_response);
            }

            if (id <= 0 || updateEmployeeDto.Id <= 0)
            {

                _response.ErrorMessages.Add("The given ids are not match!");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;


                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(UpdateEmployee)}")
                       .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                       .SetMethodType("PUT")
                       .SetErrorMessage("The given ids are not match!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return BadRequest(_response);

            }

            if (id != updateEmployeeDto.Id)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("No matching with given ids");
                _response.IsSuccess = false;


                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(UpdateEmployee)}")
                       .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                       .SetMethodType("PUT")
                       .SetErrorMessage("No matching with given ids")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return BadRequest(_response);
            }


            if (updateEmployeeDto == null)
            {

                _response.ErrorMessages.Add("The content you provide is empty!");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;

                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(UpdateEmployee)}")
                       .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                       .SetMethodType("PUT")
                       .SetErrorMessage("The content you provide is empty!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();

                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);


                return BadRequest(_response);
            }

            if (!await _employeeRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("The employee with given id is not found");
                _response.IsSuccess = false;

                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(UpdateEmployee)}")
                       .SetStatusCode(HttpStatusCode.NotFound.ToString())
                       .SetMethodType("PUT")
                       .SetErrorMessage("The employee with given id is not found!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();

                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return NotFound(_response);
            }

            var employeeModel = _mapper.Map<Employee>(updateEmployeeDto);

            var updatedEmployee = await _employeeRepository.UpdateAsync(employeeModel);

            if (!updatedEmployee)
            {

                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went error while updating the employee!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(UpdateEmployee)}")
                       .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                       .SetMethodType("PUT")
                       .SetErrorMessage("Something went wrong while updating the employee!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);


                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;

            _loggingModelBuilder
                       .SetSuccess()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(UpdateEmployee)}")
                       .SetStatusCode(HttpStatusCode.OK.ToString())
                       .SetMethodType("PUT")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


            _logger.LogInformation("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);


            return Ok(_response);
        }



        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteEmployee(int id)
        {
            List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role.ToString()).ToList();
            ClaimsPrincipal user = this.User;

            if (!ModelState.IsValid)
            {
                _response.ErrorMessages.Add("The model states is invalid!");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;

                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(DeleteEmployee)}")
                       .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                       .SetMethodType("DELETE")
                       .SetErrorMessage("The model state is invalid!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();

                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return BadRequest(_response);
            }

            if (id <= 0)
            {

                _response.ErrorMessages.Add("The given id is invalid!");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;


                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(DeleteEmployee)}")
                       .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                       .SetMethodType("DELETE")
                       .SetErrorMessage("The given is invalid!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return BadRequest(_response);
            }

            if (!await _employeeRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {

                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("The employee with given id is not found!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(DeleteEmployee)}")
                       .SetStatusCode(HttpStatusCode.NotFound.ToString())
                       .SetMethodType("DELETE")
                       .SetErrorMessage("The employee with given id is not found!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();

                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return NotFound(_response);
            }

            var employeeModel = await _employeeRepository.GetAsync(i => i.Id == id, tracked: false);

            if (employeeModel == null)
            {

                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went error while getting the employee");
 
                _response.IsSuccess = false;

                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(EmployeesController)}/{nameof(DeleteEmployee)}")
                       .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                       .SetMethodType("DELETE")
                       .SetErrorMessage("Something went error while getting the employee")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);



                return BadRequest(_response);
            }

            var deletedEmployee = await _employeeRepository.DeleteAsync(employeeModel);

            if (!deletedEmployee)
            {

                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went error while deleting the employee");
                _response.IsSuccess = false;

                _loggingModelBuilder
                      .SetFailed()
                      .SetDetails($"{nameof(EmployeesController)}/{nameof(DeleteEmployee)}")
                      .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                      .SetMethodType("DELETE")
                      .SetErrorMessage("Something went error while deleting the employee")
                      .SetRole(roleClaims.First().Value.ToString())
                      .SetUser(user.Identity.Name.ToString())
                      .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);


                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;

            _loggingModelBuilder
                      .SetSuccess()
                      .SetDetails($"{nameof(EmployeesController)}/{nameof(DeleteEmployee)}")
                      .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                      .SetMethodType("DELETE")
                      .SetRole(roleClaims.First().Value.ToString())
                      .SetUser(user.Identity.Name.ToString())
                      .Build();


            _logger.LogInformation("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

            return Ok(_response);
        }


        [HttpGet]
        [Route("{id}/Territories")]
        public async Task<ActionResult<ApiResponse>> GetTerritoriesByEmployee(int id)
        {
            List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role.ToString()).ToList();
            ClaimsPrincipal user = this.User;

            if (id <= 0)
            {
                _response.ErrorMessages.Add("The given id is invalid!");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;

                _loggingModelBuilder
                    .SetFailed()
                    .SetDetails($"{nameof(EmployeesController)}/{nameof(GetTerritoriesByEmployee)}")
                    .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                    .SetMethodType("GET")
                    .SetErrorMessage("The given is is invalid!")
                    .SetRole(roleClaims.First().Value.ToString())
                    .SetUser(user.Identity.Name.ToString())
                    .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);


                return BadRequest(_response);
            }

            if(!await _employeeRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("The employee with given id is not found");
                _response.IsSuccess = false;


                _loggingModelBuilder
                    .SetFailed()
                    .SetDetails($"{nameof(EmployeesController)}/{nameof(GetTerritoriesByEmployee)}")
                    .SetStatusCode(HttpStatusCode.NotFound.ToString())
                    .SetMethodType("GET")
                    .SetErrorMessage("The employee with given id is not found")
                    .SetRole(roleClaims.First().Value.ToString())
                    .SetUser(user.Identity.Name.ToString())
                    .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return NotFound(_response);
            }

            var territories = await _employeeRepository.GetTerritoriesByEmployeeAsync(id);

            if(territories == null)
            {
                _response.ErrorMessages.Add("Something went wrong while getting the territories");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;

                _loggingModelBuilder
                    .SetFailed()
                    .SetDetails($"{nameof(EmployeesController)}/{nameof(GetTerritoriesByEmployee)}")
                    .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                    .SetMethodType("GET")
                    .SetErrorMessage("Something went wrong while getting the territories")
                    .SetRole(roleClaims.First().Value.ToString())
                    .SetUser(user.Identity.Name.ToString())
                    .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);


                return BadRequest(_response);
            }

            var territoriesResponse = _mapper.Map<List<ReadTerritoryDto>>(territories);

            _response.data = territoriesResponse;
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;

            _loggingModelBuilder
                .SetSuccess()
                .SetDetails($"{nameof(EmployeesController)}/{nameof(GetTerritoriesByEmployee)}")
                .SetStatusCode(HttpStatusCode.OK.ToString())
                .SetMethodType("GET")
                .SetRole(roleClaims.First().Value.ToString())
                .SetUser(user.Identity.Name.ToString())
                .Build();

            _logger.LogInformation("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

            return Ok(_response);
        }


        [HttpGet]
        [Route("{id}/Orders")]
        public async Task<ActionResult<ApiResponse>> GetOrdersByEmployee(int id)
        {
            List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role.ToString()).ToList();
            ClaimsPrincipal user = this.User;

            if (id <= 0)
            {
                _response.ErrorMessages.Add("The given id is invalid");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;

                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(EmployeesController)}/{nameof(GetOrdersByEmployee)}")
                   .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("The given id is invalid!")
                   .SetRole(roleClaims.First().Value.ToString())
                   .SetUser(user.Identity.Name.ToString())
                   .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);



                return BadRequest(_response);

            }

            if(!await _employeeRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("The employee with given id not found");
                _response.IsSuccess = false;


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(EmployeesController)}/{nameof(GetOrdersByEmployee)}")
                   .SetStatusCode(HttpStatusCode.NotFound.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("The employee with given id not found!")
                   .SetRole(roleClaims.First().Value.ToString())
                   .SetUser(user.Identity.Name.ToString())
                   .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return NotFound(_response);
            }

            var orders = await _employeeRepository.GetOrdersByEmployeeAsync(id);


            if (orders.Count == 0)
            {
                _response.ErrorMessages.Add("No orders record found!");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;

                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(EmployeesController)}/{nameof(GetOrdersByEmployee)}")
                   .SetStatusCode(HttpStatusCode.NotFound.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("No orders record found!")
                   .SetRole(roleClaims.First().Value.ToString())
                   .SetUser(user.Identity.Name.ToString())
                   .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return NotFound(_response);
            }

            if (orders == null)
            {
                _response.ErrorMessages.Add("Something went wrong while getting the orders");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;

                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(EmployeesController)}/{nameof(GetOrdersByEmployee)}")
                   .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("Something went wrong while getting the orders")
                   .SetRole(roleClaims.First().Value.ToString())
                   .SetUser(user.Identity.Name.ToString())
                   .Build();


                _logger.LogError("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

                return BadRequest(_response);   
            }

            var ordersResponse = _mapper.Map<List<ReadOrderDto>>(orders);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.data = ordersResponse;


            _loggingModelBuilder
                   .SetSuccess()
                   .SetDetails($"{nameof(EmployeesController)}/{nameof(GetOrdersByEmployee)}")
                   .SetStatusCode(HttpStatusCode.OK.ToString())
                   .SetMethodType("GET")
                   .SetRole(roleClaims.First().Value.ToString())
                   .SetUser(user.Identity.Name.ToString())
                   .Build();



            _logger.LogInformation("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
                    _loggingModelBuilder.Build().Details,
                    _loggingModelBuilder.Build().StatusCode,
                    _loggingModelBuilder.Build().MethodType,
                    _loggingModelBuilder.Build().User,
                    _loggingModelBuilder.Build().Role,
                    _loggingModelBuilder.Build().Success,
                    _loggingModelBuilder.Build().Failed,
                    _loggingModelBuilder.Build().ErrorMessage);

            return Ok(_response);
        }
    }
}
