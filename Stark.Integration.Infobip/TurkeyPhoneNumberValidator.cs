using System;

namespace Stark.Integration.Infobip
{
    public class TurkeyPhoneNumberValidator : IPhoneNumberValidator
    {
        public bool IsValid(string number)
        {
            if (String.IsNullOrEmpty(number) || number.Length != 10)
            {
                return false;
            }

            return true;
        }
    }
}