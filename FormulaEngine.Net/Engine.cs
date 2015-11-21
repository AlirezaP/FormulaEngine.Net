using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaEngine.Net
{
    public class Engine
    {
        string[] Reserve = new string[] { "log10(","log(", "pow(","sin(","cos(","tan(","sqrt(" };
        Dictionary<string, double> Variable = new Dictionary<string, double>();

        public Engine(Dictionary<string, double> input)
        {
            Variable = input;
        }

        public double Process(string algorithm)
        {
            algorithm = algorithm.Replace(" ", "").Trim(); ;
            algorithm = SplitDigit(algorithm);

            string Temp = "";

            Stack<double> Values = new Stack<double>();
            Stack<string> Operations = new Stack<string>();

            algorithm = algorithm.Replace(" ", "").Trim();
            for (int i = 0; i < algorithm.Length; i++)
            {
                Temp += algorithm[i].ToString();

                if (!IsReserve(Temp) && !Pattern(algorithm, ref i, ref Temp))//here have a new code
                {
                    if (algorithm.Length - 1 > i)
                        i++;

                    while (algorithm.Length - 1 > i && !IsReserve(algorithm[i].ToString()) && !Pattern(algorithm, ref i, ref Temp))
                    {
                        Temp += algorithm[i].ToString();
                        i++;
                    }

                    Values.Push(MapVariable(Temp));

                    if (IsReserve(algorithm[i].ToString()))
                        Operations.Push(algorithm[i].ToString());

                    Calculate(Operations, Values);
                    Temp = "";
                }
                else
                {
                    Operations.Push(Temp);
                    Calculate(Operations, Values);
                    Temp = "";
                }

            }

            while (Operations.Count > 0)
            {
                Calc(Operations, Values);
            }

            return Values.Peek();
        }


        private string SplitDigit(string algorithm)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\b\d*([.]?\d*)?\b");
            var temp = regex.Matches(algorithm);

            int vIndex = 1;
            string v = "var";
            for (int i = 0; i < temp.Count; i++)
            {
                if (string.IsNullOrEmpty(temp[i].Value))
                    continue;

                while (true)
                {
                    v = "var" + vIndex.ToString();
                    var t = Variable.Keys.Where(p => p.Contains(v)).FirstOrDefault();

                    if (t == null)
                        break;

                    vIndex++;
                }
                var r = new System.Text.RegularExpressions.Regex(temp[i].Value);
                algorithm = r.Replace(algorithm, v, 1, temp[i].Index);

                Variable.Add(v, double.Parse(temp[i].Value, new System.Globalization.CultureInfo("en-US")));
            }
            return algorithm;
        }

        private bool IsReserve(string token)
        {
            switch (token)
            {
                case "(": return true;
                case ")": return true;
                case "*": return true;
                case "+": return true;
                case "-": return true;
                case "/": return true;

                case ",": return true;

                default: return false;
            }
        }

        private double MapVariable(string v)
        {
            return Variable[v];
        }

        private bool Pattern(string all, ref int index, ref string t)
        {
            for (int i = 0; i < Reserve.Length; i++)
            {
                if ((all.Length > index + Reserve[i].Length) && (all.Substring(index, Reserve[i].Length) == Reserve[i]))
                {
                    t = Reserve[i].Substring(0, Reserve[i].Length - 1);
                    index += Reserve[i].Length - 2;
                    return true;
                }
            }

            return false;
        }

        private void Calculate(Stack<string> Operations, Stack<double> Values)
        {
            if (Operations.Count > 0)
                switch (Operations.Peek())
                {
                    case ")": Operations.Pop(); Parantezcalc(Operations, Values); break;
                    case "+": LowPrivilage(Operations, Values); break;
                    case "-": LowPrivilage(Operations, Values); break;
                }
        }

        private void Parantezcalc(Stack<string> Operations, Stack<double> Values)
        {
            double a = 0;
            double b = 0;
            string t = Operations.Pop();
            while (t != "(")
            {
                a = Values.Pop();
                b = Values.Pop();

                switch (t)
                {
                    case "*": Values.Push(b * a); break;
                    case "/": Values.Push(b / a); break;
                    case "+": Values.Push(b + a); break;
                    case "-": Values.Push(b - a); break;

                    case ",": break;

                    case "log10": Values.Push(b); Values.Push(Math.Log10(a)); break;
                    case "log": Values.Push(b); Values.Push(Math.Log(a)); break;

                    default: Values.Push(a); Values.Push(b); break;
                }

                t = Operations.Pop();
            }


            if (Operations.Count >= 1)
            {
                switch (Operations.Peek())
                {
                    case "log10": Operations.Pop(); Values.Push(Math.Log10(Values.Pop())); break;
                    case "log": Operations.Pop(); Values.Push(Math.Log(Values.Pop())); break;
                    case "sin": Operations.Pop(); Values.Push(Math.Sin(Values.Pop())); break;
                    case "cos": Operations.Pop(); Values.Push(Math.Cos(Values.Pop())); break;
                    case "sqrt": Operations.Pop(); Values.Push(Math.Sqrt(Values.Pop())); break;
                    case "tan": Operations.Pop(); Values.Push(Math.Tan(Values.Pop())); break;

                    case "pow": Operations.Pop(); Values.Push(Math.Pow(b, a)); break;
                }
            }
        }

        private void LowPrivilage(Stack<string> Operations, Stack<double> Values)
        {
            if (Operations.Count < 2)
                return;

            string first = Operations.Pop();
            string secound = Operations.Pop();
            if (secound == "*" || secound == "/")
            {
                double a = Values.Pop();
                double b = Values.Pop();

                switch (secound)
                {
                    case "*": Values.Push(b * a); break;
                    case "/": Values.Push(b / a); break;
                    case "+": Values.Push(b + a); break;
                    case "-": Values.Push(b - a); break;
                }

                Operations.Push(first);
            }
            else
            {
                Operations.Push(secound);
                Operations.Push(first);
            }
        }

        private void Calc(Stack<string> Operations, Stack<double> Values)
        {
            string t = Operations.Pop();

            double a = Values.Pop();
            double b = Values.Pop();

            switch (t)
            {
                case "*": Values.Push(b * a); break;
                case "/": Values.Push(b / a); break;
                case "+": Values.Push(b + a); break;
                case "-": Values.Push(b - a); break;

                case "log10": Values.Push(b); Values.Push(Math.Log10(a)); break;
                case "log": Values.Push(b); Values.Push(Math.Log(a)); break;

                default: Values.Push(a); Values.Push(b); break;
            }

            if (Operations.Count > 1)
            {
                switch (Operations.Peek())
                {
                    case "log10": Operations.Pop(); Values.Push(Math.Log10(Values.Pop())); break;
                    case "log": Operations.Pop(); Values.Push(Math.Log(Values.Pop())); break;
                    case "sin": Operations.Pop(); Values.Push(Math.Sin(Values.Pop())); break;
                    case "cos": Operations.Pop(); Values.Push(Math.Cos(Values.Pop())); break;
                    case "Sqrt": Operations.Pop(); Values.Push(Math.Sqrt(Values.Pop())); break;
                    case "tan": Operations.Pop(); Values.Push(Math.Tan(Values.Pop())); break;
                }
            }
        }

    }
}
