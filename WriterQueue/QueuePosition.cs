using System;

namespace DataWriter.Queues
{
    public struct QueuePosition
    {
        public int Position { get; set; }
        public DateTime TimeWritten { get; set; }
    }
}
