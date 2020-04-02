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
    public class ReportTests {

        [Test]
        public void reportUserTest() {
            ReportRequest req = new ReportRequest(Constants.DOLPHIN, Constants.OTHER_EMAIL, "reportPlayer", Constants.DOLPH_ID, Constants.VALID_TOKEN);
            string json = JsonConvert.SerializeObject(req);
            string res = req.sendNetworkRequest(json);

            Assert.IsTrue(res.Contains("user reported successfully"));
        }

        [Test]
        public void failedLoginTempBanTest() {
            User user = new User("login", Constants.OTHER_EMAIL, "password", "username");
            string json = JsonConvert.SerializeObject(user);
            string res = user.sendNetworkRequest(json);

            Assert.IsTrue(res.Contains("This account is temporarily banned"));
        }

        [Test]
        public void liftTempBanTest() {
            User user = new User("login", Constants.OTHER_EMAIL, "password", "username");
            string json = JsonConvert.SerializeObject(user);
            string res = user.sendNetworkRequest(json);

            Assert.IsFalse(res.Contains("Error"));
            Assert.IsFalse(res.Contains("This account is temporarily banned"));
        }

    }
}
 