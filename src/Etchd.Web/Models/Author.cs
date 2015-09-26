using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Etchd.Web.Models
{
    public class Author : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}