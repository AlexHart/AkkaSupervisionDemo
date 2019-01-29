using Akka.Actor;
using System;
using System.Collections.Generic;

namespace AkkaSupervisionDemo
{
    internal class ChildActor : ReceiveActor
    {

        private List<string> messagesReceived;

        public ChildActor()
        {
            messagesReceived = new List<string>();

            Receive<string>(msg =>
            {
                if (string.IsNullOrEmpty(msg))
                {
                    throw new ArgumentNullException(nameof(msg));
                }

                Console.WriteLine($"{Self.Path} => {msg} :: {messagesReceived.Count}");
                messagesReceived.Add(msg);
            });
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ChildActor());
        }

        public override void AroundPreStart()
        {
            Console.WriteLine($"Messages received {messagesReceived.Count}");
            base.AroundPreStart();
        }

        public override void AroundPreRestart(Exception cause, object message)
        {
            Console.WriteLine($"Messages received {messagesReceived.Count}");
            Console.WriteLine($"Restarting {Self.Path} => {cause.Message}");
            base.AroundPreRestart(cause, message);
        }

        public override void AroundPostStop()
        {
            Console.WriteLine($"{nameof(AroundPostStop)} => {Self.Path}");
            base.AroundPostStop();
        }
    }
}
