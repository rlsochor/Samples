//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Data writer service. 
//*
//* Copyright 2015, all rights reserved
//************************************************************
using System.ServiceModel;
using System.ServiceProcess;
using System;

namespace DataWriter.Service
{
    public partial class DataWriterService : ServiceBase
    {
        /// <summary>
        /// array of SOAP services
        /// </summary>
        private ServiceHost[] _soapArray = null;

        /// <summary>
        /// Ctor for service
        /// </summary>
        public DataWriterService()
        {
            InitializeComponent();

            //load the soap service interfaces here, can be automated more using config file
            _soapArray = new ServiceHost[]
            {
                    new ServiceHost(typeof (DataWriter.SoapServices.DataWriterXMLSoapSvc)),
                    new ServiceHost(typeof (DataWriter.SoapServices.DataWriterJSONSoapSvc)),
            };
        }

        /// <summary>
        /// when starting up open (start listening) each SOAP service
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnStart(string[] arg)
        {
            try
            {
                //now 'open' the service
                foreach (ServiceHost sh in _soapArray)
                {
                    try
                    {
                        sh.Open();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG   //add the conditional symbol in the properties/build window to get rid of console code
                if (Environment.UserInteractive)
                {
                    Console.WriteLine("Soap service instantiation failed with error " + ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                else
                {
                    throw;
                }
#else
                throw;
#endif
            }
        }

        /// <summary>
        /// Close (terminate) each SOAP service
        /// </summary>
        protected override void OnStop()
        {
            foreach (ServiceHost sh in _soapArray)
            {
                try
                {
                    sh.Close();
                }
                catch (Exception ex)
                {
#if DEBUG
                    if (Environment.UserInteractive)
                    {
                        Console.WriteLine("Soap service shutdown failed with error " + ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }
                    else
                    {
                        //just eat it
                    }
#endif
                }
            }
        }


        /// <summary>
        /// public function to support running as console since OnStart is not visible
        /// </summary>
        /// <param name="args"></param>
        public void Start(string[] args)
        {
            OnStart(args);
        }

        /// <summary>
        /// public function to support running as console since OnStop is not visible
        /// </summary>
        public void Stop()
        {
            OnStop();
        }
    }
}
