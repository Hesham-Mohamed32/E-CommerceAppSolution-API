using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models;
using Services.Abstraction;
using Services.Specifications;
using Shared;
using Shared.DTOS.ProductModule;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class ProductService (IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        
        public async Task<PaginatedResult<ProductDto>> GetAllProductAsync(ProductSpecificationParameters parameters)
        {
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            var specifications = new ProductWithBrandAndTypeSpecifications(parameters);
            var Products = await ProductRepo.GetAllAsync(specifications);
            var ProductsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            var PageSize = ProductsDto.Count();
            var CountSpecifications = new ProductCountSpecification(parameters);
            var TotalCount = await ProductRepo.CountAsync(CountSpecifications);
            return new PaginatedResult<ProductDto>(parameters.PageIndex, PageSize, TotalCount, ProductsDto);
        }
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync() 
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands= await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDto>>(Brands); 
            return BrandsDto;
        }

      

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types= await _unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            var TypesDto= _mapper.Map<IEnumerable<ProductType>,IEnumerable<TypeDto>>(Types);
            return TypesDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(id);
            var Product= await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(specifications);
            //var ProductDto=_mapper.Map<Product,ProductDto>(Product);
            //return ProductDto;
            return Product is null ? throw new ProductNotFoundException(id) : _mapper.Map<Product, ProductDto>(Product);
        }
    }
}
