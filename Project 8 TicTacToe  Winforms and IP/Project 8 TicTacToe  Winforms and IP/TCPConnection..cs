using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project_8_TicTacToe__Winforms_and_IP
{
    class TCPConnection
    {
        TcpClient connection;

        public async Task Host_Game()
        {

            AddToMessageBox("No listener found, opening listener.");
            //TCPListener acts like a server, will get a host name from the first or default in the address list and set that address to internetwork
            TcpListener listener = new TcpListener(Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork), 5555);
            listener.Start();
            connection = await listener.AcceptTcpClientAsync();
            await Task.Factory.StartNew(() => ListenForPacket(connection));
            listener.Stop();
            return;

        }

        public async Task Join_Game(string ipAddress)
        {

            if (IPAddress.TryParse(ipAddress, out var address))
            {
                connection = new TcpClient(new IPEndPoint(address,5555));
                AddToMessageBox("Listener found, connection successful.");
                await Task.Factory.StartNew(() => ListenForPacket(connection));
            }
        }

        private void AddToMessageBox(string v)
        {
        }

        private void Button_SendPing_Click(object sender, EventArgs e)
        {
            SendMessage(connection, DateTime.Now.ToLongTimeString());
        }

        private void ListenForPacket(TcpClient singleConnection)
        {
            NetworkStream stream = singleConnection.GetStream();
            while (true)
            {
                byte[] bytesToRead = new byte[singleConnection.ReceiveBufferSize];
                int bytesRead = stream.Read(bytesToRead, 0, singleConnection.ReceiveBufferSize);
                string result = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                if (result != "")
                {
                    AddToMessageBox(result);
                }
            }
        }

        private void SendMessage(TcpClient singleConnection, string s)
        {
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(s);
            singleConnection.GetStream().Write(bytesToSend, 0, bytesToSend.Length);
        }
    }
}
