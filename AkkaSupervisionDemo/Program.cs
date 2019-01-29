using System;
using System.Collections.Generic;
using Akka.Actor;

namespace AkkaSupervisionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var messagesToPrint = new List<string>
            {
                "hello",
                "world",
                string.Empty,
                "lorem",
                "ipsum"
            };

            using(var system = ActorSystem.Create("sys1"))
            {
                var parent = system.ActorOf(ParentActor.Props(), "parent");

                // Schedule the messages.
                for (int i = 0; i < messagesToPrint.Count; i++)
                {
                    system.Scheduler.ScheduleTellOnce(
                        TimeSpan.FromSeconds(i),
                        parent,
                        messagesToPrint[i],
                        null);
                }

                Console.ReadLine();
                system.Terminate();
                Console.WriteLine("Finished");
            }
        }
    }
}
