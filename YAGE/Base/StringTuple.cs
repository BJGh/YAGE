using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAGE.Base
{
    internal class StringTuple
{
        private String left = "";
        private String right = "";

        // Default constructor
        public StringTuple()
        {
        }

        public StringTuple(string left, string right)
        {
            Set(left, right);
        }

        public StringTuple(in StringTuple source)
        {
            Set(source.left, source.right);
        }

        //C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
        //ORIGINAL LINE: StringTuple &operator =(const StringTuple &source)
        public StringTuple CopyFrom(in StringTuple source)
        {
            Set(source.left, source.right);
            return this;
        }

        public static bool operator ==(StringTuple ImpliedObject, in StringTuple source)
        {
            return ImpliedObject.left == source.left && ImpliedObject.right == source.right;
        }

        public static bool operator !=(StringTuple ImpliedObject, in StringTuple source)
        {
            return ImpliedObject.left != source.left && ImpliedObject.right == source.right;
        }


        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: const String &Left() const
        public String Left()
        {
            return left;
        }

        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: const String &Right() const
        public String Right()
        {
            return right;
        }

        public void Set(string left, string right)
        {
            SetLeft(left);
            SetRight(right);
        }

        public void SetLeft(string left)
        {
            this.left = left;
        }

        public void SetRight(string right)
        {
            this.right = right;
        }

        public void Delete()
        {
            left.Delete();
            right.Delete();
        }

    }
}
