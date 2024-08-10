using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project3.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewsDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NewsImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverPhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ReceiverAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrdersId);
                    table.ForeignKey(
                        name: "FK_Orders_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NewsId = table.Column<int>(type: "int", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_Comment_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "NewsId");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<double>(type: "float", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Cart_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_Cart_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<double>(type: "float", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    OrdersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "OrdersId");
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId");
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountId", "AccountStatus", "AccountType", "Address", "Avatar", "Email", "FullName", "Password", "Phone" },
                values: new object[,]
                {
                    { 1, "In force", "admin", "Tòa Nhà HTC-250 Hoàng Quốc Việt-Cổ Nhuế-Cầu Giấy-Hà Nội", "acc1.jpg", "admin@gmail.com", "Admin", "123456", "0123456789" },
                    { 2, "In force", "user", "Hà nam city", "acc2.jpg", "user@gmail.com", "User", "123456", "0328633121" },
                    { 3, "Disable", "user", "Tòa Nhà HTC-250 Hoàng Quốc Việt-Cổ Nhuế-Cầu Giấy-Hà Nội", "acc2.jpg", "user2@gmail.com", "User2", "123456", "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CategoryName", "CategoryType" },
                values: new object[,]
                {
                    { 1, "Digestive medicine", "Medical" },
                    { 2, "Headache and pain reliever", "Medical" },
                    { 3, "Fever and flu medicine", "Medical" },
                    { 4, "Microscope", "Science" },
                    { 5, "Blood pressure monitors", "Science" },
                    { 6, "Cardiac event monitor", "Science" },
                    { 7, "Pulse oximeter", "Science" },
                    { 8, "Medical Exam tables", "Science" }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewsId", "LongContent", "NewsDate", "NewsImage", "NewsType", "ShortContent", "Title" },
                values: new object[,]
                {
                    { 1, "Speaking at the conference, Deputy Minister of Health Do Xuan Tuyen said that in recent years, with the attention of the Party and State, traditional medicine has inherited, promoted, and developed, achieving important achievements. important in protecting and caring for peoples health. Directions to prioritize the development of traditional medicine (TCM), combining Traditional Medicine with modern medicine, are clearly shown in the content of the Constitution, Platform of the Party Congress, and Resolution of the Central Executive Committee. Central Committee, Politburo and directives of the Secretariat.\r\nCombining traditional medicine with modern medicine is strictly implemented in all fields, from the system of legal documents such as the Law on Medical Examination and Treatment, the Law on Pharmacy,... the state management system of health to medical examination and treatment system, training programs, scientific research, professional documents guiding the implementation of medical examination and treatment, investing in infrastructure construction, purchasing medical equipment, updating Continuous medical knowledge, improving professional qualifications for medical examination and treatment practitioners.", new DateTime(2024, 1, 3, 15, 4, 37, 153, DateTimeKind.Local).AddTicks(9512), "yte1.jpg", "Medical", "On December 11, 2023 in Nghe An, the Ministry of Health organized a conference to discuss medical examination and treatment in the field of Traditional Medicine and Pharmacy with the theme Combining traditional medicine with modern medicine. Deputy Minister of Health Do Xuan Tuyen chaired the conference.", "Conference on treatment work in the field of Traditional Medicine and Pharmacy" },
                    { 2, "Specifically, the Ministry of Health proposes to amend Point a, Clause 2, Article 54 of the Pharmacy Law, accordingly, medicinal ingredients used to produce drugs according to drug registration dossiers that already have a certificate of registration for circulation in Vietnam do not have to be processed. registration for circulation to reduce and simplify administrative procedures.\r\nThe draft also proposes to supplement Clause 6, Article 56 of the Pharmacy Law, in which, no further extension of the validity of the circulation registration certificate for drugs and medicinal ingredients that have not been circulated on the market within the validity period has been granted. circulation registration certificate.", new DateTime(2024, 1, 3, 15, 4, 37, 153, DateTimeKind.Local).AddTicks(9523), "yte2.jpg", "Medical", "In the draft Law amending and supplementing a number of articles of the Pharmacy Law, the Ministry of Health proposes to amend and supplement regulations on registration and circulation of drugs and medicinal ingredients.", "Propose amendments and supplements to regulations on registration and circulation of drugs and medicinal ingredients" },
                    { 3, "Specifically, Circular 08/2023/TT-BYT abolishes all 3 legal documents issued by the Minister of Health, including:\r\nCircular No. 03/2016/TT-BYT dated January 21, 2016 of the Minister of Health regulating pharmaceutical business activities.\r\nCircular No. 31/2019/TT-BYT dated December 5, 2019 of the Minister of Health regulating requirements for fresh milk products used in the School Milk Program.\r\nCircular No. 14/2020/TT-BYT dated July 10, 2020 of the Minister of Health regulating a number of contents in bidding for medical equipment at public medical facilities.", new DateTime(2024, 1, 3, 15, 4, 37, 153, DateTimeKind.Local).AddTicks(9523), "kh3.jpg", "Science", "The Ministry of Health has just issued Circular 08/2023/TT-BYT abolishing a number of legal documents issued by the Minister of Health. Including Circular No. 14/2020/TT-BYT dated July 10, 2020 of the Minister of Health regulating a number of contents in bidding for medical equipment at public medical facilities.", "Bidding for medical equipment has been resolved" }
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentId", "AccountId", "CommentDate", "Content", "NewsId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 3, 15, 4, 37, 153, DateTimeKind.Local).AddTicks(9535), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Obcaecati beatae non a porro quia? Cumque suscipit dolor repellendus. Eos suscipit tempore dignissimos omnis veniam magnam error hic labore! Labore, aperiam.", 2 },
                    { 2, 2, new DateTime(2024, 1, 3, 15, 4, 37, 153, DateTimeKind.Local).AddTicks(9539), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Obcaecati beatae non a porro quia? Cumque suscipit dolor repellendus. Eos suscipit tempore dignissimos omnis veniam magnam error hic labore! Labore, aperiam.", 2 },
                    { 3, 2, new DateTime(2024, 1, 3, 15, 4, 37, 153, DateTimeKind.Local).AddTicks(9540), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Obcaecati beatae non a porro quia? Cumque suscipit dolor repellendus. Eos suscipit tempore dignissimos omnis veniam magnam error hic labore! Labore, aperiam.", 3 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "CategoryId", "Description", "Price", "ProductImage", "ProductName", "ProductStatus" },
                values: new object[,]
                {
                    { 1, 1, "Brand	Culturelle\r\nFlavor Unflavored\r\nPrimary Supplement Type Probiotic\r\nUnit Count  30 Count\r\nItem Form Capsule\r\nItem Weight 4.54 Grams\r\nItem Dimensions LxWxH   1.25 x 3.66 x 5.5 inches\r\nSpecial Ingredients All Natural\r\nDiet Type Gluten Free\r\nProduct Benefits Digestive Health Support, Immune Support", 1000.0, "Culturelle.jpg", "Culturelle", "In stock" },
                    { 2, 1, "Brand	Gas-X\r\nSpecial Feature	fast_acting\r\nItem Weight	22.68 Grams\r\nItem Dimensions LxWxH	1.22 x 2.75 x 4.53 inches\r\nProduct Benefits	Provides bloating and gas relief", 1000.0, "GasXReliefTablets.jpg", "Gas-X Relief Tablets", "In stock" },
                    { 3, 2, "Brand	Advil\r\nSpecial Feature The medicine in Advil Liqui-Gels(Ibuprofen) is already dissolved in a soft gelatin capsule and works quickly to relieve pain at the site of inflammation.The medicine in Advil Liqui-Gels(Ibuprofen) is already dissolved in a soft gelatin capsule and works quickly to relieve pain at the site of inflammation.\r\nItem Weight 4.64 Ounces\r\nItem Dimensions LxWxH   6 x 5 x 4 inches\r\nProduct Benefits Headache, Menstrual Pain, Joint Pain Relief, Backache, Fever", 2000.0, "AdvilLiquiGels.jpg", "Advil Liqui-Gels", "In stock" },
                    { 4, 2, "Brand	Motrin\r\nSpecial Feature Temporarily relieves minor aches and pains\r\nItem Weight 0.18 Grams\r\nItem Dimensions LxWxH   6 x 5 x 4 inches\r\nProduct Benefits Back Pain Relief", 2000.0, "MotrinIB.jpg", "Motrin IB", "In stock" },
                    { 5, 3, "Brand	Amazon Basic Care\r\nSpecial Feature Non Drowsy\r\nItem Dimensions LxWxH   1 x 4.38 x 3.25 inches\r\nProduct Benefits Cold and Flu Control\r\nSpecific Uses For Product Cold", 3000.0, "DaytimeColdandFlu.jpg", "Daytime Cold and Flu", "In stock" },
                    { 6, 3, "Brand	Boiron\r\nSpecial Feature Non Drowsy, Fast Acting\r\nItem Weight 0.04 Ounces\r\nItem Dimensions LxWxH   2 x 2 x 2 inches\r\nProduct Benefits Cold and Flu Control", 3000.0, "BoironOscillococcinum.jpg", "Boiron Oscillococcinum", "In stock" },
                    { 7, 4, "Light Source Type	LED\r\nModel Name  M30 ABS KT2 W\r\nMaterial ABS, Plastic, Metal\r\nColor White\r\nProduct Dimensions  14.57 L x 5.12 W x 15.75H\r\nReal Angle of View  90 Degrees\r\nMagnification Maximum   1200 x\r\nItem Weight 3.7 Pounds\r\n            Brand   AmScope\r\nObjective Lens Description  Achromatic", 4000.0, "AmScope_120X_1200X.jpg", "AmScope 120X-1200X 52-pcs", "In stock" },
                    { 8, 4, "Light Source Type	LED\r\nModel Name  M150C - I 40X - 1000X\r\nMaterial    Glass\r\nColor   Silver, White, Black\r\nProduct Dimensions  10L x 7W x 15H\r\nReal Angle of View  90 Degrees\r\nMagnification Maximum   1000 x\r\nVoltage 110 Volts\r\nBrand   AmScope\r\nObjective Lens Description  Achromatic", 4000.0, "AmScope_M150CI.jpg", "AmScope M150C-I 40X-1000X", "In stock" },
                    { 9, 5, "Brand	Beurer\r\nIncluded Components Cuff\r\nPower Source Battery Powered\r\nUse for Arm\r\nDisplay Type    LCD\r\nSize    Large\r\nAge Range(Description) Adult\r\nItem Weight 12 Ounces\r\nModel Name  BM27\r\nBand Size   16.5 inch", 5000.0, "Beurer_BM27.jpg", "Beurer BM27 Upper Arm Blood Pressure Monitor", "In stock" },
                    { 10, 5, "Brand	angel wish\r\nIncluded Components Home Blood Pressure Monitor x 1; Adjustable Arm Cuff x 1, Users Manual x 1\r\nPower Source    Battery Powered/ Charging Powered\r\nUse for Arm\r\nDisplay Type    LCD\r\nSize    Regular\r\nAge Range(Description) Adult\r\nModel Name  Blood Pressure Monitor\r\nBand Size   12.6 inch\r\nMaterial Feature    Durable", 5000.0, "PortableSmall.jpg", "Portable-Small Blood-Pressure Monitors", "In stock" },
                    { 11, 6, "Brand	Garmin\r\nMaterial    Nylon\r\nColor   Black\r\nCompatible Devices  Smartphones\r\nScreen Size 0.96 Inches\r\nProduct Dimensions  1.18L x 0.47W x 23.62H\r\nItem Weight 51 Grams\r\nBattery Life    8760 Hours\r\nSensor Type Wearable\r\nBattery Description Lithium - Ion", 6000.0, "Garmin.jpg", "Garmin 010-13118-00 HRM-Pro Plus", "In stock" },
                    { 12, 6, "Brand	POLAR\r\nModel Name  Unite\r\nColor   White\r\nScreen Size 1.2 Inches\r\nSpecial Feature Bluetooth\r\nAge Range(Description) Kid\r\nDisplay Type    LED\r\nBand Color  White\r\nBattery Life    4 days\r\nConnectivity Technology Bluetooth", 6000.0, "POLARUnite.jpg", "POLAR Unite Waterproof Fitness Watch & H9 Heart Rate Monitor Bundle", "In stock" },
                    { 13, 7, "Brand   Zacurate\r\nColor   Mystic Black\r\nNumber of Batteries 2 AAA batteries required. (included)\r\nBattery Life    30 Hours\r\nAre Batteries Included  Yes", 7000.0, "Zacurate_500CElite.jpg", "Zacurate 500C Elite", "In stock" },
                    { 14, 7, "Brand	HealthSmart\r\nColor   Red OLED\r\nMeasuring Range 70 % ~99 % ± 2 digits\r\nNumber of Batteries 2 AAA batteries required. (included)\r\nModel Name  HealthSmart Pulse Ox", 7000.0, "HealthSmart.jpg", "HealthSmart", "In stock" },
                    { 15, 8, "Brand	Armedica\r\nShape   Rectangular\r\nProduct Care Instructions   Wipe with Dry Cloth\r\nMaterial    Steel, Welded", 8000.0, "ArmedicaAM_300HILO.jpg", "Armedica AM-300 HI-LO", "In stock" },
                    { 16, 8, "Brand   MedSurface\r\nShape   Rectangular\r\nProduct Care Instructions   Wipe with Dry Cloth", 8000.0, "MedSurfaceMedSurface.jpg", "MedSurface Med Surface", "In stock" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_AccountId",
                table: "Cart",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductId",
                table: "Cart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AccountId",
                table: "Comment",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_NewsId",
                table: "Comment",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrdersId",
                table: "OrderDetail",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccountId",
                table: "Orders",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
