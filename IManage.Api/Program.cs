using IManage.Api;
using IManage.Authentication;
using IManage.DomainServices.V1;
using IManage.Interfaces.V1.Repositories;
using IManage.Interfaces.V1.Services;
using IManage.Repositories.V1;
using IManage.Repositories.V1.DbContexts;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neo4j.Driver;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var a = builder.Configuration.GetConnectionString("IManage");

#region Add services to the container.

builder.Services.AddControllers();

#endregion

#region Localization

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var cultures = new List<CultureInfo> {
                    new CultureInfo("en-US"),
                    new CultureInfo("de-DE"),
                    new CultureInfo("zh-CN")
                };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SetDefaultCulture(cultures[0].ToString());
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

#endregion

#region Api Versioning

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = ApiVersion.Default;
    options.ApiVersionReader = new HeaderApiVersionReader("API-Version");
    options.ReportApiVersions = true;
});

builder.Services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                });

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
        // note: the specified format code will format the version as "'v'major[.minor][-status]"
        options.GroupNameFormat = "'v'VVV";

        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
        // can also be used to control the format of the API version in route templates
        options.SubstituteApiVersionInUrl = true;
    });
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

#region Register Services

ServiceRegistry(builder);

#endregion

builder.Services.AddDbContext<IManageContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IManage"));
});
builder.Services.AddScoped<IDriver>(s =>
{
    return GraphDatabase.Driver(new Uri(builder.Configuration.GetSection("Neo4jConfig:BoltUri")?.Value),
        AuthTokens.Basic(builder.Configuration.GetSection("Neo4jConfig:UserName")?.Value, builder.Configuration.GetSection("Neo4jConfig:Password")?.Value));
});

builder.Services.AddSwaggerGen(
        options =>
        {
            // add a custom operation filter which sets default values
            options.OperationFilter<SwaggerDefaultValues>();

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{typeof(IManage.Api.SwaggerDefaultValues).Assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

#endregion

#region Adding Cors

builder.Services.AddCors(p => p.AddPolicy("AllowOrigin", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyHeader();
}));

#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#region Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/api/error");

app.UseCors();

app.UseHttpsRedirection();

app.UseMiddleware<AuthenticationMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion


#region Internal methods


void ServiceRegistry(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<IUserService, UserService>();
    builder.Services.AddTransient<IUserRepository, UserRepository>();
    builder.Services.AddTransient<IRoleService, RoleService>();
    builder.Services.AddTransient<IRoleRepository, RoleRepository>();
    builder.Services.AddTransient<ITokenService, TokenService>();
    builder.Services.AddTransient<IProjectService, ProjectService>();
    builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
    builder.Services.AddTransient<IMenuService, MenuService>();
    builder.Services.AddTransient<IMenuRepository, MenuRepository>();
}

#endregion