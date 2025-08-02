using Carglass.TechnicalAssessment.Backend.Entities;

namespace Carglass.TechnicalAssessment.Backend.DL.Repositories;

public class ProductIMRepository : ICrudRepository<Product>
{
    private ICollection<Product> _products;

    public ProductIMRepository()
    {
        _products = new HashSet<Product>()
        {
            new Product()
            {
                Id = 1,
                ProductName = "Lapiz",
                ProductType = 10,
                NumTerminal = 1001233123,
                SoldAt = "2019-01-09 14:26:17"
            }
        };
    }

    public IEnumerable<Product> GetAll()
    {
        return _products.ToArray();
    }

    public Product? GetById(params object[] keyValues)
    {
        return _products.SingleOrDefault(x => x.Id.Equals(keyValues[0]));
    }

    public void Create(Product item)
    {
        _products.Add(item);
    }

    public void Update(Product item)
    {
        Product? product = _products.SingleOrDefault(x => x.Id.Equals(item.Id));
        if(product != null)
        {
            product.Id = item.Id;
            product.ProductName = item.ProductName;
            product.ProductType = item.ProductType; 
            product.NumTerminal = item.NumTerminal;
            product.SoldAt = item.SoldAt;
        }
    }

    public void Delete(Product item)
    {
        var toDeleteItem = _products.Single(x => x.Id.Equals(item.Id));

        _products.Remove(toDeleteItem);
    }

    public void Reset()
    {
        _products.Clear();
        _products.Add(new Product()
        {
            Id = 1,
            ProductName = "Lapiz",
            ProductType = 10,
            NumTerminal = 1001233123,
            SoldAt = "2019-01-09 14:26:17"
        });
        
    }
}
