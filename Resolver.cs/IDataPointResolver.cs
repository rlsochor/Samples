//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Data persister resolver, interface defining 
//* data mapping from queue item to well known item 
//* defined by persistor
//*
//* Copyright 2015, all rights reserved
//************************************************************

namespace DataWriter.Resolvers
{
    public interface IQueueDataResolver<Q,R> where Q: class
    {       
        R Resolver(Q QueueData);
    }
}
