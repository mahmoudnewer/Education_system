using System.ComponentModel.DataAnnotations;

namespace Education.DAL.Custom_Validtion
{
    public class DateAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value is DateTime courseDate)
                {

                    if (courseDate >= DateTime.Today)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("Date cant be old ");
                    }

                }
            }
            return new ValidationResult ("Error! Contact the Admin");
        }
    }
}
