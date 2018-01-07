using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MiniGuid.Test
{
    public class JsonSerializationTests
    {

        [Fact]
        public void Serializes_ToString()
        {
            var guid = MiniGuid.Create();

            var json = JsonConvert.SerializeObject(new { guid });

            var guidProp = JObject.Parse(json)
                            .Property("guid");

            Assert.Equal(JTokenType.String, guidProp.Value.Type);
            Assert.Equal(guid.ToString(), guidProp.Value.Value<string>());
        }


        [Fact]
        public void Deserializes_FromString()
        {
            var json = @"{ ""guid"": ""abcdeABCDEabcdeABCDEabcdeA"" }";

            var dummy = JsonConvert.DeserializeObject<Dummy>(json);

            Assert.Equal(MiniGuid.Parse("abcdeABCDEabcdeABCDEabcdeA"), dummy.Guid);
        }


        class Dummy
        {
            public MiniGuid Guid;
        }
        
    }
}
