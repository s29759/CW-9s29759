using System.ComponentModel.DataAnnotations;

namespace APBD_cw9s29759.DTOs;

public class PrescriptionCreateDto
{
    [Required]
    public PatientCreateDto Patient { get; set; } = null!;
    
    [Required]
    public PrescriptionInfoDto Prescription { get; set; } = null!;
    
    [Required]
    public ICollection<MedicamentCreateDto> Medicaments { get; set; } = null!;
}

public class PatientCreateDto
{
    [MaxLength(100)]
    [Required]
    public string FirstName { get; set; } = null!;
    
    [MaxLength(100)]
    [Required]
    public string LastName { get; set; } = null!;
    
    [Required]
    public DateTime Birthdate { get; set; }
}

public class PrescriptionInfoDto
{
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
    
    [Required]
    public int IdDoctor { get; set; }
}

public class MedicamentCreateDto
{
    [Required]
    public int IdMedicament { get; set; }
    
    [Required]
    public int Dose { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string Details { get; set; } = null!;
}