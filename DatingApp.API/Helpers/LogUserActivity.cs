using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.API.Helpers
{
  // what is a action filter - action on a higher level on a action to be executed in every method of a class when registered as a depency injection.
  public class LogUserActivity : IAsyncActionFilter
  {
      //ActionExecutingContext - do something when is being executed
      //ActionExecutionDelegate -do something after the action has been executed
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();//wait until action is been completed // this is going to be the action executed context for properties like http properties

        var userId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var repo = resultContext.HttpContext.RequestServices.GetService<IDatingRepository>();
        var user = await repo.GetUser(userId);
        user.LastActive = DateTime.Now;
        await repo.SaveAll();
    }
  }
}