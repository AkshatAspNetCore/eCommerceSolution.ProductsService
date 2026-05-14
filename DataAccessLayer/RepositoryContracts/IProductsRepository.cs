using DataAccessLayer.Entities;
using Mysqlx.Crud;
using System.Linq.Expressions;

namespace DataAccessLayer.RepositoryContracts;

/// <summary>
/// Represents a repository for managing products in the data access layer. This interface defines the contract for operations related to product entities, such as retrieving, adding, updating, and deleting products from the database.
/// </summary>
public interface IProductsRepository
{
    /// <summary>
    /// Retrieves a collection of all products from the database asynchronously. This method is asynchronous and returns a task that resolves to an enumerable of Product entities. The implementation of this method should handle the logic for querying the database and returning the results in an efficient manner.
    /// </summary>
    /// <returns>
    /// Returns all products from the database as an enumerable collection of Product entities. The method is asynchronous, so it returns a Task that resolves to the collection of products when the operation is complete.
    /// </returns>
    Task<IEnumerable<Product>> GetProducts();

    /// <summary>
    /// Retrieves a collection of products from the database that satisfy the specified condition asynchronously. The method accepts an expression that defines the condition to filter the products. This allows for flexible querying based on various criteria, such as product name, category, price range, etc. The implementation should ensure that the query is executed efficiently and returns the appropriate results based on the provided condition.
    /// </summary>
    /// <param name="conditionExpression">The condition to filter the product.</param>
    /// <returns>Returns a collection of matching products</returns>
    Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product,bool>> conditionExpression);

    /// <summary>
    /// Retrieves a single product from the database that satisfies the specified condition asynchronously. The method accepts an expression that defines the condition to filter the products. This allows for flexible querying based on various criteria, such as product name, category, price range, etc. The implementation should ensure that the query is executed efficiently and returns the appropriate result based on the provided condition. If no product matches the condition, the method should return null. 
    /// </summary>
    /// <param name="conditionExpression">The condition to filter the product.</param>
    /// <returns>Returns a single product or null if not found.</returns>
    Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression);

    /// <summary>
    /// Adds a new product to the database asynchronously. The method accepts a Product entity as a parameter and is responsible for inserting it into the database. The implementation should handle the logic for adding the product, such as validating the input, managing database connections, and ensuring that the product is saved correctly. Upon successful addition, the method should return the added Product entity, which may include any generated values (e.g., Id) after being saved to the database.
    /// </summary>
    /// <param name="product">The product to be added.</param>
    /// <returns>Returns the added product object or null if unseccessful.</returns>
    Task<Product?> AddProduct(Product product);

    /// <summary>
    /// Updates an existing product in the database asynchronously. The method accepts a Product entity as a parameter, which contains the updated information for the product. The implementation should handle the logic for updating the product, such as validating the input, managing database connections, and ensuring that the product is updated correctly in the database. Upon successful update, the method should return the updated Product entity. If the update operation fails (e.g., if the product does not exist), the method should return null.
    /// </summary>
    /// <param name="product">The product to be updated.</param>
    /// <returns>Returns the updated product object or null if unseccessful.</returns>
    Task<Product?> UpdateProduct(Product product);

    /// <summary>
    /// Deletes a product from the database asynchronously based on the provided product ID. The method accepts a Guid representing the unique identifier of the product to be deleted. The implementation should handle the logic for deleting the product, such as validating the input, managing database connections, and ensuring that the product is removed correctly from the database. Upon successful deletion, the method should return true. If the deletion operation fails (e.g., if the product does not exist), the method should return false.
    /// </summary>
    /// <param name="productID">The product ID to be deleted.</param>
    /// <returns>Returns true if the deletion is successsful, false otherwise.</returns>
    Task<bool> DeleteProduct(Guid productID);
}

