using Corporation.BLL.Common.Services.Attachments;
using Corporation.BLL.Services.Departments;
using Corporation.BLL.Services.Employees;
using Corporation.DAL.Persistence.Data;
using Corporation.DAL.Persistence.Repositories.Departments;
using Corporation.DAL.Persistence.Repositories.Employees;
using Corporation.DAL.Persistence.UintOfWork;
using Corporation.PL.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Corporation.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Confguration Services

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
            {
                optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddTransient<IAttachmentService, AttachmentService>();

            builder.Services.AddAutoMapper(M => M.AddProfile<MappingProfile>());
            /// builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));
            /// builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
            /// builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            var app = builder.Build();

            #region Confguration Kestrel Middlewares

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion

            app.Run();
        }
    }
}
