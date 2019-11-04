using System.Text.RegularExpressions;

namespace System.ComponentModel.DataAnnotations
{
    
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class EmailOrPhoneAttribute : DataTypeAttribute
    {
        public EmailOrPhoneAttribute() : base(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$|^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}")
        {
            emailAttribute = new EmailAddressAttribute();
            phoneAttribute = new PhoneAttribute();
        }

        EmailAddressAttribute emailAttribute;
        PhoneAttribute phoneAttribute;


        public override bool IsValid(object value)
        {
            var emailOrPhone = value as string;

            // Is this a valid email address?
            if (this.IsValidEmailAddress(emailOrPhone))
            {
                // Is valid email address
                return true;
            }
            else if (this.IsValidPhoneNumber(emailOrPhone))
            {
                // Assume phone number
                return true;
            }

            // Not valid email address or phone
            var regex = new Regex(base.CustomDataType);
            return regex.IsMatch(emailOrPhone);
            //return false;
        }

        private bool IsValidEmailAddress(string emailToValidate)
        {
            // Get instance of MVC email validation attribute
            //var emailAttribute = new EmailAddressAttribute();

            return emailAttribute.IsValid(emailToValidate);
        }

        private bool IsValidPhoneNumber(string phoneNumberToValidate)
        {
            // Regualr expression from https://stackoverflow.com/a/8909045/894792 for phone numbers
            //var regex = new Regex("^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$");
            
            return phoneAttribute.IsValid(phoneNumberToValidate);
            //return regex.IsMatch(phoneNumberToValidate)
        }

    }
}