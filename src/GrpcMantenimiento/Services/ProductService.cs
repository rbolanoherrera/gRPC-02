using Grpc.Core;
using GrpcMantenimiento.Data;
using GrpcMantenimiento.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcMantenimiento.Services;

public class ProductService : ProductSvc.ProductSvcBase
{
    private readonly ILogger<ProductService> _logger;
    private readonly GrpcDbContext _context;

    public ProductService(ILogger<ProductService> logger, GrpcDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public override async Task<CreateProductResponse> CreateProduct(CreateProductRequest request, 
        ServerCallContext context)
    {
        if(string.IsNullOrEmpty(request.Name))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, 
            "Name is required"));
        }

        var product = new Product
        {
            Name = request.Name,
            Description = request.Description
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Product created with Id: {Id}", product.Id);

        var response = new CreateProductResponse
        {
            Id = product.Id
        };

        return await Task.FromResult(response);
    }

    public override async Task<ReadProductResponse> GetProduct(ReadProductRequest request, 
        ServerCallContext context)
    {
        if(request.Id <= 0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, 
            "Invalid product Id"));
        }

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (product is null)
            throw new RpcException(new Status(StatusCode.NotFound, 
            "Product not found"));

        var response = new ReadProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Status = product.Status
        };

        return await Task.FromResult(response);
    }

    public override async Task<GetAllResponse> GetAll(GetAllRequest request, ServerCallContext context)
    {
        var products = await _context.Products.ToListAsync();

        var response = new GetAllResponse();
        response.Products.AddRange(products.Select(p => new ReadProductResponse
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Status = p.Status
        }));

        return await Task.FromResult(response);
    }

     public override async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request, 
        ServerCallContext context)
    {
        if(request.Id <= 0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, 
            "Invalid product Id"));
        }
        
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (product is null)
            throw new RpcException(new Status(StatusCode.NotFound, 
            "Product not found"));

        product.Name = request.Name;
        product.Description = request.Description;
        product.Status = request.Status;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        var response = new UpdateProductResponse
        {
            Id = product.Id,
            UpdatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        return await Task.FromResult(response);
    }

    public override async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request, 
        ServerCallContext context)
    {
        if(request.Id <= 0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, 
            "Invalid product Id"));
        }
        
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (product is null)
            throw new RpcException(new Status(StatusCode.NotFound, 
            "Product not found"));

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        var response = new DeleteProductResponse
        {
            Id = product.Id,
            DeletedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        return await Task.FromResult(response);
    }

}