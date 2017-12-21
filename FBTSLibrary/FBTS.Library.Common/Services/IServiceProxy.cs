using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Library.Common.Services
{
    public interface IServiceProxy
    {
        string GetBindingName(BindingTypes binding);
        BindingTypes DefaultBinding { get; }
    }
}
