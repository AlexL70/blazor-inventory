namespace IMS.CoreBusiness.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(Type entityType, string id)
            : base($"Entity \"{entityType.Name}\" with Id=\"{id}\" was not found.")
        {
        }

    }
}