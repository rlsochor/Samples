# Sample Code
Sample code for public distribution

1. The code in this sample is an example of publishing multiple servics, allowing each service to write through to different (or the same) 
persistent storage. The idea was to do 2 things. First, create something that could handle both realtime calls and other type of requests
that are not time sensitive. As is, it is only one way but retreiving data would simply be reversing which side read and write the queue.
Second, create something that was maintainable, changeable and extensible. When you look at it there really is much code, just a bunch 
of generic classes. Most of the 'code' is translating the data from one form to another. I will try to finish up today if possible.
2. Only one service writes through at this time. 
3. I think the code should go through 1-2 more refactorings prior to begin ready for testing. That means given two more passes I can 
have it pretty much as I think it should be, decoupled yet cohesive. Refactoring should get it so it is pretty close to the open/closed
principle. There are a couple of places it needs some work. Also, with a little experimentation I can get Ninject working with the service constructors and that would go a long way to 
decoupling the code. 
4. It took about 20 hours to do.

How it is supposed to work.

There are two SOAP services defined that are self-hosted in a Windows service. The service can run as a console app for debugging 
purposes or be installed as an actual service. The ServiceHost creation is not the best, given a little of work it can reflect of 
the config file and create the services dynamically removing the need for the hard coded constructors.

Each SOAP service accepts some inbound data type. The data type selection was arbitrary and does not impact the overall design. 
Each service implementation contains an injector class that is created by a factory in the service constructor. The injector takes 
the inbound data and converts it to a class that can be written to a queue. Again the data written to the queue is arbitrary and has
no impact on the design.

Each injector can own a queue or write to a queue shared with another injector. That would depend on how the data is persisted. 
The data queue is a wrapper around the generic Queue class. They in turn are contained in a queue manager as singletons (as is 
the queue manager). The queue manager is specialized for inserting into the queue or reading from the queue.

So data comes in, gets converted and gets written to a queue.

On the other side of the queue is the exact reverse. There is a reader, mapper/resolver and persister. This side should be threaded. 
The reader gets an item out of the queue, gives it to the resolver that converts it to a form the persister can use. The persister 
class, for instance something implementing SQL Server, can write to the data store.
