namespace Application.Commons.Extensions
{
    public static class ClauseExtensions 
    {
        public static T ValidateNull<T>(this T? input,
         string? parameterName = null,
         string? message = null)
        {
            if (input is null)
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentNullException(parameterName);
                }
                throw new ArgumentNullException(parameterName, message);
            }

            return input;
        }
    }
}
