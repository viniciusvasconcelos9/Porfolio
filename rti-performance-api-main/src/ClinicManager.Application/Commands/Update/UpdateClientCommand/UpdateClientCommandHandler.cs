using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using Clinic_Manager.Core.Responses;
using MediatR;

namespace ClinicManager.Application.Commands.Update.UpdateClientCommand
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ResponseBase<Client>>
    {
        private readonly IClientRepository _clientRepository;
        public UpdateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ResponseBase<Client>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<Client>();
            try
            {

                var existingClient = await _clientRepository.GetClientByIdAsync(request.Id);

                if (existingClient == null)
                {
                    response.Success = false;
                    response.Message = "Cliente não encontrado.";
                    return response;
                }

                if (!string.IsNullOrEmpty(request.Name))
                    existingClient.Name = request.Name;
                if (!string.IsNullOrEmpty(request.Cpf))
                    existingClient.Cpf = request.Cpf;
                if (!string.IsNullOrEmpty(request.Cellfone))
                    existingClient.Cellfone = request.Cellfone;
                if (!string.IsNullOrEmpty(request.Email))
                    existingClient.Email = request.Email;
                if (request.BiologicalSex.HasValue)
                    existingClient.BiologicalSex = request.BiologicalSex.Value;
                if (request.MaritalStatus.HasValue)
                    existingClient.MaritalStatus = request.MaritalStatus.Value;
                if (request.Birthday.HasValue)
                    existingClient.Birthday = request.Birthday.Value;
                if (!string.IsNullOrEmpty(request.Profession))
                    existingClient.Profession = request.Profession;
                if (request.Recommendation.HasValue)
                    existingClient.Recommendation = request.Recommendation.Value;
                if (!string.IsNullOrEmpty(request.HealthInsurance))
                    existingClient.HealthInsurance = request.HealthInsurance;
                if (!string.IsNullOrEmpty(request.Birthplace))
                    existingClient.Birthplace = request.Birthplace;
                if (request.EducationLevel.HasValue)
                    existingClient.EducationLevel = request.EducationLevel.Value;
                if (!string.IsNullOrEmpty(request.CaregiverName))
                    existingClient.CaregiverName = request.CaregiverName;
                if (!string.IsNullOrEmpty(request.CaregiverContact))
                    existingClient.CaregiverContact = request.CaregiverContact;
                if (!string.IsNullOrEmpty(request.EmergencyContactName))
                    existingClient.EmergencyContactName = request.EmergencyContactName;
                if (!string.IsNullOrEmpty(request.EmergencyContactFone))
                    existingClient.EmergencyContactFone = request.EmergencyContactFone;
                if (!string.IsNullOrEmpty(request.Address))
                    existingClient.Address = request.Address;
                if (!string.IsNullOrEmpty(request.City))
                    existingClient.City = request.City;
                if (!string.IsNullOrEmpty(request.Neighborhood))
                    existingClient.Neighborhood = request.Neighborhood;
                if (!string.IsNullOrEmpty(request.Cep))
                    existingClient.Cep = request.Cep;
                if (!string.IsNullOrEmpty(request.Uf))
                    existingClient.Uf = request.Uf;
                if (request.Classification.HasValue)
                    existingClient.Classification = request.Classification.Value;
                if (request.Status.HasValue)
                    existingClient.Status = request.Status.Value;
                if (request.Active.HasValue)
                    existingClient.Active = request.Active.Value;

                existingClient.UpdatedAt = DateTime.UtcNow;

                await _clientRepository.UpdateClientAsync(existingClient);

                response.Success = true;
                response.Message = "Cliente alterado com sucesso.";
                response.Data = existingClient;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
