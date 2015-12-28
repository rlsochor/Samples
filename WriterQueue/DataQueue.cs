//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Queue, DataQueue is wrapper around generic Queue 
//*
//* Copyright 2015, all rights reserved
//************************************************************
using System;
using System.Collections.Generic;

namespace DataWriter.Queues
{
    /// <summary>
    /// Generic Queue wrapper. T is the type being pushed onto the queue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataQueue<T>: IDataQueue where T: class
    {
        private Queue<T> _theQueue;
        private string _queueName = null;
        private object enq_lock = new object();
        private object deq_lock = new object();

        /// <summary>
        /// Queue has a unique name for external id
        /// </summary>
        public string Name
        {
            get { return _queueName; }
            set
            {
                if (_queueName == null)
                {
                    _queueName = value;
                }
                else
                {
                    throw new Exception("cannot reassign queue name");
                }
            }
        }

        /// <summary>
        /// number of items in queue
        /// </summary>
        public int Count
        {
            get { return _theQueue.Count; }
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public DataQueue()
        {
            _theQueue = new Queue<T>();
        }

        /// <summary>
        /// Proper ctor supporting naming the queue
        /// </summary>
        /// <param name="QueueName"></param>
        public DataQueue(string QueueName)
        {
            _queueName = QueueName;
            _theQueue = new Queue<T>();
        }

        /// <summary>
        /// Public implementation supporting interface
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public QueuePosition WriteQueue(object data)
        {
            return WriteQueueType(data as T);
        }

        /// <summary>
        /// Public implementation supporting interface
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public object ReadQueue()
        {
            return ReadQueueType();
        }

        /// <summary>
        /// Actual write function, has critical section to ensure the write is atomic
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private QueuePosition WriteQueueType(T data)
        {
            QueuePosition result = new QueuePosition() {Position = -1, TimeWritten = DateTime.MinValue};
            lock (enq_lock)
            {
                _theQueue.Enqueue(data);
                result.Position = _theQueue.Count;
                result.TimeWritten = DateTime.Now;
            }
            return result;
        }

        /// <summary>
        /// Actual read function, has critical section to ensure the read is atomic
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private T ReadQueueType()
        {
            lock (deq_lock)
            {
                if (_theQueue.Count == 0)
                    return default(T);
                else
                    return _theQueue.Dequeue();
            }
        }
    }
}
