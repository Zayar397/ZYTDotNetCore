using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZYTDotNetCore.ConsoleApp.Models;

namespace ZYTDotNetCore.ConsoleApp
{
    public class DotNetTrainingBatch5DbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source = DESKTOP-BPF6HTF\\SQL2022; Initial Catalog = DotNetTrainingBatch5; User Id = sa; Password = p@ssw0rd; Trust Server Certificate = True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
