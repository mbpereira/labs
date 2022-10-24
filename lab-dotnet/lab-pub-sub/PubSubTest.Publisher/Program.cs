using Google.Cloud.PubSub.V1;

var topicId = "";
var projectId = "";
var topicName = new TopicName(projectId, topicId);

var publisher = await PublisherClient.CreateAsync(topicName);

Parallel.For(0, 10, n => {
    var messageId = publisher.PublishAsync($"Hello, Pubsub, {n}").Result;
    Console.WriteLine(messageId);
});