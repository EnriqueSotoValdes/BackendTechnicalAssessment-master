using AutoMapper;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.BL;

internal class ClientAppService : IClientAppService
{
    private readonly ICrudRepositoryExtension<Client> _data;
    private readonly IMapper _mapper_Cient_ClientDto;
    private readonly IValidator<ClientDto> _validator;

    public ClientAppService( ICrudRepositoryExtension<Client> data, IMapper mapper_Cient_ClientDto, IValidator<ClientDto> validator)
    {
        _data = data;
        _mapper_Cient_ClientDto = mapper_Cient_ClientDto;
        _validator = validator;
    }

    public IEnumerable<ClientDto> GetAll()
    {
        var allClients = _data.GetAll();
        return _mapper_Cient_ClientDto.Map<IEnumerable<ClientDto>>(allClients);
    }

    public ClientDto GetById(params object[] searchID)
    {
        var foundElement = _data.GetById(searchID);

        if (null == foundElement)
        {
            throw new Exception("No existe ningún cliente con este Id");
        }

        return _mapper_Cient_ClientDto.Map<ClientDto>(foundElement);
    }

    public void Create(ClientDto newItemClientDto)
    {
        if ( null != _data.GetById(newItemClientDto.Id) )
        {
            throw new Exception("Ya existe un cliente con este Id");
        }
        if ( null != _data.GetByDocNum(newItemClientDto.DocNum) )
        {
            throw new Exception("Ya existe un cliente con este DocNum");
        }

        ValidateDto(newItemClientDto);

        _data.Create(_mapper_Cient_ClientDto.Map<Client>(newItemClientDto));
    }

    public void Update(ClientDto itemClientDto)
    {
        if (null == _data.GetById(itemClientDto.Id))
        {
            throw new Exception("No existe ningún cliente con este Id");
        }

        ValidateDto(itemClientDto);

        Client updateClient = _mapper_Cient_ClientDto.Map<Client>(itemClientDto);
        _data.Update(updateClient);
    }

    public void Delete(ClientDto removeClientDto)
    {
        if (null == _data.GetById(removeClientDto.Id))
        {
            throw new Exception("No existe ningún cliente con este Id");
        }

        _data.Delete(_mapper_Cient_ClientDto.Map<Client>(removeClientDto));
    }

    private void ValidateDto(ClientDto item)
    {
        var validationResult = _validator.Validate(item);
        if (validationResult.Errors.Any())
        {
            string toShowErrors = string.Join("; ", validationResult.Errors.Select(s => s.ErrorMessage));
            throw new Exception($"El cliente especificado no cumple los requisitos de validación. Errores: '{toShowErrors}'");
        }
    }
}