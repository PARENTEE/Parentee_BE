using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parentee_BE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vaccine_catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    code = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    recommended_age_days = table.Column<int>(type: "integer", nullable: true),
                    doses = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("vaccine_catalog_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "price",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    price_type = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false, defaultValueSql: "'VND'::text"),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    provider_price_id = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("price_pkey", x => x.id);
                    table.ForeignKey(
                        name: "price_product_id_fkey",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "audit_log",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    family_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    action = table.Column<string>(type: "text", nullable: false),
                    entity_type = table.Column<string>(type: "text", nullable: false),
                    entity_id = table.Column<Guid>(type: "uuid", nullable: true),
                    detail = table.Column<string>(type: "jsonb", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("audit_log_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "auth_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    provider = table.Column<string>(type: "text", nullable: false),
                    provider_uid = table.Column<string>(type: "text", nullable: true),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("auth_identity_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "child",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    family_id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    sex = table.Column<string>(type: "text", nullable: true),
                    photo_image_id = table.Column<Guid>(type: "uuid", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("child_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "child_photo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    child_id = table.Column<Guid>(type: "uuid", nullable: false),
                    image_id = table.Column<Guid>(type: "uuid", nullable: false),
                    taken_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    caption = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("child_photo_pkey", x => x.id);
                    table.ForeignKey(
                        name: "child_photo_child_id_fkey",
                        column: x => x.child_id,
                        principalTable: "child",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "child_vaccination",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    family_id = table.Column<Guid>(type: "uuid", nullable: false),
                    child_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vaccine_id = table.Column<Guid>(type: "uuid", nullable: true),
                    custom_name = table.Column<string>(type: "text", nullable: true),
                    scheduled_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Scheduled"),
                    administered_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    lot_number = table.Column<string>(type: "text", nullable: true),
                    provider_name = table.Column<string>(type: "text", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("child_vaccination_pkey", x => x.id);
                    table.ForeignKey(
                        name: "child_vaccination_child_id_fkey",
                        column: x => x.child_id,
                        principalTable: "child",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "child_vaccination_vaccine_id_fkey",
                        column: x => x.vaccine_id,
                        principalTable: "vaccine_catalog",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "diaper_change",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    family_id = table.Column<Guid>(type: "uuid", nullable: false),
                    child_id = table.Column<Guid>(type: "uuid", nullable: false),
                    changed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    rash_observed = table.Column<bool>(type: "boolean", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("diaper_change_pkey", x => x.id);
                    table.ForeignKey(
                        name: "diaper_change_child_id_fkey",
                        column: x => x.child_id,
                        principalTable: "child",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "entitlement",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    family_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    purchase_id = table.Column<Guid>(type: "uuid", nullable: true),
                    starts_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Active"),
                    ends_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("entitlement_pkey", x => x.id);
                    table.ForeignKey(
                        name: "entitlement_product_id_fkey",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "family",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "text", nullable: false),
                    cover_image_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("family_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "feeding",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    family_id = table.Column<Guid>(type: "uuid", nullable: false),
                    child_id = table.Column<Guid>(type: "uuid", nullable: false),
                    method = table.Column<string>(type: "text", nullable: false),
                    started_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ended_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    duration_min = table.Column<int>(type: "integer", nullable: true),
                    amount_ml = table.Column<decimal>(type: "numeric(6,1)", precision: 6, scale: 1, nullable: true),
                    side = table.Column<string>(type: "text", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("feeding_pkey", x => x.id);
                    table.ForeignKey(
                        name: "feeding_child_id_fkey",
                        column: x => x.child_id,
                        principalTable: "child",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "feeding_family_id_fkey",
                        column: x => x.family_id,
                        principalTable: "family",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    family_id = table.Column<Guid>(type: "uuid", nullable: true),
                    owner_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    url = table.Column<string>(type: "text", nullable: false),
                    mime_type = table.Column<string>(type: "text", nullable: true),
                    size_bytes = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("image_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_image_family",
                        column: x => x.family_id,
                        principalTable: "family",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    email = table.Column<string>(type: "text", nullable: true),
                    full_name = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: false),
                    signup_method = table.Column<string>(type: "text", nullable: false),
                    dob = table.Column<DateOnly>(type: "date", nullable: false),
                    avatar_image_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_premium = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pkey", x => x.id);
                    table.ForeignKey(
                        name: "user_avatar_image_id_fkey",
                        column: x => x.avatar_image_id,
                        principalTable: "image",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "measurement",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    family_id = table.Column<Guid>(type: "uuid", nullable: false),
                    child_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    measured_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    value = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: false),
                    unit = table.Column<string>(type: "text", nullable: false),
                    source = table.Column<string>(type: "text", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("measurement_pkey", x => x.id);
                    table.ForeignKey(
                        name: "measurement_child_id_fkey",
                        column: x => x.child_id,
                        principalTable: "child",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "measurement_created_by_fkey",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "measurement_family_id_fkey",
                        column: x => x.family_id,
                        principalTable: "family",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "notification_outbox",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    family_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    channel = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    body = table.Column<string>(type: "text", nullable: true),
                    scheduled_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    sent_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    attempts = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    payload = table.Column<string>(type: "jsonb", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("notification_outbox_pkey", x => x.id);
                    table.ForeignKey(
                        name: "notification_outbox_family_id_fkey",
                        column: x => x.family_id,
                        principalTable: "family",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "notification_outbox_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "purchase",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    order_code = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    family_id = table.Column<Guid>(type: "uuid", nullable: true),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    price_id = table.Column<Guid>(type: "uuid", nullable: true),
                    payment_method = table.Column<string>(type: "text", nullable: false, defaultValue: "CreditCard"),
                    provider_txn_id = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Pending"),
                    amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false, defaultValueSql: "'VND'::text"),
                    raw_payload = table.Column<string>(type: "jsonb", nullable: true),
                    paid_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("purchase_pkey", x => x.id);
                    table.ForeignKey(
                        name: "purchase_family_id_fkey",
                        column: x => x.family_id,
                        principalTable: "family",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "purchase_price_id_fkey",
                        column: x => x.price_id,
                        principalTable: "price",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "purchase_product_id_fkey",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "purchase_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sleep",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    family_id = table.Column<Guid>(type: "uuid", nullable: false),
                    child_id = table.Column<Guid>(type: "uuid", nullable: false),
                    started_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ended_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    duration_min = table.Column<int>(type: "integer", nullable: true),
                    location = table.Column<string>(type: "text", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sleep_pkey", x => x.id);
                    table.ForeignKey(
                        name: "sleep_child_id_fkey",
                        column: x => x.child_id,
                        principalTable: "child",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "sleep_created_by_fkey",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "sleep_family_id_fkey",
                        column: x => x.family_id,
                        principalTable: "family",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "task",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    family_id = table.Column<Guid>(type: "uuid", nullable: false),
                    child_id = table.Column<Guid>(type: "uuid", nullable: true),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    starts_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ends_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    all_day = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Pending"),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("task_pkey", x => x.id);
                    table.ForeignKey(
                        name: "task_child_id_fkey",
                        column: x => x.child_id,
                        principalTable: "child",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "task_created_by_fkey",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "task_family_id_fkey",
                        column: x => x.family_id,
                        principalTable: "family",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "task_updated_by_fkey",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_family_role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    family_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_family_role_pkey", x => x.id);
                    table.ForeignKey(
                        name: "user_family_role_family_id_fkey",
                        column: x => x.family_id,
                        principalTable: "family",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "user_family_role_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "invoice",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    purchase_id = table.Column<Guid>(type: "uuid", nullable: false),
                    invoice_no = table.Column<string>(type: "text", nullable: true),
                    issued_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    buyer_name = table.Column<string>(type: "text", nullable: true),
                    buyer_email = table.Column<string>(type: "citext", nullable: true),
                    buyer_tax_code = table.Column<string>(type: "text", nullable: true),
                    amount_total = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false, defaultValueSql: "'VND'::text"),
                    pdf_image_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("invoice_pkey", x => x.id);
                    table.ForeignKey(
                        name: "invoice_pdf_image_id_fkey",
                        column: x => x.pdf_image_id,
                        principalTable: "image",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "invoice_purchase_id_fkey",
                        column: x => x.purchase_id,
                        principalTable: "purchase",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "refund",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    purchase_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false, defaultValueSql: "'VND'::text"),
                    reason = table.Column<string>(type: "text", nullable: true),
                    provider_refund_id = table.Column<string>(type: "text", nullable: true),
                    refunded_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("refund_pkey", x => x.id);
                    table.ForeignKey(
                        name: "refund_purchase_id_fkey",
                        column: x => x.purchase_id,
                        principalTable: "purchase",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "reminder",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    task_id = table.Column<Guid>(type: "uuid", nullable: false),
                    remind_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    channel = table.Column<string>(type: "text", nullable: false, defaultValue: "Push"),
                    payload = table.Column<string>(type: "jsonb", nullable: true),
                    sent_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("reminder_pkey", x => x.id);
                    table.ForeignKey(
                        name: "reminder_task_id_fkey",
                        column: x => x.task_id,
                        principalTable: "task",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task_recurrence",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    task_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rule = table.Column<string>(type: "text", nullable: false),
                    timezone = table.Column<string>(type: "text", nullable: true),
                    until = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("task_recurrence_pkey", x => x.id);
                    table.ForeignKey(
                        name: "task_recurrence_task_id_fkey",
                        column: x => x.task_id,
                        principalTable: "task",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_audit_log_family_id",
                table: "audit_log",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "IX_audit_log_user_id",
                table: "audit_log",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "auth_identity_provider_provider_uid_key",
                table: "auth_identity",
                columns: new[] { "provider", "provider_uid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_auth_identity_user_id",
                table: "auth_identity",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "idx_child_family",
                table: "child",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "IX_child_photo_image_id",
                table: "child",
                column: "photo_image_id");

            migrationBuilder.CreateIndex(
                name: "IX_child_photo_child_id",
                table: "child_photo",
                column: "child_id");

            migrationBuilder.CreateIndex(
                name: "IX_child_photo_image_id1",
                table: "child_photo",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "idx_child_vacc_sched",
                table: "child_vaccination",
                columns: new[] { "child_id", "scheduled_at" });

            migrationBuilder.CreateIndex(
                name: "IX_child_vaccination_created_by",
                table: "child_vaccination",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_child_vaccination_family_id",
                table: "child_vaccination",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "IX_child_vaccination_updated_by",
                table: "child_vaccination",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_child_vaccination_vaccine_id",
                table: "child_vaccination",
                column: "vaccine_id");

            migrationBuilder.CreateIndex(
                name: "idx_diaper_child_time",
                table: "diaper_change",
                columns: new[] { "child_id", "changed_at" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_diaper_change_created_by",
                table: "diaper_change",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_diaper_change_family_id",
                table: "diaper_change",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "entitlement_family_id_product_id_starts_at_key",
                table: "entitlement",
                columns: new[] { "family_id", "product_id", "starts_at" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_entitlement_product_id",
                table: "entitlement",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_entitlement_purchase_id",
                table: "entitlement",
                column: "purchase_id");

            migrationBuilder.CreateIndex(
                name: "IX_family_cover_image_id",
                table: "family",
                column: "cover_image_id");

            migrationBuilder.CreateIndex(
                name: "idx_feeding_child_time",
                table: "feeding",
                columns: new[] { "child_id", "started_at" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_feeding_created_by",
                table: "feeding",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_feeding_family_id",
                table: "feeding",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "idx_image_family",
                table: "image",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "IX_image_owner_user_id",
                table: "image",
                column: "owner_user_id");

            migrationBuilder.CreateIndex(
                name: "invoice_invoice_no_key",
                table: "invoice",
                column: "invoice_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_invoice_pdf_image_id",
                table: "invoice",
                column: "pdf_image_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_purchase_id",
                table: "invoice",
                column: "purchase_id");

            migrationBuilder.CreateIndex(
                name: "idx_measurement_child_time",
                table: "measurement",
                columns: new[] { "child_id", "measured_at" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_measurement_created_by",
                table: "measurement",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_measurement_family_id",
                table: "measurement",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "IX_notification_outbox_family_id",
                table: "notification_outbox",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "IX_notification_outbox_user_id",
                table: "notification_outbox",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_price_product_id",
                table: "price",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "product_code_key",
                table: "product",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchase_family_id",
                table: "purchase",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_price_id",
                table: "purchase",
                column: "price_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_product_id",
                table: "purchase",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_user_id",
                table: "purchase",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_refund_purchase_id",
                table: "refund",
                column: "purchase_id");

            migrationBuilder.CreateIndex(
                name: "IX_reminder_task_id",
                table: "reminder",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "idx_sleep_child_time",
                table: "sleep",
                columns: new[] { "child_id", "started_at" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_sleep_created_by",
                table: "sleep",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_sleep_family_id",
                table: "sleep",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "idx_task_family_time",
                table: "task",
                columns: new[] { "family_id", "starts_at" });

            migrationBuilder.CreateIndex(
                name: "IX_task_child_id",
                table: "task",
                column: "child_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_created_by",
                table: "task",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_task_updated_by",
                table: "task",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_task_recurrence_task_id",
                table: "task_recurrence",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_avatar_image_id",
                table: "user",
                column: "avatar_image_id");

            migrationBuilder.CreateIndex(
                name: "user_email_key",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_family_role_family_id",
                table: "user_family_role",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_family_role_user_id",
                table: "user_family_role",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "user_family_role_user_id_family_id_key",
                table: "user_family_role",
                columns: new[] { "user_id", "family_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "vaccine_catalog_code_key",
                table: "vaccine_catalog",
                column: "code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "audit_log_family_id_fkey",
                table: "audit_log",
                column: "family_id",
                principalTable: "family",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "audit_log_user_id_fkey",
                table: "audit_log",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "auth_identity_user_id_fkey",
                table: "auth_identity",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "child_family_id_fkey",
                table: "child",
                column: "family_id",
                principalTable: "family",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "child_photo_image_id_fkey",
                table: "child",
                column: "photo_image_id",
                principalTable: "image",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "child_photo_image_id_fkey1",
                table: "child_photo",
                column: "image_id",
                principalTable: "image",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "child_vaccination_created_by_fkey",
                table: "child_vaccination",
                column: "created_by",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "child_vaccination_updated_by_fkey",
                table: "child_vaccination",
                column: "updated_by",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "child_vaccination_family_id_fkey",
                table: "child_vaccination",
                column: "family_id",
                principalTable: "family",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "diaper_change_created_by_fkey",
                table: "diaper_change",
                column: "created_by",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "diaper_change_family_id_fkey",
                table: "diaper_change",
                column: "family_id",
                principalTable: "family",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "entitlement_family_id_fkey",
                table: "entitlement",
                column: "family_id",
                principalTable: "family",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "entitlement_purchase_id_fkey",
                table: "entitlement",
                column: "purchase_id",
                principalTable: "purchase",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "family_cover_image_id_fkey",
                table: "family",
                column: "cover_image_id",
                principalTable: "image",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "feeding_created_by_fkey",
                table: "feeding",
                column: "created_by",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_image_owner",
                table: "image",
                column: "owner_user_id",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_image_family",
                table: "image");

            migrationBuilder.DropForeignKey(
                name: "fk_image_owner",
                table: "image");

            migrationBuilder.DropTable(
                name: "audit_log");

            migrationBuilder.DropTable(
                name: "auth_identity");

            migrationBuilder.DropTable(
                name: "child_photo");

            migrationBuilder.DropTable(
                name: "child_vaccination");

            migrationBuilder.DropTable(
                name: "diaper_change");

            migrationBuilder.DropTable(
                name: "entitlement");

            migrationBuilder.DropTable(
                name: "feeding");

            migrationBuilder.DropTable(
                name: "invoice");

            migrationBuilder.DropTable(
                name: "measurement");

            migrationBuilder.DropTable(
                name: "notification_outbox");

            migrationBuilder.DropTable(
                name: "refund");

            migrationBuilder.DropTable(
                name: "reminder");

            migrationBuilder.DropTable(
                name: "sleep");

            migrationBuilder.DropTable(
                name: "task_recurrence");

            migrationBuilder.DropTable(
                name: "user_family_role");

            migrationBuilder.DropTable(
                name: "vaccine_catalog");

            migrationBuilder.DropTable(
                name: "purchase");

            migrationBuilder.DropTable(
                name: "task");

            migrationBuilder.DropTable(
                name: "price");

            migrationBuilder.DropTable(
                name: "child");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "family");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "image");
        }
    }
}
