﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DinaD
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DemoLastEntities : DbContext
    {
        public DemoLastEntities()
            : base("name=DemoLastEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Action> Action { get; set; }
        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<BlockchainAction> BlockchainAction { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Field> Field { get; set; }
        public virtual DbSet<Jury> Jury { get; set; }
        public virtual DbSet<Moderator> Moderator { get; set; }
        public virtual DbSet<Organizer> Organizer { get; set; }
        public virtual DbSet<Participant> Participant { get; set; }
        public virtual DbSet<Sex> Sex { get; set; }
    }
}
