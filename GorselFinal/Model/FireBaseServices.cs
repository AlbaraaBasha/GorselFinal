using Firebase.Database;
using Firebase.Database.Query;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace GorselFinal.Model
{
    public class FireBaseServices
    {
        private readonly FirebaseClient firebaseClient;

        public FireBaseServices()
        {
            firebaseClient = new FirebaseClient("https://gorselfinal2-default-rtdb.firebaseio.com/");
        }

        public async Task<User?> AddUser(User user)
        {
            try
            {
                await firebaseClient
                  .Child("Users")
                  .PostAsync(user);
                return user;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<User?> GetUserByEmailAndPassword(string email, string password)
        {
            try
            {
                var allUsers = await firebaseClient
                  .Child("Users")
                  .OnceAsync<User>();

                return allUsers.FirstOrDefault(u => u.Object.Email == email && u.Object.Password == password)?.Object;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task AddTask(TodoTask task)
        {
            await firebaseClient
                .Child("Tasks")
                .PostAsync(task);
        }

        public async Task UpdateTask(TodoTask task)
        {
            var toUpdateTask = (await firebaseClient
                .Child("Tasks")
                .OnceAsync<TodoTask>())
                .FirstOrDefault(a => a.Object.Title == task.Title && a.Object.Description == task.Description);

            if (toUpdateTask != null)
            {
                await firebaseClient
                    .Child("Tasks")
                    .Child(toUpdateTask.Key)
                    .PutAsync(task);
            }
        }

        public async Task DeleteTask(TodoTask task)
        {
            var toDeleteTask = (await firebaseClient
                .Child("Tasks")
                .OnceAsync<TodoTask>())
                .FirstOrDefault(a => a.Object.Title == task.Title && a.Object.Description == task.Description);

            if (toDeleteTask != null)
            {
                await firebaseClient
                    .Child("Tasks")
                    .Child(toDeleteTask.Key)
                    .DeleteAsync();
            }
        }

        public async Task<List<TodoTask>> GetTasks()
        {
            var tasks = await firebaseClient
                .Child("Tasks")
                .OnceAsync<TodoTask>();

            if (tasks != null)
            {
                return tasks.Select(item => new TodoTask
                {
                    Title = item.Object.Title,
                    Description = item.Object.Description,
                    Date = item.Object.Date,
                    Time = item.Object.Time,
                    IsCompleted = item.Object.IsCompleted
                }).ToList();
            }

            return new List<TodoTask>();
        }
    }

    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public object Info { get; internal set; }
    }

    public class TodoTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public bool IsCompleted { get; set; }
    }
}