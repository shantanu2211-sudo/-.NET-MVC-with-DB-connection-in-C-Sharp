using MongoDB.Driver;
using StudentRegistration.Models;

namespace StudentRegistration.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Student> _students;

        public MongoDBService(IConfiguration config)
        {
            var client = new MongoClient(
                config["MongoDB:ConnectionString"]);

            var database = client.GetDatabase(
                config["MongoDB:DatabaseName"]);

            _students = database.GetCollection<Student>(
                config["MongoDB:CollectionName"]);
        }

        // Get all students
        public async Task<List<Student>> GetAllAsync()
        {
            return await _students.Find(x => true).ToListAsync();
        }

        // Add student
        public async Task CreateAsync(Student student)
        {
            await _students.InsertOneAsync(student);
        }

        // Find student
        public async Task<Student?> GetByIdAsync(string id)
        {
            return await _students
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        // Update student
        public async Task UpdateAsync(string id, Student student)
        {
            await _students.ReplaceOneAsync(
                x => x.Id == id, student);
        }

        // Delete student
        public async Task DeleteAsync(string id)
        {
            await _students.DeleteOneAsync(
                x => x.Id == id);
        }
    }
}