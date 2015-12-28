//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Shared Types, common types passed across boundaries
//*
//* Copyright 2015, all rights reserved
//************************************************************
using System;
using System.Runtime.Serialization;

namespace DataWriter.SharedTypes
{
    /// <summary>
    /// Sample data type passed to SOAP service
    /// </summary>
    [DataContract]
    public class SampleClass
    {
        [DataMember]
        public string CounterId { get; set; }
        [DataMember]
        public int CounterValue { get; set; }
        [DataMember]
        public DateTime CounterTime { get; set; }
        [DataMember]
        public bool CounterReset { get; set; }
    }
}
