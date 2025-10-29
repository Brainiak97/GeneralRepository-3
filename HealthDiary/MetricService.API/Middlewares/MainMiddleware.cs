namespace MetricService.API.Middlewares
{
    public class MainMiddleware
    {
        private readonly RequestDelegate next;

        public MainMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"[Время: {DateTime.Now}]");
            Console.WriteLine("Headers");
            foreach (var header in context.Request.Headers)
            {
                Console.WriteLine(header);
            }
            Console.WriteLine();

            Console.WriteLine("Content");
            Console.WriteLine(context.Request.Body);
            Console.WriteLine();

            Console.WriteLine("Path");
            Console.WriteLine(context.Request.Path);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            await next.Invoke(context);

        }
    }

}
