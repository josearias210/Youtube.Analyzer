using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Youtube.Analyzer.Models;
using Youtube.Analyzer.Services;
using Microsoft.Extensions.Options;
using Youtube.Analyzer.Application.UseCases.AnalyzerVideo;

namespace Youtube.Analyzer.Api
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton(resolver =>
                resolver.GetRequiredService<IOptions<AppSettings>>().Value);

            services.AddTransient<SearchVideoUseCase>();
            services.AddTransient<VideoAnalyzeUseCase>();
            services.AddTransient<IYoutubeService, YoutubeService>();
            services.AddTransient<IAnalyticsSentimentService, AzureAnalyticsSentimentService>();

            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, // I introduced a string constant just as a label "AllowAllOriginsPolicy"
                builder =>
                {
                    builder.AllowAnyOrigin();
                });
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseCors(builder =>
            {
                builder.WithOrigins("https://localhost:44370");
                //builder.WithOrigins("https://localhost:5001");
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });

            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
