using Microsoft.AspNetCore.Http;
using Pokedex.Common.Exceptions;
using Pokedex.DataObjects.Settings;
using Pokedex.Security.Handlers;
using ApplicationException = Pokedex.Common.Exceptions.ApplicationException;

namespace Pokedex.Validator
{
    public class PayloadValidator : IValidator
    {
        private readonly AppSettings _appSettings;
        private readonly IJwtTokenHandler _jwtTokenHandler;

        public PayloadValidator(AppSettings appSettings, IJwtTokenHandler jwtTokenHandler)
        {
            _appSettings = appSettings;
            _jwtTokenHandler = jwtTokenHandler;
        }

        (int, ApplicationException) IValidator.PayloadValidator(string accessToken, string name)
        {
            int statusCode = StatusCodes.Status200OK;
            ApplicationException result = null;

            if (_appSettings.JsonWebTokens.IsEnabled)
            {
                var token = _jwtTokenHandler.PrepareTokenFromAccessToekn(accessToken);
                if (string.IsNullOrEmpty(token))
                {
                    statusCode = StatusCodes.Status401Unauthorized;
                    result = new ApplicationException { ErrorCode = ApplicationErrorCodes.InvalidToken, Data = new ErrorData() { Field = "Token", Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidToken) } };
                    return (statusCode, result);
                }
                var (isVerified, userId) = _jwtTokenHandler.VerifyJwtSecurityToken(token);
                if ((!isVerified) || string.IsNullOrEmpty(userId))
                {
                    statusCode = StatusCodes.Status401Unauthorized;
                    result = new ApplicationException { ErrorCode = ApplicationErrorCodes.InvalidToken, Data = new ErrorData() { Field = "Token", Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidToken) } };
                    return (statusCode, result);
                }
            }
            if (string.IsNullOrEmpty(name))
            {
                statusCode = StatusCodes.Status422UnprocessableEntity;
                result = new ApplicationException { ErrorCode = ApplicationErrorCodes.EmptyName, Data = new ErrorData() { Field = "pokemon name", Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyName) } };
            }
            return (statusCode, result);
        }
    }
}
