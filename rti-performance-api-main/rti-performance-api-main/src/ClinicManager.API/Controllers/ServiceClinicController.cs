using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Interface;
using ClinicManager.Application.Commands.Create.CreateServiceClinicCommand;
using ClinicManager.Application.Queries.GetIdServiceClinic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.API.Controllers
{
    //[Authorize]
    //[Route("api/[controller]")]
    //[ApiController]
    public class ServiceClinicController : ControllerBase
    {
        private readonly IServiceClinicRepository _serviceClinicRepository;
        private readonly IMediator _mediator;

        public ServiceClinicController(IServiceClinicRepository serviceClinicRepository, IMediator mediator)

        {
            _serviceClinicRepository = serviceClinicRepository;
            _mediator = mediator;
        }

        //[HttpGet]
        public async Task<IActionResult> GetAllServiceClinicsAsync()
        {
            try
            {
                var serviceClinic = await _serviceClinicRepository.GetAllServiceClinicsAsync();
                return Ok(serviceClinic);
            }
            catch (Exception)
            {
                return StatusCode(500, $"Erro ao obter os dados fornecidos");
            }
        }

        //[HttpGet("{id}")]
        public async Task<IActionResult> GetServiceClinicByIdQuery(int id)
        {
            var query = new GetServiceClinicByIdQuery(id);
            try
            {
                var serviceClinic = await _mediator.Send(query);

                if (serviceClinic == null)
                {
                    return NotFound();
                }

                return Ok(serviceClinic);

            }

            catch (Exception)

            {
                return StatusCode(500, $"Não existe serviço clinico cadastrado!");
            }

        }

        //[HttpPost]
        public async Task<IActionResult> AddServiceClinicCommand(CreateServiceClinicCommand command)
        {
            try
            {
                if (command == null)
                {
                    return BadRequest("Os dados fornecidos estão vazios!");
                }

                var id = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetServiceClinicByIdQuery), new { id }, command);
            }

            catch (Exception)
            {
                return StatusCode(500, $"Não foi possível adicionar o Serviço Clinico!");
            }

        }

        //[HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceClinicAsync(int id, ServiceClinic serviceClinic)
        {
            if (id != serviceClinic.Id)
            {
                return BadRequest("ID do serviço clinico não corresponde ao ID fornecido nos parâmetros");
            }

            try
            {
                await _serviceClinicRepository.UpdateServiceClinicAsync(serviceClinic);
                return Ok(serviceClinic);
            }
            catch (Exception)
            {
                return StatusCode(500, $"Erro ao atualizar o serviço clinico!");
            }
        }


        //[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceClinicAsync(int id)
        {
            try
            {
                var serviceClinicToDelete = await _serviceClinicRepository.GetServiceClinicByIdAsync(id);
                if (serviceClinicToDelete == null)
                {
                    return NotFound($"Serviço clinico com o ID {id} não encontrado.");
                }

                await _serviceClinicRepository.DeleteServiceClinicAsync(id);
                return Ok(serviceClinicToDelete);
            }
            catch (Exception)
            {
                return StatusCode(500, $"Erro ao excluir o serviço clinico com o ID {id}.");
            }

        }
    }
}
