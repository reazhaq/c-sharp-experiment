using System.Diagnostics;
using System.Net;

Console.WriteLine("Http 1-2-3 client");

var httpClient = new HttpClient();

var stopWatch = Stopwatch.StartNew();
foreach (var i in Enumerable.Range(1, 10))
{
    stopWatch.Restart();
    using var httpRequest = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5001/")
    {
        //Version = HttpVersion.Version11,
        //Version = HttpVersion.Version20,
        Version = HttpVersion.Version30,
        VersionPolicy = HttpVersionPolicy.RequestVersionExact
    };

    using var httpResponse = await httpClient.SendAsync(httpRequest);
    var content = await httpResponse.Content.ReadAsStringAsync();

    Console.WriteLine($"{i:00}: response in {stopWatch.Elapsed.Microseconds} micro-seconds with content: {content}");
}
