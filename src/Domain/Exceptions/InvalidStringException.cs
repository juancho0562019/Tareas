

namespace Domain.Exceptions
{
    public class InvalidStringException : Exception
    {
      
        public InvalidStringException()
            : base($"Cadena de texto no valida")
        {
        }
    }

}
