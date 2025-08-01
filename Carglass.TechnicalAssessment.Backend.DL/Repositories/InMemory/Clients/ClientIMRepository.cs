using Carglass.TechnicalAssessment.Backend.Entities;

namespace Carglass.TechnicalAssessment.Backend.DL.Repositories;

public class ClientIMRepository : ICrudRepositoryExtension<Client>
{
    private ICollection<Client> _clients;

    public ClientIMRepository()
    {
        _clients = new HashSet<Client>()
        {
            new Client()
            {
                Id = 1,
                DocType = "nif",
                DocNum = "11223344E",
                Email = "eromani@sample.com",
                GivenName = "Enriqueta",
                FamilyName1 = "Romani",
                Phone = "668668668"
            }
        };
    }

    public IEnumerable<Client> GetAll()
    {
        return _clients.ToArray();
    }

    public Client? GetById(params object[] keyValues)
    {
        return _clients.SingleOrDefault(x => x.Id.Equals(keyValues[0]));
    }
    public Client? GetByDocNum(params object[] keyValues)
    {
        return _clients.SingleOrDefault(x => x.DocNum.Equals(keyValues[0]));
    }

    public void Create(Client item)
    {
        _clients.Add(item);
    }

    public void Update(Client item)
    {
        Client? client = _clients.SingleOrDefault(x => x.Id.Equals(item.Id));
        if(client != null)
        {
            client.Id = item.Id;
            client.DocType = item.DocType;
            client.DocNum = item.DocNum;
            client.Email = item.Email;
            client.GivenName = item.GivenName;
            client.FamilyName1 = item.FamilyName1;
            client.Phone = item.Phone;
        }
    }

    public void Delete(Client item)
    {
        var toDeleteItem = _clients.Single(x => x.Id.Equals(item.Id));

        _clients.Remove(toDeleteItem);
    }
}
