namespace Carglass.TechnicalAssessment.Backend.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string productName { get; set; }
    public int productType { get; set; }
    public long numTerminal { get; set; }
    public string soldAt { get; set; }
}

