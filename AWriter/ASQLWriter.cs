//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Data persistent implementation using SQL Server
//*
//* Copyright 2015, all rights reserved
//************************************************************
using DataWriter.Resolvers;

namespace DataWriter.SQLWriter
{
    public class ASQLWriter 
    {
        private ISQLResolutionMapper _mapper;

        public ASQLWriter(ISQLResolutionMapper Mapper)
        {
            _mapper = Mapper;
        }
    }
}
