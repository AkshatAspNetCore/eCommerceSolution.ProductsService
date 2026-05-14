namespace BusinessLogicLayer.DTO;

public record ProductUpdateRequest(Guid ProductID, string? ProductName,
   CategoryOptions Category, decimal? UnitPrice, int? QuantityInStock)
{
    public ProductUpdateRequest() : this(default, default, default, default, default)
    {
    }
}

