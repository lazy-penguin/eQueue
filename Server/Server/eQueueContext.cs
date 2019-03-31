﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eQueue
{
    class eQueueContext: DbContext
    {
        public DbSet<QueueInfo> Queues { get; set; }
        public DbSet<QueueOrder> QueueOrders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccess> UserAccesses { get; set; }

        public eQueueContext() : base(nameOrConnectionString: "Default") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}
