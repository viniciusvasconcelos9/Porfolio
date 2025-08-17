using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using Clinic_Manager.Core.Responses;

using MediatR;

namespace ClinicManager.Application.Queries.GetIdClient
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ResponseBase<Client>>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientByIdQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ResponseBase<Client>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<Client>();

            try
            {
                var client = await _clientRepository.GetClientByIdAsync(request.Id);

                if (client == null)
                {
                    response.Success = false;
                    response.Message = "Cliente não encontrado.";
                }
                else
                {
                    response.Success = true;
                    response.Data = client;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro ao obter os dados.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
}

