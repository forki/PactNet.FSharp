module PactNet.FSharp.Tests

open PactNet.FSharp.API
open NUnit.Framework

[<Test>]
let ``GetAllEvents_WhenCalled_ReturnsAllEvents`` () =
  let mockProviderService =
    serviceConsumer "Consumer"
    |> hasPactWith "Event API"
    |> mockServiceOnPort 1234

  ()
    // _mockProviderService.Given("there are events with ids '45D80D13-D5A2-48D7-8353-CBB4C0EAABF5', '83F9262F-28F1-4703-AB1A-8CFD9E8249C9' and '3E83A96B-2A0C-49B1-9959-26DF23F83AEB'")
    //     .UponReceiving("a request to retrieve all events")
    //     .With(new ProviderServiceRequest
    //     {
    //         Method = HttpVerb.Get,
    //         Path = "/events",
    //         Headers = new Dictionary<string, string>
    //         {
    //             { "Accept", "application/json" }
    //         }
    //     })
    //     .WillRespondWith(new ProviderServiceResponse
    //     {
    //         Status = 200,
    //         Headers = new Dictionary<string, string>
    //         {
    //             { "Content-Type", "application/json; charset=utf-8" }
    //         },
    //         Body = new []
    //         {
    //             new 
    //             {
    //                 eventId = Guid.Parse("45D80D13-D5A2-48D7-8353-CBB4C0EAABF5"),
    //                 timestamp = "2014-06-30T01:37:41.0660548",
    //                 eventType = "SearchView"
    //             },
    //             new
    //             {
    //                 eventId = Guid.Parse("83F9262F-28F1-4703-AB1A-8CFD9E8249C9"),
    //                 timestamp = "2014-06-30T01:37:52.2618864",
    //                 eventType = "DetailsView"
    //             },
    //             new
    //             {
    //                 eventId = Guid.Parse("3E83A96B-2A0C-49B1-9959-26DF23F83AEB"),
    //                 timestamp = "2014-06-30T01:38:00.8518952",
    //                 eventType = "SearchView"
    //             }
    //         }
    //     });

    // var consumer = new EventsApiClient(_mockProviderServiceBaseUri);

    // //Act
    // var events = consumer.GetAllEvents();

    // //Assert
    // Assert.NotEmpty(events);
    // Assert.Equal(3, events.Count());


    // _mockProviderService.VerifyInteractions();
