﻿using  ConceptArchitect.BookManagement;

using  BooksWeb02.Extensions;

namespace BooksWeb02
{ 
    public static class Startup
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            
            services.AddControllersWithViews();
            services.AddSession();
            services.AddAdoBMSRepository();

            services.AddSingleton<IAuthorService, PersistentAuthorService>();

            services.AddSingleton<IBookService, PersistentBookService>();
            services.AddSingleton<IUserService, PersistentUserService>();
            services.AddSingleton<IReviewService, PersistentReviewService>();

            return services;
        }

        public static IApplicationBuilder ConfigureMiddlewares(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            return app;
        }
            
    }
}
