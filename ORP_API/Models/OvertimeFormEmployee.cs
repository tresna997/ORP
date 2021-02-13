using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Models
{
    [Table("tb_m_overtime_form_employee")]
    public class OvertimeFormEmployee
    {
        [Key, Required(ErrorMessage ="Data tidak boleh kosong")]
        public int Id { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
