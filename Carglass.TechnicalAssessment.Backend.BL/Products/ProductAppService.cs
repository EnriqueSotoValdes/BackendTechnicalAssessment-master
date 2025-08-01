using AutoMapper;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.BL;

public class ProductAppService : IProductAppService
{
    private readonly ICrudRepository<Product> _data;
    private readonly IMapper _mapper_Product_ProductDto;
    private readonly IValidator<ProductDto> _validator;

    public ProductAppService(ICrudRepository<Product> data, IMapper mapper_Product_ProductDto, IValidator<ProductDto> validator)
    {
        _data = data;
        _mapper_Product_ProductDto = mapper_Product_ProductDto;
        _validator = validator;
    }

    public IEnumerable<ProductDto> GetAll()
    {
        var allProducts = _data.GetAll();
        return _mapper_Product_ProductDto.Map<IEnumerable<ProductDto>>(allProducts);
    }
    public ProductDto GetById(params object[] searchID)
    {
        var foundElement = _data.GetById(searchID);

        if (null == foundElement)
        {
            throw new Exception("No existe ningún producto con este Id");
        }

        return _mapper_Product_ProductDto.Map<ProductDto>(foundElement);
    }

    public void Create(ProductDto newProductDto)
    {
        if (null != _data.GetById(newProductDto.Id))
        {
            throw new Exception("Ya existe un producto con este Id");
        }
;
        ValidateProduct(newProductDto);

        _data.Create(_mapper_Product_ProductDto.Map<Product>(newProductDto));
    }
    public void Update(ProductDto itemProductDto)
    {
        if (null == _data.GetById(itemProductDto.Id))
        {
            throw new Exception("No existe ningún cliente con este Id");
        }

        ValidateProduct(itemProductDto);

        Product updateProduct = _mapper_Product_ProductDto.Map<Product>(itemProductDto);
        _data.Update(updateProduct);
    }

    public void Delete(ProductDto removeProductDto)
    {
        if (null == _data.GetById(removeProductDto.Id))
        {
            throw new Exception("No existe ningún cliente con este Id");
        }

        _data.Delete(_mapper_Product_ProductDto.Map<Product>(removeProductDto));
    }

    private void ValidateProduct(ProductDto item)
    {
        var validationResult = _validator.Validate(item);
        if (validationResult.Errors.Any())
        {
            string toShowErrors = string.Join("; ", validationResult.Errors.Select(s => s.ErrorMessage));
            throw new Exception($"El cliente especificado no cumple los requisitos de validación. Errores: '{toShowErrors}'");
        }
    }

}
