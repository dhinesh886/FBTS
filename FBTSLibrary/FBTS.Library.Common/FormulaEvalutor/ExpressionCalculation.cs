using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Library.Common.FormulaEvalutor
{
    public static class FormulaEvalutor
    {
        public static object EvaluateFormula(Formula formula)
        {
           
            return formula.Evaluate();
        }
    }
}
