using System.ComponentModel.DataAnnotations;

namespace Dev2Tools.API.Models
{
    public class EmployerCriteria
    {
        [Required(ErrorMessage = "EsdNum is required.")]
        public string EsdNum { get; set; } = "000006218003";
    }
}