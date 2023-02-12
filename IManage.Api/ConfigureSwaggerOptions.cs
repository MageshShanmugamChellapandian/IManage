using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IManage.Api
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        #region Private Fields

        /// <summary>
        /// Readonly instance of ApiVersionDescriptionProvider.
        /// </summary>
        readonly IApiVersionDescriptionProvider provider;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        #endregion

        #region Public Methods

        /// <summary>
        /// Configure method.
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                             Reference = new OpenApiReference
                                 {
                                      Type = ReferenceType.SecurityScheme,
                                      Id = "Bearer"
                                 },
                             Name = "Bearer",
                             In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
        }
        /// <summary>
        /// Swagger configurations.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            const string wikiLink = "https://wiki.siemens.com/x/yYZrDQ";
            var info = new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = "IManage Project API",
                Description = "IManage Project API specification",
                Contact = new OpenApiContact
                {
                    Name = "OData implementation details",
                    Url = new Uri(wikiLink),
                }

            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }

        #endregion
    }
}
