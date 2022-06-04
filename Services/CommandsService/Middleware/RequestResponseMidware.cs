namespace CommandsService.Middleware
{
    public class RequestResponseMidware
    {
        private readonly RequestDelegate _next;

        public RequestResponseMidware(RequestDelegate _next)
        {
            this._next = _next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var request = httpContext.Request;

            string reqMethod = request.Method.ToString();
            string reqUrl = $"{request.Scheme}://{request.Host}{request.Path}";

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{((char)9733).ToString()} [{reqMethod}] ==>> {reqUrl}");

            Console.ForegroundColor = ConsoleColor.White;
            await _next(httpContext);
        }
    }
}