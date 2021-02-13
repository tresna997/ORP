using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Models
{
    [Table("tb_m_customer")]
    public class Customer
    {
        [Key, Required(ErrorMessage = "Tidak boleh kosong")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong"), MaxLength(50, ErrorMessage = "Maksimal 50 karakter")]
        public string Name { get; set; }
    }
}
