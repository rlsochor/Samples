//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Data mapper factory
//*
//* Copyright 2015, all rights reserved
//************************************************************
using System;
using System.Reflection;

namespace DataWriter.Mapper
{
    /// <summary>
    /// abstract class supporting the factory creation
    /// of mapper and injector
    /// </summary>
    /// <typeparam name="T">Queue item type that gets mapped to</typeparam>
    public abstract class DataMapperService<T>
    {
        protected IDataInjector<T> Injector;

        protected DataMapperService(Type F)
        {
            Injector = DataMapperFactory<T>.GetFactory(F, Assembly.GetCallingAssembly().FullName);
        }
    }

    /// <summary>
    /// DataMapper factory
    /// </summary>
    /// <typeparam name="T">Queue type being mapped to</typeparam>
    public static class DataMapperFactory<T>
    {
        public static IDataInjector<T> GetFactory(Type F, string FromAssembly = null)
        {
            //generally need to supply the calling assembly name to allow moving 
            //code around
            if (string.IsNullOrWhiteSpace(FromAssembly))
                return Assembly.GetExecutingAssembly().CreateInstance(F.FullName) as IDataInjector<T>;
            else
                return Assembly.Load(FromAssembly).CreateInstance(F.FullName) as IDataInjector<T>;
        }
    }
}
