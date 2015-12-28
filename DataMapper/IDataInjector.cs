//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Data injector/mapper interface. Takes the queue item
//* being mapped to as 'T'
//*
//* Copyright 2015, all rights reserved
//************************************************************
namespace DataWriter.Mapper
{
    public interface IDataInjector<T>
    {
       T MapData(object DataIn);
        int InjectData(T Item);
    }
}
