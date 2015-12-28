using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataWriter.Queues;
using DataWriter.QueueItems;


namespace DataWriterTests
{
    [TestClass]
    public class QueueTests
    {
        [TestMethod]
        public void CreateInjectionManager()
        {
            InjectionManager<QueueItem> imTest = new InjectionManager<QueueItem>("TestQueue");
            Assert.IsNotNull(imTest);
        }

        [TestMethod]
        public void CreateQueueManagerWriteNullItem()
        {
            InjectionManager<QueueItem> imTest = new InjectionManager<QueueItem>("TestQueue");
            QueuePosition qp = imTest.InjectDataItem(new QueueItem());
            Assert.AreNotEqual(qp.Position, 0);
            Assert.AreNotEqual(qp.Position, -1);
        }
    }
}
