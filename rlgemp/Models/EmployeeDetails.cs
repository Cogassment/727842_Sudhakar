using System.ComponentModel.DataAnnotations;

namespace rlgemp.Models
{
    public class EmployeeDetails
    {
       [Required (ErrorMessage = "Only Numbers Are Allowed")]
       [RegularExpression("^[0-9]{6}$", ErrorMessage = "AssociateId with maximum length 6 digits")]

        public string Associateid { get; set; }
        [StringLength(15)]
        [Required (ErrorMessage = "Only Alphabets Are Allowed")]
        [RegularExpression("^[a-zA-Z#@$!& ]*$", ErrorMessage = "Please Enter Valid Role")]

        public string Role { get; set; }
        [StringLength(15)]
        [Required (ErrorMessage = " Only Alphabets Are Allowed")]
        [RegularExpression("^[a-zA-Z#@$!& ]*$", ErrorMessage = "Please Enter Valid Associatename")]

        public string Associatename { get; set; }
        [StringLength(15)]
        [Required (ErrorMessage = "Only Alphabets Are Allowed")]
        [RegularExpression("^[a-zA-Z#@$!& ]*$", ErrorMessage = "Please Enter Valid ProjectName")]

        public string Projectname { get; set; }
        [StringLength(15)]
        [Required(ErrorMessage = "Only Alphabets Are Allowed")]
        [RegularExpression("^[a-zA-Z#@$!& ]*$", ErrorMessage = "Please Enter Valid ManagerName")]

        public string Managername { get; set; }
    }
}
