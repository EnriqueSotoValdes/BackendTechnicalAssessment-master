using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Carglass.TechnicalAssessment.Backend.Entities;
using System.Net;
using Carglass.TechnicalAssessment.Backend.Dtos;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;

namespace Carglass.TechnicalAssement.Backend.Test;

public class ProductIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _server;
    private readonly WebApplicationFactory<Program> _factory;
    public ProductIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _server = factory.CreateClient();
    }

    [Fact]
    public async void GetProducts()
    {

        // Act
        var response = await _server.GetAsync($"/products");

        // Assert
        response.EnsureSuccessStatusCode();
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var content = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<ProductDto>>(content, jsonOptions);

        Assert.NotNull(products);
        Assert.True(products.Count == 1);
    }

    [Fact]
    public async void PostProduct()
    {
        //arrange

        var newProduct = new
        {
            id = 1111111,
            productName = "Cristal ventanilla delantera",
            productType = 25,
            numTerminal = 933933933,
            soldAt = "2019-01-09 14:26:17"
        };

        var json = JsonSerializer.Serialize(newProduct);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var responsePost = await _server.PostAsync($"/products", content);

        responsePost.EnsureSuccessStatusCode();

        var responseGet = await _server.GetAsync($"/products");
        // Assert
        responseGet.EnsureSuccessStatusCode();
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var returnContent = await responseGet.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<ClientDto>>(returnContent, jsonOptions);

        Assert.NotNull(products);
        Assert.True(products.Count == 2);


        var repository = _factory.Services.GetRequiredService<ICrudRepository<Product>>();
        repository.Reset();
    }

    [Fact]
    public async void PutProduct()
    {
        //arrange

        var newProduct = new
        {
            id = 1111111,
            productName = "Cristal ventanilla delantera",
            productType = 25,
            numTerminal = 933933933,
            soldAt = "2019-01-09 14:26:17"
        };

        var json = JsonSerializer.Serialize(newProduct);
        var postContent = new StringContent(json, Encoding.UTF8, "application/json");


        var newModifiedProduct = new
        {
            id = 1111111,
            productName = "Cristal ventanilla delantera",
            productType = 10,
            numTerminal = 933933933,
            soldAt = "2019-01-09 14:26:17"
        };

        json = JsonSerializer.Serialize(newModifiedProduct);
        var putContent = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var responsePost = await _server.PostAsync($"/products", postContent);
        responsePost.EnsureSuccessStatusCode();

        var responsePut= await _server.PutAsync($"/products", putContent);
        responsePut.EnsureSuccessStatusCode();

        var responseGet = await _server.GetAsync($"/products");
        // Assert
        responseGet.EnsureSuccessStatusCode();
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var returnContent = await responseGet.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<ProductDto>>(returnContent, jsonOptions);

        Assert.NotNull(products);
        Assert.True(products.Count == 2);

        var product = products.Find( element =>  element.Id == newModifiedProduct.id );

        Assert.NotNull(product);
        Assert.True(product.ProductType == newModifiedProduct.productType && product.ProductType != newProduct.productType);

        var repository = _factory.Services.GetRequiredService<ICrudRepository<Product>>();
        repository.Reset();
    }

    [Fact]
    public async void DeleteProduct()
    {
        //arrange

        var product = new
        {
            id = 1,
            productName= "Lapiz",
            productType= 10,
            numTerminal= 1001233123,
            soldAt = "2019-01-09 14:26:17"
        };

        var json = JsonSerializer.Serialize(product);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _server.SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri("/products", UriKind.Relative),
            Content = content
        });

        // Assert
        response.EnsureSuccessStatusCode();

        var returnContent = await response.Content.ReadAsStringAsync();

        Assert.True(string.IsNullOrEmpty(returnContent));

        var repository = _factory.Services.GetRequiredService<ICrudRepository<Product>>();
        repository.Reset();
    }

    [Fact]
    public async void GetProductID()
    {
        //arrange
        var productId = 1;

        // Act
        var response = await _server.GetAsync($"/products/{productId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var content = await response.Content.ReadAsStringAsync();
        var product = JsonSerializer.Deserialize<ProductDto>(content, jsonOptions);

        Assert.NotNull(product);
    }
}