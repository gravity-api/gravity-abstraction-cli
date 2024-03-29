using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace Gravity.Abstraction.Cli.UnitTests
{
    [TestClass]
    public class ParserTests
    {
        [DataTestMethod]
        [DataRow("{{$ --data:@from_data --macro:from_macro_{{$date --format:yyyyMMddHHmmssfff}} --field:from_field}}")]
        public void MacroAsParameterTest(string cli)
        {
            // setup
            var actual = new CliFactory(cli).Parse();

            // actual
            Assert.IsTrue(actual["data"].Equals("@from_data"));
            Assert.IsTrue(actual["macro"].Equals("from_macro_{{$date --format:yyyyMMddHHmmssfff}}"));
            Assert.IsTrue(actual["field"].Equals("from_field"));
        }

        [DataTestMethod]
        [DataRow("{{$ --xml:<xml><body<!-- comment --></body></xml> --foo:bar}}")]
        [DataRow("{{$ --xml:<xml><body<!-- comment --></body></xml> --foo}}")]
        [DataRow("{{$ --xml:<xml><body<!-- comment --></body></xml>}}")]
        public void ParameterXmlWithCommentTest(string cli)
        {
            // setup
            var actual = new CliFactory(cli).Parse();

            // actual
            Assert.IsTrue(actual["xml"].Equals("<xml><body<!-- comment --></body></xml>"));
        }

        [DataTestMethod]
        [DataRow("{{$ --//MsgId:4VGCVN4G48161125 --*:I09VU5JWLC161125 --.:2022-08-10T11:36:23.728Z --$..:2022-08-10T12:12:12Z}}")]
        public void ParameterSpecialCharactersTest(string cli)
        {
            // setup
            var actual = new CliFactory(cli).Parse();

            // actual
            Assert.IsTrue(actual["//MsgId"].Equals("4VGCVN4G48161125"));
            Assert.IsTrue(actual["*"].Equals("I09VU5JWLC161125"));
            Assert.IsTrue(actual["."].Equals("2022-08-10T11:36:23.728Z"));
            Assert.IsTrue(actual["$.."].Equals("2022-08-10T12:12:12Z"));
        }

        [DataTestMethod]
        [DataRow("{{$ --server:http://10.192.97.171:9035 --role:ALL1 --userId:SOAP1 --inMsgContext:SWIFT_IN --message:{1:F01DHBKNZNZAXXX0857137152}\n{2:O1031036130401CITINZ33AXXX02182191591304011136N}\n{3:{103:AVP}{108:001382CB03553}{115:AAA}}\n{4:\n:20:20220427042543\n:23B:CRED\n:32A:160525EUR1000\n:50K:XIAO LIN\n:59:/2222222222\nZHANG SAN\n:71A:OUR\n-}}}")]
        public void SpecialMessageTest(string cli)
        {
            // setup
            var actual = new CliFactory(cli).Parse();
            var expected =
                "{1:F01DHBKNZNZAXXX0857137152}\n" +
                "{2:O1031036130401CITINZ33AXXX02182191591304011136N}\n" +
                "{3:{103:AVP}{108:001382CB03553}{115:AAA}}\n" +
                "{4:\n" +
                ":20:20220427042543\n" +
                ":23B:CRED\n" +
                ":32A:160525EUR1000\n" +
                ":50K:XIAO LIN\n" +
                ":59:/2222222222\n" +
                "ZHANG SAN\n" +
                ":71A:OUR\n" +
                "-}";

            // actual
            Assert.IsTrue(actual["message"].Equals(expected, StringComparison.OrdinalIgnoreCase));
        }
    }
}
