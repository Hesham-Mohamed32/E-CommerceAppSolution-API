using DomainLayer.Models;
using Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecifications:BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecifications(ProductSpecificationParameters parameters) :
            base(P=>(!parameters.TypeId.HasValue || parameters.TypeId == P.TypeId)&&
                    (!parameters.BrandId.HasValue || parameters .BrandId== P.BrandId)&&
                     (string.IsNullOrEmpty(parameters.Search)||P.Name.ToLower().Contains(parameters.Search.ToLower()))                                                                  )

        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
            switch (parameters.sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                        break;
                    case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P=>P.Price);
                    break;
                    case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;
                    default:
                    break;
            }
            ApplyPagination(parameters.PageSize, parameters.PageIndex);
        }
        
        public ProductWithBrandAndTypeSpecifications(int id):base(P=>P.Id==id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
