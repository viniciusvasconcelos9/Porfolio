using Clinic_Manager.Core.Entities;

namespace Clinic_Manager.Core.Interface
{
    public interface IAttachmentRepository
    {
        Task<IEnumerable<Attachment>> GetAllAttachmentsAsync();
        Task<Attachment> GetAttachmentByIdAsync(int id);
        Task<Attachment> AddAttachmentAsync(Attachment attachment);
        Task UpdateAttachmentAsync(Attachment attachment);
        Task DeleteAttachmentAsync(int id);
    }
}
