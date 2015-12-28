//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Interface for reading/writing a data queue
//*
//* Copyright 2015, all rights reserved
//************************************************************
using System;
using DataWriter.QueueItems;

namespace DataWriter.Queues
{
    public interface IDataQueue
    {
        /// <summary>
        /// need to be able to set the queue name outside of constructor. 
        /// This needs to change to assign names in a better fashion
        /// </summary>
        string Name { get; set; }
        int Count { get; }
        QueuePosition WriteQueue(object data);
        object ReadQueue();
    }

}
