using System.Text.RegularExpressions;

namespace KraftSales.Validations
{
    public class EmailValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            var regex = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");

            return regex.IsMatch(str);
        }
    }
}
