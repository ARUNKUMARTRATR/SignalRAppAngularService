namespace SignalRAppAngular
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Hosting;
    using SignalRAppAngular.Data;
    using SignalRAppAngular.Hubs;

    /// <summary>
    /// Defines the <see cref="Startup" />.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// The ConfigureServices.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("CorsPolicy",
             builder =>
             {
                 builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials();
             }));
            var cs = Configuration.GetConnectionString("BotConnection");
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(cs));
            services.AddScoped<IChatbotMasterRepo, ChatBotMasterRepo>();

            //var sqlConBuilder = new SqlConnectionStringBuilder();
            //sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("SqlDbConnection");
            //sqlConBuilder.UserID = builder.Configuration["UserId"];
            //sqlConBuilder.Password = builder.Configuration["Password"];
            //builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));
            //builder.Services.AddScoped<IChatbotMasterRepo, ChatBotMasterRepo>();
            //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers();
            services.AddSignalR();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// The Configure.
        /// </summary>
        /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
        /// <param name="env">The env<see cref="IWebHostEnvironment"/>.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/hub");
            });

            var fileRootPath = this.Configuration.GetValue<string>("ROOT_PATH");
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(fileRootPath),
                RequestPath = new PathString(""),
                EnableDirectoryBrowsing = false,
            });

        }
    }
}
