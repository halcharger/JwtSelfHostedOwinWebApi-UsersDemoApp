using System.Collections.Generic;
using service.Domain;

namespace service
{
    public struct UserValidator
    {
        public List<Error> ValidateUser(User user)
        {
            return new List<Error>();
        }
    }
}