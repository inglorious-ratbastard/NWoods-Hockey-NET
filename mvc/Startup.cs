using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace MvcSample {
  public class Startup {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) {
      this.Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services) {
      services.AddDataProtection()
        .PersistKeysToFileSystem(
            new DirectoryInfo(
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "DataProtectionKeys")))
        .SetApplicationName("MvcSample");
      
      services.AddDefaultIdentity<IdentityUser>();

      services.AddSingleton<IUserStore<IdentityUser>>(MemoryUserStore.Instance);

      if (Environment.GetEnvironmentVariable("REPLIT_SUPPORT") == "1") {
        Console.Error.WriteLine("Enabling Replit.com IFrame Support.");
        EnableReplitIFrameHosting(services);
      }

      services.AddControllersWithViews();
      services.AddRazorPages();
    }

    private static void EnableReplitIFrameHosting(IServiceCollection services) {
      services.ConfigureApplicationCookie(options => {
        options.Cookie.SameSite = SameSiteMode.None;
      });

      services.Configure<ForwardedHeadersOptions>(options => {
        options.ForwardedHeaders =
          ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

        options.KnownNetworks.Clear();
        options.KnownProxies.Clear();
      });

      services.AddAntiforgery(options => {
        options.SuppressXFrameOptionsHeader = true;
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      app.UseForwardedHeaders();

      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      } else {
        app.UseExceptionHandler("/Home/Error");
      }

      // app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapRazorPages();
      });
    }
  }
}
