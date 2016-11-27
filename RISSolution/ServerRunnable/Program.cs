using System;
using System.ServiceModel;
using Services;

namespace RISServer {

    public class Program {

        public static void Main(string[] args) {

            var selfHost = new ServiceHost(typeof(ServiceStoly));

            try {
                selfHost.Open();
                Console.WriteLine("The service is ready. Press <ENTER> to terminate service.");
                Console.ReadLine();

                selfHost.Close();
            } catch (CommunicationException ce) {
                Console.WriteLine
                ("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }
    }
}