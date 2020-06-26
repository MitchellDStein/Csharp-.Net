# Chapter 13 - Multitasking

___

### Understanding processes, threads, and tasks

Threads execute code in a process. By default, each process only has one thread.
Threads keep track of different parts of the process, such as user auth. and internationalization.

Windows uses **preemptive multitasking** divides the processor time among the threads, allocating **time slices** to each thread.

Windows saves thread context in the **thread queue** when it switches to different threads.

All threads have **[Priority](https://docs.microsoft.com/en-us/dotnet/api/system.threading.thread.priority?view=netcore-3.1)** and **[ThreadState](https://docs.microsoft.com/en-us/dotnet/api/system.threading.thread.threadstate?view=netcore-3.1)** *properties* along with a **ThreadPool** *class* for managing multiple background threads.

The **[ThreadPool Class](https://docs.microsoft.com/en-us/dotnet/standard/threading/the-managed-thread-pool)**  provides a pool of worker threads managed by the system, allowing the developer to focus on the task of the application.
*-All ThreadPool threads are background threads and run at default priority-*
