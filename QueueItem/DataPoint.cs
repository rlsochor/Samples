//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Queue, DataPoint is basic implementation that captures
//* a single piece of data for writing. This is simply one 
//* way to implement
//*
//* Copyright 2015, all rights reserved
//************************************************************
using System;

namespace DataWriter.QueueItems
{
    /// <summary>
    /// References a single data point that may be written to a table, i.e., column value
    /// </summary>
    public class DataPoint
    {
        private object _value;
        private Type _valueType;
        public DataPoint(Type valueType, object value)
        {
            this._valueType = valueType;
            this._value = value;
        }
        public Type ValueType { get { return _valueType; } }
        public object Value { get { return _value; } }
    }
}
