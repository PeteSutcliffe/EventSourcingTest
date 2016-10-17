using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EventSourcingProtoType.Messages;
using EventSourcingProtoType.Scheduler.CommandHandlers;
using EventSourcingProtoType.Scheduler.EventHandlers;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Routing.TypeBased;

namespace EventSourcingProtoType.Scheduler
{
    class Program
    {
        public static IBus Bus;

        static void Main(string[] args)
        {
            var container = new WindsorContainer()
                .Register(Component.For<IHandleMessages<CreateSportCommand>>()
                              .ImplementedBy<CreateSportCommandHandler>()
                              .LifestyleTransient(),

                              Component.For<IHandleMessages<UpdateSportCommand>>()
                              .ImplementedBy<UpdateSportCommandHandler>()
                              .LifestyleTransient(),

                              Component.For<IHandleMessages<SportNameChanged>>()
                              .ImplementedBy<SportUpdatedEventHandler>()
                              .LifestyleTransient(),

                              Component.For<IHandleMessages<SportCreated>>()
                              .ImplementedBy<SportCreatedEventHandler>()
                              .LifestyleTransient(),
                              
                              Component.For<IRepository<Sport>>()
                              .ImplementedBy<Repository<Sport>>()
                              .LifestyleTransient(),
                              
                              Component.For<IEventStore>()
                              .ImplementedBy<EventStore>()
                              .LifestyleTransient(),
                              
                              Component.For<IEventPublisher>()
                              .ImplementedBy<EventPublisher>()
                              .LifestyleTransient());

            Bus = Configure.With(new CastleWindsorContainerAdapter(container))
                .Transport(t => t.UseMsmq("Rebus"))
                .Start();

            Console.ReadKey();
        }
    }
}
