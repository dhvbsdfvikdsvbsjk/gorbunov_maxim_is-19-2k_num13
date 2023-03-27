using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace юнитытесты
{
    public class Class1
    {
        public static bool Test1(string Pred, string St, int nu)
        {
            
            if (Pred == "Алые паруса" & St == "Vip" & nu == 10)
                return true;

            return false;

        }
        public static bool Test2(string Pred, string St, int nu)
        {

            if (Pred == "Красная шапочка" & St == "Балкон" & nu == 50)
                return true;

            return false;

        }
        public static bool Test3(string Pred)
        {

            if (Pred.Intersect("@#$%^").Count() == 1)
                return false;
            // проверка на наличие @#$%^ в строке проверки
            return true;

        }
    }
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Tes1()
        {
            string Pred = "Алые паруса";
            string St = "Vip";
            int nu = 10;
            bool ex = true;
            bool act = Class1.Test1(Pred, St, nu);
            Assert.AreEqual(ex, act);
        }
        [TestMethod]
        public void Tes2()
        {
            string Pred = "Красная шапочка";
            string St = "Балкон";
            int nu = 50;
            bool ex = true;
            bool act = Class1.Test2(Pred,St, nu);
            Assert.AreEqual(ex, act);
        }
        [TestMethod]
        public void Tes3()
        {
            string Pred = "Алые паруса";
            bool ex = true;
            bool act = Class1.Test3(Pred);
            Assert.AreEqual(ex, act);
        }

    }

}
