using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Optimizer
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
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

//            JobsController.Jobs = new List<Job>
//            {
//                new Job { Id = "a", RepaymentAmount = 1000, Done = false, MortgageDefinition = new Mortgage { AmountBorrowed = 1000, FixRate = 0.05M, IsRateFixed = true, RepaymentLenghtInMonths = 240, StartMonth = 3 } },
//                new Job { Id = "b", RepaymentAmount = 300, Done = false, MortgageDefinition = new Mortgage { AmountBorrowed = 1000, IsRateFixed = false, RepaymentLenghtInMonths = 120, StartMonth = 10 } },
//            };

        }
    }
}
