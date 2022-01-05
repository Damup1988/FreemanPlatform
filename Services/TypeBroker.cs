namespace Platform.Services
{
    public static class TypeBroker
    {
        //private static readonly IResponseFormatter _formatter = new TextResponseFormatter();
        private static readonly IResponseFormatter _formatter = new HtmlResponseFormatter();
        public static IResponseFormatter Formatter => _formatter;
    }
}