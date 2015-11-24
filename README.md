# FormulaEngine.Net
FormulaEngine.Net Is a Simple FormulaEngine For .net Application (C#,VB,...)
Today This Engine Support This Operations (+,-,/,*,Pow,Log,Log10,Sin,Cos,Sqrt,Tan)

Operation Syntax:</br>
sin(variable)</br>
cos(variable)</br>
tan(variable)</br>
pow(variable1,variable2)</br>
sqrt(variable)</br>
log(variable)</br>

Sample:</br>
var formula = "(log(pow(a,b)))+(cos(n)*(tan(m)))";

Exmaple:

Input:

1. A+B

2. A=1

3. B=1

OutPut:

2</br>

variables And variablesValue must be in the dictionary object:</br>
Dictionary(variableName, variableValue)</br></br>

*Now You Can Put Digit Directly To The Formula.</br>

please be carefull in formula text. count of '(' must be equal with count of  ')' for example: ((sin(a))) .</br></br>
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
            variables.Add("n", 30);
            variables.Add("m", 45);
            variables.Add("alakiy5", 45);
            
            FormulaEngine.Net.Engine ap = new FormulaEngine.Net.Engine(variables);
            double result = 0;

            FormulaEngine.Net.Validation validator = new FormulaEngine.Net.Validation();
            bool res = validator.SyntaxValidation("a+b"); //------> true
            res = validator.SyntaxValidation("a++b"); //------> false

            result = ap.Process("(var1*alakiy5/c)*log(c+k)");

            result = ap.Process("(var1*alakiy5/c)*log10(log10(c+k))");

            result = ap.Process("((log10(a))*b)+pow(a,b)");

            result = ap.Process("((log10(a))*b)+pow(pow(a,c),b)");

            result = ap.Process("log10(pow(a,b))");

            result = ap.Process("var1+c");

            result = ap.Process("sqrt(n)"); 

            result = ap.Process("(log(pow(a,b)))+(cos(n)*(tan(m)))"); 
            
            result = ap.Process("(var1+c)*6");
            
            result = ap.Process("sin(30)");

