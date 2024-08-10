
using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using NuGet.Protocol.Plugins;
using Project3.Models;
using SQLitePCL;
using System.Collections;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;
using System.Data;
using System.Drawing;
using System.IO.Pipelines;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Diagnostics;

namespace Project3.Data
{
    public class Sem3DBContext : DbContext
    {
        public Sem3DBContext(DbContextOptions<Sem3DBContext> context) : base(context) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>(od => od.Property(odc => odc.OrderDetailId).ValueGeneratedOnAdd());

            modelBuilder.Entity<Account>().HasData(
                new
                {
                    AccountId = 1,
                    FullName = "Admin",
                    Email = "admin@gmail.com",
                    Password = "123456",
                    Phone = "0123456789",
                    Address = "Tòa Nhà HTC-250 Hoàng Quốc Việt-Cổ Nhuế-Cầu Giấy-Hà Nội",
                    Avatar = "acc1.jpg",
                    AccountStatus = "In force",
                    AccountType = "admin"
                },
                new
                {
                    AccountId = 2,
                    FullName = "User",
                    Email = "user@gmail.com",
                    Password = "123456",
                    Phone = "0328633121",
                    Address = "Hà nam city",
                    Avatar = "acc2.jpg",
                    AccountStatus = "In force",
                    AccountType = "user"
                },
                new
                {
                    AccountId = 3,
                    FullName = "User2",
                    Email = "user2@gmail.com",
                    Password = "123456",
                    Phone = "0987654321",
                    Address = "Tòa Nhà HTC-250 Hoàng Quốc Việt-Cổ Nhuế-Cầu Giấy-Hà Nội",
                    Avatar = "acc2.jpg",
                    AccountStatus = "Disable",
                    AccountType = "user"
                });

            modelBuilder.Entity<Category>().HasData(
                new
                {
                    CategoryId = 1,
                    CategoryName = "Digestive medicine",
                    CategoryType = "Medical"
                },
                new
                {
                    CategoryId = 2,
                    CategoryName = "Headache and pain reliever",
                    CategoryType = "Medical"
                },
                new
                {
                    CategoryId = 3,
                    CategoryName = "Fever and flu medicine",
                    CategoryType = "Medical"
                },
                new
                {
                    CategoryId = 4,
                    CategoryName = "Microscope",
                    CategoryType = "Science"
                },
                new
                {
                    CategoryId = 5,
                    CategoryName = "Blood pressure monitors",
                    CategoryType = "Science"

                },
                new
                {
                    CategoryId = 6,
                    CategoryName = "Cardiac event monitor",
                    CategoryType = "Science"
                },
                new
                {
                    CategoryId = 7,
                    CategoryName = "Pulse oximeter",
                    CategoryType = "Science"
                },
                new
                {
                    CategoryId = 8,
                    CategoryName = "Medical Exam tables",
                    CategoryType = "Science"
                });

            modelBuilder.Entity<Product>().HasData(
                new
                {
                    ProductId = 1,
                    ProductName = "Culturelle",
                    Price = 1000.0,
                    Description = "Brand\tCulturelle\r\nFlavor Unflavored\r\nPrimary Supplement Type Probiotic\r\nUnit Count  30 Count\r\nItem Form Capsule\r\nItem Weight 4.54 Grams\r\nItem Dimensions LxWxH   1.25 x 3.66 x 5.5 inches\r\nSpecial Ingredients All Natural\r\nDiet Type Gluten Free\r\nProduct Benefits Digestive Health Support, Immune Support",
                    ProductImage = "Culturelle.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 1
                },
                new
                {
                    ProductId = 2,
                    ProductName = "Gas-X Relief Tablets",
                    Price = 1000.0,
                    Description = "Brand\tGas-X\r\nSpecial Feature\tfast_acting\r\nItem Weight\t22.68 Grams\r\nItem Dimensions LxWxH\t1.22 x 2.75 x 4.53 inches\r\nProduct Benefits\tProvides bloating and gas relief",
                    ProductImage = "GasXReliefTablets.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 1
                },
                new
                {
                    ProductId = 3,
                    ProductName = "Advil Liqui-Gels",
                    Price = 2000.0,
                    Description = "Brand\tAdvil\r\nSpecial Feature The medicine in Advil Liqui-Gels(Ibuprofen) is already dissolved in a soft gelatin capsule and works quickly to relieve pain at the site of inflammation.The medicine in Advil Liqui-Gels(Ibuprofen) is already dissolved in a soft gelatin capsule and works quickly to relieve pain at the site of inflammation.\r\nItem Weight 4.64 Ounces\r\nItem Dimensions LxWxH   6 x 5 x 4 inches\r\nProduct Benefits Headache, Menstrual Pain, Joint Pain Relief, Backache, Fever",
                    ProductImage = "AdvilLiquiGels.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 2
                },
                new
                {
                    ProductId = 4,
                    ProductName = "Motrin IB",
                    Price = 2000.0,
                    Description = "Brand\tMotrin\r\nSpecial Feature Temporarily relieves minor aches and pains\r\nItem Weight 0.18 Grams\r\nItem Dimensions LxWxH   6 x 5 x 4 inches\r\nProduct Benefits Back Pain Relief",
                    ProductImage = "MotrinIB.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 2
                },
                new
                {
                    ProductId = 5,
                    ProductName = "Daytime Cold and Flu",
                    Price = 3000.0,
                    Description = "Brand\tAmazon Basic Care\r\nSpecial Feature Non Drowsy\r\nItem Dimensions LxWxH   1 x 4.38 x 3.25 inches\r\nProduct Benefits Cold and Flu Control\r\nSpecific Uses For Product Cold",
                    ProductImage = "DaytimeColdandFlu.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 3
                },
                new
                {
                    ProductId = 6,
                    ProductName = "Boiron Oscillococcinum",
                    Price = 3000.0,
                    Description = "Brand\tBoiron\r\nSpecial Feature Non Drowsy, Fast Acting\r\nItem Weight 0.04 Ounces\r\nItem Dimensions LxWxH   2 x 2 x 2 inches\r\nProduct Benefits Cold and Flu Control",
                    ProductImage = "BoironOscillococcinum.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 3
                },
                new
                {
                    ProductId = 7,
                    ProductName = "AmScope 120X-1200X 52-pcs",
                    Price = 4000.0,
                    Description = "Light Source Type\tLED\r\nModel Name  M30 ABS KT2 W\r\nMaterial ABS, Plastic, Metal\r\nColor White\r\nProduct Dimensions  14.57 L x 5.12 W x 15.75H\r\nReal Angle of View  90 Degrees\r\nMagnification Maximum   1200 x\r\nItem Weight 3.7 Pounds\r\n            Brand   AmScope\r\nObjective Lens Description  Achromatic",
                    ProductImage = "AmScope_120X_1200X.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 4
                },
                new
                {
                    ProductId = 8,
                    ProductName = "AmScope M150C-I 40X-1000X",
                    Price = 4000.0,
                    Description = "Light Source Type\tLED\r\nModel Name  M150C - I 40X - 1000X\r\nMaterial    Glass\r\nColor   Silver, White, Black\r\nProduct Dimensions  10L x 7W x 15H\r\nReal Angle of View  90 Degrees\r\nMagnification Maximum   1000 x\r\nVoltage 110 Volts\r\nBrand   AmScope\r\nObjective Lens Description  Achromatic",
                    ProductImage = "AmScope_M150CI.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 4
                },
                new
                {
                    ProductId = 9,
                    ProductName = "Beurer BM27 Upper Arm Blood Pressure Monitor",
                    Price = 5000.0,
                    Description = "Brand\tBeurer\r\nIncluded Components Cuff\r\nPower Source Battery Powered\r\nUse for Arm\r\nDisplay Type    LCD\r\nSize    Large\r\nAge Range(Description) Adult\r\nItem Weight 12 Ounces\r\nModel Name  BM27\r\nBand Size   16.5 inch",
                    ProductImage = "Beurer_BM27.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 5
                },
                new
                {
                    ProductId = 10,
                    ProductName = "Portable-Small Blood-Pressure Monitors",
                    Price = 5000.0,
                    Description = "Brand\tangel wish\r\nIncluded Components Home Blood Pressure Monitor x 1; Adjustable Arm Cuff x 1, Users Manual x 1\r\nPower Source    Battery Powered/ Charging Powered\r\nUse for Arm\r\nDisplay Type    LCD\r\nSize    Regular\r\nAge Range(Description) Adult\r\nModel Name  Blood Pressure Monitor\r\nBand Size   12.6 inch\r\nMaterial Feature    Durable",
                    ProductImage = "PortableSmall.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 5
                },
                new
                {
                    ProductId = 11,
                    ProductName = "Garmin 010-13118-00 HRM-Pro Plus",
                    Price = 6000.0,
                    Description = "Brand\tGarmin\r\nMaterial    Nylon\r\nColor   Black\r\nCompatible Devices  Smartphones\r\nScreen Size 0.96 Inches\r\nProduct Dimensions  1.18L x 0.47W x 23.62H\r\nItem Weight 51 Grams\r\nBattery Life    8760 Hours\r\nSensor Type Wearable\r\nBattery Description Lithium - Ion",
                    ProductImage = "Garmin.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 6
                },
                new
                {
                    ProductId = 12,
                    ProductName = "POLAR Unite Waterproof Fitness Watch & H9 Heart Rate Monitor Bundle",
                    Price = 6000.0,
                    Description = "Brand\tPOLAR\r\nModel Name  Unite\r\nColor   White\r\nScreen Size 1.2 Inches\r\nSpecial Feature Bluetooth\r\nAge Range(Description) Kid\r\nDisplay Type    LED\r\nBand Color  White\r\nBattery Life    4 days\r\nConnectivity Technology Bluetooth",
                    ProductImage = "POLARUnite.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 6
                },
                new
                {
                    ProductId = 13,
                    ProductName = "Zacurate 500C Elite",
                    Price = 7000.0,
                    Description = "Brand   Zacurate\r\nColor   Mystic Black\r\nNumber of Batteries 2 AAA batteries required. (included)\r\nBattery Life    30 Hours\r\nAre Batteries Included  Yes",
                    ProductImage = "Zacurate_500CElite.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 7
                },
                new
                {
                    ProductId = 14,
                    ProductName = "HealthSmart",
                    Price = 7000.0,
                    Description = "Brand\tHealthSmart\r\nColor   Red OLED\r\nMeasuring Range 70 % ~99 % ± 2 digits\r\nNumber of Batteries 2 AAA batteries required. (included)\r\nModel Name  HealthSmart Pulse Ox",
                    ProductImage = "HealthSmart.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 7
                },
                new
                {
                    ProductId = 15,
                    ProductName = "Armedica AM-300 HI-LO",
                    Price = 8000.0,
                    Description = "Brand\tArmedica\r\nShape   Rectangular\r\nProduct Care Instructions   Wipe with Dry Cloth\r\nMaterial    Steel, Welded",
                    ProductImage = "ArmedicaAM_300HILO.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 8
                },
                new
                {
                    ProductId = 16,
                    ProductName = "MedSurface Med Surface",
                    Price = 8000.0,
                    Description = "Brand   MedSurface\r\nShape   Rectangular\r\nProduct Care Instructions   Wipe with Dry Cloth",
                    ProductImage = "MedSurfaceMedSurface.jpg",
                    ProductStatus = "In stock",
                    CategoryId = 8
                });

            modelBuilder.Entity<News>().HasData(
                new
                {
                    NewsId = 1,
                    Title = "Conference on treatment work in the field of Traditional Medicine and Pharmacy",
                    ShortContent = "On December 11, 2023 in Nghe An, the Ministry of Health organized a conference to discuss medical examination and treatment in the field of Traditional Medicine and Pharmacy with the theme Combining traditional medicine with modern medicine. Deputy Minister of Health Do Xuan Tuyen chaired the conference.",
                    LongContent = "Speaking at the conference, Deputy Minister of Health Do Xuan Tuyen said that in recent years, with the attention of the Party and State, traditional medicine has inherited, promoted, and developed, achieving important achievements. important in protecting and caring for peoples health. Directions to prioritize the development of traditional medicine (TCM), combining Traditional Medicine with modern medicine, are clearly shown in the content of the Constitution, Platform of the Party Congress, and Resolution of the Central Executive Committee. Central Committee, Politburo and directives of the Secretariat.\r\nCombining traditional medicine with modern medicine is strictly implemented in all fields, from the system of legal documents such as the Law on Medical Examination and Treatment, the Law on Pharmacy,... the state management system of health to medical examination and treatment system, training programs, scientific research, professional documents guiding the implementation of medical examination and treatment, investing in infrastructure construction, purchasing medical equipment, updating Continuous medical knowledge, improving professional qualifications for medical examination and treatment practitioners.",
                    NewsDate = DateTime.Now,
                    NewsImage = "yte1.jpg",
                    NewsType = "Medical"
                },
                new
                {
                    NewsId = 2,
                    Title = "Propose amendments and supplements to regulations on registration and circulation of drugs and medicinal ingredients",
                    ShortContent = "In the draft Law amending and supplementing a number of articles of the Pharmacy Law, the Ministry of Health proposes to amend and supplement regulations on registration and circulation of drugs and medicinal ingredients.",
                    LongContent = "Specifically, the Ministry of Health proposes to amend Point a, Clause 2, Article 54 of the Pharmacy Law, accordingly, medicinal ingredients used to produce drugs according to drug registration dossiers that already have a certificate of registration for circulation in Vietnam do not have to be processed. registration for circulation to reduce and simplify administrative procedures.\r\nThe draft also proposes to supplement Clause 6, Article 56 of the Pharmacy Law, in which, no further extension of the validity of the circulation registration certificate for drugs and medicinal ingredients that have not been circulated on the market within the validity period has been granted. circulation registration certificate.",
                    NewsDate = DateTime.Now,
                    NewsImage = "yte2.jpg",
                    NewsType = "Medical"
                },
                new
                {
                    NewsId = 3,
                    Title = "Bidding for medical equipment has been resolved",
                    ShortContent = "The Ministry of Health has just issued Circular 08/2023/TT-BYT abolishing a number of legal documents issued by the Minister of Health. Including Circular No. 14/2020/TT-BYT dated July 10, 2020 of the Minister of Health regulating a number of contents in bidding for medical equipment at public medical facilities.",
                    LongContent = "Specifically, Circular 08/2023/TT-BYT abolishes all 3 legal documents issued by the Minister of Health, including:\r\nCircular No. 03/2016/TT-BYT dated January 21, 2016 of the Minister of Health regulating pharmaceutical business activities.\r\nCircular No. 31/2019/TT-BYT dated December 5, 2019 of the Minister of Health regulating requirements for fresh milk products used in the School Milk Program.\r\nCircular No. 14/2020/TT-BYT dated July 10, 2020 of the Minister of Health regulating a number of contents in bidding for medical equipment at public medical facilities.",
                    NewsDate = DateTime.Now,
                    NewsImage = "kh3.jpg",
                    NewsType = "Science"
                });

            modelBuilder.Entity<Comment>().HasData(
              new
              {
                  CommentId = 1,
                  Content = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Obcaecati beatae non a porro quia? Cumque suscipit dolor repellendus. Eos suscipit tempore dignissimos omnis veniam magnam error hic labore! Labore, aperiam.",
                  CommentDate = DateTime.Now,
                  NewsId = 2,
                  AccountId = 1

              },
              new
              {
                  CommentId = 2,
                  Content = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Obcaecati beatae non a porro quia? Cumque suscipit dolor repellendus. Eos suscipit tempore dignissimos omnis veniam magnam error hic labore! Labore, aperiam.",
                  CommentDate = DateTime.Now,
                  NewsId = 2,
                  AccountId = 2

              },
              new
              {
                  CommentId = 3,
                  Content = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Obcaecati beatae non a porro quia? Cumque suscipit dolor repellendus. Eos suscipit tempore dignissimos omnis veniam magnam error hic labore! Labore, aperiam.",
                  CommentDate = DateTime.Now,
                  NewsId = 3,
                  AccountId = 2

              });
            base.OnModelCreating(modelBuilder);
        }
    }
}