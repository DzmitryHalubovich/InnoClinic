using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Profiles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPatients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonalInformationId",
                table: "PersonalInfo",
                newName: "PersonalInfoId");

            migrationBuilder.RenameColumn(
                name: "PersonalInformationId",
                table: "Accounts",
                newName: "PersonalInfoId");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_AccountId",
                table: "Doctors",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecializationId",
                table: "Doctors",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PersonalInfoId",
                table: "Accounts",
                column: "PersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AccountId",
                table: "Patients",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_PersonalInfo_PersonalInfoId",
                table: "Accounts",
                column: "PersonalInfoId",
                principalTable: "PersonalInfo",
                principalColumn: "PersonalInfoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Accounts_AccountId",
                table: "Doctors",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specializations_SpecializationId",
                table: "Doctors",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "SpecializationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_PersonalInfo_PersonalInfoId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Accounts_AccountId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specializations_SpecializationId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_AccountId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_SpecializationId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_PersonalInfoId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "PersonalInfoId",
                table: "PersonalInfo",
                newName: "PersonalInformationId");

            migrationBuilder.RenameColumn(
                name: "PersonalInfoId",
                table: "Accounts",
                newName: "PersonalInformationId");
        }
    }
}
