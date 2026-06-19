using Microsoft.AspNetCore.Identity;
using Mudrik.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Domain.Models;
public sealed class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;

    // Navigation properties
    public ParentProfile? ParentProfile { get; set; }
}
