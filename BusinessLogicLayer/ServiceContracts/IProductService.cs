using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Linq.Expressions;

namespace BusinessLogicLayer.ServiceContracts;

public interface IProductService
{
    /// <summary>
    /// Retrieves a list of products from the data access layer asynchronously. This method is responsible for fetching the product data and returning it as a list of ProductsRepository objects. The implementation should handle the logic for interacting with the data access layer, such as calling the appropriate repository methods to retrieve the products and ensuring that the data is returned in a format suitable for further processing or presentation in the business logic layer.
    /// </summary>
    /// <returns>Returns list of ProductResponse objects.</returns>
    Task<List<ProductResponse?>> GetProducts();

    /// <summary>
    /// Retrieves a list of products from the data access layer that satisfy the specified condition asynchronously. The method accepts an expression that defines the condition to filter the products. This allows for flexible querying based on various criteria, such as product name, category, price range, etc. The implementation should ensure that the query is executed efficiently and returns the appropriate results based on the provided condition. The returned list should contain ProductResponse objects that match the specified condition.
    /// </summary>
    /// <returns>Returns list of matching products.</returns>
    Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);

    /// <summary>
    /// Retrieves a single product from the data access layer that satisfies the specified condition asynchronously. The method accepts an expression that defines the condition to filter the products. This allows for flexible querying based on various criteria, such as product name, category, price range, etc. The implementation should ensure that the query is executed efficiently and returns the appropriate result based on the provided condition. If no product matches the condition, the method should return null. The returned object should be a ProductResponse that matches the specified condition.
    /// </summary>
    /// <param name="conditionExpression">Express that represents the condition to check.</param>
    /// <returns>Returns a product or null.</returns>
    Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression);

    /// <summary>
    /// Adds (inserts) a new product to the data access layer asynchronously. The method accepts a ProductAddRequest object as a parameter, which contains the necessary information for creating a new product. The implementation should handle the logic for adding the product, such as validating the input, managing interactions with the data access layer, and ensuring that the product is saved correctly. Upon successful addition, the method should return a ProductResponse object representing the added product, which may include any generated values (e.g., Id) after being saved to the database. If the addition operation fails (e.g., due to validation errors or database issues), the method should return null.
    /// </summary>
    /// <param name="productAddRequest">Products to insert.</param>
    /// <returns>Products after inserting or null if unsuccessful.</returns>
    Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);

    /// <summary>
    /// Updates the existing product in the data access layer asynchronously. The method accepts a ProductUpdateRequest object as a parameter, which contains the necessary information for updating an existing product. The implementation should handle the logic for updating the product, such as validating the input, managing interactions with the data access layer, and ensuring that the product is updated correctly in the database. Upon successful update, the method should return a ProductResponse object representing the updated product. If the update operation fails (e.g., if the product does not exist or due to validation errors), the method should return null.
    /// </summary>
    /// <param name="productUpdateRequest">Product data to update.</param>
    /// <returns>Returns product object after successful updation; otherwise null.</returns>
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);

    /// <summary>
    /// Deletes an existing product from the data access layer asynchronously based on the provided product ID. The method accepts a Guid representing the unique identifier of the product to be deleted. The implementation should handle the logic for deleting the product, such as validating the input, managing interactions with the data access layer, and ensuring that the product is removed correctly from the database. Upon successful deletion, the method should return true. If the deletion operation fails (e.g., if the product does not exist or due to database issues), the method should return false.
    /// </summary>
    /// <param name="productID">ProductID to search and delete a Product.</param>
    /// <returns>Returns true if deletion is successful; otherwise false.</returns>
    Task<bool> DeleteProduct(Guid productID);
}
