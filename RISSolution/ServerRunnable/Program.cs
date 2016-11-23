using System;
using System.ServiceModel;
using Services;

namespace ServerRunnable {

    public class Program {
        /**
        static ServiceHost host = null;

        static void StartService()
        {
            host = new ServiceHost(typeof(ServiceSprava));

            host.Open();
        }

        static void CloseService()
        {
            if (host.State != CommunicationState.Closed)
            {
                host.Close();
            }
        }

        static void Main(string[] args)
        {
            StartService();

            Console.WriteLine("RIS sprava is running....");
            Console.ReadKey();

            CloseService();
        }  
        */

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