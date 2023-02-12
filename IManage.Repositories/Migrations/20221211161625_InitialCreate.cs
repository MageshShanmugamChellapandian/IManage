using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IManage.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "function_right",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_function_right", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "function_right_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_function_right_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fullname = table.Column<string>(name: "full_name", type: "nvarchar(200)", maxLength: 200, nullable: false),
                    isexfactory = table.Column<bool>(name: "is_exfactory", type: "bit", nullable: true, defaultValueSql: "'0'"),
                    emailid = table.Column<string>(name: "email_id", type: "nvarchar(200)", maxLength: 200, nullable: false),
                    avatarurl = table.Column<string>(name: "avatar_url", type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "function_right_type_mapping",
                columns: table => new
                {
                    functionrightID = table.Column<int>(type: "int", nullable: false),
                    typeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FRTMPRIMARY", x => new { x.functionrightID, x.typeID });
                    table.ForeignKey(
                        name: "fk_id",
                        column: x => x.functionrightID,
                        principalTable: "function_right",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_typeid",
                        column: x => x.typeID,
                        principalTable: "function_right_type",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    typeid = table.Column<int>(name: "type_id", type: "int", nullable: false),
                    isexfactory = table.Column<bool>(name: "is_exfactory", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_type",
                        column: x => x.typeid,
                        principalTable: "role_type",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "role_function_right_mapping",
                columns: table => new
                {
                    roleid = table.Column<int>(name: "role_id", type: "int", nullable: false),
                    functionrightid = table.Column<int>(name: "function_right_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("RFRMPRIMARY", x => new { x.roleid, x.functionrightid });
                    table.ForeignKey(
                        name: "FK_RoleFunctionRightMapping_FunctionRight",
                        column: x => x.functionrightid,
                        principalTable: "function_right",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_RoleFunctionRightMapping_Role",
                        column: x => x.roleid,
                        principalTable: "role",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_role_mapping",
                columns: table => new
                {
                    userid = table.Column<int>(name: "user_id", type: "int", nullable: false),
                    roleid = table.Column<int>(name: "role_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.userid, x.roleid });
                    table.ForeignKey(
                        name: "FK_UserRoleMapping_Role",
                        column: x => x.roleid,
                        principalTable: "role",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_UserRoleMapping_User",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "fk_typeid_idx",
                table: "function_right_type_mapping",
                column: "typeID");

            migrationBuilder.CreateIndex(
                name: "fk_role_type_idx",
                table: "role",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_RoleFunctionRightMapping_function_right_id",
                table: "role_function_right_mapping",
                column: "function_right_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMapping_role_id",
                table: "user_role_mapping",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "function_right_type_mapping");

            migrationBuilder.DropTable(
                name: "role_function_right_mapping");

            migrationBuilder.DropTable(
                name: "user_role_mapping");

            migrationBuilder.DropTable(
                name: "function_right_type");

            migrationBuilder.DropTable(
                name: "function_right");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "role_type");
        }
    }
}
