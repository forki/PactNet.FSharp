namespace PactNet.FSharp

/// An FSharp-friendly wrapper for PactNet
module API =

  open Newtonsoft.Json
  open PactNet
  open PactNet.Mocks.MockHttpService.Models
  open PactNet.Mocks.MockHttpService

  let serviceConsumer consumer = (new PactBuilder()).ServiceConsumer(consumer)
  let hasPactWith provider (pactBuilder:IPactBuilder) = pactBuilder.HasPactWith(provider)
  let mockServiceOnPort port (pactBuilder:IPactBuilder) = pactBuilder.MockService(port)

  /// Pactnet doesn't yet use the jsonSettings safely, it uses these settings to serialize the entire request and responses so the Headers get incorrectly cased.
  /// Until this is fixed, we suggest using the mockServiceOnPort function wich will use the defaults, and decorate the request and response bodies with Newtonsoft
  /// attributes to control the serialization.
  let mockServiceOnPortWithSerializer port (jsonSettings:JsonSerializerSettings) (pactBuilder:IPactBuilder) = pactBuilder.MockService(port, jsonSettings)
  let build (pactBuilder:IPactBuilder) = pactBuilder.Build()

  type PactRequest = {
    Method  : HttpVerb
    Path    : string
    Query   : string option
    Headers : seq<string * string> option
    Body    : obj option
    }

  type PactResponse = {
    Status  : int
    Headers : seq<string * string> option
    Body    : obj option
    }

  type PactInteractionBuilder = {
    ProviderState   : string option
    Description     : string option
    Request         : PactRequest option
    Response        : PactResponse option
  }

  let toProviderServiceRequest req =
    let r = new ProviderServiceRequest()
    r.Method <- req.Method
    r.Path <- req.Path
    if req.Query.IsSome then r.Query <- req.Query.Value
    Option.iter (fun h -> r.Headers <- dict h) req.Headers
    Option.iter (fun b -> r.Body <- b) req.Body
    r

  let getQueryStringFromKeyValues keyValues =
    keyValues
    |> Map.fold (fun query key value -> sprintf "%s%s%s=%s" query (if query = "" then "" else "&" )  key value) ""

  let withRequest verb path b           = { b with Request = Some { Method = verb; Path = path; Headers = None; Body = None; Query = None } }
  let withRequestQueryString query b    = { b with Request = Option.map (fun req -> { req with Query = Some query }) b.Request }
  let withRequestQueryItems keyValues b = withRequestQueryString (keyValues |> Map.ofList |> getQueryStringFromKeyValues ) b
  let withRequestHeaders headers b      = { b with Request = Option.map (fun req -> { req with Headers = Some headers }) b.Request }
  let withRequestBody body b            = { b with Request = Option.map (fun req -> { req with Body = Some body }) b.Request }

  let toProviderServiceResponse res =
    let r = new ProviderServiceResponse()
    r.Status <- res.Status
    Option.iter (fun h -> r.Headers <- dict h) res.Headers
    Option.iter (fun b -> r.Body <- b) res.Body
    r

  let willRespondWithStatus status b = { b with Response = Some { Status = status; Headers = None; Body = None } }
  let withResponseHeaders headers b  = { b with Response = Option.map (fun response -> { response with Headers = Some headers }) b.Response }
  let withResponseBody body b        = { b with Response = Option.map (fun response -> { response with Body = Some body }) b.Response }

  let given state = {
    ProviderState   = Some state
    Description     = None
    Request         = None
    Response        = None
  }

  let uponReceiving description b = { b with Description = Some description }
  let uponReceiving' description = {
    ProviderState   = None
    Description     = Some description
    Request         = None
    Response        = None
  }


  open FSharpx.Functional.Prelude

  let registerWith (provider:IMockProviderService) = function
    | { Description = Some d; Request = Some req; Response = Some resp; ProviderState = state} ->
        provider.UponReceiving(d).With(req |> toProviderServiceRequest)
        |> flip (Option.fold (fun p s -> p.Given(s))) state
        |> fun p -> p.WillRespondWith(resp |> toProviderServiceResponse)
    | _ -> failwith "Unable to register incomplete interaction"
