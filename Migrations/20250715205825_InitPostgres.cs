using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace webBotica2.Migrations
{
    /// <inheritdoc />
    public partial class InitPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    id_categoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__CD54BC5ADAE8E0AF", x => x.id_categoria);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    documento = table.Column<string>(type: "character(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: false),
                    nombre = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    apellidoPaterno = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    apellidoMaterno = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    telefono = table.Column<string>(type: "character(9)", unicode: false, fixedLength: true, maxLength: 9, nullable: true),
                    correo = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    fecha_nac = table.Column<DateOnly>(type: "date", nullable: true),
                    direccion = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    es_registrado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Clientes__677F38F5C57E73F3", x => x.id_cliente);
                });

            migrationBuilder.CreateTable(
                name: "Laboratorio",
                columns: table => new
                {
                    id_laboratorio = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    ruc = table.Column<string>(type: "character(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    direccion = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    telefono = table.Column<string>(type: "character(9)", unicode: false, fixedLength: true, maxLength: 9, nullable: true),
                    correo = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Laborato__781B42E237B99CF8", x => x.id_laboratorio);
                });

            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    id_marca = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Marca__7E43E99E7EEDEA8B", x => x.id_marca);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rol = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rol__6ABCB5E03526DA8F", x => x.id_rol);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    id_venta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    total = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    id_cliente = table.Column<int>(type: "integer", nullable: true),
                    estado_venta = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Pendiente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ventas__459533BF74DFBDA5", x => x.id_venta);
                    table.ForeignKey(
                        name: "FK__Ventas__id_clien__59063A47",
                        column: x => x.id_cliente,
                        principalTable: "Clientes",
                        principalColumn: "id_cliente");
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    id_proveedor = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ruc = table.Column<string>(type: "character(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    razon_social = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    tipo = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    telefono = table.Column<string>(type: "character(9)", unicode: false, fixedLength: true, maxLength: 9, nullable: false),
                    correo = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    id_laboratorio = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proveedo__8D3DFE2852FB3F61", x => x.id_proveedor);
                    table.ForeignKey(
                        name: "FK__Proveedor__id_la__4316F928",
                        column: x => x.id_laboratorio,
                        principalTable: "Laboratorio",
                        principalColumn: "id_laboratorio");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    apellido = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    usuario = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    contraseña = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    id_rol = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuarios__D2D146374D944449", x => x.id_user);
                    table.ForeignKey(
                        name: "FK_USUARIOS_ROL",
                        column: x => x.id_rol,
                        principalTable: "Rol",
                        principalColumn: "id_rol");
                });

            migrationBuilder.CreateTable(
                name: "Comprobantes",
                columns: table => new
                {
                    id_comprobante = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    numero = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    id_venta = table.Column<int>(type: "integer", nullable: true),
                    estado = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Emitido")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comproba__55E5E2403CAF26AF", x => x.id_comprobante);
                    table.ForeignKey(
                        name: "FK__Comproban__id_ve__6E01572D",
                        column: x => x.id_venta,
                        principalTable: "Ventas",
                        principalColumn: "id_venta");
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    id_prod = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sku = table.Column<string>(type: "character(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    nombre = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    presentacion = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    precio_compra = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    precio_venta = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    fecha_ven = table.Column<DateOnly>(type: "date", nullable: false),
                    stock = table.Column<int>(type: "integer", nullable: false),
                    id_proveedor = table.Column<int>(type: "integer", nullable: true),
                    id_categoria = table.Column<int>(type: "integer", nullable: true),
                    id_marca = table.Column<int>(type: "integer", nullable: true),
                    foto = table.Column<byte[]>(type: "bytea", nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__0DA348730FC5C438", x => x.id_prod);
                    table.ForeignKey(
                        name: "FK__Producto__id_cat__47DBAE45",
                        column: x => x.id_categoria,
                        principalTable: "Categoria",
                        principalColumn: "id_categoria");
                    table.ForeignKey(
                        name: "FK__Producto__id_mar__48CFD27E",
                        column: x => x.id_marca,
                        principalTable: "Marca",
                        principalColumn: "id_marca");
                    table.ForeignKey(
                        name: "FK__Producto__id_pro__46E78A0C",
                        column: x => x.id_proveedor,
                        principalTable: "Proveedores",
                        principalColumn: "id_proveedor");
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    id_compra = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    total = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    id_proveedor = table.Column<int>(type: "integer", nullable: true),
                    id_usuario = table.Column<int>(type: "integer", nullable: true),
                    estado = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "Registrado")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Compras__C4BAA604E806B1EC", x => x.id_compra);
                    table.ForeignKey(
                        name: "FK__Compras__id_prov__60A75C0F",
                        column: x => x.id_proveedor,
                        principalTable: "Proveedores",
                        principalColumn: "id_proveedor");
                    table.ForeignKey(
                        name: "FK__Compras__id_usua__619B8048",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "Detalle_Venta",
                columns: table => new
                {
                    id_detalle = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cant = table.Column<int>(type: "integer", nullable: false),
                    precio_venta = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    id_prod = table.Column<int>(type: "integer", nullable: true),
                    id_venta = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detalle___4F1332DEEC7276F0", x => x.id_detalle);
                    table.ForeignKey(
                        name: "FK__Detalle_V__id_pr__5BE2A6F2",
                        column: x => x.id_prod,
                        principalTable: "Producto",
                        principalColumn: "id_prod");
                    table.ForeignKey(
                        name: "FK__Detalle_V__id_ve__5CD6CB2B",
                        column: x => x.id_venta,
                        principalTable: "Ventas",
                        principalColumn: "id_venta");
                });

            migrationBuilder.CreateTable(
                name: "Devoluciones",
                columns: table => new
                {
                    id_devolucion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_venta = table.Column<int>(type: "integer", nullable: true),
                    id_prod = table.Column<int>(type: "integer", nullable: true),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    motivo = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "Pendiente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Devoluci__0BBAEF8D50B82509", x => x.id_devolucion);
                    table.ForeignKey(
                        name: "FK__Devolucio__id_pr__6A30C649",
                        column: x => x.id_prod,
                        principalTable: "Producto",
                        principalColumn: "id_prod");
                    table.ForeignKey(
                        name: "FK__Devolucio__id_ve__693CA210",
                        column: x => x.id_venta,
                        principalTable: "Ventas",
                        principalColumn: "id_venta");
                });

            migrationBuilder.CreateTable(
                name: "Historial_Vencimiento",
                columns: table => new
                {
                    id_vencimiento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_prod = table.Column<int>(type: "integer", nullable: true),
                    fecha_ven = table.Column<DateOnly>(type: "date", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Historia__3A5DE40C5DD32FAF", x => x.id_vencimiento);
                    table.ForeignKey(
                        name: "FK__Historial__id_pr__4CA06362",
                        column: x => x.id_prod,
                        principalTable: "Producto",
                        principalColumn: "id_prod");
                });

            migrationBuilder.CreateTable(
                name: "Detalle_Compra",
                columns: table => new
                {
                    id_detalle_compra = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_compra = table.Column<int>(type: "integer", nullable: true),
                    id_prod = table.Column<int>(type: "integer", nullable: true),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    precio_unitario = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detalle___BD16E279951E0C9D", x => x.id_detalle_compra);
                    table.ForeignKey(
                        name: "FK__Detalle_C__id_co__6477ECF3",
                        column: x => x.id_compra,
                        principalTable: "Compras",
                        principalColumn: "id_compra");
                    table.ForeignKey(
                        name: "FK__Detalle_C__id_pr__656C112C",
                        column: x => x.id_prod,
                        principalTable: "Producto",
                        principalColumn: "id_prod");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Clientes__A25B3E612859D9E9",
                table: "Clientes",
                column: "documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compras_id_proveedor",
                table: "Compras",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_id_usuario",
                table: "Compras",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Comprobantes_id_venta",
                table: "Comprobantes",
                column: "id_venta");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Compra_id_compra",
                table: "Detalle_Compra",
                column: "id_compra");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Compra_id_prod",
                table: "Detalle_Compra",
                column: "id_prod");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Venta_id_prod",
                table: "Detalle_Venta",
                column: "id_prod");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Venta_id_venta",
                table: "Detalle_Venta",
                column: "id_venta");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_id_prod",
                table: "Devoluciones",
                column: "id_prod");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_id_venta",
                table: "Devoluciones",
                column: "id_venta");

            migrationBuilder.CreateIndex(
                name: "IX_Historial_Vencimiento_id_prod",
                table: "Historial_Vencimiento",
                column: "id_prod");

            migrationBuilder.CreateIndex(
                name: "UQ__Laborato__C2B74E611CFCDC5B",
                table: "Laboratorio",
                column: "ruc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Producto_id_categoria",
                table: "Producto",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_id_marca",
                table: "Producto",
                column: "id_marca");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_id_proveedor",
                table: "Producto",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_id_laboratorio",
                table: "Proveedores",
                column: "id_laboratorio");

            migrationBuilder.CreateIndex(
                name: "UQ__Proveedo__C2B74E61BA16DC0C",
                table: "Proveedores",
                column: "ruc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_id_rol",
                table: "Usuarios",
                column: "id_rol");

            migrationBuilder.CreateIndex(
                name: "UQ__Usuarios__9AFF8FC6F57D6012",
                table: "Usuarios",
                column: "usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_id_cliente",
                table: "Ventas",
                column: "id_cliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comprobantes");

            migrationBuilder.DropTable(
                name: "Detalle_Compra");

            migrationBuilder.DropTable(
                name: "Detalle_Venta");

            migrationBuilder.DropTable(
                name: "Devoluciones");

            migrationBuilder.DropTable(
                name: "Historial_Vencimiento");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Laboratorio");
        }
    }
}
