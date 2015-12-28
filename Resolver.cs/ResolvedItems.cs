//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Data Resolver, maps Queue item to a class that the 
//* persistent engine can use. Each resolved item maps to a single
//* data point in the Queue Item list.
//*
//* Copyright 2015, all rights reserved
//************************************************************
namespace DataWriter.Resolvers
{
    using System;
    using QueueItems;

    public class ResolvedItem
    {
        string _name;
        Type _dataType;
        object _dataValue;

        public ResolvedItem(DataPoint Dp, string Key)
        {
            this._name = Key;
            this._dataType = Dp.ValueType;
            this._dataValue = Dp.Value;
        }

        public string ResolvedName { get { return _name; } }
        public int AsInt {
            get { if (_dataType.Equals(typeof (int))) return Convert.ToInt32(_dataValue); else throw new InvalidCastException("Item is not of type int"); }
        }

        public string AsString {
            get { if (_dataType.Equals(typeof(string))) return Convert.ToString(_dataValue); else throw new InvalidCastException("Item is not of type string"); }
        }

        public decimal AsDecimal {
            get { if (_dataType.Equals(typeof(decimal))) return Convert.ToDecimal(_dataValue); else throw new InvalidCastException("Item is not of type decimal"); }
        }

        public DateTime AsDateTime {
            get { if (_dataType.Equals(typeof(DateTime))) return Convert.ToDateTime(_dataValue); else throw new InvalidCastException("Item is not of type DateTime"); }
        }

        public float AsFloat {
            get { if (_dataType.Equals(typeof(float))) return Convert.ToSingle(_dataValue); else throw new InvalidCastException("Item is not of type float"); }
        }
        public bool AsBool
        {
            get { if (_dataType.Equals(typeof(bool))) return Convert.ToBoolean(_dataValue); else throw new InvalidCastException("Item is not of type boolean"); }
        }

        public bool IsInt {
            get { return _dataType.Equals(typeof (int)); }
        }

        public bool IsString {
            get { return _dataType.Equals(typeof(string)); }
        }

        public bool IsDecimal {
            get { return _dataType.Equals(typeof(decimal)); }
        }

        public bool IsDateTime {
            get { return _dataType.Equals(typeof(DateTime)); }
        }

        public bool IsFloat {
            get { return _dataType.Equals(typeof(float)); }
        }

        public bool IsBool
        {
            get { return _dataType.Equals(typeof(bool)); }
        }
    }
}
