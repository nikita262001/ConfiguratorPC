//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AbdrakhmanovPCConfigurator.WPF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AbdrakhmanovDB : DbContext
    {
        public AbdrakhmanovDB()
            : base("name=AbdrakhmanovDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<FinishedPC> FinishedPCs { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
