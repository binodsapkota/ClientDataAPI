using ClientDataAPI.Data;

namespace ClientDataAPI.Services
{
    public class ClientService
    {
        private readonly ClientDbContext _dbContext;

        public ClientService(ClientDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Client> GetAllClients() => _dbContext.Clients.ToList();

        public Client GetClientById(int id) => _dbContext.Clients.Find(id);

        public void CreateClient(Client client)
        {
            client.CreatedAt = DateTime.UtcNow;
            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();
        }
    }
}
