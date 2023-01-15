using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan.Domain.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ScoreScore = table.Column<string>(name: "Score_Score", type: "text", nullable: true),
                    ScoreExplanation = table.Column<string>(name: "Score_Explanation", type: "text", nullable: true),
                    CustomerNameFirst = table.Column<string>(name: "Customer_Name_First", type: "text", nullable: true),
                    CustomerNameLast = table.Column<string>(name: "Customer_Name_Last", type: "text", nullable: true),
                    CustomerBirthdate = table.Column<DateTime>(name: "Customer_Birthdate", type: "timestamp without time zone", nullable: true),
                    CustomerMonthlyIncome = table.Column<decimal>(name: "Customer_MonthlyIncome", type: "numeric", nullable: true),
                    CustomerAddressCountry = table.Column<string>(name: "Customer_Address_Country", type: "text", nullable: true),
                    CustomerAddressZipCode = table.Column<string>(name: "Customer_Address_ZipCode", type: "text", nullable: true),
                    CustomerAddressCity = table.Column<string>(name: "Customer_Address_City", type: "text", nullable: true),
                    CustomerAddressStreet = table.Column<string>(name: "Customer_Address_Street", type: "text", nullable: true),
                    CustomerEmailMailValue = table.Column<string>(name: "Customer_Email_MailValue", type: "text", nullable: true),
                    CustomerNationalIdentifierValue = table.Column<string>(name: "Customer_NationalIdentifier_Value", type: "text", nullable: true),
                    PropertyValue = table.Column<decimal>(name: "Property_Value", type: "numeric", nullable: true),
                    PropertyAddressCountry = table.Column<string>(name: "Property_Address_Country", type: "text", nullable: true),
                    PropertyAddressZipCode = table.Column<string>(name: "Property_Address_ZipCode", type: "text", nullable: true),
                    PropertyAddressCity = table.Column<string>(name: "Property_Address_City", type: "text", nullable: true),
                    PropertyAddressStreet = table.Column<string>(name: "Property_Address_Street", type: "text", nullable: true),
                    LoanLoanAmount = table.Column<decimal>(name: "Loan_LoanAmount", type: "numeric", nullable: true),
                    LoanLoanNumberOfYears = table.Column<int>(name: "Loan_LoanNumberOfYears", type: "integer", nullable: true),
                    LoanInterestRate = table.Column<decimal>(name: "Loan_InterestRate", type: "numeric", nullable: true),
                    RegistrationRegistrationDate = table.Column<DateTime>(name: "Registration_RegistrationDate", type: "timestamp without time zone", nullable: true),
                    RegistrationRegisteredBy = table.Column<Guid>(name: "Registration_RegisteredBy", type: "uuid", nullable: true),
                    DecisionDecisionDate = table.Column<DateTime>(name: "Decision_DecisionDate", type: "timestamp without time zone", nullable: true),
                    DecisionDecisionBy = table.Column<Guid>(name: "Decision_DecisionBy", type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    CompetenceLevelAmount = table.Column<decimal>(name: "CompetenceLevel_Amount", type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanApplications");

            migrationBuilder.DropTable(
                name: "Operators");
        }
    }
}
