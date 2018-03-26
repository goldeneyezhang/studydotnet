using LearnNetCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Context
{
    public class DataContext:DbContext
    {
		//public DataContext(DbContextOptions<DataContext> options):base(options)
		//{

		//}
		public DbSet<User> Users { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//创建数据库
			var connection = "Filename=./efcoredemo.db";
			optionsBuilder.UseSqlite(connection);

		}
	}
}
