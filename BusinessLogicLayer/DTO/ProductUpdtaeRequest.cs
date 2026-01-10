using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public record ProductUpdtaeRequest(Guid ProductId,string ProductName,CategoryOptions Category, double? UnitPrice, int? QuantityInStock)
    {
        public ProductUpdtaeRequest() : this(default, default, default, default, default)
        { 
        }
    }
}
