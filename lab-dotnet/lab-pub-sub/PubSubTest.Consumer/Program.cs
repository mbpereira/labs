using Google.Cloud.PubSub.V1;

var projectId = "";
var subscriptionId = "";

var subscriptionName = new SubscriptionName(projectId, subscriptionId);

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"");

//var subscriber = await SubscriberServiceApiClient.CreateAsync();

//while (true)
//{
//    var response = subscriber.Pull(subscriptionName, maxMessages: 3000);

//    //foreach (var received in response.ReceivedMessages)
//    //{
//    //    var msg = received.Message;
//    //    Console.WriteLine($"Received message {msg.MessageId} published at {msg.PublishTime.ToDateTime()}");
//    //    Console.WriteLine($"Text: '{msg.Data.ToStringUtf8()}'");

//    //    subscriber.Acknowledge(subscriptionName, new List<string> { received.AckId });
//    //}

//    Parallel.ForEach(response.ReceivedMessages, new ParallelOptions { MaxDegreeOfParallelism = 5 }, received =>
//    {
//        var msg = received.Message;
//        Console.WriteLine($"Received message {msg.MessageId} published at {msg.PublishTime.ToDateTime()}");
//        Console.WriteLine($"Text: '{msg.Data.ToStringUtf8()}'");

//        subscriber.Acknowledge(subscriptionName, new List<string> { received.AckId });
//    });

//}


//var receivedMessages = new List<PubsubMessage>();
//// Start the subscriber listening for messages.

var subscriber = await SubscriberClient.CreateAsync(subscriptionName);

//var subscriber = new SubscriberClientBuilder()
//{
//    Settings = new SubscriberClient.Settings
//    {
//        FlowControlSettings = new FlowControlSettings(
//                maxOutstandingElementCount: 2,
//                maxOutstandingByteCount: null
//            )
//    },
//    SubscriptionName = subscriptionName,
//}.Build();

var count = 1;

await subscriber.StartAsync((msg, cancellationToken) =>
{
    Console.WriteLine($"Count {count++}");
    Console.WriteLine($"Received message {msg.MessageId} published at {msg.PublishTime.ToDateTime()}");
    Console.WriteLine($"Text: '{msg.Data.ToStringUtf8()}'");
        // Stop this subscriber after one message is received.
        // This is non-blocking, and the returned Task may be awaited.
        //subscriber.StopAsync(TimeSpan.FromSeconds(15));
    Task.Delay(3000).Wait();
        // Return Reply.Ack to indicate this message has been handled.
    return Task.FromResult(SubscriberClient.Reply.Nack);
});
