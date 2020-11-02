using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Server.Exceptions;
using Server.Middlewares;
using Server.Models.VO;
using Server.Services;
using Server.Services.Implements;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // 配置 JWT
            services.Configure<TokenManagementModel>(Configuration.GetSection("Authentication"));
            var token = Configuration.GetSection("Authentication").Get<TokenManagementModel>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    ValidIssuer = token.Issuer,
                    ValidAudience = token.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                options.Filters.Add<ApiResultFilter>();
            });

            // 配置数据库
            services.Configure<DatabaseManagementModel>(Configuration.GetSection("Database"));
            var databaseConfig = Configuration.GetSection("Database").Get<DatabaseManagementModel>();

            // 配置数据库
            services.Configure<TencentCosManagementModel>(Configuration.GetSection("TencentCos"));

            // 容器注册
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IUserService, UserService>();

            IDatabaseService databaseService = null;
            switch (databaseConfig.Type.ToLower())
            {
                case "postgresql":
                case "pgsql":
                    services.AddScoped<IDatabaseService, PostgreSqlDataBaseService>();
                    databaseService = new PostgreSqlDataBaseService(databaseConfig);
                    break;

                case "mariadb":
                case "mysql":
                    services.AddScoped<IDatabaseService, MySqlDataBaseService> ();
                    databaseService = new MySqlDataBaseService(databaseConfig);
                    break;

                default :
                    throw new InvalidArgumentException();
            }

            // 数据库初始化
            databaseService.Database.Migrate();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<AuthenticateWithHeader>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
