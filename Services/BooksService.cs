using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services
{
    public class BooksService
    {
        public readonly IMongoCollection<BookModel> _bookCollection;
        public BooksService(IOptions<DatabaseSettingModel> bookStoreDatabaseSetting)
        {
            var mongoClient = new MongoClient(bookStoreDatabaseSetting.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSetting.Value.DatabaseName);

            _bookCollection = mongoDatabase.GetCollection<BookModel>(
                bookStoreDatabaseSetting.Value.BookCollectionName);
        }

        public async Task<List<BookModel>> GetAsync() =>
            await _bookCollection.Find(_ => true).ToListAsync();

        public async Task<BookModel?> GetAsync(string id) =>
            await _bookCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(BookModel newBook) =>
            await _bookCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, BookModel updatedBook) =>
            await _bookCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _bookCollection.DeleteOneAsync(x => x.Id == id);

    }
}
