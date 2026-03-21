namespace BusinessLogicLayer.DTO;

public record ProductUpdateRequest(Guid ProductID, string? ProductName,
   CategoryOptions Category, decimal? UnitPrice, int? Stock)
{
    public ProductUpdateRequest() : this(default, default, default, default, default)
    {
    }
}

