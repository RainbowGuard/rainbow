using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Junkyard;

namespace Rainbow.Application
{
    public class Startup
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit <see href="https://go.microsoft.com/fwlink/?LinkID=398940"/>.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJunkyard("https://github.com/RainbowGuard/junkyard.git");
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
