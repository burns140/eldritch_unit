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
    public class Tests {

        
        /*[SetUp]
        public void Setup() {
        }*/



        [Test]
        public void addDolphinUser() {
            User u = new User("signup", Constants.DOLPHIN, "password", "dolphin");
            string json = JsonConvert.SerializeObject(u);
            string res = sendNetworkRequestHere(json);

            Assert.IsTrue(res.Contains("User successfully created"));
        }

        [Test]
        public void sendFriendRequestTest() {
            FriendRequest req = new FriendRequest(Constants.DOLPHIN, Constants.OTHER_EMAIL2, "sendFriendRequest", Constants.DOLPH_ID, Constants.VALID_TOKEN);
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);

            Assert.IsTrue(res.Contains("friend request sent"));
        }

        [Test]
        public void sendReverseFriendRequestTest() {
            FriendRequest req = new FriendRequest(Constants.OTHER_EMAIL, Constants.DOLPHIN, "sendFriendRequest", Constants.DOLPH_ID, Constants.VALID_TOKEN);
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);

            Assert.IsTrue(res.Contains("this user has already sent you a friend request"));
        }

        [Test]
        public void acceptFriendRequestTest() {
            FriendRequest req = new FriendRequest(Constants.OTHER_EMAIL, Constants.DOLPHIN, "acceptFriendRequest", Constants.DOLPH_ID, Constants.VALID_TOKEN);
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);

            Assert.IsTrue(res.Contains("friend request accepted"));
        }

        [Test]
        public void removeFriendTest() {
            FriendRequest req = new FriendRequest(Constants.OTHER_EMAIL2, Constants.DOLPHIN, "removeFriend", Constants.DOLPH_ID, Constants.VALID_TOKEN);
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);

            Assert.IsTrue(res.Contains("friend removed"));
        }

        [Test]
        public void getAllFriendsTest() {
            FriendRequest req = new FriendRequest("", Constants.DOLPHIN, "getAllFriends", Constants.DOLPH_ID, Constants.VALID_TOKEN);
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);

            Assert.IsFalse(res.Contains("could not get friends list"));
        }

        [Test]
        public void rejectFriendRequestTest() {
            FriendRequest req = new FriendRequest(Constants.OTHER_EMAIL2, Constants.DOLPHIN, "rejectFriendRequest", Constants.DOLPH_ID, Constants.VALID_TOKEN);
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);

            Assert.IsTrue(res.Contains("friend request rejected"));
        }

        [Test]
        public void copySharedDeckTest() {
            CopySharedDeckRequest req = new CopySharedDeckRequest(Constants.DECKNAME, "copySharedDeck", Constants.DOLPH_ID, Constants.VALID_TOKEN);
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);

            Assert.IsTrue(res.Contains("deck successfully copied"));
        }

        [Test]
        public void blockUserTest() {
            //BlockUserRequest req = new BlockUserRequest(dolphin, otherEmail, "blockUser", id, validToken);
            BlockUserRequest req = new BlockUserRequest(Constants.OTHER_EMAIL, Constants.DOLPHIN, "blockUser", Constants.MY_ID, Constants.VALID_TOKEN);
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);

            Assert.IsTrue(res.Contains("user blocked successfully"));
        }

        [Test]
        public void getBlockedUsersTest() {
            Request req = new Request(Constants.DOLPH_ID, Constants.VALID_TOKEN, "getBlockedUsers");
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);

            Assert.IsFalse(res.Contains("user not found"));
            Assert.IsFalse(res.Contains("Error"));
        }

        [Test]
        public void getBlockedByTest() {
            Request req = new Request(Constants.DOLPH_ID, Constants.VALID_TOKEN, "getBlockedByUsers");
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);

            Assert.IsFalse(res.Contains("user not found"));
            Assert.IsFalse(res.Contains("Error"));
        }

        [Test]
        public void unblockUserTest() {
            BlockUserRequest req = new BlockUserRequest(Constants.DOLPHIN, Constants.OTHER_EMAIL, "unblockUser", Constants.MY_ID, Constants.VALID_TOKEN);
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);

            Assert.IsTrue(res.Contains("user unblocked successfully"));
        }

        [Test]
        public void resendVerificationEmail() {
            ResendVerifyRequest req = new ResendVerifyRequest(Constants.DOLPHIN, "resendVerify", Constants.MY_ID, Constants.VALID_TOKEN);

            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);

            Assert.IsTrue(res.Contains("Verification email resent"));
        }

        [Test]
        public void changeEmailTest() {
            ChangeEmailRequest req = new ChangeEmailRequest(Constants.ALT_EMAIL, "changeEmail", Constants.DOLPH_ID, Constants.VALID_TOKEN);

            string json = JsonConvert.SerializeObject(req);
            string res = req.sendNetworkRequest(json);

            Assert.IsTrue(res.Contains("Email sent successfully"));
        }

        [Test]
        public void newSignupTest() {
            Random rnd = new Random();
            int val = rnd.Next(10000);

            User user1 = new User("signup", val.ToString() + "@test.edu", "password", val.ToString());
            string json = JsonConvert.SerializeObject(user1);
            string res = sendNetworkRequestHere(json);

            Assert.IsTrue(res.Contains("User successfully created"));
        }

        [Test]
        public void shareDeckTest() {
            //ShareDeckRequest req = new ShareDeckRequest(Constants.DOLPHIN, Constants.DECKNAME, "shareDeck", Constants.MY_ID, Constants.VALID_TOKEN);
            ShareDeckRequest req = new ShareDeckRequest("thomasrsimons@gmail.com", "Sharing to t Deck", "shareDeck", Constants.DOLPH_ID, Constants.VALID_TOKEN);
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);

            Assert.IsTrue(res.Contains("deck shared successfully"));
        }

        [Test]
        public void addQueueTest() {
            User user1 = new User("enterQueue", "aphantomdolphin@gmail.com", "a", "rand");
            string json = JsonConvert.SerializeObject(user1);
            string res = sendNetworkRequestHere(json);
            Debug.WriteLine(res);
        }

        [Test]
        public void loginTest() {
            User user = new User("login", "aphantomdolphin@gmail.com", "password", "username");
            string json = JsonConvert.SerializeObject(user);
            string res = sendNetworkRequestHere(json);
            Regex rx = new Regex(@"[A-Za-z0-9]+\.[A-Za-z0-9]+\.[A-Za-z0-9]+.*");
            Assert.IsTrue(rx.Match(res).Success);
        }

        [Test]
        public void getCollectionArrayTest() {
            UserInfo info = new UserInfo(Constants.MY_ID, Constants.VALID_TOKEN, "getCollection");
            string json = JsonConvert.SerializeObject(info);
            string res = sendNetworkRequestHere(json);

            Assert.IsFalse(res.Contains("Error"));
        }

        [Test]
        public void addCardToCollectionTest() {
            AddCardRequest cardRequest = new AddCardRequest(Constants.MY_ID, Constants.VALID_TOKEN, "fake", "addCardToCollection");
            string json = JsonConvert.SerializeObject(cardRequest);
            string res = sendNetworkRequestHere(json);
            Assert.IsTrue(res.Contains("card added successfully"));
        }

        [Test]
        public void addDeckTest() {
            Random rnd = new Random();
            int val = rnd.Next(10000);

            string[] deckArr = { "card1-1", "card2-1", "card3-4", "card4-2" };

            Deck deck = new Deck("saveDeck", val.ToString(), Constants.MY_ID, deckArr);
            string json = JsonConvert.SerializeObject(deck);
            string res = sendNetworkRequestHere(json);

            Assert.IsTrue(res.Contains("Deck added successfully"));
        }

        [Test]
        public void getAllDecksTest() {
            GetAllDecksRequest req = new GetAllDecksRequest(Constants.MY_ID, Constants.VALID_TOKEN, "getAllDecks");
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);

            Assert.IsFalse(res.Contains("Error"));
        }

        [Test]
        public void getOneDeckTest() {
            GetOneDeckRequest req = new GetOneDeckRequest("7822", Constants.MY_ID, Constants.VALID_TOKEN, "getOneDeck");
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);
            Assert.IsFalse(res.Contains("Error"));
        }

        /* Delete a deck. Deckname must be valid. Will be necessary
         * to add a deck before running this test. */
        [Test]
        public void deleteDeckTest() {
            DeleteDeckRequest req = new DeleteDeckRequest("5399", Constants.MY_ID, Constants.VALID_TOKEN, "deleteDeck");
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);
            Assert.IsTrue(res.Contains("deck successfully deleted"));
        }

        /* Try to perform a command with an invalid token
         * Expected: invalid token error */
        [Test]
        public void invalidTokenTest() {
            GetOneDeckRequest req = new GetOneDeckRequest("7822", Constants.MY_ID, Constants.INVALID_TOKEN, "getOneDeck");
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);
            Assert.IsTrue(res.Contains("Invalid token"));
        }

        /* Send an email with a temporary password */
        [Test]
        public void getPassTest() {
            PassRequest req = new PassRequest(Constants.DOLPHIN, "tempPassword");
            string json = JsonConvert.SerializeObject(req);
            string res = sendNetworkRequestHere(json);
            Assert.IsTrue(res.Contains("Email sent with temporary password"));
        }

        /* In order to run this test, you must first run a getPassTest with the email you plug in here */
        [Test]
        public void tempPassTest() {
            User user = new User("login", Constants.DOLPHIN, "1k\"X=^|i.l", "username");
            string json = JsonConvert.SerializeObject(user);
            string res = sendNetworkRequestHere(json);
            Regex rx = new Regex(@"[A-Za-z0-9]+\.[A-Za-z0-9]+\.[A-Za-z0-9]+.*");
            Assert.IsTrue(rx.Match(res).Success);
        }

        public string sendNetworkRequestHere(string obj) {
            Int32 port = 8000;
            TcpClient client = new TcpClient("localhost", port);

            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

            Byte[] data = System.Text.Encoding.ASCII.GetBytes(obj);
            NetworkStream stream = client.GetStream();

            stream.Write(data, 0, data.Length);
            data = new Byte[256];
            string responseData = string.Empty;

            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine(responseData);

            client.Close();

            return responseData;
        }

        
    }

}