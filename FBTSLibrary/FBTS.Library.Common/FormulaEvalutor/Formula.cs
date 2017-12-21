using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Library.Common.FormulaEvalutor
{
    public class Formula : Expression
    {
        public Formula(string formula)
            : base(formula)
        {

        } 
        public object EvaluateFormula()
        { 
            return this.Evaluate();
        }
        public void passDelegateParameter(string[] name1, double[] args1)
        {
            for (var i = 0; i < name1.Length; i++)
            {
                this.EvaluateParameter += delegate(string name, ParameterArgs args)
                {
                    if (name == name[i].ToString())
                        args.Result = args1[i];
                };
            }
        }
    }
}
