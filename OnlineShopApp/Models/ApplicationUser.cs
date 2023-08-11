using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopApp.Models
{
	public class ApplicationUser:IdentityUser
	{
		[Required]
		public string Name { get; set; }

	}
}
