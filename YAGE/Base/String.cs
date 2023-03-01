using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YAGE.Base
{
    internal class String
    {
        private String & String.CopyFrom(string b)
{
	Debug.Assert(b != null);


	char copy = AllocateString(b);

	if (string != null)
	{
		// then deallocate current
		SystemAllocator.Free(string);
	}

    // and reassign
    string = copy;
	length = string.Length;

	return this;
    }

    //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
    //ORIGINAL LINE: String String::operator +(const char * b) const
    private static String String.operator +(string b)
    {
        Debug.Assert(b != null);

        String copy = "";

        uint curSize = (uint)string.Length;
        uint bSize = (uint)b.Length;

        uint size = curSize + bSize;

        // +1 for '\0'
        copy.string = (string)SystemAllocator.Allocate(size + 1);

        // copy from this to copy without '\0'
        //C++ TO C# CONVERTER TASK: The memory management function 'memcpy' has no equivalent in C#:
        memcpy(copy.string, string, curSize);
        // copy from b to copy with '\0'
        // starting after char with index curSize
        //C++ TO C# CONVERTER TASK: The memory management function 'memcpy' has no equivalent in C#:
        memcpy(copy.string + curSize, b, bSize + 1);

        copy.length = size;

        return copy;
    }

    //C++ TO C# CONVERTER TASK: The += operator cannot be overloaded in C#:
    private static String & String.operator += (string b)
    {

        Debug.Assert(b != null);

    uint curSize = (uint)string.Length;
    uint bSize = (uint)b.Length;

    uint size = curSize + bSize;

    // allocate for result
    // +1 for '\0'
    char[] result = (string)SystemAllocator.Allocate(size + 1);

    // copy from this
    //C++ TO C# CONVERTER TASK: The memory management function 'memcpy_s' has no equivalent in C#:
    memcpy_s(result, curSize, string, curSize);
    // copy from b from the end of this
    //C++ TO C# CONVERTER TASK: The memory management function 'memcpy_s' has no equivalent in C#:
    memcpy_s(result + curSize, bSize, b, bSize);

    // null terminated
    result[size] = '\0';

	// delete this
	SystemAllocator.Free(string);

    // reassign
    this.string = result;
	this.length = size;

	return this;
    }



    public partial class String
    {
        public String(string[] orig)
        {
            this.string = AllocateString(orig);
            this.length = string.Length;
        }
        public void Dispose()
        {
            Delete();
        }
        public string AllocateString(string[] orig)
        {
            Debug.Assert(orig != null);
            // length of string
            uint size = (uint)orig.Length;

            // allocate memory for copy, +1 for '\0'
            //C++ TO C# CONVERTER TASK: C# does not have an equivalent to pointers to value types:
            //ORIGINAL LINE: char *copy = (char*)SystemAllocator::Allocate(size + 1);
            string[] copy = SystemAllocator.Allocate(size + 1);
            // copy, +1 for '\0'
            //C++ TO C# CONVERTER TASK: The memory management function 'memcpy' has no equivalent in C#:
            Array.Copy(copy, orig, size + 1);

            return copy;
        }
        public void Delete()
        {
            if (string != null)
            {
                SystemAllocator.Free(string);
                string = null;
            }
        }
        public bool Compare(string a, string b)
        {
            return string.Compare(a, b) == 0;
        }
        public Vector3 ToVector3(string str)
        {
            const int Dim = 3;

            Vector3 result = new Vector3();
            uint index = 0;

            // create copy
            char[] temp = AllocateString(str);

            // pointer to the beginning of float to parse
            string ptr = temp;

            uint length = (uint)str.Length;

            // <= to check last symbol
            for (uint i = 0; i <= length; i++)
            {
                if (str[i] == ' ' || str[i] == '\0')
                {
                    Debug.Assert(index < Dim);

                    temp[i] = '\0';

                    // convert to float till '\0'
                    result[(int)index] = (float)Convert.ToDouble(ptr);

                    // update ptr
                    ptr = temp + i + 1;
                    index++;
                }
            }

            // delete copy
            SystemAllocator.Free(temp);

            // convert unparsed to zero
            for (uint i = index; i < Dim; i++)
            {
                result[(int)i] = 0;
            }

            return new Vector3((float)result);
        }
        public Quaternion ToQuaternion(string str)
        {
            const int Dim = 4;

            Quaternion result = new Quaternion();
            uint index = 0;

            // create copy
            char[] temp = AllocateString(str);

            // pointer to the beginning of float to parse
            string ptr = temp;

            uint length = (uint)str.Length;

            // <= to check last symbol
            for (uint i = 0; i <= length; i++)
            {
                if (str[i] == ' ' || str[i] == '\0')
                {
                    Debug.Assert(index < Dim);

                    temp[i] = '\0';

                    // convert to float till '\0'
                    result[index] = (float)Convert.ToDouble(ptr);

                    // update ptr
                    ptr = temp + i + 1;
                    index++;
                }
            }

            // delete copy
            SystemAllocator.Free(temp);

            // convert unparsed to zero
            for (uint i = index; i < Dim; i++)
            {
                if (i == 3)
                {
                    result[(int)i] = 255;

                }
                else { result[(int)i] = 0; }
            }
            return result;
            
        }
        public bool ToBool(string str)
        {
            return ToInt(str) != 0;
        }
        public int ToInt(string str)
        {
            return Convert.ToInt32(str);
        }
        public float ToFloat(string str)
        {
            return (float)Convert.ToDouble(str);
        }
        public Vector3 ToVector3()
        {
            return ToVector3(string);
        }
        public Quaternion ToQuaternion()
        {
            return ToQuaternion(string);
        }
        public Color4 ToColor4()
        {
            return ToColor4(string);
        }
        private string string;
	private uint length; // string length without '\0'
                         // memory allocated for string is (length+1)

        // Allocates new string
        //C++ TO C# CONVERTER TASK: The implementation of the following method could not be found:
        //	static string AllocateString(string orig);

        // Creates empty string ("")
        public String() : this("")
        {
        }

        // Creates copy of orig
        public String(in String orig) : this(orig.string)
        {
        }

        // Creates copy from orig
        //C++ TO C# CONVERTER TASK: The implementation of the following method could not be found:
        //	String(string orig);
        // Destructor
        //C++ TO C# CONVERTER TASK: The implementation of the following method could not be found:
        //	public void Dispose();

        public char this[int i]
        {
            get
            {
                Debug.Assert(i >= 0 && i <= (int)length); // == to allow to read '\0'
                return string[i];
            }
            set
            {
                string = StringFunctions.ChangeCharacter(string, i, value);
            }
        }

        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: inline char operator [](int i) const
        public char this[int i]
        {
            get
            {
                Debug.Assert(i >= 0 && i <= (int)length); // == to allow to read '\0'
                return string[i];
            }
        }

        /
        public static bool operator ==(String ImpliedObject, in String b)
        {
            return ImpliedObject == b.string;
        }

       
        public static bool operator ==(String ImpliedObject, string b)
        {
            return Compare(ImpliedObject.string, b);
        }

        
        public static bool operator !=(String ImpliedObject, in String b)
        {
            return !(ImpliedObject == b.string);
        }

       
        public static bool operator !=(String ImpliedObject, string b)
        {
            return !(*ImpliedObject == b);
        }

      
        public String CopyFrom(in String b)
        {
            this = b.string;
            return this;
        }

        
        public static String.implicit operator char(String ImpliedObject)
    	{
		return ImpliedObject.string;
        }

  
    public static String operator +(String ImpliedObject, in String b)
    {
        return (*ImpliedObject + b.string);
    }

    public static String operator += (in String b)
    {
        this += b.string;
        return this;
    }

    public uint Length()
    {
        return length;
    }

    // Makes string empty
    public void Clear()
    {
        this = "";
    }

   
    public bool ToBool()
    {
        return ToBool(string);
    }


    public int ToInt()
    {
        return ToInt(string);
    }

    
    public float ToFloat()
    {
        return ToFloat(string);
    }

 
    public string GetCharPtr()
    {
        return string;
    }

    // Hash function for a string
    public static uint StringHash(String toHash)
    {
        /
        byte* str = (byte)toHash.string;

        uint hash = 5381;
        int c;

        while (c = *str++)
        {
            hash = (uint)(((hash << 5) + hash) + c); // hash * 33 + c
        }

        return hash;
    }

    /
}

//Helper class added by C++ to C# Converter:

//----------------------------------------------------------------------------------------
//	Copyright © 2006 - 2023 Tangible Software Solutions, Inc.
//	This class can be used by anyone provided that the copyright notice remains intact.
//
//	This class provides the ability to replicate various classic C string functions
//	which don't have exact equivalents in the .NET Framework.
//----------------------------------------------------------------------------------------
internal static class StringFunctions
{
    //------------------------------------------------------------------------------------
    //	This method allows replacing a single character in a string, to help convert
    //	C++ code where a single character in a character array is replaced.
    //------------------------------------------------------------------------------------
    public static string ChangeCharacter(string sourceString, int charIndex, char newChar)
    {
        return (charIndex > 0 ? sourceString.Substring(0, charIndex) : "")
            + newChar.ToString() + (charIndex < sourceString.Length - 1 ? sourceString.Substring(charIndex + 1) : "");
    }

    //------------------------------------------------------------------------------------
    //	This method replicates the classic C string function 'isxdigit' (and 'iswxdigit').
    //------------------------------------------------------------------------------------
    public static bool IsXDigit(char character)
    {
        if (char.IsDigit(character))
            return true;
        else if ("ABCDEFabcdef".IndexOf(character) > -1)
            return true;
        else
            return false;
    }

    //------------------------------------------------------------------------------------
    //	This method replicates the classic C string function 'strchr' (and 'wcschr').
    //------------------------------------------------------------------------------------
    public static string StrChr(string stringToSearch, char charToFind)
    {
        int index = stringToSearch.IndexOf(charToFind);
        if (index > -1)
            return stringToSearch.Substring(index);
        else
            return null;
    }

    //------------------------------------------------------------------------------------
    //	This method replicates the classic C string function 'strrchr' (and 'wcsrchr').
    //------------------------------------------------------------------------------------
    public static string StrRChr(string stringToSearch, char charToFind)
    {
        int index = stringToSearch.LastIndexOf(charToFind);
        if (index > -1)
            return stringToSearch.Substring(index);
        else
            return null;
    }

    //------------------------------------------------------------------------------------
    //	This method replicates the classic C string function 'strstr' (and 'wcsstr').
    //------------------------------------------------------------------------------------
    public static string StrStr(string stringToSearch, string stringToFind)
    {
        int index = stringToSearch.IndexOf(stringToFind);
        if (index > -1)
            return stringToSearch.Substring(index);
        else
            return null;
    }

    //------------------------------------------------------------------------------------
    //	This method replicates the classic C string function 'strtok' (and 'wcstok').
    //	Note that the .NET string 'Split' method cannot be used to replicate 'strtok' since
    //	it doesn't allow changing the delimiters between each token retrieval.
    //------------------------------------------------------------------------------------
    private static string activeString;
    private static int activePosition;
    public static string StrTok(string stringToTokenize, string delimiters)
    {
        if (stringToTokenize != null)
        {
            activeString = stringToTokenize;
            activePosition = -1;
        }

        //the stringToTokenize was never set:
        if (activeString == null)
            return null;

        //all tokens have already been extracted:
        if (activePosition == activeString.Length)
            return null;

        //bypass delimiters:
        activePosition++;
        while (activePosition < activeString.Length && delimiters.IndexOf(activeString[activePosition]) > -1)
        {
            activePosition++;
        }

        //only delimiters were left, so return null:
        if (activePosition == activeString.Length)
            return null;

        //get starting position of string to return:
        int startingPosition = activePosition;

        //read until next delimiter:       do
        {
            activePosition++;
        } while (activePosition < activeString.Length && delimiters.IndexOf(activeString[activePosition]) == -1);

        return activeString.Substring(startingPosition, activePosition - startingPosition);
    }


}
}
