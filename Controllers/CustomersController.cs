﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using NorthwindBasedWebAPI.Repositories.IRepository;
using NorthwindBasedWebAPI.Models.Common;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Models.Dtos.CustomerDtos;
using NorthwindBasedWebAPI.Models.Dtos.CustomerDemographicDtos;
using NorthwindBasedWebAPI.Models.Dtos.OrderDtos;

namespace NorthwindBasedWebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private ApiResponse _response;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersController> _logger;
        private readonly LoggingModelBuilder _loggingModelBuilder;

        public CustomersController(ICustomerRepository customerRepository, IMapper mapper,
            ILogger<CustomersController> logger)
        {
            _customerRepository = customerRepository;
            _response = new();
            _mapper = mapper;
            _logger = logger;
            _loggingModelBuilder = new();
        }




        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetCustomers()
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
                        .SetDetails($"{nameof(CustomersController)}/{nameof(GetCustomers)}")
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

            var customers = await _customerRepository.GetAllAsync(tracked: false);

            if(customers.Count == 0)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("No record of customer found!");


                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(GetCustomers)}")
                        .SetStatusCode(HttpStatusCode.NotFound.ToString())
                        .SetMethodType("GET")
                        .SetErrorMessage("No record of customer found!")
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

            if (customers == null)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Something went wrong while getting the customers!");


                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(GetCustomers)}")
                        .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                        .SetMethodType("GET")
                        .SetErrorMessage("Something went wrong while getting the customers!")
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

            var customersResponse = _mapper.Map<List<ReadCustomerDto>>(customers);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.data = customersResponse;

            _loggingModelBuilder
                    .SetSuccess()
                    .SetDetails($"{nameof(CustomersController)}/{nameof(GetCustomers)}")
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
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse>> GetCustomer(int id)
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
                        .SetDetails($"{nameof(CustomersController)}/{nameof(GetCustomer)}")
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
                        .SetDetails($"{nameof(CustomersController)}/{nameof(GetCustomers)}")
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

            if (!await _customerRepository.IsExistAsync(i => i.Id == id))
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("No customer found with given id!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(GetCustomers)}")
                        .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                        .SetMethodType("GET")
                        .SetErrorMessage("No customer found with given id!")
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

            var customer = await _customerRepository.GetAsync(i => i.Id == id, tracked: false);



            if (customer == null)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while getting the customer with given id!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(GetCustomers)}")
                        .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                        .SetMethodType("GET")
                        .SetErrorMessage("Something went wrong while getting the customer with given id!")
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

            var customerResponse = _mapper.Map<ReadCustomerDto>(customer);

            _response.StatusCode = HttpStatusCode.OK;
            _response.data = customerResponse;
            _response.IsSuccess = true;

            _loggingModelBuilder
                        .SetSuccess()
                        .SetDetails($"{nameof(CustomersController)}/{GetCustomers}")
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




        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateCustomer(CreateCustomerDto createCustomerDto)
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
                        .SetDetails($"{nameof(CustomersController)}/{nameof(CreateCustomer)}")
                        .SetStatusCode(HttpStatusCode.BadRequest.ToString())
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

            if (createCustomerDto == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The content that send by user is empty!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(CreateCustomer)}")
                        .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                        .SetMethodType("POST")
                        .SetErrorMessage("The content that send by user is empty!")
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

            if (await _customerRepository.IsExistAsync(cn => cn.ContactName.Trim() == createCustomerDto.ContactName.Trim()))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The customer's contact name is exists, please choose another!");
                _response.IsSuccess = false;

                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(CreateCustomer)}")
                        .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                        .SetMethodType("POST")
                        .SetErrorMessage("The customer's contact name is exists, please choose another!")
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

            var customerModel = _mapper.Map<Customer>(createCustomerDto);


            var createdCustomer = await _customerRepository.CreateAsync(customerModel);


            if (!createdCustomer)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while getting the customer record");
                _response.IsSuccess = false;


                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(CreateCustomer)}")
                        .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                        .SetMethodType("POST")
                        .SetErrorMessage("Something went wrong while getting the customer record!")
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
                        .SetDetails($"{nameof(CustomersController)}/{nameof(CreateCustomer)}")
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
        public async Task<ActionResult<ApiResponse>> UpdateCustomer(int id, UpdateCustomerDto updateCustomerDto)
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
                        .SetDetails($"{nameof(CustomersController)}/{nameof(UpdateCustomer)}")
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

            if (id <= 0 || updateCustomerDto.Id <= 0)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The given id is invalid!");
                _response.IsSuccess = false;

                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(UpdateCustomer)}")
                        .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                        .SetMethodType("PUT")
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

            if (id != updateCustomerDto.Id)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("No matching with given ids");
                _response.IsSuccess = false;

                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(UpdateCustomer)}")
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


            if (updateCustomerDto == null)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The content of give model is empty");
                _response.IsSuccess = false;


                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(UpdateCustomer)}")
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

            if (!await _customerRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("No customer found with the given Id!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(UpdateCustomer)}")
                        .SetStatusCode(HttpStatusCode.NotFound.ToString())
                        .SetMethodType("PUT")
                        .SetErrorMessage("No customer found with given id!")
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

            var ToUpdateCustomerModel = _mapper.Map<Customer>(updateCustomerDto);


            var updatedCustomer = await _customerRepository.UpdateAsync(ToUpdateCustomerModel);

            if (!updatedCustomer)
            {

                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went error while updating the customer!");
                _response.IsSuccess = false;

                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(UpdateCustomer)}")
                        .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                        .SetMethodType("PUT")
                        .SetErrorMessage("Something went error while updating the customer!")
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
                        .SetDetails($"{nameof(CustomersController)}/{nameof(UpdateCustomer)}")
                        .SetStatusCode(HttpStatusCode.OK.ToString())
                        .SetMethodType("PUT")
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


            return Ok(_response);
        }



        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteCustomer(int id)
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
                        .SetDetails($"{nameof(CustomersController)}/{nameof(DeleteCustomer)}")
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

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The given id is not valid!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(DeleteCustomer)}")
                        .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                        .SetMethodType("DELETE")
                        .SetErrorMessage("The given id is not valid!")
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

            if (!await _customerRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {

                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("The customer with given id is not found!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(DeleteCustomer)}")
                        .SetStatusCode(HttpStatusCode.NotFound.ToString())
                        .SetMethodType("DELETE")
                        .SetErrorMessage("The customer with given id is not found!")
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

            var customerModel = await _customerRepository.GetAsync(i => i.Id == id, tracked: false);

            if (customerModel == null)
            {

                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went error while getting the customer record!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(DeleteCustomer)}")
                        .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                        .SetMethodType("DELETE")
                        .SetErrorMessage("Something went wrong while getting the customer record!")
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

            var deletedCustomer = await _customerRepository.DeleteAsync(customerModel);

            if (!deletedCustomer)
            {

                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while deleting the customer record");
                _response.IsSuccess = false;

                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(DeleteCustomer)}")
                        .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                        .SetMethodType("DELETE")
                        .SetErrorMessage("Something went wrong while deleting the customer record!")
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
                        .SetDetails($"{nameof(CustomersController)}/{nameof(DeleteCustomer)}")
                        .SetStatusCode(HttpStatusCode.OK.ToString())
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
        [Route("{id}/CustomerDemographics")]
        public async Task<ActionResult<ApiResponse>> GetCustomerDemographicsByCustomer(int id)
        {
            List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role.ToString()).ToList();
            ClaimsPrincipal user = this.User;

            if (id <= 0)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The given id is not valid!");
                _response.IsSuccess = false;

                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(GetCustomerDemographicsByCustomer)}")
                        .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                        .SetMethodType("GET")
                        .SetErrorMessage("The given id is not valid")
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

            if (!await _customerRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {

                _response.IsSuccess = false;
                _response.ErrorMessages.Add("The customer with given id is not found");
                _response.StatusCode = HttpStatusCode.NotFound;


                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(GetCustomerDemographicsByCustomer)}")
                        .SetStatusCode(HttpStatusCode.NotFound.ToString())
                        .SetMethodType("GET")
                        .SetErrorMessage("The customer with given id is not found!")
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

            var customerDemographics = await _customerRepository.GetCustomerDemographicsByCustomerAsync(id);


            if (customerDemographics.Count() == 0)
            {
                _response.ErrorMessages.Add("No record was found!");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;


                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(CustomersController)}/{nameof(GetCustomerDemographicsByCustomer)}")
                       .SetStatusCode(HttpStatusCode.NotFound.ToString())
                       .SetMethodType("GET")
                       .SetErrorMessage("No record was found!")
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



            if (customerDemographics == null)
            {
                _response.ErrorMessages.Add("Something went wrong while getting customer demographics");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;


                _loggingModelBuilder
                        .SetFailed()
                        .SetDetails($"{nameof(CustomersController)}/{nameof(GetCustomerDemographicsByCustomer)}")
                        .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                        .SetMethodType("GET")
                        .SetErrorMessage("Something went wrong while getting customer demographics")
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


            

            var customerDemographicsResponse = _mapper.Map<List<ReadCustomerDemographicDto>>(customerDemographics);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.data = customerDemographicsResponse;

            _loggingModelBuilder
                       .SetSuccess()
                       .SetDetails($"{nameof(CustomersController)}/{nameof(GetCustomerDemographicsByCustomer)}")
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
        public async Task<ActionResult<ApiResponse>> GetOrdersByCustomer(int id)
        {
            List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role.ToString()).ToList();
            ClaimsPrincipal user = this.User;

            if (id <= 0)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The given id is not valid!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(CustomersController)}/{nameof(GetOrdersByCustomer)}")
                       .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                       .SetMethodType("GET")
                       .SetErrorMessage("The given id is not valid!")
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

            if (!await _customerRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {

                _response.ErrorMessages.Add("The customer with given id is not found");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;

                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(CustomersController)}/{nameof(GetOrdersByCustomer)}")
                       .SetStatusCode(HttpStatusCode.NotFound.ToString())
                       .SetMethodType("GET")
                       .SetErrorMessage("The customer with given id is not found!")
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


            var orders = await _customerRepository.GetOrdersByCustomerAsync(id);


            if (orders.IsNullOrEmpty())
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("No record was found!");


                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(CustomersController)}/{nameof(GetOrdersByCustomer)}")
                       .SetStatusCode(HttpStatusCode.NotFound.ToString())
                       .SetMethodType("GET")
                       .SetErrorMessage("No record was found!")
                       .SetRole(roleClaims.First().Value.ToString())
                       .SetUser(user.Identity.Name.ToString())
                       .Build();


                _logger.LogWarning("{Details}|{StatusCode}|{MethodType}|{User}|{Role}|{Success}{Failed}|{ErrorMessage}",
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

            if(orders == null)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Something went wrong while getting orders!");


                _loggingModelBuilder
                       .SetFailed()
                       .SetDetails($"{nameof(CustomersController)}/{nameof(GetOrdersByCustomer)}")
                       .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                       .SetMethodType("GET")
                       .SetErrorMessage("Something went wrong while getting orders!")
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
                       .SetDetails($"{nameof(CustomersController)}/{nameof(GetOrdersByCustomer)}")
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

            return Ok(ordersResponse);
        } 
    }
}
