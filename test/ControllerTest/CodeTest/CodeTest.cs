using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace ControllerTest
{
    public class CodeTest
    {
        [Fact]
        public void Test1()
        {
            string sp = "";
            string vp = "";
            PathString p = new PathString("~/Home/Index");
            sp = p.ToString();
            vp = p.Value;
            Assert.Equal(vp, "/Home/Index?a=A&好=就是好");
        }
        [Fact]
        public void TestA()
        {
            var T1 = new TestBase();
            T1.Do();
            var T2 = new TestR();
            T2.Do();

        }


        public void A()
        {
            IServiceCollection services = new ServiceCollection();
        }

        [AttributeUsage(AttributeTargets.Class)]
        class XAttribute:Attribute
        {
            public string X { get; set; } = "X";
            public string Y = "Y";
        }
        [X]
        class TestBase
        {
            public virtual void Do()
            {
                Type t = this.GetType();
                TypeInfo ti = t.GetTypeInfo();
                bool r = false;
                r = ti.IsDefined(typeof(XAttribute));
                Console.WriteLine(r);

            }
        }

        class TestR : TestBase
        {
            public override void Do()
            {
                Type t = this.GetType();
                TypeInfo ti = t.GetTypeInfo();
                bool r = false;
                r = ti.IsDefined(typeof(XAttribute));
                Console.WriteLine(r);
            }
        }
    }
}
