using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Configurations
{
    public class PaginationParams
    {
        private const int maxPageSize = 50;
        private int pageSize;

        public int PageSize { get => pageSize; set => pageSize = value > maxPageSize ? maxPageSize : value; }
        public int PageIndex { get; set; }
    }
}
