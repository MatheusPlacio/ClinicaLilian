using Domain.DTOs.EnderecoDTO;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IService;
using Domain.Models;
using Newtonsoft.Json;

namespace Service.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public bool AtualizarEndereco(EnderecoDTO endereco)
        {
            var enderecoDb = new Endereco
            {
                EnderecoId = endereco.EnderecoId,
                Logradouro = endereco.Logradouro,
                Complemento = endereco.Complemento,
                Numero = endereco.Numero,
                Cep = endereco.Cep,
                Bairro = endereco.Bairro,
                localidade = endereco.localidade,
                UF = endereco.UF
            };

            if (enderecoDb == null)
            {
                return false;
            }

            _enderecoRepository.Update(enderecoDb);

            return true;
        }

        public void AdicionarEndereco(EnderecoCepDTO cep)
        {
            try
            {
                var viaCepUrl = $"https://viacep.com.br/ws/{cep.Cep}/json/";
                using (var httpClient = new HttpClient())
                {
                    var response = httpClient.GetAsync(viaCepUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        var viaCepResult = JsonConvert.DeserializeObject<Endereco>(content);

                        if (!string.IsNullOrEmpty(viaCepResult.Logradouro))
                        {
                            var endereco = new Endereco
                            {
                                Logradouro = viaCepResult?.Logradouro ?? "Logradouro não encontrado",
                                Complemento = viaCepResult?.Complemento ?? "Complemento inválido",
                                Numero = cep.Numero,
                                Cep = viaCepResult.Cep,
                                Bairro = viaCepResult?.Bairro ?? "Bairro não encontrado",
                                localidade = viaCepResult?.localidade ?? "Localidade não encontrada",
                                UF = viaCepResult?.UF ?? "UF não encontrada"
                            };

                            _enderecoRepository.Add(endereco);
                        }
                        else
                        {
                            throw new Exception("Não foi possível obter os dados de endereço do CEP informado.");
                        }
                    }
                    else
                    {
                        throw new Exception("Erro ao consultar o serviço de CEP. O serviço retornou um código de erro.");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao consultar o serviço de CEP. Ocorreu um erro na requisição HTTP.", ex);
            }
        }

        public bool DeletarEndereco(int id)
        {
            var resultado = _enderecoRepository.GetById(id);
            if (resultado == null)
                return false;

            _enderecoRepository.Delete(resultado);
            return true;
        }

        public EnderecoDTO ObterEnderecoPorId(int id)
        {
            var endereco = _enderecoRepository.GetById(id);

            var enderecosDTO = new EnderecoDTO
            {
                EnderecoId = endereco.EnderecoId,
                Logradouro = endereco?.Logradouro ?? "Logradouro não encontrado",
                Complemento = endereco?.Complemento ?? "Complemento inválido",
                Numero = endereco.Numero,
                Cep = endereco.Cep,
                Bairro = endereco?.Bairro ?? "Bairro não encontrado",
                localidade = endereco?.localidade ?? "Localidade não encontrada",
                UF = endereco?.UF ?? "UF não encontrada"
            };

            return enderecosDTO;
        }

        public IList<EnderecoGetDTO> GetTodosEnderecos()
        {
            var enderecos = _enderecoRepository.Get();

            var enderecosDTO = enderecos.Select(c => new EnderecoGetDTO
            {
                EnderecoId = c.EnderecoId,
                Logradouro = c?.Logradouro ?? "Logradouro não encontrado",
                Complemento = c?.Complemento ?? "Complemento inválido",
                Numero = c.Numero,
                Cep = c.Cep,
                Bairro = c?.Bairro ?? "Bairro não encontrado",
                localidade = c?.localidade ?? "Localidade não encontrada",
                UF = c?.UF ?? "UF não encontrada"
            }).ToList();

            return enderecosDTO;
        }
    }
}
