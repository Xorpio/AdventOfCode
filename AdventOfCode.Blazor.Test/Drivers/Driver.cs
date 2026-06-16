using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdventOfCode.Blazor.Test.Drivers;

public class Driver : IDisposable
{
    private readonly Task<IPage> _page;
    private IBrowserContext _context;
    public IPage Page => _page.Result;

    protected static readonly Uri RootUri = new("http://127.0.0.1");

    private readonly WebApplicationFactory<Program> _webApplicationFactory = new();

    private HttpClient? _httpClient;


    public Driver()
    {
        _page = InitializePlaywright();
    }
    public async Task<IPage> InitializePlaywright()
    {

        _httpClient = _webApplicationFactory.CreateClient(new()
        {
            BaseAddress = RootUri,
        });


        var factory = new DriverFactory(GetBrowserTypeFromEnv());
        var driver = await factory.CreateDriver();

        var browser = await driver.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
        });

        _context = await browser.NewContextAsync();
        //_context = await browser.NewContextAsync(new BrowserNewContextOptions
        ////{
        //    //BaseURL = "https://uibank.uipath.com"
        //});

        await _context.RouteAsync($"{RootUri.AbsoluteUri}**", async route =>
        {
            var request = route.Request;
            var content = request.PostDataBuffer is { } postDataBuffer
                ? new ByteArrayContent(postDataBuffer)
                : null;

            var requestMessage = new HttpRequestMessage(new(request.Method), request.Url)
            {
                Content = content,
            };

            foreach (var header in request.Headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            var response = await _httpClient.SendAsync(requestMessage);
            var responseBody = await response.Content.ReadAsByteArrayAsync();
            var responseHeaders = response.Content.Headers.Select(h => KeyValuePair.Create(h.Key, string.Join(",", h.Value)));

            await route.FulfillAsync(new()
            {
                BodyBytes = responseBody,
                Headers = responseHeaders,
                Status = (int)response.StatusCode,
            });
        });


        return await _context.NewPageAsync();
    }

    public BrowserType GetBrowserTypeFromEnv()
    {
        string envBrowserType = Environment.GetEnvironmentVariable("BROWSER_TYPE");
        string browserType = string.IsNullOrEmpty(envBrowserType) ? "Chrome" : envBrowserType;

        if (!Enum.TryParse(browserType, true, out BrowserType type))
            throw new ArgumentException($"Invalid browser type: {browserType}");
        return type;
    }

    public void Dispose()
    {
        _context?.CloseAsync();
        _httpClient?.Dispose();
    }
}
