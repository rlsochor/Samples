//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: SOAP Service accepting XML package as input
//*
//* Copyright 2015, all rights reserved
//************************************************************
using System;
using System.Xml;
using System.Runtime.Serialization;
using System.ServiceModel;
using DataWriter.Mapper;
using DataWriter.QueueItems;

namespace DataWriter.SoapServices
{
    [ServiceContract]
    public interface IDataWriterXMLSoapSvc : IDataWriterSoapSvc<XmlData> { }

    public class DataWriterXMLSoapSvc : DataMapperService<QueueItem>, IDataWriterXMLSoapSvc
    {
        public DataWriterXMLSoapSvc():base(typeof (XmlInjector)) { }
        public bool Ping()
        {
            return true;
        }
        public bool WriteData(XmlData value)
        {
            try
            {

                //call the data mapper
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    [DataContract]
    public class XmlData
    {
        DateTime _sent = DateTime.Now;
        XmlDocument _data = null;

        [DataMember]
        public DateTime TimeSent
        {
            get { return _sent; }
            set { _sent = value; }
        }

        [DataMember]
        public XmlDocument Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }

    /// <summary>
    /// Mapper/Queue injector, not implemented yet
    /// </summary>
    public class XmlInjector: IDataInjector<QueueItem>
    {
        public XmlInjector() { }

        public int InjectData(QueueItem Item)
        {
            throw new NotImplementedException();
        }
        public QueueItem MapData(object DataIn)
        {
            throw new NotImplementedException();
        }
    }
}
