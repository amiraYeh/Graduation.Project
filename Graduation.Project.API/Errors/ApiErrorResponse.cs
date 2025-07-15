namespace GP.Focusi.APIs.Errors
{
	public class ApiErrorResponse
	{
        public int StatusCode { get; set; }
		public string? ErrorMessage { get; set; }

		public ApiErrorResponse(int statusCode, string? errorMessage = null)
		{
			StatusCode = statusCode;
			ErrorMessage = errorMessage ?? getDefaultMassageForStatusCode(statusCode);
		}

		private string? getDefaultMassageForStatusCode(int statusCode)
		{
			var message = statusCode switch
			{
				400 => "A bad request, you have made",
				401 => "U are Not Authorized",
				404 => "Resource was not found",
				500 => "Server Error",
				 _  => null

			};
			return message;
		}

	}
}
