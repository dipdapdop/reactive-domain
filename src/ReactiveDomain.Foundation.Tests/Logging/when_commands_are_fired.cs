﻿using System;
using ReactiveDomain.Foundation.Tests.EventStore;
using ReactiveDomain.Messaging;
using ReactiveDomain.Messaging.Bus;
using ReactiveDomain.Messaging.Testing;
using Xunit;

namespace ReactiveDomain.Foundation.Tests.Logging
{

    // ReSharper disable once InconsistentNaming
    [Collection("ESEmbeded")]
    public class when_commands_are_fired :
        with_message_logging_enabled,
        IHandle<Message>
    {
        private const int MaxCountedCommands = 25;

        private Guid _correlationId;
        private IListener _listener;

        private int _multiFireCount;
        private int _testCommandCount;

        private TestCommandSubscriber _cmdHandler;


        static when_commands_are_fired()
        {
            Messaging.BootStrap.Load();
        }

        public when_commands_are_fired(EmbeddedEventStoreFixture fixture):base(fixture.Connection)
        {
        }
        protected override void When()
        {
            _correlationId = Guid.NewGuid();

            // command must have a commandHandler
            _cmdHandler = new TestCommandSubscriber(Bus);

            _multiFireCount = 0;
            _testCommandCount = 0;

            _listener = Repo.GetListener(Logging.FullStreamName);
            _listener.EventStream.Subscribe<Message>(this);

            _listener.Start(Logging.FullStreamName);

            // create and fire a set of commands
            for (int i = 0; i < MaxCountedCommands; i++)
            {
                // this is just an example command - choice to fire this one was random
                var cmd = new TestCommands.TestCommand2(
                                        Guid.NewGuid(),
                                        null);
                Bus.Fire(cmd,
                    $"exception message{i}",
                    TimeSpan.FromSeconds(2));

            }
            var tstCmd = new TestCommands.TestCommand3(
                        Guid.NewGuid(),
                        null);

            Bus.Fire(tstCmd,
                "Test Command exception message",
                TimeSpan.FromSeconds(1));

        }


        public void all_commands_are_logged()
        {
            // Wait  for last command to be queued
            TestQueue.WaitFor<TestCommands.TestCommand3>(TimeSpan.FromSeconds(5));

            Assert.IsOrBecomesTrue(() => _multiFireCount == MaxCountedCommands, 9000);
            Assert.True(_multiFireCount == MaxCountedCommands, $"Command count {_multiFireCount} doesn't match expected index {MaxCountedCommands}");
            Assert.IsOrBecomesTrue(() => _testCommandCount == 1, 1000);

            Assert.True(_testCommandCount == 1, $"Last event count {_testCommandCount} doesn't match expected value {1}");

        }

        public void Handle(Message msg)
        {
            if (msg is TestCommands.TestCommand2) _multiFireCount++;
            if (msg is TestCommands.TestCommand3) _testCommandCount++;
        }
    }
}

