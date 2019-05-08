using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Helpers;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
  public interface IDatingRepository
  {
      void Add<T>(T entity) where T: class;//method of Add where argument is a class (Model)
      void Delete<T>(T entity) where T: class;
      Task<bool> SaveAll();
      Task<PagedList<User>> GetUsers(UserParams UserParams);
      Task<User> GetUser(int id);
      Task<Photo> GetPhoto(int id);
      Task<Photo> GetMainPhotoFromUser(int userId);
      Task<Like> GetLike(int userId, int recipientId);//check like if already exits for a user
      Task<Message> GetMessage(int id);//get a single message from the db
      Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);//get all message of user
      Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
  }
}