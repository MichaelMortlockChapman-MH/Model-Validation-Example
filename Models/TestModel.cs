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

        [Required]
        public string AString { get; set; }
        [Required]
        public string AString2 { get; set; }
        [Required]
        public DateTime? ADate { get; set; } = null;

        public List<bool> CheckboxList { get; set; } = new();

        [Required(ErrorMessage = "PICK 1")]
        public int? RadioListSelected { get; set; } = null;
        public List<int> RadioList { get; set; } = new();

        [AtLeast1Selected("CheckboxList", ErrorMessage = "PICK 1")]
        public string? CheckboxListDummy { get; set; }

        [AtLeast1Selected("Applicants", ErrorMessage = "PICK 1")]
        [TotalSelectedIs100(".Applicants-group", ".Applicants-num-group", "Applicants", true, ErrorMessage = "MUST TOTAL 100")]
        public string? Dummy {get; set;}
    }

    public class ApplicantModel
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; } = false;
        public decimal? OwnedPercentage { get; set; } = 0M;
    }
}
