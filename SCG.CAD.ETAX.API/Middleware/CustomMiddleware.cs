using SCG.CAD.ETAX.MODEL.etaxModel;
using System.IO;

namespace SCG.CAD.ETAX.API.Middleware
{
    public class LoggingHandler
    {
        private readonly RequestDelegate next;
        private readonly ITraceLogApiRepository repo;

        public LoggingHandler(RequestDelegate next)
        {
            this.next = next;
            this.repo = new TraceLogApiRepository();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // create a new log object
                var log = new TraceLogApi
                {
                    RequestPath = context.Request.Path,
                    RequestMethod = context.Request.Method,
                    RequestContentType = context.Request.ContentType,
                    RequestUri = context.Request.Host.Host + ":" + context.Request.Host.Port,
                    //QueryString = context.Request.QueryString.ToString()
                };

                // check if the Request is a POST call 
                // since we need to read from the body
                if (context.Request.Method == "POST")
                {
                    context.Request.EnableBuffering();
                    var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                    context.Request.Body.Position = 0;
                    log.RequestBody = body;
                }

                log.RequestTimestamp = DateTime.Now;

                await next.Invoke(context);

                //var size = context.Response.Body.Length;
                using (Stream originalRequest = context.Response.Body)
                {
                    try
                    {
                        using (var memStream = new MemoryStream())
                        {
                            //context.Response.Body.CopyTo(memStream);
                            context.Response.Body = memStream;
                            // All the Request processing as described above 
                            // happens from here.
                            // Response handling starts from here
                            // set the pointer to the beginning of the 
                            // memory stream to read
                            memStream.Position = 0;
                            //memStream.Seek(0, SeekOrigin.Begin);
                            // read the memory stream till the end
                            var response = await new StreamReader(memStream).ReadToEndAsync();
                            //var response = new StreamReader(memStream).ReadToEnd();
                            // write the response to the log object
                            log.ResponseBody = response;
                            log.ResponseStatusCode = context.Response.StatusCode.ToString();
                            log.ResponseContentType = context.Response.ContentType;
                            log.ResponseTimestamp = DateTime.Now;

                            // add the log object to the logger stream 
                            // via the Repo instance injected
                            repo.INSERT(log);

                            // since we have read till the end of the stream, 
                            // reset it onto the first position
                            memStream.Position = 0;

                            // now copy the content of the temporary memory 
                            // stream we have passed to the actual response body 
                            // which will carry the response out.
                            await memStream.CopyToAsync(originalRequest);
                        }
                    }
                    catch (Exception ex)
                    {
                        var xx = ex.Message.ToString();
                        Console.WriteLine(ex);
                    }
                    finally
                    {
                        // assign the response body to the actual context
                        context.Response.Body = originalRequest;
                    }
                }
            }
            catch(Exception ex)
            {
                var xx = ex.Message.ToString();
                Console.WriteLine(ex);
            }
        }
    }



    public static class MiddlewareRegistrationExtension
    {
        public static void UseLoggingHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<LoggingHandler>();
        }
    }
}
