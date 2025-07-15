namespace GP.Focusi.APIs.Errors
{
	public class ApiExceptionResponse : ApiErrorResponse
	{
        public string? Details { get; set; }
        public ApiExceptionResponse(int statusCode, string? errorMessage = null, string? details = null) 
			: base(statusCode, errorMessage)
		{
			Details = details;
		}
	}
}
