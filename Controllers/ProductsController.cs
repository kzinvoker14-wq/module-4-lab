using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ProductsApi.Common;
using ProductsApi.Dtos;
using ProductsApi.Entities;
using ProductsApi.Repositories;

namespace ProductsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductsController(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ReturnResult<IEnumerable<ProductDto>>>> GetAll()
    {
        var products = await _repository.GetAllAsync();
        var dto = _mapper.Map<IEnumerable<ProductDto>>(products);
        return Ok(ReturnResult<IEnumerable<ProductDto>>.Success(dto));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReturnResult<ProductDto>>> GetById(int id)
    {
        var product = await _repository.GetAsync(id);
        if (product is null)
        {
            return NotFound(ReturnResult<ProductDto>.Fail($"Товар с Id = {id} не найден."));
        }

        return Ok(ReturnResult<ProductDto>.Success(_mapper.Map<ProductDto>(product)));
    }

    [HttpPost]
    public async Task<ActionResult<ReturnResult<ProductDto>>> Create([FromBody] ProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        product.Id = 0;

        await _repository.CreateAsync(product);

        var resultDto = _mapper.Map<ProductDto>(product);
        return CreatedAtAction(nameof(GetById), new { id = resultDto.Id },
            ReturnResult<ProductDto>.Success(resultDto));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ReturnResult<ProductDto>>> Update(int id, [FromBody] ProductDto dto)
    {
        var existing = await _repository.GetAsync(id);
        if (existing is null)
        {
            return NotFound(ReturnResult<ProductDto>.Fail($"Товар с Id = {id} не найден."));
        }

        existing.Name = dto.Name;
        existing.Price = dto.Price;

        await _repository.UpdateAsync(existing);

        return Ok(ReturnResult<ProductDto>.Success(_mapper.Map<ProductDto>(existing)));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ReturnResult<string>>> Delete(int id)
    {
        var existing = await _repository.GetAsync(id);
        if (existing is null)
        {
            return NotFound(ReturnResult<string>.Fail($"Товар с Id = {id} не найден."));
        }

        await _repository.DeleteAsync(id);
        return Ok(ReturnResult<string>.Success($"Товар с Id = {id} удалён."));
    }
}
