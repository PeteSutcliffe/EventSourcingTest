using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EventSourcingProtoType.Messages.Commands;
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
            var windsorContainer = new WindsorContainer()
                .Register(
                    //why does this only register event handlers???
                    Types.FromAssemblyInThisApplication()
                        .Where(t => typeof (IHandleMessages).IsAssignableFrom(t))
                        .WithServiceAllInterfaces()
                        .LifestyleTransient(),

                    //Component.For<IHandleMessages<CreateSportCommand>>()
                    //    .ImplementedBy<CreateSportCommandHandler>()
                    //    .LifestyleTransient(),

                    //Component.For<IHandleMessages<CreateFixtureCommand>>()
                    //    .ImplementedBy<CreateFixtureCommandHandler>()
                    //    .LifestyleTransient(),

                    Component.For<IHandleMessages<CreateCompetitorCommand>>()
                        .ImplementedBy<CreateCompetitorCommandHandler>()
                        .LifestyleTransient(),

                    //Component.For<IHandleMessages<UpdateSportCommand>>()
                    //    .ImplementedBy<UpdateSportCommandHandler>()
                    //    .LifestyleTransient(),

                    Component.For<IHandleMessages<UpdateCompetitorCommand>>()
                        .ImplementedBy<UpdateCompetitorCommandHandler>()
                        .LifestyleTransient(),

                    //Component.For<IHandleMessages<UpdateFixtureCommand>>()
                    //    .ImplementedBy<UpdateFixtureCommandHandler>()
                    //    .LifestyleTransient(),

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
            return windsorContainer;
        }
    }
}
