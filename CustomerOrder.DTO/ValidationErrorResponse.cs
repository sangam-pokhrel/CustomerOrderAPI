namespace CustomerOrder.DTO
{
    /// <summary>
    /// Defines errors
    /// </summary>
    public class ValidationErrorResponse
    {
        /// <summary>
        /// The Invalid Property
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}