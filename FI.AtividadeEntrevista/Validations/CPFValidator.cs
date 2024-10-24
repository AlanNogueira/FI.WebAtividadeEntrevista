using FI.AtividadeEntrevista.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.Validations
{
    public class CPFValidator : ValidationAttribute
    {
        public CPFValidator()
        {

        }

        public override bool IsValid(object value)
        {
            if (value != null)
                return Utils.Utils.ValidarCPF(value.ToString());

            return false;
        }

        
    }
}
