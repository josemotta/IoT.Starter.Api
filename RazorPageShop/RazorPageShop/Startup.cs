using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorPageShop.Core;
using Microsoft.AspNetCore.Http;
using React.AspNet;
using Microsoft.EntityFrameworkCore;

namespace RazorPageShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();
            //services.AddScoped<ICheckoutManager, CheckoutManager>();
            //services.AddReact();

            services.AddScoped<ICheckoutManager, CheckoutManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact();
            services.AddMvc();

            string connectionString =
                Configuration.GetSection("ConnectionStrings")
                    .GetValue<string>("Default");
            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseReact(config =>
            {
                config
                    .AddScript("~/lib/react-bootstrap/react-bootstrap.min.js")
                    .AddScript("~/js/Components.jsx")
                    .AddScript("~/js/Cart.jsx")
                    .AddScript("~/js/CheckoutSuccess.jsx")
                    .SetUseDebugReact(true);
            });
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            ICheckoutManager checkoutManager = serviceProvider
                .GetService<ICheckoutManager>();

            checkoutManager.InitializeDB();
        }
    }
}
