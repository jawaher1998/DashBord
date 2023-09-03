using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DashBord.Areas.Identity.Data;

// Add profile data for application users by adding properties to the DashBordUser class
public class DashBordUser : IdentityUser
{
	public String FirstName { get; set; }
	public String LastName { get; set; }
}

