using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services;

public class ProductsService : IProductService
{
    private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
    private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
    private readonly IMapper _mapper;
    private readonly IProductsRepository _productsRepository;

    public ProductsService(IValidator<ProductAddRequest> productAddRequestValidator, IValidator<ProductUpdateRequest> productUpdateRequestValidator, IMapper mapper, IProductsRepository productsRepository)
    {
        _productAddRequestValidator = productAddRequestValidator;
        _productUpdateRequestValidator = productUpdateRequestValidator;
        _mapper = mapper;
        _productsRepository = productsRepository;
    }

    public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
    {
        if (productAddRequest == null) 
        { 
            throw new ArgumentNullException(nameof(productAddRequest));
        }

        //Validation is a responsibility of the business logic layer, so we will validate the productAddRequest using Fluent Validation before adding it to the database. If the validation fails, we will throw an exception with the validation errors. If the validation is successful, we will proceed to add the product to the database using the repository and return the added product as a ProductResponse object.
        ValidationResult validationResult 
            = await _productAddRequestValidator.ValidateAsync(productAddRequest);

        //Check if the validation failed or not
        if(!validationResult.IsValid) 
        {
            //Handle the validation failure case 
            string errors = string.Join(",",validationResult.Errors.Select(temp => temp.ErrorMessage)); //Error1, Error2
            throw new ArgumentException(errors);
        }

        //If validation is successful, add the product to the database using the repository
        //Map the ProductAddRequest object to a Product type using AutoMapper. It invokes ProductAddRequestToProductMappingProfile
        Product productInput = _mapper.Map<Product>(productAddRequest); //ProductAddRequest(Source) -> Product(Destination)
        Product? addedProduct = await _productsRepository.AddProduct(productInput);

        if(addedProduct == null) return null;

        //Map the AddedProduct object to a ProductResponse type using AutoMapper. It invokes ProductToProductResponseMappingProfile
        ProductResponse addedProductResponse = _mapper.Map<ProductResponse>(addedProduct); //Added Product(Source) -> ProductResponse(Destination)

        return addedProductResponse;
    }

    public async Task<bool> DeleteProduct(Guid productID)
    {
        Product? existingProduct = await _productsRepository.GetProductByCondition(temp => temp.ProductID == productID);
        
        if (existingProduct == null) return false;

        //Attempt to delete the product from the database using the repository
        bool productDeleted = await _productsRepository.DeleteProduct(productID);
        return productDeleted;
    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        Product? product = await _productsRepository.GetProductByCondition(conditionExpression);

        if (product == null) 
        { 
            return null;
        }

        ProductResponse productFoundResponse = _mapper.Map<ProductResponse>(product); // Invoke ProductToProductResponseMappingProfile
        return productFoundResponse;
    }

    public async Task<List<ProductResponse?>> GetProducts()
    {
        IEnumerable<Product?> products = await _productsRepository.GetProducts();

        IEnumerable<ProductResponse?> productsFoundResponse = _mapper.Map<IEnumerable<ProductResponse>>(products); // Invoke ProductToProductResponseMappingProfile
        return [.. productsFoundResponse];
    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        IEnumerable<Product?> products = await _productsRepository.GetProductsByCondition(conditionExpression);

        IEnumerable<ProductResponse?> productsFoundResponse = _mapper.Map<IEnumerable<ProductResponse>>(products); // Invoke ProductToProductResponseMappingProfile
        return [.. productsFoundResponse];
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        Product? existingProduct = await _productsRepository.GetProductByCondition(temp => temp.ProductID == productUpdateRequest.ProductID);

        if (existingProduct == null)
        {
            throw new ArgumentNullException("Invalid Product Id");
        }

        //Validation is a responsibility of the business logic layer, so we will validate the productUpdateRequest using Fluent Validation before updating it in the database. If the validation fails, we will throw an exception with the validation errors. If the validation is successful, we will proceed to update the product in the database using the repository and return the updated product as a ProductResponse object.
        ValidationResult validationResult
            = await _productUpdateRequestValidator.ValidateAsync(productUpdateRequest);

        if (!validationResult.IsValid)
        {
            //Handle the validation failure case 
            string errors = string.Join(",", validationResult.Errors.Select(temp => temp.ErrorMessage)); //Error1, Error2
            throw new ArgumentException(errors);
        }

        //If validation is successful, update the product in the database using the repository
        Product productUpdateInput = _mapper.Map<Product>(productUpdateRequest); //ProductUpdateRequest(Source) -> Product(Destination)
        Product? updatedProduct = await _productsRepository.UpdateProduct(productUpdateInput);

        if (updatedProduct == null) return null;

        //Map the UpdatedProduct object to a ProductResponse type using AutoMapper. It invokes ProductToProductResponseMappingProfile
        ProductResponse updatedProductResponse = _mapper.Map<ProductResponse>(updatedProduct); // Updated Product(Source) -> ProductResponse(Destination)

        return updatedProductResponse;
    }
}
