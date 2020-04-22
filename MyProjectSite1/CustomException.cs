///*Author : Renelen Verceles
// * Date : 21-04-2020
// * Purpose : To create reusable custom exceptions to be used by different modules of the application
// * Contributors:

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProjectSite1
{
    public class CustomException
    {
        public class RangeException : Exception {

        }

        public class UserNotFoundException: Exception {
            public UserNotFoundException(string message) : base(message) {

            }
        }

        public class UserRegistrationFailedException : Exception {
            public UserRegistrationFailedException(string message) : base(message) {

            }
        }

        public class InvalidAccountException : Exception {
            public InvalidAccountException(string message) : base(message) {

            }
        }
    }
}