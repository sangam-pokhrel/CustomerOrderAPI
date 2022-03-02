namespace CustomerOrder.Common.Constants
{
    public static class ErrorConstants
    {
        public const string DuplicateOrderIdMsg = "The provided OrderId already exists. Please provide another OrderId.";
        public const string RecordNotFoundMsg = "Record Not found in the System";
        public const string UnableToProcessReqMsg = "The request could not be processed";
        public const string OrderNotFoundMsg = "No data was found for the given OrderId. Please try with another OrderId.";
        public const string IncorrectProductTypeMsg = "The given 'ProductType' is not correct. Please enter a correct 'ProductType'.";
    }
}