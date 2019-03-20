using System;
using System.Net;
using System.ServiceModel;
using System.Threading;
using Services;
using System.Windows.Forms;

namespace RISServerNew
{

    public class Program
    {

        public static void Main(string[] args)
        {

            var selfHost = new ServiceHost(typeof(ServiceStoly));

            string externalip = new WebClient().DownloadString("http://ipinfo.io/ip").Replace("\n", "");

            var firstUri = selfHost.Description.Endpoints[0].Address.Uri.ToString().Replace("localhost", externalip);
            Console.WriteLine(firstUri + " - copied to clipboard! Try ctrl+v");
            var th = new Thread(() =>
            {
                Clipboard.SetText(firstUri);
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            th.Join();

            try
            {
                selfHost.Open();

                Console.WriteLine("The web service is running. Press <ENTER> to terminate service.");
                Console.ReadLine();
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine
                ("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }

            Console.ReadLine();
        }
    }
}