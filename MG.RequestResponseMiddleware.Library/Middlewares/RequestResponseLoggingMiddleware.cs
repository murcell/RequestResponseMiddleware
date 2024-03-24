using MG.RequestResponseMiddleware.Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using System.Diagnostics;

namespace MG.RequestResponseMiddleware.Library.Middlewares;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate next;
    private readonly RequestResponseOptions requestResponseOptions;
    private readonly RecyclableMemoryStreamManager recyclableMemoryStreamManager;
    public RequestResponseLoggingMiddleware(RequestDelegate next, RequestResponseOptions requestResponseOptions)
    {
        this.next = next;
        this.requestResponseOptions = requestResponseOptions;
        recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
    }

    public async Task Invoke(HttpContext context)
    {
        // no response
        var requestBody = await GetRequestBody(context);
        var originalBodyStream = context.Response.Body;

        await using var responseBody = recyclableMemoryStreamManager.GetStream();
        context.Response.Body = responseBody;
        var sw = Stopwatch.StartNew();
        await next(context);
        // reponse
        sw.Stop();
        
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        string responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        var reqResContext = new RequestResponseContext(context)
        {
            RequestBody = requestBody,
            ResponseCreationTime = TimeSpan.FromTicks(sw.ElapsedTicks),
            ResponseBody= responseBodyText
        };

        this.requestResponseOptions.ResResHandler?.Invoke(reqResContext);
    }


    private static string ReadStreamInChunks(Stream stream)
    {
        const int readChunkBufferLength = 4096;

        stream.Seek(0, SeekOrigin.Begin);

        using var textWriter = new StringWriter();
        using var reader = new StreamReader(stream, Encoding.UTF8);

        var readChunk = new char[readChunkBufferLength];
        int readChunkLength;

        do
        {
            readChunkLength = reader.ReadBlock(readChunk,
                                               0,
                                               readChunkBufferLength);
            textWriter.Write(readChunk, 0, readChunkLength);

        } while (readChunkLength > 0);

        return textWriter.ToString();
    }

    private async Task<string> GetRequestBody(HttpContext context)
    {
        context.Request.EnableBuffering();

        await using var requestStream = recyclableMemoryStreamManager.GetStream();
        await context.Request.Body.CopyToAsync(requestStream);

        string reqBody = ReadStreamInChunks(requestStream);

        context.Request.Body.Seek(0, SeekOrigin.Begin);

        return reqBody;
    }
}
