using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace NUnitTestProject1 {
    class Class1 {

        public void Main() {
            Int32 port = 8000;
            TcpClient client = new TcpClient("localhost", port);
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            NetworkStream stream = client.GetStream();
            Socket sock = client.Client;

            while (true) {
                sock.Listen(500);
                Socket accepted = sock.Accept();
                Byte[] data = new byte[accepted.SendBufferSize];
                int bytesRead = accepted.Receive(data);

                byte[] formatted = new byte[bytesRead];
                for (int i = 0; i < bytesRead; i++) {
                    formatted[i] = data[i];
                }

                string strData = Encoding.ASCII.GetString(formatted);
                Console.WriteLine(strData);
            }
            

   
        }
    }
}
