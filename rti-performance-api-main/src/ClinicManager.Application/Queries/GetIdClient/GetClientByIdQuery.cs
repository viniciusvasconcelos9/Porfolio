using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Responses;
using MediatR;

namespace ClinicManager.Application.Queries.GetIdClient
{
    public class GetClientByIdQuery : IRequest<ResponseBase<Client>>
    {
        public Guid Id { get; set; }
    }
}

