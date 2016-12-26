using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;

namespace Domen.Migrations
{
    public class HistoryCtx:HistoryContext
    {
        public HistoryCtx(DbConnection dbConnection, string defaultSchema):base(dbConnection, defaultSchema)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HistoryRow>().ToTable(tableName: "MigrationHistory", schemaName: "masterShema");
            modelBuilder.Entity<HistoryRow>().Property(row => row.MigrationId).HasColumnName("Migration_ID");
        }
    }

    public class ModelConfig : DbConfiguration
    {
        public ModelConfig()
        {
            this.SetHistoryContext("System.Data.SqlClient", (connection, defaultShema) => new HistoryCtx(connection, defaultShema));
        }
    }
}