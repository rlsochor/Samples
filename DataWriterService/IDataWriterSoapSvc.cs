//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: SOAP service default interface, defines the 
//* default interface and the type that can be written
//*
//* Copyright 2015, all rights reserved
//************************************************************
using System.ServiceModel;

namespace DataWriter.SoapServices
{
    [ServiceContract]    
    public interface IDataWriterSoapSvc<T> where T: class
    {
        [OperationContract]
        bool Ping();

        [OperationContract]
        bool WriteData(T value);
    } 
}
