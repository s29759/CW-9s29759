using APBD_cw9s29759.Data;
using APBD_cw9s29759.DTOs;
using APBD_cw9s29759.Exceptions;
using APBD_cw9s29759.Models;
using Microsoft.EntityFrameworkCore;
namespace APBD_cw9s29759.Service;

public interface IDbService
{
    public Task<PrescriptionGetDto> CreatePrescriptionAsync(PrescriptionCreateDto prescription);
    public Task<PatientGetDto> GetPatientAsync(int patientId);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<PrescriptionGetDto> CreatePrescriptionAsync(PrescriptionCreateDto prescriptionData)
    {
     
        if (prescriptionData.Prescription.DueDate < prescriptionData.Prescription.Date)
        {
            throw new ArgumentException("DueDate musi być większa lub równa Date");
        }
        
        if (prescriptionData.Medicaments.Count > 10)
        {
            throw new ArgumentException("Recepta nie może mieć więcej niż 10 leków");
        }
        
        
        var doctor = await data.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == prescriptionData.Prescription.IdDoctor);
        if (doctor is null)
        {
            throw new NotFoundException($"Doktor o id: {prescriptionData.Prescription.IdDoctor} nie istnieje");
        }

       
        var medicamentIds = prescriptionData.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMedicaments = await data.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .ToListAsync();

        if (existingMedicaments.Count != medicamentIds.Count)
        {
            var missingIds = medicamentIds.Except(existingMedicaments.Select(m => m.IdMedicament)).ToList();
            throw new NotFoundException($"Leki o id: {string.Join(", ", missingIds)} nie istnieją");
        }

        
        var existingPatient = await data.Patients.FirstOrDefaultAsync(p =>
            p.FirstName == prescriptionData.Patient.FirstName &&
            p.LastName == prescriptionData.Patient.LastName &&
            p.Birthdate == prescriptionData.Patient.Birthdate);

        Patient patient;
        if (existingPatient is null)
        {
            patient = new Patient
            {
                FirstName = prescriptionData.Patient.FirstName,
                LastName = prescriptionData.Patient.LastName,
                Birthdate = prescriptionData.Patient.Birthdate
            };
            await data.Patients.AddAsync(patient);
            await data.SaveChangesAsync();
        }
        else
        {
            patient = existingPatient;
        }


        var prescription = new Prescription
        {
            Date = prescriptionData.Prescription.Date,
            DueDate = prescriptionData.Prescription.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = prescriptionData.Prescription.IdDoctor
        };
        await data.Prescriptions.AddAsync(prescription);
        await data.SaveChangesAsync();
        foreach (var medicamentData in prescriptionData.Medicaments)
        {
            var prescriptionMedicament = new PrescriptionMedicament
            {
                IdPrescription = prescription.IdPrescription,
                IdMedicament = medicamentData.IdMedicament,
                Dose = medicamentData.Dose,
                Details = medicamentData.Details
            };
            await data.PrescriptionMedicaments.AddAsync(prescriptionMedicament);
        }
        await data.SaveChangesAsync();

        return new PrescriptionGetDto
        {
            IdPrescription = prescription.IdPrescription,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            Doctor = new DoctorGetDto
            {
                IdDoctor = doctor.IdDoctor,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName
            },
            Medicaments = prescriptionData.Medicaments.Select(m =>
            {
                var medicament = existingMedicaments.First(em => em.IdMedicament == m.IdMedicament);
                return new MedicamentGetDto
                {
                    IdMedicament = medicament.IdMedicament,
                    Name = medicament.Name,
                    Dose = m.Dose,
                    Description = medicament.Description
                };
            }).ToList()
        };
    }

    public async Task<PatientGetDto> GetPatientAsync(int patientId)
    {
        var result = await data.Patients.Select (p => new PatientGetDto
        {
            IdPatient = p.IdPatient,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Birthdate = p.Birthdate,
            Prescriptions = p.Prescriptions.OrderBy(pr => pr.DueDate).Select(pr => new PrescriptionGetDto
            {
                IdPrescription = pr.IdPrescription,
                Date = pr.Date,
                DueDate = pr.DueDate,
                Doctor = new DoctorGetDto
                {
                    IdDoctor = pr.Doctor.IdDoctor,
                    FirstName = pr.Doctor.FirstName,
                    LastName = pr.Doctor.LastName,
                },
                Medicaments = pr.PrescriptionMedicaments.Select(pm => new MedicamentGetDto
                {
                    IdMedicament = pm.Medicament.IdMedicament,
                    Name = pm.Medicament.Name,
                    Dose = pm.Dose,
                    Description = pm.Medicament.Description
                }).ToList()
            }).ToList()
        }).FirstOrDefaultAsync(p => p.IdPatient == patientId);
        
        return result ?? throw new NotFoundException($"Pacjent o id: {patientId} nie istnieje");
    }
}