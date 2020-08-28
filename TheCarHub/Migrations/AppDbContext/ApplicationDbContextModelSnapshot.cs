﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheCarHub.Data;

namespace TheCarHub.Migrations.AppDbContext
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TheCarHub.Models.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Make")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Trim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Car");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Make = "Mazda",
                            Model = "Miata",
                            Trim = "LE",
                            VIN = "",
                            Year = 1991
                        },
                        new
                        {
                            Id = 2,
                            Make = "Jeep",
                            Model = "Liberty",
                            Trim = "Sport",
                            VIN = "",
                            Year = 2007
                        },
                        new
                        {
                            Id = 3,
                            Make = "Ford",
                            Model = "Explorer",
                            Trim = "XLT",
                            VIN = "",
                            Year = 2017
                        },
                        new
                        {
                            Id = 4,
                            Make = "Honda",
                            Model = "Civic",
                            Trim = "LX",
                            VIN = "",
                            Year = 2008
                        },
                        new
                        {
                            Id = 5,
                            Make = "Volkswagen",
                            Model = "GTI",
                            Trim = "S",
                            VIN = "",
                            Year = 2016
                        },
                        new
                        {
                            Id = 6,
                            Make = "Ford",
                            Model = "Edge",
                            Trim = "SEL",
                            VIN = "",
                            Year = 2013
                        });
                });

            modelBuilder.Entity("TheCarHub.Models.Entities.Listing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateLastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("money");

                    b.Property<DateTime?>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SellingPrice")
                        .HasColumnType("money");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("StatusId");

                    b.ToTable("Listing");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CarId = 1,
                            DateCreated = new DateTime(2020, 8, 28, 12, 17, 12, 873, DateTimeKind.Local).AddTicks(2293),
                            DateLastUpdated = new DateTime(2020, 8, 28, 12, 17, 12, 876, DateTimeKind.Local).AddTicks(9025),
                            Description = "This is surely one of the most recognisable sports cars on the planet. Perhaps the most recognisable. Mazda has (thus far) sold almost 1.1 million MX-5s in 29 years, making it the biggest selling roadster on the planet. Guinness certified, too.This is the fourth generation of MX-5, and it’s just received a mid-life update. By MX-5 standards, it’s actually a biggie, because for the first time in ages there’s actually more power.While the MX-5 sticks to its roots, only powering its rear wheels with naturally aspirated petrol engines – no all-wheel drive or turbos here – those engines are cleverer than ever, benefiting from all the nuanced tech that comes from Mazda’s ‘Skyactiv’ engine development.So while they remain 1.5- and 2.0-litre engines, they have tweaks. The former sees its power and torque figures pretty much as before – at 130bhp and 112lb ft – but it’s the latter where lots of interesting work has been done.The range-topping 2.0 has seen power climb by 24bhp, to 182bhp, which in a car that basically weighs a ton is big news indeed. It cuts nearly a second from the 0-62mph time, now 6.5sec. The rev limit has also climbed by 700rpm, to 7,500rpm, so you should have a bit more fun working the engine hard via its sweet six-speed manual gearbox.Extra performance hasn’t come about from a simple ECU remap, but from deeply nerdy mechanical things like shaving some weight from the pistons. Hard work, when the power climb that could have been doubled by simply sticking on a turbo. We’re glad Mazda took the tricky, technical route though. The MX-5’s always stood out for sticking resolutely to its simple mechanical layout.So, weight has been shaved from the most intricate engine components to make the unit as punchy and efficient as possible, while there’s even stop/start and brake regen to eke out further fuel economy from a car already unfathomably good at approaching 50mpg, even when driven with vigour.Other tweaks for the mk4 MX-5’s update include the addition of (optional) new tech – including Apple CarPlay alongside active safety stuff, such as lane-departure warning and blind-spot monitoring systems – as well as some interior quality improvements, the most telling of which for those who love driving is that the steering wheel now extends for reach, as well as height.Stop scoffing: among 50:50 weight distribution, perfectly placed pedals and a low-slung seat, the inability to pull the wheel out to your chest was a real bugbear on the MX-5, and fixing it could prove as tangible an improvement as those extra bhp",
                            PurchaseDate = new DateTime(2019, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 1800m,
                            SellingPrice = 9900m,
                            StatusId = 1,
                            Title = "1991 Mazda Miata"
                        },
                        new
                        {
                            Id = 2,
                            CarId = 2,
                            DateCreated = new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2430),
                            DateLastUpdated = new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2463),
                            Description = "Air filtration, Front air conditioning, Front air conditioning zones: single, Airbag deactivation: occupant sensing passenger, Front airbags: dual, Side curtain airbags: front, Antenna type: mast, Auxiliary audio input: jack, In-Dash CD: single disc, Premium brand: Infinity, Radio: AM/FM, Satellite radio: SiriusXM, Subwoofer: 1, Total speakers: 8, ABS: 4-wheel, Electronic brakeforce distribution, Front brake diameter: 11.9, Front brake type: ventilated disc, Front brake width: 1.1, Rear brake diameter: 12.4, Rear brake type: disc, Rear brake width: 0.47, Floor mat material: carpet, Floor material: carpet, Floor mats: front, Parking brake trim: leather, Shift knob trim: leather, Steering wheel trim: leather, Cargo area light, Cargo cover: retractable, Center console: front console with storage, Cruise control, Cupholders: front, Memorized settings: 2 driver, Multi-function remote: keyless entry, One-touch windows: 2, Overhead console: front, Power outlet(s): 115V, Power steering, Reading lights: front, Rearview mirror: auto-dimming, Retained accessory power, Steering wheel mounted controls: audio, Steering wheel: tilt, Storage: cargo net, Universal remote transmitter: garage door opener, Vanity mirrors: dual, Liftgate window: manual flip-up, Rear door type: liftgate, 4WD selector: manual hi-lo, 4WD type: part time, Axle ratio: 3.73, Alternator: 140 amps, Battery rating: 600 CCA, Battery: maintenance-free, Body side moldings: chrome, Door handle color: black, Front bumper color: body-color, Grille color: chrome, Mirror color: black, Rear bumper color: body-color, Clock, Compass, Gauge: tachometer, Multi-function display, Multi-functional information center, Warnings and reminders: lamp failure low fuel engine oil coolant, Front fog lights, Headlights: auto delay off, Side mirror adjustments: manual folding, Side mirrors: heated, Roof rails, Roof rails color: chrome, Active head restraints: dual front, Child safety door locks, Child seat anchors: LATCH system, Rear seatbelts: center 3-point, Seatbelt pretensioners: driver only, Driver seat manual adjustments: lumbar, Driver seat power adjustments: height, Driver seat: heated, Front headrests: adjustable, Front seat type: bucket, Passenger seat folding: folds flat, Passenger seat power adjustments: 2, Passenger seat: heated, Rear headrests: adjustable, Rear seat type: 60-40 split bench, Upholstery: leather, 2-stage unlocking doors, Anti-theft system: alarm, Power door locks: auto-locking, Hill descent control, Hill holder control, Roll stability control, Stability control, Traction control, Front shock type: gas, Front spring type: coil, Front stabilizer bar, Front suspension classification: independent, Front suspension type: upper and lower control arms, Rear shock type: gas, Rear spring type: coil, Rear stabilizer bar, Rear suspension classification: solid live axle, Rear suspension type: multi-link, Spare tire mount location: outside, Spare tire size: temporary, Spare wheel type: steel, Tire Pressure Monitoring System, Tire type: performance, Wheels: aluminum, Cargo tie downs, Front wipers: variable intermittent, Power windows, Rear privacy glass, Rear wiper: with washer, Window defogger: rear",
                            PurchaseDate = new DateTime(2019, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 4500m,
                            SellingPrice = 5350m,
                            StatusId = 1,
                            Title = "2007 Jeep Liberty"
                        },
                        new
                        {
                            Id = 3,
                            CarId = 3,
                            DateCreated = new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2513),
                            DateLastUpdated = new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2517),
                            Description = "Right off the bat, the new Explorer steers, brakes and accelerates way better than the model it replaces. The steering has more feel, body roll is better checked and as a result you feel far more confident throwing it around. Off-road, the Explorer is more than capable enough to handle even quite unreasonably savage grades, rocks, water and mud. Those observations are true of all the models tested.Where the real differences lie is in the drivetrains. Even though this is the first time we have driven the hybrid version, and it was a pre-production model, we came away disappointed with it. In normal driving it doesn’t offer any benefit over the base-engined car. It’s noisier, spends more time hunting for gears and is generally less smooth in action.Over the test route it didn’t appear any more economical and the brake pedal lacked feel to boot. Which makes it currently nigh on impossible to justify the $4,150 extra cost of selecting the hybrid over the 2.3-litre four.The 3.0-litre twin turbo engine in the Platinum is a much easier sell. Feeling perfectly tuned to move the Explorer with confidence, reliability and civility, it is absolutely the engine to choose if your budget can run to it. It gives the whole package a more effortless, luxurious feel.Likewise, the 400bhp ST model, distinctly unlike the Edge ST, is a properly sorted sports SUV. While lacking the outright ferocity of some of the competition, it nonetheless moves down the road and around whatever you point it at with style, speed and fine-level control. Just like an ST badged vehicle should. A head-to-head with the Dodge Durango SRT will be an interesting match.Away from the drivetrains and chassis, the optional active safety package works well, only making itself known as and when the need arises. Likewise, road and wind noise is well suppressed in all models, which will make longer journeys less taxing.",
                            PurchaseDate = new DateTime(2019, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 1800m,
                            SellingPrice = 2990m,
                            StatusId = 1,
                            Title = "2017 Ford Explorer"
                        },
                        new
                        {
                            Id = 4,
                            CarId = 4,
                            DateCreated = new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2526),
                            DateLastUpdated = new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2530),
                            Description = "The high-geared steering would feel nervous if the car’s actual reactions weren’t so progressive. It rolls less than most rival hatches, and just gets on with the job of steering round the arc you set. There’s not a lot of steering feel, but the general chassis confidence makes up for it. It copes well with mid-corner bumps too.No surprise then that the ride is relatively taut, but it never gets harsh over small bumps, and on big intrusions it usually finds something in reserve. The adaptive damper system is nice to have, but not transformative.The engines aren’t quite such a success. The 1.0 certainly has enough urge to get the car up hills, making a distinctive triple-cylinder chatter as it goes. But because it needs high boost to make its power and torque, there’s definite lag across the rev range, especially below 3,000rpm. Also the rev limit is just 5,600rpm, and we kept bouncing against it. Most unlike high-revving Honda engines of old.The 1.5 will rev higher, to 6,500rpm, and lags less. Even so, you can’t help the feeling Honda pulled back on the tech. How much more responsive would it have been with VTEC and a twin-scroll turbo? (The VTEC Turbo badge is a dummy – there’s no VTEC here.) Still, let’s not bicker - for a relatively mainstream hatch, this is impressively lively. On boost it does 0-62 mph in the low-8-second range, depending on transmission and tyres.The 1.6 diesel is good by small capacity diesel standards: smooth and quiet, if you keep it below 2,500rpm, but fairly loud and uncouth above that point. But its 221lb ft is so chunky that you can make perfectly brisk progress right at the bottom of the rev range, and it disappears into the background when you keep the revs low. It’s an easygoing engine, not an exciting one.For excitement, you want the Type R. Beneath its abundant styling is one of the sharpest, most focused hot hatches in recent memory, though its adapative suspension and new exhaust system make it really refined and comfy when you just want to get somewhere calmly. It’s a better all-rounder than before yet acts like a tarmac rally car when you press the right buttons and you’re in the mood. It’s brilliant.The manual transmission in all Civics has a well-oiled notchy lever action and wisely chosen ratios. The optional CVT (on the sensible engines, obviously) is decently predictable in light driving. But if you press on, or take control using the paddles to choose between the seven virtual ratios, it slurs annoyingly.",
                            PurchaseDate = new DateTime(2019, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 24350m,
                            SellingPrice = 25950m,
                            StatusId = 1,
                            Title = "2008 Honda Civic"
                        },
                        new
                        {
                            Id = 5,
                            CarId = 5,
                            DateCreated = new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2538),
                            DateLastUpdated = new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2542),
                            Description = "If you’ve not driven a Golf GTI from the last five years, we’d suggest you do, as in its seventh generation the Golf’s ride and handling is its most impressive achievement. The MQB platform on which it is based is always a good start, but there is something transient in the Golf’s chassis specifically that makes it a more entertaining car to drive than other VW Group MQB-based models.Compared to the standard hatch the GTI’s suspension is 15mm lower, and now features a standard electronically controlled front locking differential. The set-up is nothing like as stiff as some rivals, being firm, but incredibly well controlled, with impeccable wheel control and a fluid primary and secondary ride, even on the larger 19-inch wheel option. The suspension itself is by MacPherson struts at the front and a multi-link set-up at the rear. Sometimes British B-roads can be the undoing of a car, and sometimes they can be the making of it. With the GTI it’s the latter. The bumps, humps and general rough and tumble of broken tarmac reveal just how talented the chassis is, as it tracks the road in a display of beautifully controlled damping.",
                            PurchaseDate = new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 4000m,
                            SellingPrice = 4975m,
                            StatusId = 1,
                            Title = "2016 Volkswagen GTI"
                        },
                        new
                        {
                            Id = 6,
                            CarId = 6,
                            DateCreated = new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2549),
                            DateLastUpdated = new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2553),
                            Description = "Buyers looking for a midsize SUV that's a little nicer than the mainstream choices without a budget-busting price will find plenty to like in the Ford Edge. It's tight and controlled on the road and drives much like a sedan. The quietness of its cabin and the expansive passenger and cargo space are impressive, and it has one of the roomiest interiors in its class.The Edge can be equipped at near-luxury levels, but even in base form it offers many advanced driver safety aids and a robust list of standard features. The standard turbocharged four-cylinder engine does a commendable job hauling the Edge's not-insignificant mass, and it even achieves respectable fuel economy while doing it.The Sync 3 infotainment system, which is standard across the lineup, is one of the better tech interfaces on the market, especially when you take advantage of its voice controls. For 2020, however, the Edge finally ditches the CD player. With Apple CarPlay and Android Auto included on all trims, most people won't miss it. But, please, a moment of silence.There are a few other desirable models that offer an upscale approach to the crossover SUV formula. The GMC Acadia or the Kia Sorento might be a better pick for buyers seeking a third row, and the recently redesigned Hyundai Santa Fe is also a compelling choice. Still, the Edge's roomy interior, respectable fuel economy and enjoyable driving character make it a worthwhile addition to the test-drive list.",
                            PurchaseDate = new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 15250m,
                            SellingPrice = 16190m,
                            StatusId = 1,
                            Title = "2013 Ford Edge"
                        });
                });

            modelBuilder.Entity("TheCarHub.Models.Entities.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ListingId");

                    b.ToTable("Media");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FileName = "miata_1.jpg",
                            ListingId = 1
                        },
                        new
                        {
                            Id = 2,
                            FileName = "jeep_liberty_1.jpg",
                            ListingId = 2
                        },
                        new
                        {
                            Id = 3,
                            FileName = "jeep_liberty_2.jpg",
                            ListingId = 2
                        },
                        new
                        {
                            Id = 5,
                            FileName = "ford_explorer_1.jpg",
                            ListingId = 3
                        },
                        new
                        {
                            Id = 6,
                            FileName = "ford_explorer_2.jpg",
                            ListingId = 3
                        },
                        new
                        {
                            Id = 8,
                            FileName = "honda_civic_1.jpg",
                            ListingId = 4
                        },
                        new
                        {
                            Id = 9,
                            FileName = "honda_civic_2.jpg",
                            ListingId = 4
                        },
                        new
                        {
                            Id = 10,
                            FileName = "vw_gti_1.jpg",
                            ListingId = 5
                        },
                        new
                        {
                            Id = 11,
                            FileName = "vw_gti_2.jpg",
                            ListingId = 5
                        },
                        new
                        {
                            Id = 13,
                            FileName = "ford_edge_1.jpg",
                            ListingId = 6
                        },
                        new
                        {
                            Id = 14,
                            FileName = "ford_edge_2.jpg",
                            ListingId = 6
                        });
                });

            modelBuilder.Entity("TheCarHub.Models.Entities.RepairJob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("money");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingId")
                        .HasColumnType("int");

                    b.Property<decimal>("Tax")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("ListingId")
                        .IsUnique();

                    b.ToTable("RepairJob");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cost = 7600m,
                            Description = "Full restoration",
                            ListingId = 1,
                            Tax = 0m
                        },
                        new
                        {
                            Id = 2,
                            Cost = 350m,
                            Description = "Front wheel bearings",
                            ListingId = 2,
                            Tax = 0m
                        },
                        new
                        {
                            Id = 3,
                            Cost = 690m,
                            Description = "Radiator, brakes",
                            ListingId = 3,
                            Tax = 0m
                        },
                        new
                        {
                            Id = 4,
                            Cost = 1100m,
                            Description = "Tires, brakes",
                            ListingId = 4,
                            Tax = 0m
                        },
                        new
                        {
                            Id = 5,
                            Cost = 475m,
                            Description = "AC, brakes",
                            ListingId = 5,
                            Tax = 0m
                        },
                        new
                        {
                            Id = 6,
                            Cost = 440m,
                            Description = "Tires",
                            ListingId = 6,
                            Tax = 0m
                        });
                });

            modelBuilder.Entity("TheCarHub.Models.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Available"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Sold"
                        });
                });

            modelBuilder.Entity("TheCarHub.Models.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("TheCarHub.Models.ListingTag", b =>
                {
                    b.Property<int>("ListingId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("ListingId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ListingTag");
                });

            modelBuilder.Entity("TheCarHub.Models.MediaTag", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("MediaId")
                        .HasColumnType("int");

                    b.HasKey("TagId", "MediaId");

                    b.HasIndex("MediaId");

                    b.ToTable("MediaTag");
                });

            modelBuilder.Entity("TheCarHub.Models.Entities.Listing", b =>
                {
                    b.HasOne("TheCarHub.Models.Entities.Car", "Car")
                        .WithMany("Listings")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheCarHub.Models.Entities.Status", "Status")
                        .WithMany("Listings")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheCarHub.Models.Entities.Media", b =>
                {
                    b.HasOne("TheCarHub.Models.Entities.Listing", "Listing")
                        .WithMany("Media")
                        .HasForeignKey("ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheCarHub.Models.Entities.RepairJob", b =>
                {
                    b.HasOne("TheCarHub.Models.Entities.Listing", "Listing")
                        .WithOne("RepairJob")
                        .HasForeignKey("TheCarHub.Models.Entities.RepairJob", "ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheCarHub.Models.ListingTag", b =>
                {
                    b.HasOne("TheCarHub.Models.Entities.Listing", "Listing")
                        .WithMany("Tags")
                        .HasForeignKey("ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheCarHub.Models.Entities.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheCarHub.Models.MediaTag", b =>
                {
                    b.HasOne("TheCarHub.Models.Entities.Media", "Media")
                        .WithMany("Tags")
                        .HasForeignKey("MediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheCarHub.Models.Entities.Tag", "Tag")
                        .WithMany("Tags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}