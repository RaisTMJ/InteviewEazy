using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Recipe.Application.Features
{
   public record LoginQuery(
       [Required(ErrorMessage = "Email is required")]
       [EmailAddress(ErrorMessage = "Please enter a valid email address")]
       string Email,
       
       [Required(ErrorMessage = "Password is required")]
       string Password
   ) : IRequest<string>;
}
