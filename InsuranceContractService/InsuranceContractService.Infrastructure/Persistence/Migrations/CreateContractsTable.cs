using FluentMigrator;

namespace InsuranceContractService.Infrastructure.Persistence.Migrations
{
    [Migration(202508271)]
    public sealed class CreateContractsTable : Migration
    {
        public override void Up()
        {
            Create.Table("contracts")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("proposal_id").AsGuid().NotNullable()
                .WithColumn("contracted_at").AsDate().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("contracts");
        }
    }
}