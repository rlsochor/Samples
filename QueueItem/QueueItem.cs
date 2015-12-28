//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Queue, this is the data type written to the queue.
//* The queue item can ultimately be any type. In this case the
//* contains a list of individual data points. This allows the
//* type to support multiple inbound/outbound types.
//*
//* Copyright 2015, all rights reserved
//************************************************************
using System;
using System.Collections;
using System.Linq;

namespace DataWriter.QueueItems
{
    public class QueueItem : IQueueItem<string, DataPoint>
    {
        private Hashtable dataItems;
        private object updates = new object();

        public QueueItem()
        {
            dataItems = new Hashtable();
        }

        /// <summary>
        /// Adds a data point to the collection of data
        /// </summary>
        /// <param name="Name">key into the collection</param>
        /// <param name="Data">data point to add</param>
        public void AddDataItem(string Name, DataPoint Data)
        {
            //need to use critical section to prevent multiple items with same key from being added
            lock (updates)
            {
                if (!string.IsNullOrEmpty(Name) && IsValidData(Data))
                {
                    dataItems.Add(Name, Data);
                }
                else
                {
                    throw new ArgumentException("Both a data point name and valid data point must be provided.");
                }
            }
        }

        public IQueryable<DataPoint> GetAllDataItems()
        {
            return dataItems.Values.AsQueryable() as IQueryable<DataPoint>;
        }

        public IQueryable<string> GetAllKeys()
        {
            return dataItems.Keys.AsQueryable() as IQueryable<string>;
        }

        public object GetDataPoint(string Name)
        {
            if (string.IsNullOrEmpty(Name) || !dataItems.ContainsKey(Name))
            {
                return null;
            }
            else
            {
                return (dataItems[Name] as DataPoint).Value;
            }
        }

        private bool IsValidData(DataPoint dp)
        {
            return dp.Value != null && dp.ValueType != null;
        }

    }
}
