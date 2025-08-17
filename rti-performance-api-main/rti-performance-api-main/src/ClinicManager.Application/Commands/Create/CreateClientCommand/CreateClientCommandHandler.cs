using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using Clinic_Manager.Core.Responses;
using ClinicManager.Infrastructure.Persistence;
using MediatR;

namespace ClinicManager.Application.Commands.Create.CreateClientCommand
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ResponseBase<Guid>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateClientCommandHandler(IUnitOfWork unitOfWork, IClientRepository clientRepository)
        {
            _unitOfWork = unitOfWork;
            _clientRepository = clientRepository;
        }

        public async Task<ResponseBase<Guid>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<Guid>();

            try
            {
                var client = new Client
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Cpf = request.Cpf,
                    Cellfone = request.Cellfone,
                    Email = request.Email,
                    BiologicalSex = request.BiologicalSex,
                    MaritalStatus = request.MaritalStatus,
                    Birthday = request.Birthday,
                    Profession = request.Profession,
                    Recommendation = request.Recommendation,
                    HealthInsurance = request.HealthInsurance,
                    Birthplace = request.Birthplace,
                    EducationLevel = request.EducationLevel,
                    CaregiverName = request.CaregiverName,
                    CaregiverContact = request.CaregiverContact,
                    EmergencyContactName = request.EmergencyContactName,
                    EmergencyContactFone = request.EmergencyContactFone,
                    Address = request.Address,
                    City = request.City,
                    Neighborhood = request.Neighborhood,
                    Cep = request.Cep,
                    Uf = request.Uf,
                    Classification = request.Classification,
                    Status = request.Status,
                    Active = request.Active,
                };

                await _clientRepository.AddClientAsync(client);
                await _unitOfWork.CommitAsync();

                response.Success = true;
                response.Data = client.Id;
                response.Message = "Cliente criado com sucesso.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar cliente: {ex.GetType().Name}";
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
}