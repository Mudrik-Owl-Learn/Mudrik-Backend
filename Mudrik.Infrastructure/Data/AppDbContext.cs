using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Mudrik.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options), IAppDbContext
    {
        
    }
}
