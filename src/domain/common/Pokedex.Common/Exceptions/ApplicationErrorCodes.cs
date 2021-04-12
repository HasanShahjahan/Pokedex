using Pokedex.Common.Extensions;
using Pokedex.Common.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Common.Exceptions
{
    public sealed class ApplicationErrorCodes : StringEnum
    {
        public ApplicationErrorCodes(string value) : base(value)
        {
        }
        public const string EmptyName = "EMPTY_POKEMON_NAME";
        public const string InvalidToken = "INVALID_ACCESS_TOKEN";
        public const string DatabaseError = "DATABASE_CONFIGURATION_ERROR";
        public const string InternalSeverError = "INTERNAL_SERVER_ERROR";
        public const string Unauthorized = "UNAUTHORIZED";
        public const string NotFound = "NOT_FOUND";

        public static string GetMessage(string value)
        {
            switch (value)
            {
                case InvalidToken:
                    return ErrorMessage.InvalidToken;
                case EmptyName:
                    return ErrorMessage.EmptyName;
                case DatabaseError:
                    return ErrorMessage.DatabaseError;
                case InternalSeverError:
                    return ErrorMessage.InternalSeverError;
                case Unauthorized:
                    return ErrorMessage.Unauthorized;
                case NotFound:
                    return ErrorMessage.NotFound;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}
