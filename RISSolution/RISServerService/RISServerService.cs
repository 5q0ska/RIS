using System.ServiceProcess;
using System.ServiceModel;
using Services;

namespace RISServerService
{
    public partial class RISServerService : ServiceBase
    {
        private ServiceHost serviceStoly;

        public RISServerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (serviceStoly != null)
            {
                serviceStoly.Close();
            }

            // Create a ServiceHost for the CalculatorService type and 
            // provide the base address.
            serviceStoly = new ServiceHost(typeof(ServiceStoly));

            // Open the ServiceHostBase to create listeners and start 
            // listening for messages.
            serviceStoly.Open();           
        }

        protected override void OnStop()
        {
            if (serviceStoly != null)
            {
                serviceStoly.Close();
                serviceStoly = null;
            }
        }
    }
}
