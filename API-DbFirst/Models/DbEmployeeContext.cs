using Microsoft.EntityFrameworkCore;

namespace API_DbFirst.Models;

public partial class DbEmployeeContext : DbContext
{
    public DbEmployeeContext()
    {
    }

    public DbEmployeeContext(DbContextOptions<DbEmployeeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbMAccount> TbMAccounts { get; set; }

    public virtual DbSet<TbMEducation> TbMEducations { get; set; }

    public virtual DbSet<TbMEmployee> TbMEmployees { get; set; }

    public virtual DbSet<TbMRole> TbMRoles { get; set; }

    public virtual DbSet<TbMUniversity> TbMUniversities { get; set; }

    public virtual DbSet<TbTrAccountRole> TbTrAccountRoles { get; set; }

    public virtual DbSet<TbTrProfiling> TbTrProfilings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbMAccount>(entity =>
        {
            entity.HasKey(e => e.EmployeeNik);

            entity.ToTable("tb_m_accounts");

            entity.Property(e => e.EmployeeNik)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("employee_nik");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");

            entity.HasOne(d => d.EmployeeNikNavigation).WithOne(p => p.TbMAccount).HasForeignKey<TbMAccount>(d => d.EmployeeNik);
        });

        modelBuilder.Entity<TbMEducation>(entity =>
        {
            entity.ToTable("tb_m_educations");

            entity.HasIndex(e => e.UniversityId, "IX_tb_m_educations_university_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Degree)
                .HasMaxLength(10)
                .HasColumnName("degree");
            entity.Property(e => e.Gpa)
                .HasMaxLength(5)
                .HasColumnName("gpa");
            entity.Property(e => e.Major)
                .HasMaxLength(100)
                .HasColumnName("major");
            entity.Property(e => e.UniversityId).HasColumnName("university_id");

            entity.HasOne(d => d.University).WithMany(p => p.TbMEducations).HasForeignKey(d => d.UniversityId);
        });

        modelBuilder.Entity<TbMEmployee>(entity =>
        {
            entity.HasKey(e => e.Nik);

            entity.ToTable("tb_m_employees");

            entity.HasIndex(e => new { e.Email, e.PhoneNumber }, "IX_tb_m_employees_email_phone_number")
                .IsUnique()
                .HasFilter("([phone_number] IS NOT NULL)");

            entity.Property(e => e.Nik)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("nik");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.HiringDate).HasColumnName("hiring_date");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<TbMRole>(entity =>
        {
            entity.ToTable("tb_m_roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TbMUniversity>(entity =>
        {
            entity.ToTable("tb_m_universities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TbTrAccountRole>(entity =>
        {
            entity.ToTable("tb_tr_account_roles");

            entity.HasIndex(e => e.AccountNik, "IX_tb_tr_account_roles_account_nik");

            entity.HasIndex(e => e.RoleId, "IX_tb_tr_account_roles_role_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountNik)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("account_nik");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.AccountNikNavigation).WithMany(p => p.TbTrAccountRoles).HasForeignKey(d => d.AccountNik);

            entity.HasOne(d => d.Role).WithMany(p => p.TbTrAccountRoles).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<TbTrProfiling>(entity =>
        {
            entity.ToTable("tb_tr_profilings");

            entity.HasIndex(e => e.EducationId, "IX_tb_tr_profilings_education_id");

            entity.HasIndex(e => e.EmployeeNik, "IX_tb_tr_profilings_employee_nik");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EducationId).HasColumnName("education_id");
            entity.Property(e => e.EmployeeNik)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("employee_nik");

            entity.HasOne(d => d.Education).WithMany(p => p.TbTrProfilings).HasForeignKey(d => d.EducationId);

            entity.HasOne(d => d.EmployeeNikNavigation).WithMany(p => p.TbTrProfilings).HasForeignKey(d => d.EmployeeNik);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
