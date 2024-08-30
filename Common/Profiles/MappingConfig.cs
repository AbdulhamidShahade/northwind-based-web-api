using AutoMapper;

using NorthwindBasedWebApplication.API.Models;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Models.Dtos.CategoryDtos;
using NorthwindBasedWebAPI.Models.Dtos.CustomerDtos;
using NorthwindBasedWebAPI.Models.Dtos.CustomerDemographicDtos;
using NorthwindBasedWebAPI.Models.Dtos.EmployeeDtos;
using NorthwindBasedWebAPI.Models.Dtos.OrderDtos;
using NorthwindBasedWebAPI.Models.Dtos.ProductDtos;
using NorthwindBasedWebAPI.Models.Dtos.RegionDtos;
using NorthwindBasedWebAPI.Models.Dtos.ShipperDtos;
using NorthwindBasedWebAPI.Models.Dtos.SupplierDtos;
using NorthwindBasedWebAPI.Models.Dtos.TerritoryDtos;

namespace NorthwindBasedWebAPI.Common.Profiles
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Category, ReadCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();


            CreateMap<Customer, ReadCustomerDto>().ReverseMap();
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();


            CreateMap<CustomerDemographic, ReadCustomerDemographicDto>().ReverseMap();
            CreateMap<CustomerDemographic, CreateCustomerDemographicDto>().ReverseMap();
            CreateMap<CustomerDemographic, UpdateCustomerDemographicDto>().ReverseMap();


            CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeDto>().ReverseMap();
            CreateMap<Employee, ReadEmployeeDto>().ReverseMap();


            CreateMap<Order, ReadOrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();



            CreateMap<Product, ReadProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();



            CreateMap<Region, ReadRegionDto>().ReverseMap();
            CreateMap<Region, CreateRegionDto>().ReverseMap();
            CreateMap<Region, UpdateRegionDto>().ReverseMap();



            CreateMap<Shipper, ReadShipperDto>().ReverseMap();
            CreateMap<Shipper, CreateShipperDto>().ReverseMap();
            CreateMap<Shipper, UpdateShipperDto>().ReverseMap();



            CreateMap<Supplier, ReadSupplierDto>().ReverseMap();
            CreateMap<Supplier, CreateSupplierDto>().ReverseMap();
            CreateMap<Supplier, UpdateSupplierDto>().ReverseMap();



            CreateMap<Territory, ReadTerritoryDto>().ReverseMap();
            CreateMap<Territory, UpdateTerritoryDto>().ReverseMap();
            CreateMap<Territory, CreateTerritoryDto>().ReverseMap();
        }
    }
}
