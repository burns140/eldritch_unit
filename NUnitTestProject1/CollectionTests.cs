using NUnit.Framework;
using System.Threading;
using Newtonsoft.Json;
using System.Net.Sockets;
using System;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;

namespace NUnitTestProject1 {

    [TestFixture]
    class CollectionTests {

        [Test]
        public void openPackTest() {
            OpenPackRequest req = new OpenPackRequest("openPack", Constants.DOLPH_ID, Constants.VALID_TOKEN);
            string json = JsonConvert.SerializeObject(req);
            string res = req.sendNetworkRequest(json);

            Assert.IsFalse(res.Contains("Error"));
        }
    }
}
