using System;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Models;
using TheCarHub.Models.Entities;

namespace TheCarHub.Data
{
    public class ApplicationDbContext : DbContext
    {
        private const string lipsum =
            "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Consequatur consequuntur esse exercitationem, nisi pariatur perspiciatis. Delectus molestiae officiis voluptatem. Accusamus commodi consequuntur deserunt error est fugit hic id ipsa iusto molestias placeat, praesentium quae qui quisquam tempora, totam, ullam velit voluptatibus. Alias, asperiores at blanditiis corporis dignissimos dolor esse, inventore labore nisi obcaecati quaerat quas qui unde. Aliquid aperiam architecto blanditiis commodi dignissimos dolores dolorum enim, eos esse exercitationem expedita fuga fugiat ipsam iste magnam maiores modi, officia possimus provident quas. Cupiditate dolor quae quaerat quia saepe sed voluptatibus? Accusamus at deserunt dolorem doloribus est harum illum impedit in incidunt itaque maxime modi molestiae nesciunt odio, perferendis quam quas quia repellendus repudiandae, vero voluptatem voluptates voluptatum? Culpa excepturi expedita maiores molestias quam voluptate? Aliquam assumenda, dicta ea iusto magni placeat reprehenderit velit vero voluptatem voluptatibus? Ab, animi assumenda consequatur doloremque earum eos est eum explicabo fuga id in labore maiores modi mollitia nemo nihil, nulla optio provident quae recusandae sequi ullam ut! Ab alias aliquam aliquid at commodi ducimus eaque, eligendi eum, facilis harum laudantium mollitia nobis officiis perferendis placeat praesentium quaerat quas quasi recusandae reiciendis saepe sapiente soluta veniam voluptas voluptatibus! Assumenda cum cumque cupiditate deleniti dolore eius eos expedita ipsa iste nisi officiis provident quo quod ratione, reiciendis repudiandae sed, tenetur voluptatum. Dolores, in maiores officiis reprehenderit soluta vel! Aliquam aperiam at deserunt dolor, eius error facilis impedit neque non officia possimus vel. Alias animi consequatur doloribus ipsum labore odio quam recusandae repellendus repudiandae voluptatum. Aspernatur, laboriosam, voluptatem? Deserunt dignissimos expedita ipsam nisi placeat recusandae repellendus ullam. Consectetur, nam, saepe. Assumenda at consequuntur debitis impedit labore officiis omnis, quam similique veritatis. Aliquam amet animi asperiores consequatur cumque dolore, eveniet ipsa itaque magni minima modi nihil non provident qui quia sequi sunt voluptatum. Alias beatae deserunt doloribus eum eveniet facere illum in iste nobis officia, officiis optio pariatur perferendis possimus quibusdam quod reiciendis repellat sint tempore voluptate! Iure laudantium nulla provident quas? Accusantium at atque, beatae cupiditate facilis, in iste libero magni numquam optio perferendis perspiciatis quaerat quasi quo quos ratione sequi totam vel veniam voluptates! Accusamus accusantium ad at consectetur doloremque dolorum expedita maiores necessitatibus nulla optio porro quam quo sed, sint sit tempore vitae? At corporis deleniti dolor error, facilis ipsum iste itaque, labore maxime necessitatibus nihil obcaecati odit quam reprehenderit sapiente suscipit unde. Commodi obcaecati porro quo quod saepe suscipit! Animi, aspernatur cum, ex exercitationem facilis illo inventore ipsam iure, laboriosam magnam mollitia non odio officiis optio possimus quidem quod repellat repudiandae rerum sequi totam veritatis vero vitae. Amet animi beatae consequatur debitis dolor doloremque eius esse et impedit ipsum iure, laboriosam maxime molestiae nesciunt nisi non odit quos repellendus tempore vitae? Consectetur excepturi harum hic illo illum nihil non quasi ratione rem, repellat. Adipisci aliquam autem delectus deleniti deserunt ea earum eius, eligendi exercitationem fuga harum illum laborum libero magnam modi molestias, neque nihil nobis non numquam odit officiis praesentium provident quam qui quidem quod, recusandae reprehenderit sequi sit suscipit tenetur voluptatem voluptates! Asperiores autem distinctio doloremque enim, fugiat itaque labore, numquam odio optio repellat tempore tenetur, veniam. Alias aliquam aperiam corporis ea eaque eligendi eum expedita fugiat illum iusto maiores minima nostrum, odio possimus quam quidem tempora tempore unde ut, vel velit veritatis voluptatem. Accusamus accusantium alias aliquam aperiam architecto aspernatur cum deserunt distinctio doloremque dolorum eius harum id ipsum iste iure laudantium libero nostrum quia quod quos rerum sapiente sunt, ullam unde vel veritatis vitae voluptates! Blanditiis consequatur cupiditate delectus enim ipsam omnis saepe sunt tempora totam. Accusamus ad, aperiam architecto commodi fugit incidunt molestias nobis officiis optio quasi sit vel, veniam! Ab alias consequuntur dignissimos dolore dolorum, ea earum eos esse est id laborum magnam maiores molestias neque non numquam odio perspiciatis, quae quam quibusdam sed, soluta suscipit ullam voluptate voluptates! Aperiam cupiditate deleniti eos in labore laboriosam molestiae nam nemo nihil, nulla perferendis reiciendis ullam vero! Ad alias deserunt dolore doloribus esse, incidunt, odit provident quaerat quo sequi, tempora totam vero voluptatum! Beatae ducimus quod tempore unde ut! Ad, explicabo officia. Accusamus ad aliquid beatae, blanditiis culpa dolorum esse est illo impedit inventore, ipsam ipsum laboriosam molestiae molestias mollitia optio porro possimus quam quasi qui, quibusdam quisquam rem sint ut voluptate. Accusantium alias asperiores atque cum cumque doloribus enim ex harum hic ipsam placeat quibusdam reiciendis rem repellat sed sint temporibus tenetur, totam ut vero voluptate voluptatem, voluptates voluptatum? Fuga nostrum officia quod. Accusamus amet animi aperiam commodi consequuntur corporis, deserunt dignissimos eius error et eveniet fuga fugiat fugit illum incidunt ipsum itaque minima molestias nulla numquam quam quas qui quod recusandae repellendus rerum sint, unde velit veritatis voluptas? Ab accusantium ad aliquam architecto commodi consequatur consequuntur cupiditate dolor dolores doloribus ea earum eius, enim eos et exercitationem facere facilis in incidunt itaque laboriosam magni nam necessitatibus nulla officiis perspiciatis possimus praesentium quam recusandae reiciendis repellat sapiente sit totam unde vel veritatis voluptates. A adipisci at cupiditate, deserunt dicta eligendi eum exercitationem facere fugiat fugit harum illum impedit iste iusto magni nemo neque obcaecati officiis perspiciatis quam qui quisquam recusandae reiciendis reprehenderit sunt temporibus ullam vitae! Alias et incidunt ipsum iure praesentium quidem quod reprehenderit temporibus! Architecto dicta dolore expedita illum iusto, labore nisi odio! Cupiditate debitis dolor in ipsum nam nesciunt quasi quia sed voluptatum. Beatae ducimus fugiat hic in labore neque nisi obcaecati possimus repudiandae tempora. Atque dolore doloribus et eum exercitationem facere fugit ipsum libero officiis pariatur quasi, quibusdam, tempora. Aliquam aliquid asperiores corporis cumque cupiditate deleniti dignissimos, dolor doloremque eaque eligendi eos esse eum expedita explicabo, fugit inventore itaque nemo non omnis quidem reprehenderit sit sunt tempore veritatis vitae. Eius inventore ipsum nesciunt nulla officiis pariatur quaerat. Alias aliquid delectus excepturi minus molestias, reiciendis rerum tempora! Aut corporis delectus eveniet facere id ipsam minus molestiae nulla, quae quia quis, suscipit ullam unde ut, voluptas! Ab aspernatur commodi, eaque exercitationem facere harum impedit officia placeat quos similique voluptas, voluptatum? Amet aspernatur consequuntur dignissimos eaque iure odio, voluptatum? Aspernatur expedita fuga ipsa non nostrum numquam reiciendis soluta. Optio, provident.";
        
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Car { get; set; }
        public DbSet<Listing> Listing { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<RepairJob> RepairJob { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Listing>()
                .Property(l => l.PurchasePrice)
                .HasColumnType("money");

            builder.Entity<Listing>()
                .Property(l => l.SellingPrice)
                .HasColumnType("money");

            builder.Entity<RepairJob>()
                .Property(rj => rj.Cost)
                .HasColumnType("money");

            builder.Entity<RepairJob>()
                .Property(rj => rj.Tax)
                .HasColumnType("money");

            builder.Entity<MediaTag>()
                .HasKey(mt => new {TagId = mt.TagId, MediaId = mt.MediaId});

            builder.Entity<ListingTag>()
                .HasKey(lt => new {ListingId = lt.ListingId, TagId = lt.TagId});

            builder.Entity<Car>()
                .HasData(
                    new Car
                    {
                        Id = 1,
                        VIN = "",
                        Year = 1991,
                        Make = "Mazda",
                        Model = "Miata",
                        Trim = "LE",
                    },
                    new Car
                    {
                        Id = 2,
                        VIN = "",
                        Year = 2007,
                        Make = "Jeep",
                        Model = "Liberty",
                        Trim = "Sport",
                    },
                    new Car
                    {
                        Id = 3,
                        VIN = "",
                        Year = 2017,
                        Make = "Ford",
                        Model = "Explorer",
                        Trim = "XLT",
                    },
                    new Car
                    {
                        Id = 4,
                        VIN = "",
                        Year = 2008,
                        Make = "Honda",
                        Model = "Civic",
                        Trim = "LX",
                    },
                    new Car
                    {
                        Id = 5,
                        VIN = "",
                        Year = 2016, 
                        Make = "Volkswagen",
                        Model = "GTI",
                        Trim = "S",
                    },
                    new Car
                    {
                        Id = 6,
                        VIN = "",
                        Year = 2013,
                        Make = "Ford",
                        Model = "Edge",
                        Trim = "SEL",
                    });

            builder.Entity<Listing>()
                .HasData(
                    new Listing
                    {
                        Id = 1,
                        CarId = 1,
                        StatusId = 1,
                        Description = lipsum,
                        Title = "Lorem ipsum dolor sit amet, consectetur adipisicing elit.",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = DateTime.Now,
                        PurchasePrice = 1800,
                        SellingPrice = 9900,
                        PurchaseDate = new DateTime(2019, 7, 1)
                        
                    },
                    new Listing
                    {
                        Id = 2,
                        CarId = 2,
                        StatusId = 1,
                        Description = lipsum,
                        Title = "Lorem ipsum dolor sit amet, consectetur adipisicing elit.",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = DateTime.Now,
                        PurchasePrice = 4500,
                        SellingPrice = 5350,
                        PurchaseDate = new DateTime(2019, 4, 2)
                    },
                    new Listing
                    {
                        Id = 3,
                        CarId = 3,
                        StatusId = 1,
                        Description = lipsum,
                        Title = "Lorem ipsum dolor sit amet, consectetur adipisicing elit.",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = DateTime.Now,
                        PurchasePrice = 1800,
                        SellingPrice = 2990,
                        PurchaseDate = new DateTime(2019, 4, 4)
                    },
                    new Listing
                    {
                        Id = 4,
                        CarId = 4,
                        StatusId = 1,
                        Description = lipsum,
                        Title = "Lorem ipsum dolor sit amet, consectetur adipisicing elit.",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = DateTime.Now,
                        PurchasePrice = 24350,
                        SellingPrice = 25950,
                        PurchaseDate = new DateTime(2019, 4, 5)
                    },
                    new Listing
                    {
                        Id = 5,
                        CarId = 5,
                        StatusId = 1,
                        Description = lipsum,
                        DateCreated = DateTime.Now,
                        Title = "Lorem ipsum dolor sit amet, consectetur adipisicing elit.",
                        DateLastUpdated = DateTime.Now,
                        PurchasePrice = 4000,
                        SellingPrice = 4975,
                        PurchaseDate = new DateTime(2019, 4, 6)
                    },
                    new Listing
                    {
                        Id = 6,
                        CarId = 6,
                        StatusId = 1,
                        Description = lipsum,
                        Title = "Lorem ipsum dolor sit amet, consectetur adipisicing elit.",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = DateTime.Now,
                        PurchasePrice = 15250,
                        SellingPrice = 16190,
                        PurchaseDate = new DateTime(2019, 4, 6)
                    }
                );

            builder.Entity<Status>()
                .HasData(
                    new Status
                    {
                        Id = 1,
                        Name = "Available"
                    },
                    new Status
                    {
                        Id = 2,
                        Name = "Sold"
                    }
                );

            builder.Entity<Media>()
                .HasData(new Media
                    {
                        ListingId = 1,
                        Id = 1,
                        FileName = "miata_1.jpg"
                    },
                    new Media
                    {
                        ListingId = 2,
                        Id = 2,
                        FileName = "jeep_liberty_1.jpg"
                    },
                    new Media
                    {
                        ListingId = 2,
                        Id = 3,
                        FileName = "jeep_liberty_2.jpg"
                    },
                    new Media
                    {
                        ListingId = 2,
                        Id = 4,
                        FileName = "jeep_liberty_3.jpg"
                    },
                    new Media
                    {
                        ListingId = 3,
                        Id = 5,
                        FileName = "ford_explorer_1.jpg"
                    },
                    new Media
                    {
                        ListingId = 3,
                        Id = 6,
                        FileName = "ford_explorer_2.jpg"
                    },
                    new Media
                    {
                        ListingId = 3,
                        Id = 7,
                        FileName = "ford_explorer_3.jpg"
                    },
                    new Media
                    {
                        ListingId = 4,
                        Id = 8,
                        FileName = "honda_civic_1.jpg"
                    },
                    new Media
                    {
                        ListingId = 4,
                        Id = 9,
                        FileName = "honda_civic_2.jpg"
                    },
                    new Media
                    {
                        ListingId = 5,
                        Id = 10,
                        FileName = "vw_gti_1.jpg"
                    },
                    new Media
                    {
                        ListingId = 5,
                        Id = 11,
                        FileName = "vw_gti_2.jpg"
                    },
                    new Media
                    {
                        ListingId = 5,
                        Id = 12,
                        FileName = "vw_gti_3.jpg"
                    },
                    new Media
                    {
                        ListingId = 6,
                        Id = 13,
                        FileName = "ford_edge_1.png"
                    },
                    new Media
                    {
                        ListingId = 6,
                        Id = 14,
                        FileName = "ford_edge_2.png"
                    },
                    new Media
                    {
                        ListingId = 6,
                        Id = 15,
                        FileName = "ford_edge_3.jpg"
                    }
                );

            builder.Entity<RepairJob>()
                .HasData(
                    new RepairJob
                    {
                        Id = 1,
                        ListingId = 1,
                        Description = "Full restoration",
                        Cost = 7600
                    },
                    new RepairJob
                    {
                        Id = 2,
                        ListingId = 2,
                        Description = "Front wheel bearings",
                        Cost = 350
                    },
                    new RepairJob
                    {
                        Id = 3, 
                        ListingId = 3,
                        Description = "Radiator, brakes",
                        Cost = 690
                    },
                    new RepairJob
                    {
                        Id = 4,
                        ListingId = 4,
                        Description = "Tires, brakes",
                        Cost = 1100
                    },
                    new RepairJob
                    {
                        Id = 5,
                        ListingId = 5,
                        Description = "AC, brakes",
                        Cost = 475
                    },
                    new RepairJob
                    {
                        Id = 6,
                        ListingId = 6,
                        Description = "Tires",
                        Cost = 440
                    }
                );
        }
    }
}