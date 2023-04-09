using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OVERTIME.MANAGER.MAIN.Models;

public partial class OvertimeManagerContext : DbContext
{
    public OvertimeManagerContext()
    {
    }

    public OvertimeManagerContext(DbContextOptions<OvertimeManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<JobPosition> JobPositions { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<OverTimeInWorkingShift> OverTimeInWorkingShifts { get; set; }

    public virtual DbSet<Overtime> Overtimes { get; set; }

    public virtual DbSet<OvertimeEmployee> OvertimeEmployees { get; set; }

    public virtual DbSet<WorkingShift> WorkingShifts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=NNHIEP\\SQLEXPRESS01;Initial Catalog=OvertimeManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF14C2E23C8");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.EmployeeCode, "IX_employee_EmployeeCode");

            entity.HasIndex(e => e.EmployeeName, "IX_employee_EmployeeName");

            entity.HasIndex(e => e.PhoneNumber, "IX_employee_PhoneNumber");

            entity.HasIndex(e => e.EmployeeCode, "IX_overtime_EmployeeCode");

            entity.HasIndex(e => e.EmployeeName, "IX_overtime_EmployeeName");

            entity.HasIndex(e => e.EmployeeCode, "UQ__Employee__1F642548DBCC0111").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ__Employee__85FB4E382DCBCF2E").IsUnique();

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.Account)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.Addr)
                .HasMaxLength(255)
                .IsUnicode(true);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.JobPositionId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("JobPositionID");
            entity.Property(e => e.JobPositionName)
                .HasMaxLength(255)
                .IsUnicode(true);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrganizationId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("OrganizationID");
            entity.Property(e => e.OrganizationName)
                .HasMaxLength(255)
                .IsUnicode(true);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Pwd)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.HasOne(d => d.JobPosition).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobPositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__JobPos__5EBF139D");

            entity.HasOne(d => d.Organization).WithMany(p => p.Employees)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__Organi__5DCAEF64");
        });

        modelBuilder.Entity<JobPosition>(entity =>
        {
            entity.HasKey(e => e.JobPositionId).HasName("PK__JobPosit__37957EFDFB944F38");

            entity.ToTable("JobPosition");

            entity.HasIndex(e => e.JobPositionCode, "UQ__JobPosit__4A98967A44BA4782").IsUnique();

            entity.Property(e => e.JobPositionId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("JobPositionID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.JobPositionCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.JobPositionName)
                .HasMaxLength(255)
                .IsUnicode(true)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("PK__Organiza__CADB0B7261F20488");

            entity.ToTable("Organization");

            entity.HasIndex(e => e.OrganizationCode, "UQ__Organiza__61B3792C264DF92C").IsUnique();

            entity.Property(e => e.OrganizationId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("OrganizationID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrganizationCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.OrganizationName)
                .HasMaxLength(255)
                .IsUnicode(true)
                .HasDefaultValueSql("('')");
        });

        modelBuilder.Entity<OverTimeInWorkingShift>(entity =>
        {
            entity.HasKey(e => e.OverTimeInWorkingShiftId).HasName("PK__OverTime__0C2979F8B3AD1FCB");

            entity.ToTable("OverTimeInWorkingShift");

            entity.Property(e => e.OverTimeInWorkingShiftId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("OverTimeInWorkingShiftID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OverTimeInWorkingShiftCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.OverTimeInWorkingShiftName)
                .HasMaxLength(255)
                .IsUnicode(true)
                .HasDefaultValueSql("('')");
        });

        modelBuilder.Entity<Overtime>(entity =>
        {
            entity.HasKey(e => e.OverTimeId).HasName("PK__Overtime__A9590B5D4247A148");

            entity.ToTable("Overtime");

            entity.Property(e => e.OverTimeId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("OverTimeID");
            entity.Property(e => e.ApplyDate).HasColumnType("datetime");
            entity.Property(e => e.ApprovalId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("ApprovalID");
            entity.Property(e => e.ApprovalName)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.BreakTimeFrom).HasColumnType("datetime");
            entity.Property(e => e.BreakTimeTo).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Dsc)
                .HasMaxLength(255)
                .IsUnicode(true);
            entity.Property(e => e.EmployeeCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.JobPositionId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("JobPositionID");
            entity.Property(e => e.JobPositionName)
                .HasMaxLength(255)
                .IsUnicode(true);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrganizationId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("OrganizationID");
            entity.Property(e => e.OrganizationName)
                .HasMaxLength(255)
                .IsUnicode(true);
            entity.Property(e => e.OverTimeEmployeeCodes).HasColumnType("text");
            entity.Property(e => e.OverTimeEmployeeIds)
                .HasColumnType("text")
                .HasColumnName("OverTimeEmployeeIDs");
            entity.Property(e => e.OverTimeEmployeeNames).HasColumnType("text");
            entity.Property(e => e.OverTimeInWorkingShiftCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.OverTimeInWorkingShiftId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("OverTimeInWorkingShiftID");
            entity.Property(e => e.OverTimeInWorkingShiftName)
                .HasMaxLength(255)
                .IsUnicode(true)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .IsUnicode(true)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.WorkingShiftCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.WorkingShiftId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("WorkingShiftID");
            entity.Property(e => e.WorkingShiftName)
                .HasMaxLength(255)
                .IsUnicode(true)
                .HasDefaultValueSql("('')");

            entity.HasOne(d => d.Approval).WithMany(p => p.OvertimeApprovals)
                .HasForeignKey(d => d.ApprovalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Overtime__Approv__0D7A0286");

            entity.HasOne(d => d.Employee).WithMany(p => p.OvertimeEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Overtime__Employ__0C85DE4D");

            entity.HasOne(d => d.JobPosition).WithMany(p => p.Overtimes)
                .HasForeignKey(d => d.JobPositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Overtime__JobPos__0F624AF8");

            entity.HasOne(d => d.Organization).WithMany(p => p.Overtimes)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Overtime__Organi__0E6E26BF");

            entity.HasOne(d => d.OverTimeInWorkingShift).WithMany(p => p.Overtimes)
                .HasForeignKey(d => d.OverTimeInWorkingShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Overtime__OverTi__114A936A");

            entity.HasOne(d => d.WorkingShift).WithMany(p => p.Overtimes)
                .HasForeignKey(d => d.WorkingShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Overtime__Workin__10566F31");
        });

        modelBuilder.Entity<OvertimeEmployee>(entity =>
        {
            entity.HasKey(e => e.OverTimeDetailId).HasName("PK__Overtime__BDB04881A74D2445");

            entity.ToTable("OvertimeEmployee");

            entity.Property(e => e.OverTimeDetailId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("OverTimeDetailID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.JobPositionId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("JobPositionID");
            entity.Property(e => e.JobPositionName)
                .HasMaxLength(255)
                .IsUnicode(true);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrganizationId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("OrganizationID");
            entity.Property(e => e.OrganizationName)
                .HasMaxLength(255)
                .IsUnicode(true);
            entity.Property(e => e.OverTimeId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("OverTimeID");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.OvertimeEmployeesNavigation)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OvertimeE__Emplo__367C1819");

            entity.HasOne(d => d.JobPosition).WithMany(p => p.OvertimeEmployees)
                .HasForeignKey(d => d.JobPositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OvertimeE__JobPo__395884C4");

            entity.HasOne(d => d.Organization).WithMany(p => p.OvertimeEmployees)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OvertimeE__Organ__3864608B");

            entity.HasOne(d => d.OverTime).WithMany(p => p.OvertimeEmployees)
                .HasForeignKey(d => d.OverTimeId)
                .HasConstraintName("FK__OvertimeE__OverT__37703C52");
        });

        modelBuilder.Entity<WorkingShift>(entity =>
        {
            entity.HasKey(e => e.WorkingShiftId).HasName("PK__WorkingS__96E6042F77AB592A");

            entity.ToTable("WorkingShift");

            entity.Property(e => e.WorkingShiftId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .IsFixedLength()
                .HasColumnName("WorkingShiftID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.WorkingShiftCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.WorkingShiftName)
                .HasMaxLength(255)
                .IsUnicode(true)
                .HasDefaultValueSql("('')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
