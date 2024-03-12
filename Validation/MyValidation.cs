using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace website_shopping.Validation
{
    public class MyValidation : ValidationAttribute
    {
        public MyValidation()
        {
            ErrorMessage = "Số điện thoại gồm 10 số, vd : 0365737582";
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            Regex regex = new Regex(@"0\d{9}");
            Console.WriteLine("check phone number : " + regex.IsMatch(value.ToString()));
            return regex.IsMatch(value.ToString());
        }
    }
}