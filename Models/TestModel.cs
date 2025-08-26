using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class TestModel
    {
        [ContainZ]
        public string? dumbstring { get; set; }
        public List<ApplicantModel> Applicants { get; set; }
        [Required]
        [Range(1, 1000000000)]
        public decimal Num { get; set; }

        [AtLeast1Selected("Applicants", ErrorMessage = "PICK 1")]
        [TotalSelectedIs100(".applicant-group", ".applicant-num-group", "Applicants", true, ErrorMessage = "MUST TOTAL 100")]
        public string? Dummy {get; set;}
    }

    public class ApplicantModel
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; } = false;
        public decimal? OwnedPercentage { get; set; } = 0M;
    }
}
