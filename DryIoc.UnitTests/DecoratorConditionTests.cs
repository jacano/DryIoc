﻿using NUnit.Framework;

namespace DryIoc.UnitTests
{
    [TestFixture]
    public class DecoratorConditionTests
    {
        [Test]
        public void Can_register_decorator_with_condition_based_on_service_name()
        {
            var container = new Container();
            container.Register<IHandler, FastHandler>(named: "fast");
            container.Register<IHandler, SlowHandler>(named: "slow");
            container.Register<IHandler, LoggingHandlerDecorator>(
                setup: DecoratorSetup.New(request => Equals(request.ServiceKey, "slow")));

            Assert.That(container.Resolve<IHandler>("fast"), Is.InstanceOf<FastHandler>());
            Assert.That(container.Resolve<IHandler>("slow"), Is.InstanceOf<LoggingHandlerDecorator>());
        }
    }

    public interface IHandler
    {
    }

    class FastHandler : IHandler
    {

    }

    class SlowHandler : IHandler
    {

    }

    class LoggingHandlerDecorator : IHandler
    {
        public IHandler Handler { get; set; }

        public LoggingHandlerDecorator(IHandler handler)
        {
            Handler = handler;
        }
    }
}
