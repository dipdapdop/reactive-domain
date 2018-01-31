﻿using System.Threading;
using ReactiveDomain.Messaging.Bus;
using ReactiveDomain.Messaging.Testing;

namespace ReactiveDomain.Messaging.Tests.Subscribers.QueuedSubscriber
{
    public class TestCommandSubscriber :
                IHandleCommand<TestCommands.TestCommand2>,
                IHandleCommand<TestCommands.TestCommand3>
    {
        public long TestCommand2Handled;
        public long TestCommand3Handled;

        private IGeneralBus _bus;

        public TestCommandSubscriber(IGeneralBus bus)
        {
            _bus = bus;
            TestCommand2Handled = 0;
            _bus.Subscribe<TestCommands.TestCommand2> (this);
            _bus.Subscribe<TestCommands.TestCommand3>(this);
        }
              

        public CommandResponse Handle(TestCommands.TestCommand2 command)
        {
            Interlocked.Increment(ref TestCommand2Handled);
            return command.Succeed();
        }

        public CommandResponse Handle(TestCommands.TestCommand3 command)
        {
            Interlocked.Increment(ref TestCommand3Handled);
            return command.Succeed();
        }
    }
}
