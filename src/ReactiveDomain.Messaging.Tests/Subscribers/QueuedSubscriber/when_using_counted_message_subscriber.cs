﻿using ReactiveDomain.Messaging.Tests.Specifications;

namespace ReactiveDomain.Messaging.Tests.Subscribers.QueuedSubscriber
{
    // ReSharper disable InconsistentNaming
    public abstract class when_using_counted_message_subscriber : CommandBusSpecification
    {
        protected CountedMessageSubscriber _messageSubscriber;

        protected override void Given()
        {
            _messageSubscriber = new CountedMessageSubscriber(Bus);
        }

    }
}