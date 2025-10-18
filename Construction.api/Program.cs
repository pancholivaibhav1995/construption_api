using Construction.Core.Concrete;
using Construction.Core.Construct;
using Construction.Core.mapper;
using Construction.Entity.Models;
using Construction.Repository.Concrete;
using Construction.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Construction.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // 1. Add Authentication
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "https://localhost:7047/",
                        ValidAudience = "https://localhost:7047/",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });
            builder.Services.AddAuthorization();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAutoMapper(typeof(SiteMapper).Assembly);
            builder.Services.AddAutoMapper(typeof(OrganisationMapper).Assembly);
            builder.Services.AddDbContext<constructiondbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IRoleRepository, RoleRepository>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();
            builder.Services.AddTransient<ISiteService, SiteService>();
            builder.Services.AddTransient<ISiteRepository, SiteRepository>();
            builder.Services.AddTransient<IRoleService, RoleService>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();
            builder.Services.AddTransient<IOrganisationService, OrganisationService>();
            builder.Services.AddTransient<IOrganisationRepository, OrganisationRepository>();
            builder.Services.AddTransient<ITransactionService, TransactionService>();
            builder.Services.AddTransient<ITransactionServiceRepository , TransactionServiceRepository>();
            builder.Services.AddTransient<IDashboardService, DashboardService>();
            builder.Services.AddTransient<IDashboardRepository, DashboardRepository>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();
            builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
            builder.Services.AddScoped<IMaterialService, MaterialService>();
            builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();
            builder.Services.AddScoped<IReceiptService, ReceiptService>();

            // Register Page repository and service
            builder.Services.AddScoped<IPageRepository, PageRepository>();
            builder.Services.AddScoped<IPageService, PageService>();

            // Register RolePageMapping repository and service
            builder.Services.AddScoped<IRolePageMappingRepository, RolePageMappingRepository>();
            builder.Services.AddScoped<IRolePageMappingService, RolePageMappingService>();

            // Register LabourPayment repository and service
            builder.Services.AddScoped<ILabourPaymentRepository, LabourPaymentRepository>();
            builder.Services.AddScoped<ILabourPaymentService, LabourPaymentService>();

            // Register EmployeeAttendance repository and service
            builder.Services.AddScoped<IEmployeeAttendanceRepository, EmployeeAttendanceRepository>();
            builder.Services.AddScoped<IEmployeeAttendanceService, EmployeeAttendanceService>();

            // Register SiteTransaction repository and service
            builder.Services.AddScoped<ISiteTransactionRepository, SiteTransactionRepository>();
            builder.Services.AddScoped<ISiteTransactionService, SiteTransactionService>();

            // Register ExpenseCategory repository and service
            builder.Services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
            builder.Services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();

            // Site repository is already registered earlier; ensure it's present
            builder.Services.AddScoped<ISiteRepository, SiteRepository>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy => policy.WithOrigins("http://localhost:3000")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });

           
            var app = builder.Build();

            // UseRouting is required before UseCors/UseAuthentication/UseAuthorization when using endpoint routing
            app.UseRouting();

            app.UseCors("AllowFrontend");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
