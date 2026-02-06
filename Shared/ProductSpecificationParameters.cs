using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpecificationParameters
    {
        private const int deafultPageSize = 5;
        private const int MaxPageSize = 10;
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptions sort {  get; set; }
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        private int  _pageSize=deafultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value>MaxPageSize?MaxPageSize:value; }
        }
    }
}
