using Microsoft.AspNetCore.Identity;
using CloudNest.Api.Models;

public class User : IdentityUser
{
    public ICollection<DirectoryShare> DirectoryShares { get; set; } = new List<DirectoryShare>();
}