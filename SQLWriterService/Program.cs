using System;
using System.ServiceProcess;
using DataWriter.Service;

namespace SQLWriterService
{
    /// <summary>
    /// Primary program running the services
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {

#if !REMOVE_TEST //add the conditional symbol in the properties/build window to get rid of console code
            //by default it will run as windows console app. 
            //To run as windows service it has to be installed as service, then started (e.g., NET Start...)
            if (Environment.UserInteractive)
            {
                try
                {
                    DataWriterService _dataWriter = new DataWriterService();
                    Console.WriteLine("Starting SOAP services...");
                    _dataWriter.Start(args);
                    Console.WriteLine("SOAP services running...press any key to stop");
                    Console.Read();
                    Console.WriteLine("Stopping SOAP services...");
                    _dataWriter.Stop();
                }
                catch (Exception ex)
                {
                        Console.WriteLine("Soap service failed with error " + ex.Message);
                        Console.WriteLine(ex.StackTrace);
                }
            }
            else //default, runs as windows service when installed as service
            {
#endif
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new DataWriterService()
                };
                ServiceBase.Run(ServicesToRun);
#if !REMOVE_TEST
            }
#endif
        }
    }
}
