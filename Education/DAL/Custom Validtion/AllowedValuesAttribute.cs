using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace Education.DAL.Custom_Validtion
{
    public class AllowedValuesAttribute: ValidationAttribute
    {
        private readonly string [] AllowedValues;
        public AllowedValuesAttribute(string[] Allowed_values)
        {
            AllowedValues = Allowed_values;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string Value = value.ToString();
                if (AllowedValues.Contains(Value))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Hire date cant be before company start date");
                }

                }
            
      
            return new ValidationResult("Error! Contact the Admin");
        }
    }
}
