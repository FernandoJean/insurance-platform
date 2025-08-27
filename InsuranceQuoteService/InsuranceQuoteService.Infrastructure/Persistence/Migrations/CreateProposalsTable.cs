using FluentMigrator;

namespace InsuranceQuoteService.Infrastructure.Persistence.Migrations
{
    [Migration(202508261)]
    public sealed class CreateProposalsTable : Migration
    {
        public override void Up()
        {
            Create.Table("proposals")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("customer_name").AsString(255).NotNullable()
                .WithColumn("insurance_type").AsString(100).NotNullable()
                .WithColumn("coverage_amount").AsDecimal(18, 2).NotNullable()
                .WithColumn("status").AsString(50).NotNullable().WithDefaultValue("Em Análise")
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("updated_at").AsDateTime().Nullable();
        }

        public override void Down()
        {
            if (Schema.Table("proposals").Exists())
            {
                Delete.Table("proposals");
            }
        }
    }
}