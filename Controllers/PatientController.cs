using APBD_cw9s29759.DTOs;
using APBD_cw9s29759.Exceptions;
using APBD_cw9s29759.Service;
using Microsoft.AspNetCore.Mvc;

namespace APBD_cw9s29759.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController(IDbService service) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionCreateDto prescriptionData)
    {
        try
        {
            var prescription = await service.CreatePrescriptionAsync(prescriptionData);
            return Ok(prescription);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("{patientId}")]
    public async Task<IActionResult> GetPatientsDetails([FromRoute] int patientId)
    {
        try
        {
            return Ok(await service.GetPatientAsync(patientId));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
       
    }
    
    
    
    
    
}