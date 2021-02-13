using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Models
{
    [Table("tb_m_role")]
    public class Role
    {
        [Key, Required(ErrorMessage = "Tidak boleh kosong")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong"), MaxLength(20, ErrorMessage = "Maksimal 20 karakter")]
        public string Name { get; set; }
    }
}
