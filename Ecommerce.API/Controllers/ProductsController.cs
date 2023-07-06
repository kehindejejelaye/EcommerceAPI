using AutoMapper;
using Ecommerce.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IRepositoryManager _repoManager;
    private readonly IMapper _mapper;

    public ProductsController(IRepositoryManager repoManager, IMapper mapper)
    {
        _repoManager = repoManager;
        _mapper = mapper;
    }
}
