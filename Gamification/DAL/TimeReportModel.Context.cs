﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gamification.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PEIS_DATABASEEntities : DbContext
    {
        public PEIS_DATABASEEntities()
            : base("name=PEIS_DATABASEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<GTAssignment> GTAssignment { get; set; }
        public DbSet<GTResource> GTResource { get; set; }
        public DbSet<GTTimeEntry> GTTimeEntry { get; set; }
        public DbSet<GTTimePeriod> GTTimePeriod { get; set; }
        public DbSet<GTTimeSheet> GTTimeSheet { get; set; }
    }
}
