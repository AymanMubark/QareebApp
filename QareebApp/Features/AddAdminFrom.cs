using Microsoft.AspNetCore.Components.Forms;
using Qareeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QareebApp.Features
{
    public class AddAdminFrom 
    {
        public string Name { get; set; }
        public IBrowserFile Image { get; set; }
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
