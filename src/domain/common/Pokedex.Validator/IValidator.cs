using System;
using System.Collections.Generic;
using System.Text;
using ApplicationException = Pokedex.Common.Exceptions.ApplicationException;

namespace Pokedex.Validator
{
    public interface IValidator
    {
        (int, ApplicationException) PayloadValidator(string accessToken, string name);
    }
}
