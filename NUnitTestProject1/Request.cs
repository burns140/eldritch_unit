using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject1 {
    public class Request {
        public string id;
        public string token;
        public string cmd;

        public Request(string id, string token, string cmd) {
            this.id = id;
            this.token = token;
            this.cmd = cmd;
        }

        public Request(string cmd) {
            this.id = "";
            this.token = "";
            this.cmd = cmd;
        }

        public virtual string sendNetworkRequest(string obj) {
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
