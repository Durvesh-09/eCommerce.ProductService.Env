using eCommerce.BusinessLogicLayer.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.ProductsService.BusinessLogicLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            // To Do : Add Business logic layer sevices into the IoC Container
            services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);
            return services;
        }
    }
}
