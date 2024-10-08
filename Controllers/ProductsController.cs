﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using NorthwindBasedWebAPI.Repositories.IRepository;
using NorthwindBasedWebAPI.Models.Common;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Models.Dtos.ProductDtos;
using NorthwindBasedWebAPI.Models.Dtos.SupplierDtos;
using NorthwindBasedWebAPI.Models.Dtos.CategoryDtos;
using NorthwindBasedWebAPI.Models.Dtos.OrderDtos;

namespace NorthwindBasedWebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private ApiResponse _response;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;
        private readonly LoggingModelBuilder _loggingModelBuilder;

        public ProductsController(IProductRepository productRepository, IMapper mapper,
            ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _response = new();
            _mapper = mapper;
            _logger = logger;
            _loggingModelBuilder = new();
        }




        [HttpGet]
        //[Authorize(Roles = "Admin,Customer")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> GetProducts()
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
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetProducts)}")
                   .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("The model state is invalid!")
                   .SetRole("")
                   .SetUser("")
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

            var productsModel = await _productRepository.GetAllAsync(tracked: false);

            if (productsModel == null)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Something went wrong while getting the products");


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetProducts)}")
                   .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("Something went wrong while getting the products")
                   .SetRole("")
                   .SetUser("")
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

            var productsResponse = _mapper.Map<List<ReadProductDto>>(productsModel);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.data = productsResponse;

            _loggingModelBuilder
                   .SetSuccess()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetProducts)}")
                   .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                   .SetMethodType("GET")
                   .SetRole("")
                   .SetUser("")
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


        [HttpGet("{id:int}")]
        //[Authorize(Roles = "Admin,Customer")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> GetProduct(int id)
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
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetProduct)}")
                   .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("The model states is invalid")
                   .SetRole("")
                   .SetUser("")
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
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetProduct)}")
                   .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("The given id is invalid")
                   .SetRole("")
                   .SetUser("")
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

            if (!await _productRepository.IsExistAsync(i => i.Id == id))
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("No product found with given id!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetProduct)}")
                   .SetStatusCode(HttpStatusCode.NotFound.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("No product found with given id!")
                   .SetRole("")
                   .SetUser("")
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

            var productModel = await _productRepository.GetAsync(i => i.Id == id, tracked: false);

            if (productModel == null)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while getting the product!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetProduct)}")
                   .SetStatusCode(HttpStatusCode.NotFound.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("Something went wrong while getting the product")
                   .SetRole("")
                   .SetUser("")
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

            var productResponse = _mapper.Map<ReadProductDto>(productModel);

            _response.StatusCode = HttpStatusCode.OK;
            _response.data = productResponse;
            _response.IsSuccess = true;


            _loggingModelBuilder
                   .SetSuccess()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetProduct)}")
                   .SetStatusCode(HttpStatusCode.OK.ToString())
                   .SetMethodType("GET")
                   .SetRole("")
                   .SetUser("")
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
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> CreateProduct([FromQuery]int categoryId,
            [FromQuery]int supplierId, [FromBody]CreateProductDto createProductDto)
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
                   .SetDetails($"{nameof(ProductsController)}/{nameof(CreateProduct)}")
                   .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                   .SetMethodType("POST")
                   .SetErrorMessage("The model states is invalid!")
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

            if (createProductDto == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The content that send by user is empty!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(CreateProduct)}")
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

            if (await _productRepository.IsExistAsync(cn => cn.ProductName.Trim() == createProductDto.ProductName.Trim()))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The product name is exists, please choose another!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(CreateProduct)}")
                   .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                   .SetMethodType("POST")
                   .SetErrorMessage("The product name is exists, please choose another!")
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

            var productModel = _mapper.Map<Product>(createProductDto);

            productModel.CategoryId = categoryId;
            productModel.SupplierId = supplierId;


            var createdProduct = await _productRepository.CreateAsync(productModel);


            if (!createdProduct)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while creating the product!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(CreateProduct)}")
                   .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                   .SetMethodType("POST")
                   .SetErrorMessage("Something went wrong while creating the product!")
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
                   .SetDetails($"{nameof(ProductsController)}/{nameof(CreateProduct)}")
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
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> UpdateProduct(int id, 
            [FromQuery]int categoryId, [FromQuery] int supplierId, UpdateProductDto updateProductDto)
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
                   .SetDetails($"{nameof(ProductsController)}/{nameof(UpdateProduct)}")
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

            if (id <= 0 || updateProductDto.Id <= 0)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The given id is invalid!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(UpdateProduct)}")
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

            if (id != updateProductDto.Id)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("No matching with given ids");
                _response.IsSuccess = false;

                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(UpdateProduct)}")
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


            if (updateProductDto == null)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The content of give model is empty");
                _response.IsSuccess = false;


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(UpdateProduct)}")
                   .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                   .SetMethodType("PUT")
                   .SetErrorMessage("The content of give model is empty")
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

            if (!await _productRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("No product found with the given Id");
                _response.IsSuccess = false;

                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(UpdateProduct)}")
                   .SetStatusCode(HttpStatusCode.NotFound.ToString())
                   .SetMethodType("PUT")
                   .SetErrorMessage("No product found with the given Id")
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

            var productModel = _mapper.Map<Product>(updateProductDto);

            productModel.SupplierId = supplierId;
            productModel.CategoryId = categoryId;


            var updatedProduct = await _productRepository.UpdateAsync(productModel);

            if (!updatedProduct)
            {

                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while updating the product");
                _response.IsSuccess = false;

                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(UpdateProduct)}")
                   .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                   .SetMethodType("PUT")
                   .SetErrorMessage("Something went wrong while updating the product")
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
                   .SetDetails($"{nameof(ProductsController)}/{nameof(UpdateProduct)}")
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
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> DeleteProduct(int id)
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
                   .SetDetails($"{nameof(ProductsController)}/{nameof(DeleteProduct)}")
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
                   .SetDetails($"{nameof(ProductsController)}/{nameof(DeleteProduct)}")
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

            if (!await _productRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {

                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("The product with given id is not found!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(DeleteProduct)}")
                   .SetStatusCode(HttpStatusCode.NotFound.ToString())
                   .SetMethodType("DELETE")
                   .SetErrorMessage("The product with given id is not found!")
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

            var productModel = await _productRepository.GetAsync(i => i.Id == id, tracked: false);

            if (productModel == null)
            {

                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while getting the product!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(DeleteProduct)}")
                   .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                   .SetMethodType("DELETE")
                   .SetErrorMessage("Something went wrong while getting the product!")
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

            var deletedProduct = await _productRepository.DeleteAsync(productModel);

            if (!deletedProduct)
            {

                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while getting the product!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(DeleteProduct)}")
                   .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                   .SetMethodType("DELETE")
                   .SetErrorMessage("Something went wrong while getting the product!")
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
                   .SetDetails($"{nameof(ProductsController)}/{nameof(DeleteProduct)}")
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
        [Route("{id}/Category")]
        //[Authorize(Roles = "Admin,Customer")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> GetCategoryByProduct(int id)
        {

            List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role.ToString()).ToList();
            ClaimsPrincipal user = this.User;

            if (id <= 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("The given id is invalid!");


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetCategoryByProduct)}")
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

            if(!await _productRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("The product with given id is not found!");


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetCategoryByProduct)}")
                   .SetStatusCode(HttpStatusCode.NotFound.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("The product with given id is not found!")
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

            var category = await _productRepository.GetCategoryByProductAsync(id);

            if(category == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while getting the category");

                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetCategoryByProduct)}")
                   .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("Something went wrong while getting the category")
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

            var categoryResponse = _mapper.Map<ReadCategoryDto>(category);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.data = categoryResponse;


            _loggingModelBuilder
                   .SetSuccess()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetCategoryByProduct)}")
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
        [Route("{id}/Supplier")]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> GetSupplierByProduct(int id)
        {

            List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role.ToString()).ToList();
            ClaimsPrincipal user = this.User;


            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The given id is invalid!");

                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetSupplierByProduct)}")
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

            if(!await _productRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("The product with given id is not found!");


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetSupplierByProduct)}")
                   .SetStatusCode(HttpStatusCode.NotFound.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("The product with given id is not found!")
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

            var supplier = await _productRepository.GetSupplierByProductAsync(id);

            if(supplier == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while getting the supplier of the product");


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetSupplierByProduct)}")
                   .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("Something went wrong while getting the supplier of the product")
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

            var supplierResponse = _mapper.Map<ReadSupplierDto>(supplier);

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.data = supplierResponse;


            _loggingModelBuilder
                   .SetSuccess()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetSupplierByProduct)}")
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
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> GetOrdersByProduct(int id)
        {

            List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role.ToString()).ToList();
            ClaimsPrincipal user = this.User;


            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The given id is invalid!");


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetOrdersByProduct)}")
                   .SetStatusCode(HttpStatusCode.NotFound.ToString())
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

            if(!await _productRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("The product with given id is not found!");

                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetOrdersByProduct)}")
                   .SetStatusCode(HttpStatusCode.NotFound.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("The product with given id is not found!")
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

            var orders = await _productRepository.GetOrdersByProductAsync(id);

            if(orders == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while getting the orders of this products!");


                _loggingModelBuilder
                   .SetFailed()
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetOrdersByProduct)}")
                   .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                   .SetMethodType("GET")
                   .SetErrorMessage("Something went wrong while getting the orders of this products!")
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
                   .SetDetails($"{nameof(ProductsController)}/{nameof(GetOrdersByProduct)}")
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
