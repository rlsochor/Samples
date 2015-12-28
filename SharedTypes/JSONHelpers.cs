//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Shared Types, JSON; serializes/deserializes JSON
//* data. Used by the SOAP service to deserialize into object,
//* using by test driver to serialize JSON object
//*
//* Copyright 2015, all rights reserved
//************************************************************
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace DataWriter.SharedTypes
{
    public static class JSONHelper
    {
        public static object Deserialize(string Value, Type JSONObject)
        {
            object result = null;

            using (MemoryStream _stream = new MemoryStream(UTF8Encoding.Default.GetBytes(Value)))
            {
                _stream.Position = 0;
                if (_stream.Length > 0)
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(JSONObject);
                    result = serializer.ReadObject(_stream);
                }
            }
            return result;
        }

        public static string Serialize(Type type, object Item)
        {
            try
            {
                MemoryStream _stream = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(type);
                serializer.WriteObject(_stream, Item);
                string result = Encoding.UTF8.GetString(_stream.ToArray());
                return result;
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
