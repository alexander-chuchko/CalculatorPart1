using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorPart1
{
    public class Calculator
    {
        private string inputString= string.Empty;
        private Stack<string> operatorsStack = new Stack<string>();
        private string temp;
        private double ro;
        private double lo;
        public double reternResult;
        private ContainerOperator containerOperator;

        public Calculator()
        {
            var listOperation = new List<Operator>() 
            {
                new Operator() { simbol = "^", priority = 4 },
                new Operator() { simbol = "*", priority = 3 },
                new Operator() { simbol = "/", priority = 3 },
                new Operator() { simbol = "+", priority = 2 },
                new Operator() { simbol = "-", priority = 2 },
                new Operator() { simbol = "(", priority = 1 },
            };

            containerOperator = new ContainerOperator();
            containerOperator.operators.AddRange(listOperation);
        }

        public double ConvertTheTermIntoPolishNotation(string formula)
        {
            foreach (char c in formula)
            {
                //Выполняем проверку число или оператор
                if (char.IsDigit(c))
                {
                    inputString += c;
                    continue;
                }
                else if (operatorsStack.Count is 0) 
                {
                    operatorsStack.Push(c.ToString());
                    continue;
                }//Если элементы есть
                else if(operatorsStack.Count is not 0)
                {
                }


            }

            return 0.0;
        }

        public double PerformCalculation(string formula) 
        {
            foreach (char symbol in formula)
            {
                //Проверяем является ли текущий символ числом или оператором, если число - то помещаем в выходную строку
                if (Char.IsDigit(symbol))
                {
                    this.inputString += symbol;
                    //дальше итерируемся
                    continue;
                }
                //Если текущий символ является оператором, то заходим в if
                if (symbol == '+' || symbol == '-' || symbol == '*' || symbol == '/' || symbol == '^') 
                {
                    //Проверяем явл. ли стек пустым, если да - то просто добавлеям элемент
                    if (this.operatorsStack.Count == 0)
                    {
                        this.operatorsStack.Push(Convert.ToString(symbol));
                        continue;
                    } //Если в стеке уже есть элемент, если да то заходим
                    else if (operatorsStack.Count != 0) 
                    {
                        //Проверяем приоритет добавляемого оператора и того, который находится на вершине стека
                        if (containerOperator.FindOperator(operatorsStack.Peek()).priority< containerOperator.FindOperator(Convert.ToString(symbol)).priority)
                        {
                            operatorsStack.Push(Convert.ToString(symbol));
                            continue; 
                        }
                    }

                    //Если приоритет добавляемого оператора меньше или равен элемента,
                    //который находится на вершине стека
                    if (operatorsStack.Count != 0)
                    {
                        try
                        {
                            while (containerOperator.FindOperator(operatorsStack.Peek()).priority >= containerOperator.FindOperator(Convert.ToString(symbol)).priority)
                            {
                                inputString += operatorsStack.Pop();
                            }
                        }
                        catch (Exception)
                        {
                        
                        }

                        if (operatorsStack.Count == 0)
                        {
                            operatorsStack.Push(Convert.ToString(symbol));
                            continue;
                        }
                        else if (operatorsStack.Count != 0) 
                        {
                            if (containerOperator.FindOperator(operatorsStack.Peek()).priority < containerOperator.FindOperator(Convert.ToString(symbol)).priority)
                            {
                                operatorsStack.Push(Convert.ToString(symbol));
                                continue;
                            }
                        }
                    }
                }

                //Помещаем скобку в стек
                if (symbol == '(')
                {
                    operatorsStack.Push(Convert.ToString(symbol));
                    continue;
                }

                if (symbol ==')')
                {
                    while (operatorsStack.Peek()!= "(") 
                    {
                        inputString += operatorsStack.Pop();
                    }
                    //Уничтожаем закрывающеюся скобку
                    operatorsStack.Pop();
                    continue;
                }
            }

            //После перебора всех элементов, добавляем все операторы в выходную строку
            while (operatorsStack.Count != 0) 
            {
                inputString += operatorsStack.Pop();
            }

            //Записано выражение в обратной польской записи
            var result = inputString;


            //Производим расчет выражения
            foreach (char symbol in inputString)
            {

                //Если число то добавляем в стек
                if (Char.IsDigit(symbol))
                {
                    operatorsStack.Push(Convert.ToString(symbol));
                    continue;

                }//Если симвод не является для этого инвертироват
                else if (!Char.IsDigit(symbol))
                {
                    if (symbol == '+')
                    {
                        ro = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        if (operatorsStack.Count == 0)
                        {
                            lo = 0;
                        }
                        else
                        {
                            lo = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        }

                        temp = Convert.ToString(lo + ro);
                    }
                    else if (symbol == '-')
                    {
                        ro = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));

                        if (operatorsStack.Count == 0)
                        {
                            lo = 0;
                        }
                        else 
                        {
                            lo = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        }

                        temp = Convert.ToString(lo - ro);
                    }
                    else if (symbol == '*')
                    {
                        ro = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        lo = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        temp = (lo*ro).ToString();
                    }
                    else if (symbol == '/')
                    {
                        ro = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        lo = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        temp = Convert.ToString(lo / ro);
                    }
                    else if (symbol == '^')
                    {
                        ro = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        lo = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        temp = RaiseToPower(lo, Convert.ToInt32(ro)).ToString();
                    }

                    operatorsStack.Push(temp);
                    inputString = String.Empty;
                }
            }

            var res = reternResult = double.Parse(operatorsStack.Pop());

            return res;
        }

        private double GetResultCalculation(string inputString) 
        {
            double leftOperand = 0.0;
            double rightOperand = 0.0;
            string temp = string.Empty;
            Stack<string> operations = new Stack<string>();

            //Производим расчет выражения
            foreach (char symbol in inputString)
            {

                //Если число то добавляем в стек
                if (Char.IsDigit(symbol))
                {
                    operations.Push(Convert.ToString(symbol));
                    continue;

                }//Если симвод не является для этого инвертироват
                else if (!Char.IsDigit(symbol))
                {
                    if (symbol == '+')
                    {
                        rightOperand = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        if (operations.Count == 0)
                        {
                            leftOperand = 0;
                        }
                        else
                        {
                            leftOperand = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        }

                        temp = Convert.ToString(leftOperand + rightOperand);
                    }
                    else if (symbol == '-')
                    {
                        rightOperand = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));

                        if (operatorsStack.Count == 0)
                        {
                            leftOperand = 0;
                        }
                        else
                        {
                            leftOperand = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        }

                        temp = Convert.ToString(leftOperand - rightOperand);
                    }
                    else if (symbol == '*')
                    {
                        rightOperand = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        leftOperand = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        temp = (leftOperand * rightOperand).ToString();
                    }
                    else if (symbol == '/')
                    {
                        rightOperand = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        leftOperand = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        temp = Convert.ToString(leftOperand / rightOperand);
                    }
                    else if (symbol == '^')
                    {
                        leftOperand = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        rightOperand = Convert.ToDouble(Convert.ToString(operatorsStack.Pop()));
                        temp = RaiseToPower(lo, Convert.ToInt32(ro)).ToString();
                    }

                    operatorsStack.Push(temp);
                    inputString = String.Empty;
                }
            }

            return 0;
        }

        private double PerformMathematicalOperations(string calculationOperator, Stack<string> stackOfResults)
        {
            double leftOperand = 0.0;
            double rightOperand = 0.0;
            double temp = 0.0;

            rightOperand = Convert.ToDouble(Convert.ToString(stackOfResults.Pop()));
            leftOperand = Convert.ToDouble(Convert.ToString(stackOfResults.Pop()));

            switch (calculationOperator)
            {
                case "+":
                    temp = leftOperand + rightOperand;
                    break;
                case "-":
                    temp = leftOperand - rightOperand;
                    break;
                case "*":
                    temp = leftOperand* rightOperand;
                    break;
                case "/":
                    temp = leftOperand / rightOperand;
                    break;
                case "^":
                    temp = RaiseToPower(leftOperand, Convert.ToInt32(rightOperand));
                    break;
            }

            return temp;
        }



        private double RaiseToPower(double value, int exponent)
        {
            if (exponent > 0)
            {
                value = Exponential(value, exponent);
            }
            else if (exponent == 0)
            {
                value = 1;
            }
            else if (exponent < 0)
            {
                value = 1 / Exponential(value, exponent);
            }

            return value;
        }

        /*
         *The method by which we raise to a power
         * */
        private double Exponential(double value, int exponent)
        {
            double temp = value;
            int limit = 0;

            if (exponent < 0)
            {
                limit = -exponent;
            }
            else if (exponent > 0)
            {
                limit = exponent;
            }

            /*Raising to a power in a loop*/

            for (int i = 1; i < limit; i++)
            {
                value *= temp;
            }

            return value;
        }
    }
}
