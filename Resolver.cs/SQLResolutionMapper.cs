//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Persistence engine, in this case writing to SQL database. 
//* To be finished.
//*
//* Copyright 2015, all rights reserved
//************************************************************
using System.Collections.Generic;
using DataWriter.QueueItems;

namespace DataWriter.Resolvers
{
    public interface ISQLResolutionMapper: IQueueDataResolver<QueueItem, List<ResolvedItem>>
    {
        List<SQLMappedItem> ResolvedItemToMappedItem(List<ResolvedItem> Items);
    }

    public class SQLResolutionMapper: ISQLResolutionMapper
    {
        public SQLResolutionMapper()
        {
        }

        public List<SQLMappedItem> ResolvedItemToMappedItem(List<ResolvedItem> Items)
        {
            List<SQLMappedItem> result = new List<SQLMappedItem>();
            foreach (ResolvedItem ri in Items)
            {
                result.Add(new SQLMappedItem(ri));
            }
            return result;
        }

        public List<ResolvedItem> Resolver(QueueItem Item)
        {
            List<ResolvedItem> result = new List<ResolvedItem>();

            foreach (var item in Item.GetAllKeys())
            {
                result.Add(new ResolvedItem(Item.GetDataPoint(item) as DataPoint, item));
            }
            return result;
        }
    }
}
