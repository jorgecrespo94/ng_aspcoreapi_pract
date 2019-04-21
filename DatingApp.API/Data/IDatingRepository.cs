using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
  public interface IDatingRepository
  {
      void Add<T>(T entity) where T: class;//method of Add where argument is a class (Model)
      void Delete<T>(T entity) where T: class;

      Task<bool> SaveAll();

      Task<IEnumerable<User>> GetUsers();
      Task<User> GetUser(int id);

      Task<Photo> GetPhoto(int id);

      Task<Photo> GetMainPhotoFromUser(int userId);
  }
}