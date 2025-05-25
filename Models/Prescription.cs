using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace APBD_cw9s29759.Models;


[Table("Prescription")]
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime Date { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime DueDate { get; set; }
    
    [Column("IdPatient")]
    public int IdPatient { get; set; }
    
    [Column("IdDoctor")]
    public int IdDoctor { get; set; }
    
    [ForeignKey(nameof(IdPatient))]
    public virtual Patient Patient { get; set; } = null!;
    
    [ForeignKey(nameof(IdDoctor))]
    public virtual Doctor Doctor { get; set; } = null!;
    
    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = null!;
}