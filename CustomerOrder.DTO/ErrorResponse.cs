namespace CustomerOrder.DTO
{
    /// <summary>
    /// Defines errors
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Error Title
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}