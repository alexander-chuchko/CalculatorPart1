using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorPart1
{
    public class ContainerOperator
    {
        public List<Operator> operators = new List<Operator>();
        public void AddOperators(Operator op)
        {
            operators.Add(op);
        }

        public Operator FindOperator(string s) 
        {
            foreach (var item in operators)
            {
                if (item.simbol == s)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
