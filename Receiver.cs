using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace text_from_photo_master
{
    public class Receiver
    {
        private static TcpListener listener = null;

        private string downloadsFolder;
        public static string Message = "Stopped";
        private Iupdatable updatable;

        public Receiver(string downloadsFolder, Iupdatable updatable)
        {
            this.updatable = updatable;
            this.downloadsFolder = downloadsFolder;
            if (listener == null)
                listener = new TcpListener(IPAddress.Any, 2020);
        }

        public void Start()
        {
            try
            {
                listener.Start();
                Console.WriteLine("Receiving started!");
                while (true)
                {
                    using (var client = listener.AcceptTcpClient())
                    using (var stream = client.GetStream())
                    {
                        byte[] fileNameLengthBytes = new byte[4];
                        stream.Read(fileNameLengthBytes, 0, 4);
                        int fileNameLength = BitConverter.ToInt32(fileNameLengthBytes, 0);
                        byte[] fileNameBytes = new byte[fileNameLength];
                        stream.Read(fileNameBytes, 0, fileNameLength);
                        string fileName = Encoding.ASCII.GetString(fileNameBytes, 0, fileNameLength);

                        string file = $"{downloadsFolder}{fileName}";

                        using (var output = File.Create(file))
                        {
                            Console.WriteLine($"Saving file...");
                            var buffer = new byte[1024];
                            int bytesRead;
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                // progresui ideali vieta
                                output.Write(buffer, 0, bytesRead);
                            }
                        }
                        Console.WriteLine($"File saved [{fileName}]...");
                        
                    }
                }
            }
            catch (Exception exc)
            {
                if (listener != null)
                    listener.Stop();
                Console.WriteLine(exc.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
                Console.WriteLine("Receiving stopped!");
            }
        }

        public void Stop()
        {
            if (listener != null)
                listener.Stop();
            listener = null;
        }
    }
}
