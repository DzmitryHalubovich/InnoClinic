namespace Offices.Domain.Exceptions
{
    public sealed class OfficeNotFoundException : NotFoundException
    {
        public OfficeNotFoundException(string id) : base($"The office with the identifier {id} was not found.") 
        { }
    }
}
