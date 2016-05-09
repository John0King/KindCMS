using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using KindCMS.Utility;

namespace ControllerTest
{
    public class AdminAuthJsonTest
    {
        public AdminAuthJsonTest()
        {

        }
        private const string Json = @"
{
    ""Issuer"":""Admin"",
    ""Id"":""1"",
    ""Name"":""AdminUser"",
    ""Clames"":{
        ""a"":""A"",
        ""b"":""B""
    }
}";
        [Fact]
        public void JsonConverTest_S()
        {
            var O = new AdminAuthInfo();
            O.Id = 12.ToString();
            O.IsLogin = false;
            O.Name = "I Don't want you to know that :)";
            O.Issuer = "Test";
            O.Clames = new Dictionary<string, string>() { { "Hi", "I am ok" }, { "How", " are you"} };

            OutJson = JsonConvert.SerializeObject(O,new JsonSerializerSettings() {  Formatting = Formatting.Indented });

            Assert.True(!string.IsNullOrEmpty(OutJson));
        }
        [Fact]
        public void JsonConverTest_D()
        {
            R = JsonConvert.DeserializeObject<AdminAuthInfo>(Json);
            Assert.True(R != null);
            Assert.Equal(R.Id, "1");
            Assert.Equal(R.IsLogin, true);
            Assert.Equal(R.Issuer, "Admin");
            Assert.Equal(R.Clames.Count, 2);

        }

        private AdminAuthInfo R;
        private string OutJson;
    }
}
