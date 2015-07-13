﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MinHangWisdomParkWeb.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class ajIIPdbEntities1 : DbContext
    {
        public ajIIPdbEntities1()
            : base("name=ajIIPdbEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Functions> Functions { get; set; }
        public DbSet<mtFunction> mtFunction { get; set; }
        public DbSet<mtOwner> mtOwner { get; set; }
        public DbSet<mtPower> mtPower { get; set; }
        public DbSet<mtRfidCode> mtRfidCode { get; set; }
        public DbSet<mtUniversalCode> mtUniversalCode { get; set; }
        public DbSet<mtUser> mtUser { get; set; }
        public DbSet<tbCar> tbCar { get; set; }
        public DbSet<tbFiles> tbFiles { get; set; }
        public DbSet<tbIORecord> tbIORecord { get; set; }
        public DbSet<tbOwnerRfid> tbOwnerRfid { get; set; }
        public DbSet<tbPeople> tbPeople { get; set; }
        public DbSet<tbProduct> tbProduct { get; set; }
        public DbSet<tbReceive> tbReceive { get; set; }
        public DbSet<mtRegistrant> mtRegistrant { get; set; }
        public DbSet<tbCheckInOut> tbCheckInOut { get; set; }
        public DbSet<tbCheckInOuts> tbCheckInOuts { get; set; }
        public DbSet<mtConfirmFlow> mtConfirmFlow { get; set; }
        public DbSet<mtConfirmLevel> mtConfirmLevel { get; set; }
        public DbSet<mdlPeblish> mdlPeblish { get; set; }
        public DbSet<tbPeblish> tbPeblish { get; set; }
        public DbSet<tbConfirmState> tbConfirmState { get; set; }
        public DbSet<tbRepair> tbRepair { get; set; }
        public DbSet<mtActor> mtActor { get; set; }
        public DbSet<tbActorPower> tbActorPower { get; set; }
        public DbSet<tbApplyBill> tbApplyBill { get; set; }
        public DbSet<tbTemp> tbTemp { get; set; }
        public DbSet<tbBuniess> tbBuniess { get; set; }
    
        public virtual int Proc_RegistrantID(string registrantType)
        {
            var registrantTypeParameter = registrantType != null ?
                new ObjectParameter("RegistrantType", registrantType) :
                new ObjectParameter("RegistrantType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Proc_RegistrantID", registrantTypeParameter);
        }
    }
}
