//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: SOAP service interface for accepting JSON 
//*         formatted data
//*
//* Copyright 2015, all rights reserved
//************************************************************
using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using DataWriter.QueueItems;
using DataWriter.Mapper;
using DataWriter.SharedTypes;
using DataWriter.Queues;

namespace DataWriter.SoapServices
{
    /// <summary>
    /// Public contract inheriting from strongly typed base contract
    /// </summary>
    [ServiceContract]
    public interface IDataWriterJSONSoapSvc : IDataWriterSoapSvc<JSONData> { }
    /// <summary>
    /// Implementation SOAP service for JSON data. Implements abstract class 
    /// strongly typing output of associated mapper
    /// </summary>
    public class DataWriterJSONSoapSvc: DataMapperService<QueueItem>,  IDataWriterJSONSoapSvc
    {
        /// <summary>
        /// Ctor passing default type of mapper for factory to instantiate
        /// </summary>
        public DataWriterJSONSoapSvc() : base(typeof (JsonInjector)) { }

        /// <summary>
        /// Default ping method for testing availability of interface
        /// </summary>
        /// <returns>true when service is available</returns>
        public bool Ping()
        {
            return true;
        }

        /// <summary>
        /// Write data to the service for persistence
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteData(JSONData value)
        {
            try
            {
#if DEBUG
                Console.WriteLine(value.TimeSent.ToString());
#endif
                //call the data mapper
                int qp = Injector.InjectData(Injector.MapData(value));
                return qp > 0;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                throw;
            }
        }
    }

    /// <summary>
    /// Sample type to push across to service, just for discussion
    /// </summary>
    [DataContract]
    public class JSONData
    {

        DateTime _sent = DateTime.Now;
        string _data = null;

        [DataMember]
        public DateTime TimeSent
        {
            get { return _sent; }
            set { _sent = value; }
        }

        [DataMember]
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }

    /// <summary>
    /// The injector creates an item to place in the queue and then 'injects' that
    /// item into the Queue. 
    /// This class is specialized and need to understand the structure of
    /// the queued data.
    /// </summary>
    public class JsonInjector: IDataInjector<QueueItem>
    {
        private readonly string _queueName = "JsonQueue";
        private InjectionManager<QueueItem> _injectionPoint = null;

        public JsonInjector()
        {
            _injectionPoint = new InjectionManager<QueueItem>(_queueName);
        }

        public int InjectData(QueueItem Item)
        {
            QueuePosition qp = _injectionPoint.InjectDataItem(Item);
#if DEBUG
            Console.WriteLine("Queue: Position=" + qp.Position.ToString() + ", TimeWritten=" + qp.TimeWritten.ToString());
#endif
            return qp.Position;
        }

        public QueueItem MapData(object DataIn)
        {
            try
            {
                JSONData data = (JSONData)DataIn;
                SampleClass jd1 = (SampleClass)JSONHelper.Deserialize(data.Data, typeof(SampleClass));
                //do the queue building here
                QueueItem qi = new QueueItem();
                qi.AddDataItem("CounterId", new DataPoint(typeof(string), jd1.CounterId));
                qi.AddDataItem("CounterValue", new DataPoint(typeof(int), jd1.CounterValue));
                qi.AddDataItem("CounterTime", new DataPoint(typeof(DateTime), jd1.CounterTime));
                qi.AddDataItem("CounterReset", new DataPoint(typeof(bool), jd1.CounterReset));
                return qi;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                throw;
            }
        }
    }
}
