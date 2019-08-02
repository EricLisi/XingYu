using KGM.Framework.Domain;
using KGM.Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel;
using System.IO;

namespace KGM.Framework.RepositoryEF
{
    public class UserCompanyContext : DbContext
    {
       
        public DbSet<UserCompanyEntity> Company { get; set; }
        public DbSet<UserDepartmentEntity> Department { get; set; }
        /// <summary>
        /// 重写自定义Map配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          
            EntityMapping<UserCompanyEntity>.MappingToTable(modelBuilder);
            EntityMapping<UserDepartmentEntity>.MappingToTable(modelBuilder);
        }

        /// <summary>
        /// 重写连接数据库
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            EFConnection.InitConnetion(optionsBuilder);
            base.OnConfiguring(optionsBuilder); 
        }
    }
}

