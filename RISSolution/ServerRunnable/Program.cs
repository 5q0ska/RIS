using System;
using System.Net;
using System.ServiceModel;
using System.Threading;
using Services;

namespace RISServer {

    public class Program {

        public static void Main(string[] args) {

            var selfHost = new ServiceHost(typeof(ServiceStoly));
            var selfHost2 = new ServiceHost(typeof(ServiceSprava));

            string externalip = new WebClient().DownloadString("http://ipinfo.io/ip").Replace("\n","");

            var firstUri = selfHost.Description.Endpoints[0].Address.Uri.ToString().Replace("localhost", externalip);
            var secondUri = selfHost2.Description.Endpoints[0].Address.Uri.ToString()
                .Replace("localhost", externalip);
            Console.WriteLine(firstUri + " - copied to clipboard! Try ctrl+v");
            Console.WriteLine(secondUri);
            var th = new Thread(() =>
            {
                Clipboard.SetText(firstUri);
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            th.Join();

            try {
                selfHost.Open();
                selfHost2.Open();

                Console.WriteLine("The web service is running. Press <ENTER> to terminate service.");
                Console.ReadLine();

                selfHost2.Close();
                selfHost.Close();
            } catch (CommunicationException ce) {
                Console.WriteLine
                ("An exception occurred: {0}", ce.Message);
                selfHost2.Abort();
                selfHost.Abort();
            }
        }
    }
}