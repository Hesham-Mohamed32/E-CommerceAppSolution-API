using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class ProductCountSpecification:BaseSpecifications<Product,int>
    {
        public ProductCountSpecification(ProductSpecificationParameters parameters) :
            base(P => (!parameters.TypeId.HasValue || parameters.TypeId == P.TypeId) &&
                    (!parameters.BrandId.HasValue || parameters.BrandId == P.BrandId) &&
                     (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search.ToLower())))
        {

        }
    }
}
