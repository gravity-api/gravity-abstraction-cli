using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gravity.Abstraction.Cli.UnitTests
{
    [TestClass]
    public class ParserTests
    {
        [DataTestMethod]
        [DataRow("{{$ --data:@from_data --macro:from_macro_{{$date --format:yyyyMMddHHmmssfff}} --field:from_field}}")]
        public void MacroAsParameter(string cli)
        {
            // setup
            var actual = new CliFactory(cli).Parse();

            // actual
            Assert.IsTrue(actual["data"].Equals("@from_data"));
            Assert.IsTrue(actual["macro"].Equals("from_macro_{{$date --format:yyyyMMddHHmmssfff}}"));
            Assert.IsTrue(actual["field"].Equals("from_field"));
        }
    }
}
