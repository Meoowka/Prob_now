using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Prob_now.Entities
{
    public class FtpConnectionHandler
    {
        public FtpClient client = new FtpClient("", "", "");

        public void ConnectFtp(string host, NetworkCredential credential)
        {
            client.Host = host;
            client.Credentials = credential;
            client.Connect();
        }

        public void Disconnect()
        {
            client!.Disconnect();
        }
    }
}
