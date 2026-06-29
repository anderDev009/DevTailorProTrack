using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TailorProTrack.infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BANK",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BANK", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BANK_ACCOUNT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BANK_ACCOUNT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FK_BANK = table.Column<int>(type: "int", nullable: false),
                    BALANCE = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CREDIT_AMOUNT = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DEBIT_AMOUNT = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BANK_ACCOUNT", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BUY_INVENTORY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    COMPANY = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RNC = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NCF = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DATE_MADE = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TOTAL_SALE = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    USED = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BUY_INVENTORY", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CATEGORYSIZE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CATEGORY = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORYSIZE", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CLIENT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FIRST_NAME = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LAST_NAME = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FIRST_SURNAME = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LAST_SURNAME = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DNI = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RNC = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENT", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "COLOR",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    COLORNAME = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CODE_COLOR = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COLOR", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "INVENTORY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_SIZE = table.Column<int>(type: "int", nullable: false),
                    FK_PRODUCT = table.Column<int>(type: "int", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INVENTORY", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MissedInv",
                columns: table => new
                {
                    NombreProducto = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Size = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ColorPrimary = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ColorSecondary = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CantidadFaltante = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PAYMENT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_ORDER = table.Column<int>(type: "int", nullable: false),
                    FK_TYPE_PAYMENT = table.Column<int>(type: "int", nullable: false),
                    ACCOUNT_PAYMENT = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FK_BANK_ACCOUNT = table.Column<int>(type: "int", nullable: true),
                    AMOUNT = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ACCOUNT_NUMBER = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NOTE_CREDIT = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENT", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PAYMENT_CREDIT_NOTE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_PAYMENT = table.Column<int>(type: "int", nullable: false),
                    FK_CREDIT = table.Column<int>(type: "int", nullable: false),
                    AMOUNT = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENT_CREDIT_NOTE", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PAYMENT_TYPE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TYPE_PAYMENT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENT_TYPE", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PHONE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TYPE = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NUMBER = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FK_CLIENT = table.Column<int>(type: "int", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHONE", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SUPPLIERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NAME_SUPPLIER = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RNC = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUPPLIERS", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TYPE_PROD",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TYPE_PROD = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYPE_PROD", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EXPENSES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DESCR = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AMOUNT = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    VOUCHER = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DOCUMENT_NUMBER = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    COMPLETED = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    FK_BUY = table.Column<int>(type: "int", nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EXPENSES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EXPENSES_BUY_INVENTORY_FK_BUY",
                        column: x => x.FK_BUY,
                        principalTable: "BUY_INVENTORY",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SIZE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SIZE = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FKCATEGORYSIZE = table.Column<int>(type: "int", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIZE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SIZE_CATEGORYSIZE_FKCATEGORYSIZE",
                        column: x => x.FKCATEGORYSIZE,
                        principalTable: "CATEGORYSIZE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NOTE_CREDIT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_CLIENT = table.Column<int>(type: "int", nullable: false),
                    AMOUNT = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTE_CREDIT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NOTE_CREDIT_CLIENT_FK_CLIENT",
                        column: x => x.FK_CLIENT,
                        principalTable: "CLIENT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PRE_ORDER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_CLIENT = table.Column<int>(type: "int", nullable: false),
                    DATE_DELIVERY = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    COMPLETED = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    FINISHED = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    ITBIS = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRE_ORDER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRE_ORDER_CLIENT_FK_CLIENT",
                        column: x => x.FK_CLIENT,
                        principalTable: "CLIENT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "INVENTORY_COLOR",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_COLOR_PRIMARY = table.Column<int>(type: "int", nullable: false),
                    FK_COLOR_SECONDARY = table.Column<int>(type: "int", nullable: true),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    FK_INVENTORY = table.Column<int>(type: "int", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INVENTORY_COLOR", x => x.ID);
                    table.ForeignKey(
                        name: "FK_INVENTORY_COLOR_INVENTORY_FK_INVENTORY",
                        column: x => x.FK_INVENTORY,
                        principalTable: "INVENTORY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ACCOUNT_DEBIT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_BANK_ACC = table.Column<int>(type: "int", nullable: false),
                    FK_PAYMENT = table.Column<int>(type: "int", nullable: true),
                    AMOUNT = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCOUNT_DEBIT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ACCOUNT_DEBIT_BANK_ACCOUNT_FK_BANK_ACC",
                        column: x => x.FK_BANK_ACC,
                        principalTable: "BANK_ACCOUNT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ACCOUNT_DEBIT_PAYMENT_FK_PAYMENT",
                        column: x => x.FK_PAYMENT,
                        principalTable: "PAYMENT",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NAME_PRODUCT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DESCRIPTION_PRODUCT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SALE_PRICE = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    FK_TYPE = table.Column<int>(type: "int", nullable: false),
                    LAST_REPLENISHMENT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUCT_TYPE_PROD_FK_TYPE",
                        column: x => x.FK_TYPE,
                        principalTable: "TYPE_PROD",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ACCOUNT_CREDIT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_BANK_ACC = table.Column<int>(type: "int", nullable: false),
                    FK_EXPENSE = table.Column<int>(type: "int", nullable: false),
                    AMOUNT = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCOUNT_CREDIT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ACCOUNT_CREDIT_BANK_ACCOUNT_FK_BANK_ACC",
                        column: x => x.FK_BANK_ACC,
                        principalTable: "BANK_ACCOUNT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ACCOUNT_CREDIT_EXPENSES_FK_EXPENSE",
                        column: x => x.FK_EXPENSE,
                        principalTable: "EXPENSES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PAYMENT_EXPENSES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_EXPENSE = table.Column<int>(type: "int", nullable: false),
                    FK_PAYMENT_TYPE = table.Column<int>(type: "int", nullable: false),
                    FK_BANK_ACCOUNT = table.Column<int>(type: "int", nullable: true),
                    AMOUNT = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENT_EXPENSES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PAYMENT_EXPENSES_BANK_ACCOUNT_FK_BANK_ACCOUNT",
                        column: x => x.FK_BANK_ACCOUNT,
                        principalTable: "BANK_ACCOUNT",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PAYMENT_EXPENSES_EXPENSES_FK_EXPENSE",
                        column: x => x.FK_EXPENSE,
                        principalTable: "EXPENSES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PAYMENT_EXPENSES_PAYMENT_TYPE_FK_PAYMENT_TYPE",
                        column: x => x.FK_PAYMENT_TYPE,
                        principalTable: "PAYMENT_TYPE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ORDERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_USER = table.Column<int>(type: "int", nullable: false),
                    FK_CLIENT = table.Column<int>(type: "int", nullable: false),
                    FK_PREORDER = table.Column<int>(type: "int", nullable: false),
                    CHECKED = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AMOUNT = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    OBSERVATION = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DESCRIPTION_JOB = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    STATUS_ORDER = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SEND_TO = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDERS_CLIENT_FK_CLIENT",
                        column: x => x.FK_CLIENT,
                        principalTable: "CLIENT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ORDERS_PRE_ORDER_FK_PREORDER",
                        column: x => x.FK_PREORDER,
                        principalTable: "PRE_ORDER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SALES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_PREORDER = table.Column<int>(type: "int", nullable: false),
                    COD_ISC = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ITBIS = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    B14 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    INVOICED = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    TOTAL_AMOUNT = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SALES_PRE_ORDER_FK_PREORDER",
                        column: x => x.FK_PREORDER,
                        principalTable: "PRE_ORDER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BUY_INVENTORY_DETAIL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_BUY_INVENTORY = table.Column<int>(type: "int", nullable: false),
                    FK_PRODUCT = table.Column<int>(type: "int", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    FK_SIZE = table.Column<int>(type: "int", nullable: false),
                    COLOR_PRIMARY = table.Column<int>(type: "int", nullable: false),
                    COLOR_SECONDARY = table.Column<int>(type: "int", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BUY_INVENTORY_DETAIL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BUY_INVENTORY_DETAIL_BUY_INVENTORY_FK_BUY_INVENTORY",
                        column: x => x.FK_BUY_INVENTORY,
                        principalTable: "BUY_INVENTORY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BUY_INVENTORY_DETAIL_COLOR_COLOR_PRIMARY",
                        column: x => x.COLOR_PRIMARY,
                        principalTable: "COLOR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BUY_INVENTORY_DETAIL_COLOR_COLOR_SECONDARY",
                        column: x => x.COLOR_SECONDARY,
                        principalTable: "COLOR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BUY_INVENTORY_DETAIL_PRODUCT_FK_PRODUCT",
                        column: x => x.FK_PRODUCT,
                        principalTable: "PRODUCT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BUY_INVENTORY_DETAIL_SIZE_FK_SIZE",
                        column: x => x.FK_SIZE,
                        principalTable: "SIZE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PRE_ORDER_PRODUCTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_PREORDER = table.Column<int>(type: "int", nullable: false),
                    FK_PRODUCT = table.Column<int>(type: "int", nullable: false),
                    FK_SIZE = table.Column<int>(type: "int", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    COLOR_PRIMARY = table.Column<int>(type: "int", nullable: false),
                    COLOR_SECONDARY = table.Column<int>(type: "int", nullable: true),
                    CUSTOM_PRICE = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRE_ORDER_PRODUCTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRE_ORDER_PRODUCTS_COLOR_COLOR_PRIMARY",
                        column: x => x.COLOR_PRIMARY,
                        principalTable: "COLOR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRE_ORDER_PRODUCTS_COLOR_COLOR_SECONDARY",
                        column: x => x.COLOR_SECONDARY,
                        principalTable: "COLOR",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PRE_ORDER_PRODUCTS_PRE_ORDER_FK_PREORDER",
                        column: x => x.FK_PREORDER,
                        principalTable: "PRE_ORDER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRE_ORDER_PRODUCTS_PRODUCT_FK_PRODUCT",
                        column: x => x.FK_PRODUCT,
                        principalTable: "PRODUCT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRE_ORDER_PRODUCTS_SIZE_FK_SIZE",
                        column: x => x.FK_SIZE,
                        principalTable: "SIZE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PRODUCTS_COLOR",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_PRODUCT = table.Column<int>(type: "int", nullable: false),
                    FK_COLOR = table.Column<int>(type: "int", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS_COLOR", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUCTS_COLOR_COLOR_FK_COLOR",
                        column: x => x.FK_COLOR,
                        principalTable: "COLOR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUCTS_COLOR_PRODUCT_FK_PRODUCT",
                        column: x => x.FK_PRODUCT,
                        principalTable: "PRODUCT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PRODUCTS_SIZE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_SIZE = table.Column<int>(type: "int", nullable: false),
                    FK_PRODUCT = table.Column<int>(type: "int", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS_SIZE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUCTS_SIZE_PRODUCT_FK_PRODUCT",
                        column: x => x.FK_PRODUCT,
                        principalTable: "PRODUCT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUCTS_SIZE_SIZE_FK_SIZE",
                        column: x => x.FK_SIZE,
                        principalTable: "SIZE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ORDER_PRODUCTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FK_ORDER = table.Column<int>(type: "int", nullable: false),
                    FK_INVENTORYCOLOR = table.Column<int>(type: "int", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USER_MOD = table.Column<int>(type: "int", nullable: true),
                    USER_CREATED = table.Column<int>(type: "int", nullable: false),
                    REMOVED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER_PRODUCTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDER_PRODUCTS_INVENTORY_COLOR_FK_INVENTORYCOLOR",
                        column: x => x.FK_INVENTORYCOLOR,
                        principalTable: "INVENTORY_COLOR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ORDER_PRODUCTS_ORDERS_FK_ORDER",
                        column: x => x.FK_ORDER,
                        principalTable: "ORDERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ACCOUNT_CREDIT_FK_BANK_ACC",
                table: "ACCOUNT_CREDIT",
                column: "FK_BANK_ACC");

            migrationBuilder.CreateIndex(
                name: "IX_ACCOUNT_CREDIT_FK_EXPENSE",
                table: "ACCOUNT_CREDIT",
                column: "FK_EXPENSE");

            migrationBuilder.CreateIndex(
                name: "IX_ACCOUNT_DEBIT_FK_BANK_ACC",
                table: "ACCOUNT_DEBIT",
                column: "FK_BANK_ACC");

            migrationBuilder.CreateIndex(
                name: "IX_ACCOUNT_DEBIT_FK_PAYMENT",
                table: "ACCOUNT_DEBIT",
                column: "FK_PAYMENT");

            migrationBuilder.CreateIndex(
                name: "IX_BUY_INVENTORY_DETAIL_COLOR_PRIMARY",
                table: "BUY_INVENTORY_DETAIL",
                column: "COLOR_PRIMARY");

            migrationBuilder.CreateIndex(
                name: "IX_BUY_INVENTORY_DETAIL_COLOR_SECONDARY",
                table: "BUY_INVENTORY_DETAIL",
                column: "COLOR_SECONDARY");

            migrationBuilder.CreateIndex(
                name: "IX_BUY_INVENTORY_DETAIL_FK_BUY_INVENTORY",
                table: "BUY_INVENTORY_DETAIL",
                column: "FK_BUY_INVENTORY");

            migrationBuilder.CreateIndex(
                name: "IX_BUY_INVENTORY_DETAIL_FK_PRODUCT",
                table: "BUY_INVENTORY_DETAIL",
                column: "FK_PRODUCT");

            migrationBuilder.CreateIndex(
                name: "IX_BUY_INVENTORY_DETAIL_FK_SIZE",
                table: "BUY_INVENTORY_DETAIL",
                column: "FK_SIZE");

            migrationBuilder.CreateIndex(
                name: "IX_EXPENSES_FK_BUY",
                table: "EXPENSES",
                column: "FK_BUY");

            migrationBuilder.CreateIndex(
                name: "IX_INVENTORY_COLOR_FK_INVENTORY",
                table: "INVENTORY_COLOR",
                column: "FK_INVENTORY");

            migrationBuilder.CreateIndex(
                name: "IX_NOTE_CREDIT_FK_CLIENT",
                table: "NOTE_CREDIT",
                column: "FK_CLIENT");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_PRODUCTS_FK_INVENTORYCOLOR",
                table: "ORDER_PRODUCTS",
                column: "FK_INVENTORYCOLOR");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_PRODUCTS_FK_ORDER",
                table: "ORDER_PRODUCTS",
                column: "FK_ORDER");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_FK_CLIENT",
                table: "ORDERS",
                column: "FK_CLIENT");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_FK_PREORDER",
                table: "ORDERS",
                column: "FK_PREORDER");

            migrationBuilder.CreateIndex(
                name: "IX_PAYMENT_EXPENSES_FK_BANK_ACCOUNT",
                table: "PAYMENT_EXPENSES",
                column: "FK_BANK_ACCOUNT");

            migrationBuilder.CreateIndex(
                name: "IX_PAYMENT_EXPENSES_FK_EXPENSE",
                table: "PAYMENT_EXPENSES",
                column: "FK_EXPENSE");

            migrationBuilder.CreateIndex(
                name: "IX_PAYMENT_EXPENSES_FK_PAYMENT_TYPE",
                table: "PAYMENT_EXPENSES",
                column: "FK_PAYMENT_TYPE");

            migrationBuilder.CreateIndex(
                name: "IX_PRE_ORDER_FK_CLIENT",
                table: "PRE_ORDER",
                column: "FK_CLIENT");

            migrationBuilder.CreateIndex(
                name: "IX_PRE_ORDER_PRODUCTS_COLOR_PRIMARY",
                table: "PRE_ORDER_PRODUCTS",
                column: "COLOR_PRIMARY");

            migrationBuilder.CreateIndex(
                name: "IX_PRE_ORDER_PRODUCTS_COLOR_SECONDARY",
                table: "PRE_ORDER_PRODUCTS",
                column: "COLOR_SECONDARY");

            migrationBuilder.CreateIndex(
                name: "IX_PRE_ORDER_PRODUCTS_FK_PREORDER",
                table: "PRE_ORDER_PRODUCTS",
                column: "FK_PREORDER");

            migrationBuilder.CreateIndex(
                name: "IX_PRE_ORDER_PRODUCTS_FK_PRODUCT",
                table: "PRE_ORDER_PRODUCTS",
                column: "FK_PRODUCT");

            migrationBuilder.CreateIndex(
                name: "IX_PRE_ORDER_PRODUCTS_FK_SIZE",
                table: "PRE_ORDER_PRODUCTS",
                column: "FK_SIZE");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_FK_TYPE",
                table: "PRODUCT",
                column: "FK_TYPE");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_COLOR_FK_COLOR",
                table: "PRODUCTS_COLOR",
                column: "FK_COLOR");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_COLOR_FK_PRODUCT",
                table: "PRODUCTS_COLOR",
                column: "FK_PRODUCT");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_SIZE_FK_PRODUCT",
                table: "PRODUCTS_SIZE",
                column: "FK_PRODUCT");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_SIZE_FK_SIZE",
                table: "PRODUCTS_SIZE",
                column: "FK_SIZE");

            migrationBuilder.CreateIndex(
                name: "IX_SALES_FK_PREORDER",
                table: "SALES",
                column: "FK_PREORDER");

            migrationBuilder.CreateIndex(
                name: "IX_SIZE_FKCATEGORYSIZE",
                table: "SIZE",
                column: "FKCATEGORYSIZE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCOUNT_CREDIT");

            migrationBuilder.DropTable(
                name: "ACCOUNT_DEBIT");

            migrationBuilder.DropTable(
                name: "BANK");

            migrationBuilder.DropTable(
                name: "BUY_INVENTORY_DETAIL");

            migrationBuilder.DropTable(
                name: "MissedInv");

            migrationBuilder.DropTable(
                name: "NOTE_CREDIT");

            migrationBuilder.DropTable(
                name: "ORDER_PRODUCTS");

            migrationBuilder.DropTable(
                name: "PAYMENT_CREDIT_NOTE");

            migrationBuilder.DropTable(
                name: "PAYMENT_EXPENSES");

            migrationBuilder.DropTable(
                name: "PHONE");

            migrationBuilder.DropTable(
                name: "PRE_ORDER_PRODUCTS");

            migrationBuilder.DropTable(
                name: "PRODUCTS_COLOR");

            migrationBuilder.DropTable(
                name: "PRODUCTS_SIZE");

            migrationBuilder.DropTable(
                name: "SALES");

            migrationBuilder.DropTable(
                name: "SUPPLIERS");

            migrationBuilder.DropTable(
                name: "PAYMENT");

            migrationBuilder.DropTable(
                name: "INVENTORY_COLOR");

            migrationBuilder.DropTable(
                name: "ORDERS");

            migrationBuilder.DropTable(
                name: "BANK_ACCOUNT");

            migrationBuilder.DropTable(
                name: "EXPENSES");

            migrationBuilder.DropTable(
                name: "PAYMENT_TYPE");

            migrationBuilder.DropTable(
                name: "COLOR");

            migrationBuilder.DropTable(
                name: "PRODUCT");

            migrationBuilder.DropTable(
                name: "SIZE");

            migrationBuilder.DropTable(
                name: "INVENTORY");

            migrationBuilder.DropTable(
                name: "PRE_ORDER");

            migrationBuilder.DropTable(
                name: "BUY_INVENTORY");

            migrationBuilder.DropTable(
                name: "TYPE_PROD");

            migrationBuilder.DropTable(
                name: "CATEGORYSIZE");

            migrationBuilder.DropTable(
                name: "CLIENT");
        }
    }
}
