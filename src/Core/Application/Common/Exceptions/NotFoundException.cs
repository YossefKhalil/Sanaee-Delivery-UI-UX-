namespace Core.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string name, object key)
            : base($"The object {name} with the key {key} is not found!") { }
    }
}