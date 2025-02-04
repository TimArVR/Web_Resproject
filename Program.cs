namespace Web_siteResume
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<Web_siteResume.BL.Auth.IAuth, Web_siteResume.BL.Auth.Auth>();
            builder.Services.AddSingleton<Web_siteResume.BL.Auth.IEncrypt, Web_siteResume.BL.Auth.Encrypt>();
            builder.Services.AddScoped<Web_siteResume.BL.Auth.ICurrentUser, Web_siteResume.BL.Auth.CurrentUser>();//��������� ��� ������
            builder.Services.AddSingleton<Web_siteResume.DAL.IAuthDAL, Web_siteResume.DAL.AuthDAL>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSingleton<Web_siteResume.DAL.IDbSessionDAL, Web_siteResume.DAL.DbSessionDAL>();
            builder.Services.AddScoped<Web_siteResume.BL.Auth.IDbSession, Web_siteResume.BL.Auth.DbSession>();//��������� DI ��������� ������ ������


            builder.Services.AddMvc()/*.AddSessionStateTempDataProvider()*/;
            //builder.Services.AddSession();
            
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            //app.UseSession();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
