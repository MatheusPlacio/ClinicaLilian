using AutoMapper;
using Domain.DTOs.FuncionariosDTO;
using Domain.DTOs.PacientesDTO;
using Domain.Models;

namespace ClinicaLilian.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<PacienteRegisterDTO, Paciente>().ReverseMap();
            CreateMap<PacienteUpdateDTO, Paciente>().ReverseMap();
            CreateMap<PacienteDTO, Paciente>().ReverseMap();
            CreateMap<PacienteDTO, PacienteUpdateDTO>().ReverseMap();
            CreateMap<PacienteUpdateDTO, PacienteDTO>().ReverseMap();


            CreateMap<FuncionarioDTO, Funcionario>().ReverseMap();
        }
    }
}
