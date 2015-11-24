using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FormulaEngine.Net
{
   public class Validation
    {
        public bool SyntaxValidation(string formula)
        {
            string pattern = @"([+-/*][+-/*]+)|(((log|sin|cos|tan|sqrt)[(](.*)[)])((log|sin|cos|tan|sqrt)[(](.*)[)])+)|([+-/*][()]*[/*-+])|^[-+/*]|[-+/*]$";
            Regex regex = new Regex(pattern);
            if (regex.Matches(formula).Count > 0)
                return false;

            return true;
        }

        public bool BracketValidation(string formula,out string message)
        {
            int openCount = 0;
            int closeCount = 0;
            for (int i = 0; i < formula.Length; i++)
            {
                if (formula[i] == '(')
                    openCount++;

                if (formula[i] == ')')
                    closeCount++;
            }

            if (openCount != closeCount)
            {
                message = "Count Of '(' is not equal with ')'";
                return false;
            }

            message = null;
            return true;
        }
    }
}
