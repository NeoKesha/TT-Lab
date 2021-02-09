using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.ViewModels
{
    public abstract class SavebleViewModel : ObservableObject
    {
        public abstract void Save(object? o = null);
    }
}
