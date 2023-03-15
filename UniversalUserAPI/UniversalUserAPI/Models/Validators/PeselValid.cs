using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Globalization;

namespace UniversalUserAPI.Models.Validators
{
    public class PeselValid:ValidationAttribute
    {
        public PeselValid() 
        {
        }

        protected override ValidationResult IsValid(object? value,ValidationContext validationContext)
        {
            ValidationResult result = ValidationResult.Success;
            String pesel=(String)value;

            if(pesel.Length!=11)
            {
                result = new ValidationResult("Incorrect PESEL length");
            }
            else if(!pesel.All(Char.IsDigit))
            {
                result = new ValidationResult("PESEL contains only numbers");
            }
            else if(!CheckControlNumber(ref pesel))
            {
                result = new ValidationResult("Incorrect PESEL");
            }
            return result;
        }

        /// <summary>
        /// Checks if control number (last digit) is valid
        /// more here: https://pl.wikipedia.org/wiki/PESEL
        /// </summary>
        /// <param name="pesel">PESEL number</param>
        /// <returns> true if control number is valid; false otherwise </returns>
        private bool CheckControlNumber(ref String pesel)
        {
            //product of sum of digits that have weight==1
            int productWithWeight1 = Convert.ToInt32(pesel.ElementAt(0)) +
                                    Convert.ToInt32(pesel.ElementAt(4)) +
                                    Convert.ToInt32(pesel.ElementAt(8))+
                                    Convert.ToInt32(pesel.ElementAt(10));
            //product of sum of digits that have weight==3
            int productWithWeight3 = (Convert.ToInt32(pesel.ElementAt(1)) +
                                      Convert.ToInt32(pesel.ElementAt(5)) +
                                      Convert.ToInt32(pesel.ElementAt(9)))*3;
            //product of sum of digits that have weight==7
            int productWithWeight7 = (Convert.ToInt32(pesel.ElementAt(2)) +
                                      Convert.ToInt32(pesel.ElementAt(6))) * 7;
            //product of sum of digits that have weight==9
            int productWithWeight9 = (Convert.ToInt32(pesel.ElementAt(3)) +
                                      Convert.ToInt32(pesel.ElementAt(7))) * 3;

            int sValue=productWithWeight1 + productWithWeight3+productWithWeight7+productWithWeight9;

            return (sValue%10)==0;

        }
        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
            ErrorMessageString, name);
        }
    }
}
