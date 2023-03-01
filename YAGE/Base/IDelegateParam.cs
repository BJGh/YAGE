using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAGE.Base
{
    public interface IDelegateParam<D>
{
       public void Invoke(D data);
}
}
