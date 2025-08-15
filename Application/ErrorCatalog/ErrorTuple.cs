namespace Application.ErrorCatalog
{
    public class ErrorTuple
    {
        public string ErrorCode { get; set; }
        public string PropertyName { get; set; }

        public ErrorTuple(string errorCode, string propertyName)
        {
            ErrorCode = errorCode;
            PropertyName = propertyName;
        }
    }
}
