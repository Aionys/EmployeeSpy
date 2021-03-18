﻿// <auto-generated />
using System;
using EmployeeSpy.DataAccessEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeSpy.DataAccessEF.Migrations
{
    [DbContext(typeof(EmployeeSpyContext))]
    [Migration("20210315085723_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmployeeSpy.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("EmployeeSpy.Models.GateKeeper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DoorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoorId");

                    b.ToTable("GateKeepers");
                });

            modelBuilder.Entity("EmployeeSpy.Models.Door", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EntranceControlId")
                        .HasColumnType("int");

                    b.Property<int?>("ExitControlId")
                        .HasColumnType("int");

                    b.Property<int?>("GuardId")
                        .HasColumnType("int");

                    b.Property<int>("KeepOpenSeconds")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(10);

                    b.HasKey("Id");

                    b.HasIndex("EntranceControlId");

                    b.HasIndex("ExitControlId");

                    b.HasIndex("GuardId");

                    b.ToTable("Doors");
                });

            modelBuilder.Entity("EmployeeSpy.Models.MovementLogRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MoveDirection")
                        .HasColumnType("int");

                    b.Property<DateTime>("MoveTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PassedDoorId")
                        .HasColumnType("int");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PassedDoorId");

                    b.HasIndex("PersonId");

                    b.ToTable("MovementLogs");
                });

            modelBuilder.Entity("EmployeeSpy.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Person");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("EmployeeSpy.Models.Employee", b =>
                {
                    b.HasBaseType("EmployeeSpy.Models.Person");

                    b.Property<int?>("WorkPlaceId")
                        .HasColumnType("int");

                    b.HasIndex("WorkPlaceId");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("EmployeeSpy.Models.Visitor", b =>
                {
                    b.HasBaseType("EmployeeSpy.Models.Person");

                    b.HasDiscriminator().HasValue("Visitor");
                });

            modelBuilder.Entity("EmployeeSpy.Models.GateKeeper", b =>
                {
                    b.HasOne("EmployeeSpy.Models.Door", "Door")
                        .WithMany()
                        .HasForeignKey("DoorId");

                    b.Navigation("Door");
                });

            modelBuilder.Entity("EmployeeSpy.Models.Door", b =>
                {
                    b.HasOne("EmployeeSpy.Models.GateKeeper", "EntranceControl")
                        .WithMany()
                        .HasForeignKey("EntranceControlId");

                    b.HasOne("EmployeeSpy.Models.GateKeeper", "ExitControl")
                        .WithMany()
                        .HasForeignKey("ExitControlId");

                    b.HasOne("EmployeeSpy.Models.Employee", "Guard")
                        .WithMany("DoorsUderControl")
                        .HasForeignKey("GuardId");

                    b.Navigation("EntranceControl");

                    b.Navigation("ExitControl");

                    b.Navigation("Guard");
                });

            modelBuilder.Entity("EmployeeSpy.Models.MovementLogRecord", b =>
                {
                    b.HasOne("EmployeeSpy.Models.Door", "PassedDoor")
                        .WithMany()
                        .HasForeignKey("PassedDoorId");

                    b.HasOne("EmployeeSpy.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.Navigation("PassedDoor");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("EmployeeSpy.Models.Employee", b =>
                {
                    b.HasOne("EmployeeSpy.Models.Room", "WorkPlace")
                        .WithMany()
                        .HasForeignKey("WorkPlaceId");

                    b.Navigation("WorkPlace");
                });

            modelBuilder.Entity("EmployeeSpy.Models.Employee", b =>
                {
                    b.Navigation("DoorsUderControl");
                });
#pragma warning restore 612, 618
        }
    }
}
