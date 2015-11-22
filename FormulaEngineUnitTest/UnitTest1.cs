using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FormulaEngineUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestFormula()
        {
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

            //case1
            double result = ap.Process("(var1*alakiy5/c)*log10(c+k)");
            Assert.AreEqual(17.176365169907847, result, "case1");

            //case2
            result = ap.Process("(var1*alakiy5/c)*log10(log10(c+k))");
            Assert.AreEqual(-0.36614232839810484, result, "case2");

            //case3
            result = ap.Process("((log10(a))*b)+pow(a,b)");
            Assert.AreEqual(8.9030899869919438, result, "case3");

            //case4
            result = ap.Process("((log10(a))*b)+pow(pow(a,c),b)");
            Assert.AreEqual(32768.90308998699, result, "case4");

            //case5
            result = ap.Process("log10(pow(a,b))");
            Assert.AreEqual(0.90308998699194354, result, "case5");

            //case6
            result = ap.Process("var1+c");
            Assert.AreEqual(7, result, "case6");

            //case7
            result = ap.Process("sqrt(n)");
            Assert.AreEqual(5.4772255750516612, result, "case7");

            //case8
            result = ap.Process("(log(pow(a,b)))+(cos(n)*(tan(m)))");
            Assert.AreEqual(2.329294213313164, result, "case8");

            //case9
            result = ap.Process("(var1+c)*6");
            Assert.AreEqual(42, result, "case9");

            //case10
            result = ap.Process("sin(30)");
            Assert.AreEqual(-0.98803162409286183, result, "case10");

        }
    }
}
