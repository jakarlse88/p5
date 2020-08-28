using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCarHub.Migrations.AppDbContext
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VIN = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Trim = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Listing",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    CarId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "money", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "money", nullable: false),
                    SaleDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listing_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Listing_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListingTag",
                columns: table => new
                {
                    ListingId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingTag", x => new { x.ListingId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ListingTag_Listing_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    ListingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Media_Listing_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairJob",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Cost = table.Column<decimal>(type: "money", nullable: false),
                    Tax = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairJob_Listing_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaTag",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false),
                    MediaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTag", x => new { x.TagId, x.MediaId });
                    table.ForeignKey(
                        name: "FK_MediaTag_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Car",
                columns: new[] { "Id", "Make", "Model", "Trim", "VIN", "Year" },
                values: new object[,]
                {
                    { 1, "Mazda", "Miata", "LE", "", 1991 },
                    { 2, "Jeep", "Liberty", "Sport", "", 2007 },
                    { 3, "Ford", "Explorer", "XLT", "", 2017 },
                    { 4, "Honda", "Civic", "LX", "", 2008 },
                    { 5, "Volkswagen", "GTI", "S", "", 2016 },
                    { 6, "Ford", "Edge", "SEL", "", 2013 }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Available" },
                    { 2, "Sold" }
                });

            migrationBuilder.InsertData(
                table: "Listing",
                columns: new[] { "Id", "CarId", "DateCreated", "DateLastUpdated", "Description", "PurchaseDate", "PurchasePrice", "SaleDate", "SellingPrice", "StatusId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 8, 28, 12, 17, 12, 873, DateTimeKind.Local).AddTicks(2293), new DateTime(2020, 8, 28, 12, 17, 12, 876, DateTimeKind.Local).AddTicks(9025), "This is surely one of the most recognisable sports cars on the planet. Perhaps the most recognisable. Mazda has (thus far) sold almost 1.1 million MX-5s in 29 years, making it the biggest selling roadster on the planet. Guinness certified, too.This is the fourth generation of MX-5, and it’s just received a mid-life update. By MX-5 standards, it’s actually a biggie, because for the first time in ages there’s actually more power.While the MX-5 sticks to its roots, only powering its rear wheels with naturally aspirated petrol engines – no all-wheel drive or turbos here – those engines are cleverer than ever, benefiting from all the nuanced tech that comes from Mazda’s ‘Skyactiv’ engine development.So while they remain 1.5- and 2.0-litre engines, they have tweaks. The former sees its power and torque figures pretty much as before – at 130bhp and 112lb ft – but it’s the latter where lots of interesting work has been done.The range-topping 2.0 has seen power climb by 24bhp, to 182bhp, which in a car that basically weighs a ton is big news indeed. It cuts nearly a second from the 0-62mph time, now 6.5sec. The rev limit has also climbed by 700rpm, to 7,500rpm, so you should have a bit more fun working the engine hard via its sweet six-speed manual gearbox.Extra performance hasn’t come about from a simple ECU remap, but from deeply nerdy mechanical things like shaving some weight from the pistons. Hard work, when the power climb that could have been doubled by simply sticking on a turbo. We’re glad Mazda took the tricky, technical route though. The MX-5’s always stood out for sticking resolutely to its simple mechanical layout.So, weight has been shaved from the most intricate engine components to make the unit as punchy and efficient as possible, while there’s even stop/start and brake regen to eke out further fuel economy from a car already unfathomably good at approaching 50mpg, even when driven with vigour.Other tweaks for the mk4 MX-5’s update include the addition of (optional) new tech – including Apple CarPlay alongside active safety stuff, such as lane-departure warning and blind-spot monitoring systems – as well as some interior quality improvements, the most telling of which for those who love driving is that the steering wheel now extends for reach, as well as height.Stop scoffing: among 50:50 weight distribution, perfectly placed pedals and a low-slung seat, the inability to pull the wheel out to your chest was a real bugbear on the MX-5, and fixing it could prove as tangible an improvement as those extra bhp", new DateTime(2019, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800m, null, 9900m, 1, "1991 Mazda Miata" },
                    { 2, 2, new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2430), new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2463), "Air filtration, Front air conditioning, Front air conditioning zones: single, Airbag deactivation: occupant sensing passenger, Front airbags: dual, Side curtain airbags: front, Antenna type: mast, Auxiliary audio input: jack, In-Dash CD: single disc, Premium brand: Infinity, Radio: AM/FM, Satellite radio: SiriusXM, Subwoofer: 1, Total speakers: 8, ABS: 4-wheel, Electronic brakeforce distribution, Front brake diameter: 11.9, Front brake type: ventilated disc, Front brake width: 1.1, Rear brake diameter: 12.4, Rear brake type: disc, Rear brake width: 0.47, Floor mat material: carpet, Floor material: carpet, Floor mats: front, Parking brake trim: leather, Shift knob trim: leather, Steering wheel trim: leather, Cargo area light, Cargo cover: retractable, Center console: front console with storage, Cruise control, Cupholders: front, Memorized settings: 2 driver, Multi-function remote: keyless entry, One-touch windows: 2, Overhead console: front, Power outlet(s): 115V, Power steering, Reading lights: front, Rearview mirror: auto-dimming, Retained accessory power, Steering wheel mounted controls: audio, Steering wheel: tilt, Storage: cargo net, Universal remote transmitter: garage door opener, Vanity mirrors: dual, Liftgate window: manual flip-up, Rear door type: liftgate, 4WD selector: manual hi-lo, 4WD type: part time, Axle ratio: 3.73, Alternator: 140 amps, Battery rating: 600 CCA, Battery: maintenance-free, Body side moldings: chrome, Door handle color: black, Front bumper color: body-color, Grille color: chrome, Mirror color: black, Rear bumper color: body-color, Clock, Compass, Gauge: tachometer, Multi-function display, Multi-functional information center, Warnings and reminders: lamp failure low fuel engine oil coolant, Front fog lights, Headlights: auto delay off, Side mirror adjustments: manual folding, Side mirrors: heated, Roof rails, Roof rails color: chrome, Active head restraints: dual front, Child safety door locks, Child seat anchors: LATCH system, Rear seatbelts: center 3-point, Seatbelt pretensioners: driver only, Driver seat manual adjustments: lumbar, Driver seat power adjustments: height, Driver seat: heated, Front headrests: adjustable, Front seat type: bucket, Passenger seat folding: folds flat, Passenger seat power adjustments: 2, Passenger seat: heated, Rear headrests: adjustable, Rear seat type: 60-40 split bench, Upholstery: leather, 2-stage unlocking doors, Anti-theft system: alarm, Power door locks: auto-locking, Hill descent control, Hill holder control, Roll stability control, Stability control, Traction control, Front shock type: gas, Front spring type: coil, Front stabilizer bar, Front suspension classification: independent, Front suspension type: upper and lower control arms, Rear shock type: gas, Rear spring type: coil, Rear stabilizer bar, Rear suspension classification: solid live axle, Rear suspension type: multi-link, Spare tire mount location: outside, Spare tire size: temporary, Spare wheel type: steel, Tire Pressure Monitoring System, Tire type: performance, Wheels: aluminum, Cargo tie downs, Front wipers: variable intermittent, Power windows, Rear privacy glass, Rear wiper: with washer, Window defogger: rear", new DateTime(2019, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4500m, null, 5350m, 1, "2007 Jeep Liberty" },
                    { 3, 3, new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2513), new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2517), "Right off the bat, the new Explorer steers, brakes and accelerates way better than the model it replaces. The steering has more feel, body roll is better checked and as a result you feel far more confident throwing it around. Off-road, the Explorer is more than capable enough to handle even quite unreasonably savage grades, rocks, water and mud. Those observations are true of all the models tested.Where the real differences lie is in the drivetrains. Even though this is the first time we have driven the hybrid version, and it was a pre-production model, we came away disappointed with it. In normal driving it doesn’t offer any benefit over the base-engined car. It’s noisier, spends more time hunting for gears and is generally less smooth in action.Over the test route it didn’t appear any more economical and the brake pedal lacked feel to boot. Which makes it currently nigh on impossible to justify the $4,150 extra cost of selecting the hybrid over the 2.3-litre four.The 3.0-litre twin turbo engine in the Platinum is a much easier sell. Feeling perfectly tuned to move the Explorer with confidence, reliability and civility, it is absolutely the engine to choose if your budget can run to it. It gives the whole package a more effortless, luxurious feel.Likewise, the 400bhp ST model, distinctly unlike the Edge ST, is a properly sorted sports SUV. While lacking the outright ferocity of some of the competition, it nonetheless moves down the road and around whatever you point it at with style, speed and fine-level control. Just like an ST badged vehicle should. A head-to-head with the Dodge Durango SRT will be an interesting match.Away from the drivetrains and chassis, the optional active safety package works well, only making itself known as and when the need arises. Likewise, road and wind noise is well suppressed in all models, which will make longer journeys less taxing.", new DateTime(2019, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800m, null, 2990m, 1, "2017 Ford Explorer" },
                    { 4, 4, new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2526), new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2530), "The high-geared steering would feel nervous if the car’s actual reactions weren’t so progressive. It rolls less than most rival hatches, and just gets on with the job of steering round the arc you set. There’s not a lot of steering feel, but the general chassis confidence makes up for it. It copes well with mid-corner bumps too.No surprise then that the ride is relatively taut, but it never gets harsh over small bumps, and on big intrusions it usually finds something in reserve. The adaptive damper system is nice to have, but not transformative.The engines aren’t quite such a success. The 1.0 certainly has enough urge to get the car up hills, making a distinctive triple-cylinder chatter as it goes. But because it needs high boost to make its power and torque, there’s definite lag across the rev range, especially below 3,000rpm. Also the rev limit is just 5,600rpm, and we kept bouncing against it. Most unlike high-revving Honda engines of old.The 1.5 will rev higher, to 6,500rpm, and lags less. Even so, you can’t help the feeling Honda pulled back on the tech. How much more responsive would it have been with VTEC and a twin-scroll turbo? (The VTEC Turbo badge is a dummy – there’s no VTEC here.) Still, let’s not bicker - for a relatively mainstream hatch, this is impressively lively. On boost it does 0-62 mph in the low-8-second range, depending on transmission and tyres.The 1.6 diesel is good by small capacity diesel standards: smooth and quiet, if you keep it below 2,500rpm, but fairly loud and uncouth above that point. But its 221lb ft is so chunky that you can make perfectly brisk progress right at the bottom of the rev range, and it disappears into the background when you keep the revs low. It’s an easygoing engine, not an exciting one.For excitement, you want the Type R. Beneath its abundant styling is one of the sharpest, most focused hot hatches in recent memory, though its adapative suspension and new exhaust system make it really refined and comfy when you just want to get somewhere calmly. It’s a better all-rounder than before yet acts like a tarmac rally car when you press the right buttons and you’re in the mood. It’s brilliant.The manual transmission in all Civics has a well-oiled notchy lever action and wisely chosen ratios. The optional CVT (on the sensible engines, obviously) is decently predictable in light driving. But if you press on, or take control using the paddles to choose between the seven virtual ratios, it slurs annoyingly.", new DateTime(2019, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 24350m, null, 25950m, 1, "2008 Honda Civic" },
                    { 5, 5, new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2538), new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2542), "If you’ve not driven a Golf GTI from the last five years, we’d suggest you do, as in its seventh generation the Golf’s ride and handling is its most impressive achievement. The MQB platform on which it is based is always a good start, but there is something transient in the Golf’s chassis specifically that makes it a more entertaining car to drive than other VW Group MQB-based models.Compared to the standard hatch the GTI’s suspension is 15mm lower, and now features a standard electronically controlled front locking differential. The set-up is nothing like as stiff as some rivals, being firm, but incredibly well controlled, with impeccable wheel control and a fluid primary and secondary ride, even on the larger 19-inch wheel option. The suspension itself is by MacPherson struts at the front and a multi-link set-up at the rear. Sometimes British B-roads can be the undoing of a car, and sometimes they can be the making of it. With the GTI it’s the latter. The bumps, humps and general rough and tumble of broken tarmac reveal just how talented the chassis is, as it tracks the road in a display of beautifully controlled damping.", new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 4000m, null, 4975m, 1, "2016 Volkswagen GTI" },
                    { 6, 6, new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2549), new DateTime(2020, 8, 28, 12, 17, 12, 877, DateTimeKind.Local).AddTicks(2553), "Buyers looking for a midsize SUV that's a little nicer than the mainstream choices without a budget-busting price will find plenty to like in the Ford Edge. It's tight and controlled on the road and drives much like a sedan. The quietness of its cabin and the expansive passenger and cargo space are impressive, and it has one of the roomiest interiors in its class.The Edge can be equipped at near-luxury levels, but even in base form it offers many advanced driver safety aids and a robust list of standard features. The standard turbocharged four-cylinder engine does a commendable job hauling the Edge's not-insignificant mass, and it even achieves respectable fuel economy while doing it.The Sync 3 infotainment system, which is standard across the lineup, is one of the better tech interfaces on the market, especially when you take advantage of its voice controls. For 2020, however, the Edge finally ditches the CD player. With Apple CarPlay and Android Auto included on all trims, most people won't miss it. But, please, a moment of silence.There are a few other desirable models that offer an upscale approach to the crossover SUV formula. The GMC Acadia or the Kia Sorento might be a better pick for buyers seeking a third row, and the recently redesigned Hyundai Santa Fe is also a compelling choice. Still, the Edge's roomy interior, respectable fuel economy and enjoyable driving character make it a worthwhile addition to the test-drive list.", new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 15250m, null, 16190m, 1, "2013 Ford Edge" }
                });

            migrationBuilder.InsertData(
                table: "Media",
                columns: new[] { "Id", "Caption", "FileName", "ListingId" },
                values: new object[,]
                {
                    { 1, null, "miata_1.jpg", 1 },
                    { 13, null, "ford_edge_1.jpg", 6 },
                    { 11, null, "vw_gti_2.jpg", 5 },
                    { 10, null, "vw_gti_1.jpg", 5 },
                    { 9, null, "honda_civic_2.jpg", 4 },
                    { 14, null, "ford_edge_2.jpg", 6 },
                    { 8, null, "honda_civic_1.jpg", 4 },
                    { 5, null, "ford_explorer_1.jpg", 3 },
                    { 3, null, "jeep_liberty_2.jpg", 2 },
                    { 2, null, "jeep_liberty_1.jpg", 2 },
                    { 6, null, "ford_explorer_2.jpg", 3 }
                });

            migrationBuilder.InsertData(
                table: "RepairJob",
                columns: new[] { "Id", "Cost", "Description", "ListingId", "Tax" },
                values: new object[,]
                {
                    { 3, 690m, "Radiator, brakes", 3, 0m },
                    { 2, 350m, "Front wheel bearings", 2, 0m },
                    { 4, 1100m, "Tires, brakes", 4, 0m },
                    { 5, 475m, "AC, brakes", 5, 0m },
                    { 1, 7600m, "Full restoration", 1, 0m },
                    { 6, 440m, "Tires", 6, 0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listing_CarId",
                table: "Listing",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Listing_StatusId",
                table: "Listing",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ListingTag_TagId",
                table: "ListingTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_ListingId",
                table: "Media",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaTag_MediaId",
                table: "MediaTag",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairJob_ListingId",
                table: "RepairJob",
                column: "ListingId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingTag");

            migrationBuilder.DropTable(
                name: "MediaTag");

            migrationBuilder.DropTable(
                name: "RepairJob");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Listing");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
