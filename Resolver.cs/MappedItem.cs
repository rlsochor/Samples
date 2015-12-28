//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Item used by SQL implementation taking type and 
//* converting to SQL data types. This would be used for
//* dynamic creation of sql statements. (TBD)
//*
//* Copyright 2015, all rights reserved
//************************************************************
using System;
using System.Data.SqlTypes;

namespace DataWriter.Resolvers
{
    public class SQLMappedItem
    {
        private ResolvedItem _item; 
        public SQLMappedItem(ResolvedItem Item)
        {
            this._item = Item;

            if (_item.IsInt)
                ColumnType = typeof(SqlInt32);
            else if (_item.IsString)
                ColumnType = typeof (SqlString);
            else if (_item.IsBool)
                ColumnType = typeof(SqlBoolean);
            else if (_item.IsDecimal)
                ColumnType = typeof(SqlDecimal);
            else if (_item.IsFloat)
                ColumnType = typeof(SqlDouble);
            else if (_item.IsDateTime)
                ColumnType = typeof (SqlDateTime);
            else
                ColumnType = typeof (SqlString);

        }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public Type ColumnType { get; }
        public ResolvedItem Item { get; }

        public string GetAsSQLValue()
        {
            if (ColumnType.Equals(typeof(SqlString)))
                return Quoted(Item.AsString);
            else if (ColumnType.Equals(typeof(SqlBoolean)))
                return ((SqlInt32)Item.AsInt).ToString();
            else if (ColumnType.Equals(typeof(SqlBoolean)))
                return (Item.AsBool?1:0).ToString();
            else if (ColumnType.Equals(typeof(SqlDateTime)))
                return ((SqlDateTime)Item.AsDateTime).ToString();
            else if (ColumnType.Equals(typeof(SqlDecimal)))
                return ((SqlDecimal)Item.AsDecimal).ToString();
            else if (ColumnType.Equals(typeof (SqlDouble)))
                return ((SqlDouble) Item.AsFloat).ToString();
            else
                return Quoted(Item.AsString);
        }

        private string Quoted(string value)
        {
            return string.Concat("'",value,"'");
        }
    }
}
