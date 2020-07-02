# Chapter 13 - Multitasking

---

## Understanding processes, threads, and tasks

Threads execute code in a process. By default, each process only has one thread.
Threads keep track of different parts of the process, such as user auth. and internationalization.

Windows uses **preemptive multitasking** divides the processor time among the threads, allocating **time slices** to each thread.

Windows saves thread context in the **thread queue** when it switches to different threads.

All threads have **[Priority](https://docs.microsoft.com/en-us/dotnet/api/system.threading.thread.priority?view=netcore-3.1)** and **[ThreadState](https://docs.microsoft.com/en-us/dotnet/api/system.threading.thread.threadstate?view=netcore-3.1)** _properties_ along with a **ThreadPool** _class_ for managing multiple background threads.

The **[ThreadPool Class](https://docs.microsoft.com/en-us/dotnet/standard/threading/the-managed-thread-pool)** provides a pool of worker threads managed by the system, allowing the developer to focus on the task of the application.
_-All ThreadPool threads are background threads and run at default priority-_

---

## Evaluating Efficiency

Efficiency can be measured by the following factors:

- Functionality
- Memory Size
- Performance
- Future Needs

The best way to know what types of data would work best for your application would be to test each type yourself and test its result.
For instance a short might be valid for now but an int for future uses of the program.

### Example

**String.Concat** and **StringBuilder** are two ways to create strings. In the situation in MonitoringApp, **StringBuilder** is nearly _1,000x_ faster and _10,000x_ more efficient at concatenating text.

---

## Running Asynchronously

The **Thread** class was available since the release of .NET but can be tricky to with with directly.
This has been helped in 2010 with .NET 4.0 with the **Task** class to act as a wrapper around a thread that enables easier creating and management of threads. Using this will allow code to run asynchronously.

But what is one thread is requiring the output of another?
This is when **continuation tasks** are needed. This will allow the second task to run only after getting a result from the first.
