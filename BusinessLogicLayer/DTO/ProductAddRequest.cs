namespace BusinessLogicLayer.DTO;

public record ProductAddRequest(string? ProductName, 
   CategoryOptions Category, decimal? UnitPrice, int? Stock)
{
    public ProductAddRequest() : this(default, default, default, default)
    {
    }
}


