using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerInquiry.Migrations
{
    public partial class AddInvoiceColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Invoice",
                table: "Transactions",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactEmail",
                table: "Customers",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Invoice",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "ContactEmail",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);
        }
    }
}
