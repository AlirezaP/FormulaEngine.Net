# FormulaEngine.Net
FormulaEngine.Net Is a Simple FormulaEngine For .net Application (C#,VB,...)
Today This Engine Support This Operations (+,-,/,*,Pow,Log,Sin)

Exmaple:

Input:

1. A+B

2. A=1

3. B=1

OutPut:

2

Sample Code:

            Dictionary<string, double> variables = new Dictionary<string, double>();

            variables.Add("a", 2);
            variables.Add("b", 3);
            variables.Add("c", 5);
            variables.Add("k", 4);
            variables.Add("f", 4);
            variables.Add("d", 8);
            variables.Add("var1", 2);
            variables.Add("some", 3);
            
            FormulaEngine.Net.Engine ap = new FormulaEngine.Net.Engine(variables);
            double result = 0;

            result = ap.Process("(var1*some/c)*log(c+k)");

            result = ap.Process("(var1*some/c)*log(log(c+k))");//-0.024409488559873654

            result = ap.Process("((log(a))*b)+pow(a,b)");//8.9030899869919438

            result = ap.Process("((log(a))*b)+pow(pow(a,c),b)");//32768.90308998699

            result = ap.Process("log(pow(a,b))");//0.90308998699194354

            result = ap.Process("var1+c");//7
