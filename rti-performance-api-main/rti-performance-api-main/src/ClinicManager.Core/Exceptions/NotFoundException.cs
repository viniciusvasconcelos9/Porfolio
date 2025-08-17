namespace Clinic_Manager.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string entityName, int id)
        : base($"Entity '{entityName}' com o ID '{id}' not found") { }
    }
}
