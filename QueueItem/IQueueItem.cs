using System.Linq;

namespace DataWriter.QueueItems
{
    public interface IQueueItem<K,T>
    {
        void AddDataItem(K Key, T data);
        object GetDataPoint(K Key);

        IQueryable<K> GetAllKeys();
        IQueryable<T> GetAllDataItems();
    }
}
