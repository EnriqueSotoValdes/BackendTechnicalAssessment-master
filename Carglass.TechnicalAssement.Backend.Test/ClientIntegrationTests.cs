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

public class ClientIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _server;
    private readonly WebApplicationFactory<Program> _factory;
    public ClientIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _server = factory.CreateClient();
    }

    [Fact]
    public async void GetClients()
    {

        // Act
        var response = await _server.GetAsync($"/clients");

        // Assert
        response.EnsureSuccessStatusCode();
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var content = await response.Content.ReadAsStringAsync();
        var clients = JsonSerializer.Deserialize<List<ClientDto>>(content, jsonOptions);

        Assert.NotNull(clients);
        Assert.True(clients.Count == 1);
    }

    [Fact]
    public async void PostClient()
    {
        //arrange

        var newClient = new
        {
            id = 12345,
	        docType = "nif",
	        docNum = "11223344A",
	        email = "eromani@sample.com",
	        givenName = "Enriqueta",
	        familyName1 = "Romani",
	        phone = "668668668"
        };

        var json = JsonSerializer.Serialize(newClient);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var responsePost = await _server.PostAsync($"/clients", content);

        responsePost.EnsureSuccessStatusCode();

        var responseGet = await _server.GetAsync($"/clients");
        // Assert
        responseGet.EnsureSuccessStatusCode();
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var returnContent = await responseGet.Content.ReadAsStringAsync();
        var clients = JsonSerializer.Deserialize<List<ClientDto>>(returnContent, jsonOptions);

        Assert.NotNull(clients);
        Assert.True(clients.Count == 2);


        var repository = _factory.Services.GetRequiredService<ICrudRepositoryExtension<Client>>();
        repository.Reset();
    }

    [Fact]
    public async void PutClient()
    {
        //arrange

        var newClient = new
        {
            id = 12345,
            docType = "nif",
            docNum = "11223344A",
            email = "eromani@sample.com",
            givenName = "Enriqueta",
            familyName1 = "Romani",
            phone = "668668668"
        };

        var json = JsonSerializer.Serialize(newClient);
        var postContent = new StringContent(json, Encoding.UTF8, "application/json");


        var newModifiedClient = new
        {
            id = 12345,
            docType = "nif",
            docNum = "11223344C",
            email = "eromani@sample.com",
            givenName = "Enriqueta",
            familyName1 = "Romani",
            phone = "668668668"
        };

        json = JsonSerializer.Serialize(newModifiedClient);
        var putContent = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var responsePost = await _server.PostAsync($"/clients", postContent);
        responsePost.EnsureSuccessStatusCode();

        var responsePut= await _server.PutAsync($"/clients", putContent);
        responsePut.EnsureSuccessStatusCode();

        var responseGet = await _server.GetAsync($"/clients");
        // Assert
        responseGet.EnsureSuccessStatusCode();
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var returnContent = await responseGet.Content.ReadAsStringAsync();
        var clients = JsonSerializer.Deserialize<List<ClientDto>>(returnContent, jsonOptions);

        Assert.NotNull(clients);
        Assert.True(clients.Count == 2);

        var client = clients.Find( element =>  element.Id == newModifiedClient.id );

        Assert.NotNull(client);
        Assert.True(client.DocNum == newModifiedClient.docNum && client.DocNum != newClient.docNum);

        var repository = _factory.Services.GetRequiredService<ICrudRepositoryExtension<Client>>();
        repository.Reset();
    }

    [Fact]
    public async void DeleteClient()
    {
        //arrange

        var client = new
        {
            id = 1,
            docType = "nif",
            docNum = "11223344E",
            email = "eromani@sample.com",
            givenName = "Enriqueta",
            familyName1 = "Romani",
            phone = "668668668"
        };

        var json = JsonSerializer.Serialize(client);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _server.SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri("/clients", UriKind.Relative),
            Content = content
        });

        // Assert
        response.EnsureSuccessStatusCode();
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var returnContent = await response.Content.ReadAsStringAsync();

        Assert.True(string.IsNullOrEmpty(returnContent));

        var repository = _factory.Services.GetRequiredService<ICrudRepositoryExtension<Client>>();
        repository.Reset();
    }

    [Fact]
    public async void GetClientID()
    {
        //arrange
        var clientId = 1;

        // Act
        var response = await _server.GetAsync($"/clients/{clientId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var content = await response.Content.ReadAsStringAsync();
        var client = JsonSerializer.Deserialize<ClientDto>(content, jsonOptions);

        Assert.NotNull(client);
    }
}