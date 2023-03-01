using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using YAGE.Base;
namespace YAGE.Base
{
    public class Delegate<T> : IDelegate
{
        public delegate void TFunction();
        private TFunction tfunc;
        private T @object;
        public Delegate(T @object, TFunction objFunction)
        {
            this.@object = @object;
            this.tfunc = objFunction;
        }
        public void Invoke()
        {
            tfunc();
        }
        
}
  public class DelegateParam<T,D> : IDelegateParam<D>
    {
        public delegate void TFunction(D data);
        private TFunction tfunc;
        private T @object;
        public DelegateParam(T @object, TFunction objFunction)
        {
            this.@object = @object;
            this.tfunc = objFunction;
        }
        public void Invoke(D data)
        {
            tfunc(data);
        }
    }
}
