using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Emergence_WPF.Comm
{
    public class IsPhoneNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value as string) )
            {
                return new ValidationResult(false, "值不能为空！");
            }
            else
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch((value as string), @"^[1]+[3,4,5,7,8,9]+\d{9}"))
                {
                    return new ValidationResult(false, "无效的手机号码！");
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
