namespace APBD_cw9s29759.DTOs;

public class PatientGetDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime Birthdate { get; set; }
    public ICollection<PrescriptionGetDto> Prescriptions { get; set; } = null!;
}

public class PrescriptionGetDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorGetDto Doctor { get; set; } = null!;
    public ICollection<MedicamentGetDto> Medicaments { get; set; } = null!;
}

public class DoctorGetDto
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}


public class MedicamentGetDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; } = null!;
    public int Dose { get; set; }
    public string Description { get; set; } = null!;
}