using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Web.InputModels.AddDelete
{
    public class DeleteGameInputModel
    {
        [Required]
        [Display(Name = "Game name")]
        public string Name { get; set; }
    }
}
