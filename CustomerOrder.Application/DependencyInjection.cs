using CustomerOrder.Application.Interfaces;
using CustomerOrder.Application.Services;
using CustomerOrder.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerOrder.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IDeliveryBinService, DeliveryBinService>();
            var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CustomerOrderContext>(options =>
               options.UseSqlServer(defaultConnectionString));

            return services;
        }
    }
}