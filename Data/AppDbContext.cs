using APBD_cw9s29759.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_cw9s29759.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Medicament> Medicaments { get; set; } = null!;
    public DbSet<Prescription> Prescriptions { get; set; } = null!;
    
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var doctor = new Doctor
        {
            IdDoctor = 1,
            FirstName = "Jan",
            LastName = "Kowalski",
            Email = "jan.kowalski@gmail.com",
        };

        var patient = new Patient
        {
            IdPatient = 1,
            FirstName = "Ola",
            LastName = "Nowak",
            Birthdate = new DateTime(1985, 3, 15)
        };

        var medicament = new Medicament
        {
            IdMedicament = 1,
            Name = "Ibuprom",
            Description = "Opis leku",
            Type = "Przeciwbólowy"
        };

        var prescription = new Prescription
        {
            IdPrescription = 1,
            Date = new DateTime(2025, 1, 15),
            DueDate = new DateTime(2027, 1, 15),
            IdPatient = 1,
            IdDoctor = 1
        };

        var prescriptionMedicament = new PrescriptionMedicament
        {
            IdMedicament = 1,
            IdPrescription = 1,
            Dose = 7,
            Details = "Jedna dawka dziennie"
        };

        modelBuilder.Entity<Doctor>().HasData(doctor);
        modelBuilder.Entity<Patient>().HasData(patient);
        modelBuilder.Entity<Medicament>().HasData(medicament);
        modelBuilder.Entity<Prescription>().HasData(prescription);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionMedicament);
    }
    
}