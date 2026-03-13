using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Services;

public class TodoService
{
    private readonly IMongoCollection<Todo> _todosCollection;

    public TodoService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _todosCollection = mongoDatabase.GetCollection<Todo>(mongoDbSettings.Value.TodosCollectionName);
    }

    public async Task<List<Todo>> GetAllAsync() =>
        await _todosCollection.Find(_ => true).ToListAsync();

    public async Task<Todo?> GetByIdAsync(string id) =>
        await _todosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Todo newTodo) =>
        await _todosCollection.InsertOneAsync(newTodo);

    public async Task UpdateAsync(string id, Todo updatedTodo) =>
        await _todosCollection.ReplaceOneAsync(x => x.Id == id, updatedTodo);

    public async Task DeleteAsync(string id) =>
        await _todosCollection.DeleteOneAsync(x => x.Id == id);
}
