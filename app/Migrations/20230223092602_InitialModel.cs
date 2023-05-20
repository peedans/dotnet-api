using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Action",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Primary Key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    priority = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false, comment: "Flag Soft Delete"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime2", nullable: true, comment: "Audit column, created date"),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true, comment: "Audit column, created by"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "datetime2", nullable: true, comment: "Audit column, updated date"),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true, comment: "Audit column, updated by")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Primary Key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    emailaddress = table.Column<string>(name: "email_address", type: "nvarchar(255)", maxLength: 255, nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false, comment: "Flag Soft Delete"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime2", nullable: true, comment: "Audit column, created date"),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true, comment: "Audit column, created by"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "datetime2", nullable: true, comment: "Audit column, updated date"),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true, comment: "Audit column, updated by")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Primary Key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false, comment: "Flag Soft Delete"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime2", nullable: true, comment: "Audit column, created date"),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true, comment: "Audit column, created by"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "datetime2", nullable: true, comment: "Audit column, updated date"),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true, comment: "Audit column, updated by")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Primary Key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false, comment: "Flag Soft Delete"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime2", nullable: true, comment: "Audit column, created date"),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true, comment: "Audit column, created by"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "datetime2", nullable: true, comment: "Audit column, updated date"),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true, comment: "Audit column, updated by")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Primary Key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false, comment: "Flag Soft Delete"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime2", nullable: true, comment: "Audit column, created date"),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true, comment: "Audit column, created by"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "datetime2", nullable: true, comment: "Audit column, updated date"),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true, comment: "Audit column, updated by")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Primary Key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false, comment: "Flag Soft Delete"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime2", nullable: true, comment: "Audit column, created date"),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true, comment: "Audit column, created by"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "datetime2", nullable: true, comment: "Audit column, updated date"),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true, comment: "Audit column, updated by")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Primary Key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false, comment: "Flag Soft Delete"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime2", nullable: true, comment: "Audit column, created date"),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true, comment: "Audit column, created by"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "datetime2", nullable: true, comment: "Audit column, updated date"),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true, comment: "Audit column, updated by")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPost",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Primary Key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    coverimage = table.Column<string>(name: "cover_image", type: "nvarchar(255)", maxLength: 255, nullable: true),
                    authorid = table.Column<long>(name: "author_id", type: "bigint", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false, comment: "Flag Soft Delete"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime2", nullable: true, comment: "Audit column, created date"),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true, comment: "Audit column, created by"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "datetime2", nullable: true, comment: "Audit column, updated date"),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true, comment: "Audit column, updated by")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPost", x => x.id);
                    table.ForeignKey(
                        name: "FK_BlogPost_Author_author_id",
                        column: x => x.authorid,
                        principalTable: "Author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleAction",
                columns: table => new
                {
                    roleid = table.Column<long>(name: "role_id ", type: "bigint", nullable: false),
                    actionid = table.Column<long>(name: "action_id ", type: "bigint", nullable: false),
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Primary Key"),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false, comment: "Flag Soft Delete"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime2", nullable: true, comment: "Audit column, created date"),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true, comment: "Audit column, created by"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "datetime2", nullable: true, comment: "Audit column, updated date"),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true, comment: "Audit column, updated by")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAction", x => new { x.roleid, x.actionid });
                    table.ForeignKey(
                        name: "FK_RoleAction_Action_action_id ",
                        column: x => x.actionid,
                        principalTable: "Action",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleAction_Role_role_id ",
                        column: x => x.roleid,
                        principalTable: "Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Primary Key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastlogin = table.Column<DateTime>(name: "last_login", type: "datetime2", nullable: true),
                    lastlogout = table.Column<DateTime>(name: "last_logout", type: "datetime2", nullable: true),
                    departmentid = table.Column<long>(name: "department_id", type: "bigint", nullable: false),
                    roleid = table.Column<long>(name: "role_id", type: "bigint", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false, comment: "Flag Soft Delete"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime2", nullable: true, comment: "Audit column, created date"),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true, comment: "Audit column, created by"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "datetime2", nullable: true, comment: "Audit column, updated date"),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true, comment: "Audit column, updated by")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Department_department_id",
                        column: x => x.departmentid,
                        principalTable: "Department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Role_role_id",
                        column: x => x.roleid,
                        principalTable: "Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostCategory",
                columns: table => new
                {
                    blogpostid = table.Column<long>(name: "blogpost_id ", type: "bigint", nullable: false),
                    categoryid = table.Column<long>(name: "category_id ", type: "bigint", nullable: false),
                    isactive = table.Column<bool>(name: "is_active", type: "bit", nullable: false),
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Primary Key"),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false, comment: "Flag Soft Delete"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime2", nullable: true, comment: "Audit column, created date"),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true, comment: "Audit column, created by"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "datetime2", nullable: true, comment: "Audit column, updated date"),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true, comment: "Audit column, updated by")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostCategory", x => new { x.blogpostid, x.categoryid });
                    table.ForeignKey(
                        name: "FK_BlogPostCategory_BlogPost_blogpost_id ",
                        column: x => x.blogpostid,
                        principalTable: "BlogPost",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostCategory_Category_category_id ",
                        column: x => x.categoryid,
                        principalTable: "Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostTag",
                columns: table => new
                {
                    blogpostid = table.Column<long>(name: "blog_post_id", type: "bigint", nullable: false),
                    tagid = table.Column<long>(name: "tag_id", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostTag", x => new { x.blogpostid, x.tagid });
                    table.ForeignKey(
                        name: "FK_BlogPostTag_BlogPost_blog_post_id",
                        column: x => x.blogpostid,
                        principalTable: "BlogPost",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostTag_Tag_tag_id",
                        column: x => x.tagid,
                        principalTable: "Tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salary",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Primary Key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<long>(name: "user_id", type: "bigint", nullable: false),
                    companyid = table.Column<long>(name: "company_id", type: "bigint", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<float>(type: "real", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false, comment: "Flag Soft Delete"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime2", nullable: true, comment: "Audit column, created date"),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true, comment: "Audit column, created by"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "datetime2", nullable: true, comment: "Audit column, updated date"),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true, comment: "Audit column, updated by")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salary", x => x.id);
                    table.ForeignKey(
                        name: "FK_Salary_Company_company_id",
                        column: x => x.companyid,
                        principalTable: "Company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Salary_User_user_id",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCompany",
                columns: table => new
                {
                    userid = table.Column<long>(name: "user_id", type: "bigint", nullable: false),
                    companyid = table.Column<long>(name: "company_id", type: "bigint", nullable: false),
                    ismain = table.Column<bool>(name: "is_main", type: "bit", nullable: false),
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Primary Key"),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false, comment: "Flag Soft Delete"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime2", nullable: true, comment: "Audit column, created date"),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true, comment: "Audit column, created by"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "datetime2", nullable: true, comment: "Audit column, updated date"),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true, comment: "Audit column, updated by")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompany", x => new { x.userid, x.companyid });
                    table.ForeignKey(
                        name: "FK_UserCompany_Company_company_id",
                        column: x => x.companyid,
                        principalTable: "Company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCompany_User_user_id",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaryHistory",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Primary Key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    salaryid = table.Column<long>(name: "salary_id", type: "bigint", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "bit", nullable: false, comment: "Flag Soft Delete"),
                    createddate = table.Column<DateTime>(name: "created_date", type: "datetime2", nullable: true, comment: "Audit column, created date"),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: true, comment: "Audit column, created by"),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "datetime2", nullable: true, comment: "Audit column, updated date"),
                    updatedby = table.Column<long>(name: "updated_by", type: "bigint", nullable: true, comment: "Audit column, updated by")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryHistory", x => x.id);
                    table.ForeignKey(
                        name: "FK_SalaryHistory_Salary_salary_id",
                        column: x => x.salaryid,
                        principalTable: "Salary",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_author_id",
                table: "BlogPost",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostCategory_category_id ",
                table: "BlogPostCategory",
                column: "category_id ");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTag_tag_id",
                table: "BlogPostTag",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAction_action_id ",
                table: "RoleAction",
                column: "action_id ");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_company_id",
                table: "Salary",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_user_id",
                table: "Salary",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryHistory_salary_id",
                table: "SalaryHistory",
                column: "salary_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_department_id",
                table: "User",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_role_id",
                table: "User",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompany_company_id",
                table: "UserCompany",
                column: "company_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostCategory");

            migrationBuilder.DropTable(
                name: "BlogPostTag");

            migrationBuilder.DropTable(
                name: "RoleAction");

            migrationBuilder.DropTable(
                name: "SalaryHistory");

            migrationBuilder.DropTable(
                name: "UserCompany");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "BlogPost");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Action");

            migrationBuilder.DropTable(
                name: "Salary");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
