﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using NorthwindBasedWebAPI.Repositories.IRepository;
using NorthwindBasedWebAPI.Models.Common;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Models.Dtos.TerritoryDtos;
using NorthwindBasedWebAPI.Models.Dtos.RegionDtos;
using NorthwindBasedWebAPI.Models.Dtos.EmployeeDtos;

namespace NorthwindBasedWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TerritoriesController : ControllerBase
    {
        private readonly ITerritoryRepository _territoryRepository;
        private ApiResponse _response;
        private readonly IMapper _mapper;
        private readonly ILogger<TerritoriesController> _logger;
        private readonly LoggingModelBuilder _loggingModelBuilder;

        public TerritoriesController(ITerritoryRepository territoryRepository, IMapper mapper,
            ILogger<TerritoriesController> logger)
        {
            _territoryRepository = territoryRepository;
            _response = new();
            _mapper = mapper;
            _logger = logger;
            _loggingModelBuilder = new();
        }




        [HttpGet]
        //[Authorize(Roles = "Admin,Customer")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> GetTerritories()
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
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetTerritories)}")
                 .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                 .SetMethodType("GET")
                 .SetRole(roleClaims.First().Value.ToString())
                 .SetUser(user.Identity.Name.ToString())
                 .SetErrorMessage("The model state is invalid!")
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

            var territoriesModel = await _territoryRepository.GetAllAsync(tracked: false);

            if (territoriesModel == null)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Something went wrong while getting territories!");

                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetTerritories)}")
                 .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                 .SetMethodType("GET")
                 .SetErrorMessage("Something went wrong while getting territories!")
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

            var territoriesResponse = _mapper.Map<List<ReadTerritoryDto>>(territoriesModel);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.data = territoriesResponse;

            _loggingModelBuilder
                 .SetSuccess()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetTerritories)}")
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


        [HttpGet]
        [Route("{id}")]
        //[Authorize(Roles = "Admin,Customer")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> GetTerritory(int id)
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
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetTerritory)}")
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
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetTerritory)}")
                 .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                 .SetMethodType("GET")
                 .SetErrorMessage("The given id is invalid")
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

            if (!await _territoryRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("No shipper found with given id!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetTerritory)}")
                 .SetStatusCode(HttpStatusCode.NotFound.ToString())
                 .SetMethodType("GET")
                 .SetErrorMessage("No shipper found with given id!")
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

            var territoryModel = await _territoryRepository.GetAsync(i => i.Id == id, tracked: false);

            if (territoryModel == null)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while getting the territory!");
                _response.IsSuccess = false;

                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetTerritory)}")
                 .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                 .SetMethodType("GET")
                 .SetErrorMessage("Something went wrong while getting the territory!")
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
            }

            var territoryResponse = _mapper.Map<ReadTerritoryDto>(territoryModel);

            _response.StatusCode = HttpStatusCode.OK;
            _response.data = territoryResponse;
            _response.IsSuccess = true;

            _loggingModelBuilder
                 .SetSuccess()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetTerritory)}")
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
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> CreateTerritory(
            [FromQuery] int regionId, [FromBody] CreateTerritoryDto createTerritoryDto)
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
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(CreateTerritory)}")
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

            if (createTerritoryDto == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The content that send by user is empty!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(CreateTerritory)}")
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

            if (await _territoryRepository.IsExistAsync(cn => cn.TerritoryDescription.Trim() == createTerritoryDto.TerritoryDescription.Trim()))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The territory's description is exists, please choose another!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(CreateTerritory)}")
                 .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                 .SetMethodType("POST")
                 .SetErrorMessage("The territory's description is exists, please choose another!")
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

            var territoryModel = _mapper.Map<Territory>(createTerritoryDto);

            territoryModel.RegionId = regionId;


            var createdTerritory = await _territoryRepository.CreateAsync(territoryModel);


            if (!createdTerritory)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while creating territory!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(CreateTerritory)}")
                 .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                 .SetMethodType("POST")
                 .SetErrorMessage("Something went wrong while creating territory!")
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
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(CreateTerritory)}")
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
        public async Task<ActionResult<ApiResponse>> UpdateTerritory(int id,
            [FromQuery] int regionId, [FromBody] UpdateTerritoryDto updateTerritoryDto)
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
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(UpdateTerritory)}")
                 .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
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

            if (id <= 0 || updateTerritoryDto.Id <= 0)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The given id is invalid!");
                _response.IsSuccess = false;


                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(UpdateTerritory)}")
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

            if (id != updateTerritoryDto.Id)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("No matching with given ids");
                _response.IsSuccess = false;


                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(UpdateTerritory)}")
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


            if (updateTerritoryDto == null)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The content of give model is empty");
                _response.IsSuccess = false;


                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(UpdateTerritory)}")
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

            if (!await _territoryRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("No territory found with the given Id");
                _response.IsSuccess = false;

                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(UpdateTerritory)}")
                 .SetStatusCode(HttpStatusCode.NotFound.ToString())
                 .SetMethodType("PUT")
                 .SetErrorMessage("No territory found with the given Id")
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

            var territoryModel = _mapper.Map<Territory>(updateTerritoryDto);

            territoryModel.RegionId = regionId;


            var updatedTerritory = await _territoryRepository.UpdateAsync(territoryModel);

            if (!updatedTerritory)
            {

                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while updating the territory!");

                _response.IsSuccess = false;

                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(UpdateTerritory)}")
                 .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                 .SetMethodType("PUT")
                 .SetErrorMessage("Something went wrong while updating the territory!")
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
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(UpdateTerritory)}")
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
        public async Task<ActionResult<ApiResponse>> DeleteTerritory(int id)
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
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(DeleteTerritory)}")
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
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(DeleteTerritory)}")
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

            if (!await _territoryRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {

                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("The territory with given id is not found!");
                _response.IsSuccess = false;

                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(DeleteTerritory)}")
                 .SetStatusCode(HttpStatusCode.NotFound.ToString())
                 .SetMethodType("DELETE")
                 .SetErrorMessage("The territory with given id is not found!")
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

            var territoryModel = await _territoryRepository.GetAsync(i => i.Id == id, tracked: false);

            if (territoryModel == null)
            {

                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while getting the territory model");
                _response.IsSuccess = false;

                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(DeleteTerritory)}")
                 .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                 .SetMethodType("DELETE")
                 .SetErrorMessage("Something went wrong while getting the territory model")
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

            var deletedTerritory = await _territoryRepository.DeleteAsync(territoryModel);

            if (!deletedTerritory)
            {

                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while deleting the territory model");
                _response.IsSuccess = false;


                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(DeleteTerritory)}")
                 .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                 .SetMethodType("DELETE")
                 .SetErrorMessage("Something went wrong while getting the territory model")
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
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(DeleteTerritory)}")
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
        [Route("{id}/Employees")]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> GetEmployeesByTerritory(int id)
        {

            List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role.ToString()).ToList();
            ClaimsPrincipal user = this.User;


            if (id <= 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The given id is invalid!");
                _response.IsSuccess = false;

                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetEmployeesByTerritory)}")
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

            if(!await _territoryRepository.IsExistAsync(i => i.Id == id, tracked:false))
            {
                _response.ErrorMessages.Add("The territory with given id is not found!");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;

                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetEmployeesByTerritory)}")
                 .SetStatusCode(HttpStatusCode.NotFound.ToString())
                 .SetMethodType("GET")
                 .SetErrorMessage("The territory with given id is not found!")
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

            if (!ModelState.IsValid)
            {
                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetEmployeesByTerritory)}")
                 .SetStatusCode(HttpStatusCode.BadRequest.ToString())
                 .SetMethodType("GET")
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

                return BadRequest(ModelState);
            }

            var employees = await _territoryRepository.GetEmployeesByTerritoryAsync(id);

            if(employees == null)
            {
                _response.ErrorMessages.Add("Something went error while getting the employees of the territory");
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;

                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetEmployeesByTerritory)}")
                 .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                 .SetMethodType("GET")
                 .SetErrorMessage("Something went error while getting the employees of the territory")
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
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetEmployeesByTerritory)}")
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
        [Route("{id:int}/Region")]
        //[Authorize(Roles = "Admin,Customer")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> GetRegionByTerritory(int id)
        {

            List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role.ToString()).ToList();
            ClaimsPrincipal user = this.User;


            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("The given id is invalid!");
                _response.StatusCode = HttpStatusCode.BadRequest;

                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetRegionByTerritory)}")
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

            if(!await _territoryRepository.IsExistAsync(i => i.Id == id, tracked: false))
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("The territory with given id is not found!");

                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetRegionByTerritory)}")
                 .SetStatusCode(HttpStatusCode.NotFound.ToString())
                 .SetMethodType("GET")
                 .SetErrorMessage("The territory with given id is not found!")
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

            if (!ModelState.IsValid)
            {



                return BadRequest(_response);
            }

            var region = await _territoryRepository.GetRegionByTerritoryAsync(id);

            if(region == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add("Something went wrong while getting the region of the territory!");


                _loggingModelBuilder
                 .SetFailed()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetRegionByTerritory)}")
                 .SetStatusCode(HttpStatusCode.InternalServerError.ToString())
                 .SetMethodType("GET")
                 .SetErrorMessage("Something went wrong while getting the region of the territory!")
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

            var regionResponse = _mapper.Map<ReadRegionDto>(region);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.data = regionResponse;

            _loggingModelBuilder
                 .SetSuccess()
                 .SetDetails($"{nameof(TerritoriesController)}/{nameof(GetRegionByTerritory)}")
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
