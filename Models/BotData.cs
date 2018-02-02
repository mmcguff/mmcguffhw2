namespace SimpleEchoBot.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BotData : DbContext
    {
        public BotData()
            : base("name=BotData")
        {
        }

        public virtual DbSet<UserLog> UserLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
