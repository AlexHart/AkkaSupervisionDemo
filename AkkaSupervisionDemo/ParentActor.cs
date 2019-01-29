using Akka.Actor;
using System;

namespace AkkaSupervisionDemo
{
    internal class ParentActor : UntypedActor
    {
        private readonly IActorRef child;

        public ParentActor()
        {
            child = Context.ActorOf(ChildActor.Props(), "child");
        }

        protected override void OnReceive(object message)
        {
            switch(message)
            {
                case string s:
                    child.Tell(s);
                    break;
                default:
                    Unhandled(message);
                    break;
            }
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ParentActor());
        }
    }
}
