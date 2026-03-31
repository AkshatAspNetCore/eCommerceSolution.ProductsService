using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using FluentValidation;
using FluentValidation.Results;

namespace ProductsMicroservice.API.APIEndpoints;

public static class ProductAPIEndpoints
{
    public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
    {
        //GET /api/products
        app.MapGet("/api/products", async (IProductService productsService) =>
        {
            //This is a placeholder for the actual implementation of the GetProducts endpoint.
            List<ProductResponse?> products = await productsService.GetProducts();
            return Results.Ok(products);
        });

        //GET /api/products/search/product-id/00000-00000-00000-00000
        app.MapGet("/api/products/search/product-id/{IdOfAProduct:guid}", async (IProductService productsService, Guid IdOfAProduct) =>
        {
            //This is a placeholder for the actual implementation of the GetProductByCondition endpoint.
            ProductResponse? product = await productsService.GetProductByCondition(temp => temp.ProductID == IdOfAProduct);
            if(product == null) return Results.NotFound($"No product found with the ID: {IdOfAProduct}");   
            return Results.Ok(product);
        });

        //GET /api/products/search/abc... (search string)
        app.MapGet("/api/products/search/{SearchString}", async (IProductService productsService, string SearchString) =>
        {
            //This is a placeholder for the actual implementation of the GetProductsByCondition endpoint.
            List<ProductResponse?> productsByProductName = await productsService.GetProductsByCondition(temp => temp.ProductName != null && temp.ProductName.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
            List<ProductResponse?> productsByCategory = await productsService.GetProductsByCondition(temp => temp.Category != null && temp.Category.Contains(SearchString, StringComparison.OrdinalIgnoreCase));

            var products = productsByProductName.Union(productsByCategory);
            return Results.Ok(products);
        });

        //POST /api/products
        app.MapPost("/api/products", async (IProductService productsService, IValidator<ProductAddRequest> productAddValidatorRequest, ProductAddRequest productAddRequest) =>
        {
            //This is a placeholder for the actual implementation of the AddProduct endpoint.
            //Validate the productAddRequest using Fluent validation
            //Each layer is responsible for its own validation, so we will validate the productAddRequest in the business logic layer using Fluent Validation. If the validation fails, we will throw an exception with the validation errors. If the validation is successful, we will proceed to add the product to the database using the repository and return the added product as a ProductResponse object.
            ValidationResult validationResult = await productAddValidatorRequest.ValidateAsync(productAddRequest);

            if (!validationResult.IsValid) 
            { 
                Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(temp => temp.PropertyName).ToDictionary(grp => grp.Key, grp => grp.Select(err => err.ErrorMessage).ToArray());
                return Results.ValidationProblem(errors);
            }

            ProductResponse? addedProduct = await productsService.AddProduct(productAddRequest);

            if (addedProduct == null) return Results.Problem("Failed to add product.");

            return Results.Created($"/api/products/search/product-id/{addedProduct.ProductID}",addedProduct);
        });

        //PUT /api/products
        app.MapPut("/api/products", async (IProductService productsService, IValidator<ProductUpdateRequest> productUpdateValidatorRequest, ProductUpdateRequest productUpdateRequest) =>
        {
            //This is a placeholder for the actual implementation of the AddProduct endpoint.
            //Validate the productAddRequest using Fluent validation
            //Each layer is responsible for its own validation, so we will validate the productAddRequest in the business logic layer using Fluent Validation. If the validation fails, we will throw an exception with the validation errors. If the validation is successful, we will proceed to add the product to the database using the repository and return the added product as a ProductResponse object.
            ValidationResult validationResult = await productUpdateValidatorRequest.ValidateAsync(productUpdateRequest);

            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(temp => temp.PropertyName).ToDictionary(grp => grp.Key, grp => grp.Select(err => err.ErrorMessage).ToArray());
                return Results.ValidationProblem(errors);
            }

            ProductResponse? updatedProduct = await productsService.UpdateProduct(productUpdateRequest);

            if (updatedProduct == null) return Results.Problem("Failed to update product.");

            return Results.Ok(updatedProduct);
        });

        //DELETE /api/products/00000-00000-00000-00000
        app.MapDelete("/api/products/{IdOfAProduct:guid}", async (IProductService productsService, Guid IdOfAProduct) =>
        {
            //This is a placeholder for the actual implementation of the AddProduct endpoint.
            bool isDeleted = await productsService.DeleteProduct(IdOfAProduct);

            if (!isDeleted) return Results.Problem("Failed to delete product.");

            return Results.Ok(true);
        });

        return app;
    }
}
