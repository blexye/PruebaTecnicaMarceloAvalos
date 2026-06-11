/*namespace PruebaTecnicaMarceloAvalos.Middleware
{
	public class ApiKeyMiddleware
	{
		private readonly RequestDelegate _next;
		private const string HeaderName = "X-API-KEY";

		public ApiKeyMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
		{
			if (!context.Request.Headers.TryGetValue(HeaderName, out var extractedApiKey))
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync("API Key missing.");
				return;
			}

			var apiKey = configuration["ApiSettings:ApiKey"];

			if (apiKey != extractedApiKey)
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync("Invalid API Key.");
				return;
			}

			await _next(context);
		}
	}
}
*/