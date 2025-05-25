using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace APBD_cw9s29759.Models;

[Table("Prescription_Medicament")]
[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
public class PrescriptionMedicament
{
    [Column("IdMedicament")]
    public int IdMedicament { get; set; }
    
    [Column("IdPrescription")]
    public int IdPrescription { get; set; }
    
    public int Dose { get; set; }
    
    [Column(TypeName = "nvarchar(100)")]
    public string Details { get; set; } = null!;
    
    [ForeignKey(nameof(IdMedicament))]
    public virtual Medicament Medicament { get; set; } = null!;
    
    [ForeignKey(nameof(IdPrescription))]
    public virtual Prescription Prescription { get; set; } = null!;
}