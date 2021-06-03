using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace text_from_photo_master
{
    class Sender
    {
        private List<TcpClient> clients = new List<TcpClient>();
        private string fileName;
        private string message;

        public Sender(string fileName)
        {
            this.fileName = fileName;
        }

        public void Send()
        {
<<<<<<< Updated upstream
            try
            {
                IPAddress receiverIP = IPAddress.Parse("192.168.1.149");
                IPEndPoint endPoint = new IPEndPoint(receiverIP, 2021);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
=======
            IPAddress receiverIP = IPAddress.Parse("10.16.5.65"); 
            IPEndPoint endPoint = new IPEndPoint(receiverIP, 2021); 
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
>>>>>>> Stashed changes

                byte[] fileNameBytes = Encoding.ASCII.GetBytes(Path.GetFileName(fileName));
                byte[] fileNameLength = BitConverter.GetBytes(Path.GetFileName(fileName).Length);
                byte[] fullBuffer = new byte[4 + fileNameBytes.Length];

                fileNameLength.CopyTo(fullBuffer, 0);
                fileNameBytes.CopyTo(fullBuffer, 4);

                socket.Connect(endPoint);
                socket.SendFile(fileName, fullBuffer, null, TransmitFileOptions.UseDefaultWorkerThread);
                socket.Close();
            }
            catch
            {
                Console.WriteLine("Could not connect to receiver!");
            }
            
        }
    }
}
