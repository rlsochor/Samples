//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Queue Manager is a wrapper around the data queue class.
//* The InjectionManager and RequestManager are specializations that
//* either hide the read/write. 
//* NOTE: Queue manager could wrap the actual Queue object and the
//* specializations can be done away with
//*
//* Copyright 2015, all rights reserved
//************************************************************
using System;
using System.Collections.Generic;

namespace DataWriter.Queues
{
    /// <summary>
    /// Only for writing to the queue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InjectionManager<T> where T: class
    {
        private string _queueName = null;
        private QueueManager qm = null;

        public InjectionManager(string QueueName)
        {
            this._queueName = QueueName;
            qm = QueueManager.Instance;
            qm.NewQueue(typeof(T), QueueName);
        }

        public QueuePosition InjectDataItem(T Item)
        {
            try
            {
                return qm.EnqueueItem(_queueName, Item);
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
    /// Only for reading from the queue, T is the type returned from the queue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RequestManager<T> where T : class
    {
        private string _queueName = null;
        private QueueManager qm = null;

        public RequestManager(string QueueName)
        {
            this._queueName = QueueName;
            qm = QueueManager.Instance;

            //even though this is a reader, it is possible that this
            //gets instantiated first
            qm.NewQueue(typeof(T), QueueName);
        }

        public T RequestDataItem()
        {
            try
            {
                return qm.DequeueItem(_queueName) as T;
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
    /// Queue data wrapper. Implemented as singleton to 
    /// ensure there is only one of each type of queue
    /// </summary>
    internal class QueueManager
    {
        private static object _locker = new object();
        private static Dictionary<string, IDataQueue> _dataQueues = null;
        private static QueueManager _instance; 

        private QueueManager()
        {
            _dataQueues = new Dictionary<string, IDataQueue>();
        }
        public static QueueManager Instance
        {
            get
            {
                if (_instance == null)
                { 
                    _instance = new QueueManager();
                }
                return _instance;
            }
            
        }

        /// <summary>
        /// Creates a new queue if it doesn't exist, otherwise returns the existing queue
        /// from the dictionary. Creates the queue using reflection, this eliminates having
        /// to know each data type used by each queue
        /// </summary>
        /// <param name="QueueType"></param>
        /// <param name="QueueName"></param>
        /// <returns></returns>
        public IDataQueue NewQueue(Type QueueType, string QueueName)
        {
            lock (_locker)
            {
                if (_dataQueues.ContainsKey(QueueName))
                    return _dataQueues[QueueName];
                else
                {
                    Type qt = typeof (DataQueue<>);
                    Type[] typeArgs = {QueueType};
                    Type preq = qt.MakeGenericType(typeArgs);
                    object q = Activator.CreateInstance(preq);
                    _dataQueues.Add(QueueName, q as IDataQueue);
                    (q as IDataQueue).Name = QueueName;
                    return _dataQueues[QueueName];
                }
            }
        }

        public QueuePosition EnqueueItem(string QueueName, object Item)
        {
            return _dataQueues[QueueName].WriteQueue(Item);
        }
        public object DequeueItem(string QueueName)
        {
            return _dataQueues[QueueName].ReadQueue();
        }
    }
}
