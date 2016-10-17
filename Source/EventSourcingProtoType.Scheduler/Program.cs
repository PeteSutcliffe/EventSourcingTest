using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EventSourcingProtoType.Messages;
using EventSourcingProtoType.Scheduler.CommandHandlers;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Handlers;

namespace EventSourcingProtoType.Scheduler
{
    internal class Program
    {
        public static IBus Bus;

        private static void Main()
        {
            var container = Bootstrap();

            Bus = Configure.With(new CastleWindsorContainerAdapter(container))
                .Transport(t => t.UseMsmq("Rebus"))
                //.Subscriptions(s => s.StoreInMemory())
                .Start();

            Console.ReadKey();
        }

        private static IWindsorContainer Bootstrap()
        {
            return new WindsorContainer()
                .Register(
                    //Component.For<IHandleMessages<CreateSportCommand>>()
                    //.ImplementedBy<CreateSportCommandHandler>()
                    //.LifestyleTransient(),

                    Classes.FromAssemblyInThisApplication()
                    .InNamespace("EventSourcingProtoType.Scheduler.CommandHandlers")
                    .WithServiceAllInterfaces()
                    .LifestyleTransient(),

                    //Classes.FromThisAssembly()
                    //.InNamespace("EventSourcingProtoType.Scheduler.EventHandlers")
                    //.WithServiceAllInterfaces()
                    //.LifestyleTransient(),

                    Component.For<IRepositoryFactory>()
                        .ImplementedBy<RepositoryFactory>()
                        .LifestyleTransient(),

                    Component.For<IUnitOfWork>()
                    .ImplementedBy<UnitOfWork>()
                    .LifestyleTransient(),

                    Component.For<IEventStore>()
                        .ImplementedBy<EventStore>()
                        .LifestyleTransient(),

                    Component.For<IEventPublisher>()
                        .ImplementedBy<EventPublisher>()
                        .LifestyleTransient());
        }
    }
}
