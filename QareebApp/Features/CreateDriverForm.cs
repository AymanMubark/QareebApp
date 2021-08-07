using Microsoft.AspNetCore.Components.Forms;
using Qareeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QareebApp.Features
{
    public class CreateDriverForm : AddDriverRequest
    {
        public new IBrowserFile Image { get; set; }
    }
}
