
using EntityFrameworkCore.UnitOfWork.Extensions;
using interfaces.DataBase;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace infrastructure.DataBase
{
    public static class RegisterServicesExtend
    {

        public static void RegisterDataBase(this IServiceCollection services,
          IConfiguration configuration, string keyConnectionString, int timeout = 15)
        {
            if (services == null || configuration == null || string.IsNullOrEmpty(keyConnectionString))
                return;

            var cnn = configuration[keyConnectionString];

            // Crear la cadena de conexión para SQL Server
            var builder = new SqlConnectionStringBuilder(cnn)
            {
                ConnectTimeout = timeout, // Tiempo de espera de conexión 
                TrustServerCertificate = true
            };

            // Crear la cadena de conexión completa
            cnn = builder.ConnectionString;

            services.AddDbContext<DemoControlContext>(options => options.UseSqlServer(cnn));

            services.AddUnitOfWork();

            services.AddScoped<DbContext, DemoControlContext>();
            services.AddScoped<IMyUnitOfWork, MyUnitOfWork>();

            //   services.AddDbContext<TuDbContext>(options =>
            //options.UseSqlServer(configuration.GetConnectionString(cnn)));


        }
    }
}
