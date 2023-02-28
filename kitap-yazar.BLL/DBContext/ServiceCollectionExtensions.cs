using kitap_yazar.DOMAIN.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kitap_yazar.BLL.DBContext
{
    public static class ServiceCollectionExtensions
    {
        public static void AddKitapYazarDbContext(this IServiceCollection services)
        {
            services.AddDbContext<KitapYazarDatabaseContext>();
        }
    }
}
