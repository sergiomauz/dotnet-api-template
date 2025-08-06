namespace Application.ErrorCatalog
{
    public class CodePropertyNamePair
    {
        public string ErrorCode { get; set; }
        public string PropertyName { get; set; }

        public CodePropertyNamePair(string errorCode, string propertyName)
        {
            ErrorCode = errorCode;
            PropertyName = propertyName;
        }
    }
}
