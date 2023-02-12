using FluentValidation;
using IManage.Api.V1.ApiModels.Request;
using Microsoft.Extensions.Localization;

namespace IManage.Api.V1.Validators
{
    /// <summary>
    /// Validator class for ApiTokenExchangeReq model.
    /// </summary>
    public class ApiTokenExchangeReqValidator : AbstractValidator<ApiTokenExchangeReq>
    {
        #region Fields

        //Grant type
        private readonly string _grantType = "urn:ietf:params:oauth:grant-type:token-exchange";

        //Token type for the subject token
        private readonly string _tokenType = "urn:ietf:params:oauth:token-type:access_token";

        #endregion

        #region Constructor

        /// <summary>
        /// Initialise an instance for ApiTokenExchangeReqValidator.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/></param>
        public ApiTokenExchangeReqValidator(IStringLocalizer<ApiTokenExchangeReqValidator> localizer)
        {
            //Ruleset for the new token

            RuleSet("New_Token", () =>
            {
                RuleFor(item => item.GrantType)
                     .NotNull().WithMessage(localizer["grant type field is missing"].Value)
                     .NotEmpty().WithMessage(localizer["grant type field can not be empty"].Value)
                     .Equal(_grantType).WithMessage(localizer["Invalid grant type"].Value);

                RuleFor(item => item.SubjectToken)
                    .NotNull().WithMessage(localizer["subject token field is missing"].Value)
                    .NotEmpty().WithMessage(localizer["subject token field can not be empty"].Value);

                RuleFor(item => item.SubjectTokenType)
                    .NotNull().WithMessage(localizer["subject token type field is missing"].Value)
                    .NotEmpty().WithMessage(localizer["subject token type field can not be empty"].Value)
                    .Equal(_tokenType).WithMessage(localizer["Invalid token type"].Value);
            });
        }
        #endregion
    }
}
