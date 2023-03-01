using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YAGE.Base
{
    internal class Random
{
        public bool GetBool()
        {
            return RandomNumbers.NextNumber() % 2 != 0;
        }
        public int GetInt(int max)
        {
            Debug.Assert(max <= Int32.MaxValue);
            return RandomNumbers.NextNumber() % (max + 1);
        }
        public int GetInt(int min, int max)
        {
            Debug.Assert(max - min < Int32.MaxValue);
            return (RandomNumbers.NextNumber() % (max + 1 - min)) + min;
        }
        public float GetFloat()
        {
            return (float)RandomNumbers.NextNumber() / (float)(Int32.MaxValue);
        }
        public float GetFloat(float max)
        {
            return (float)RandomNumbers.NextNumber() / (float)(Int32.MaxValue) * max;
        }
        public float GetFloat(float min, float max)
        {
            return ((float)RandomNumbers.NextNumber() / (float)(Int32.MaxValue) * (max - min)) + min;
        }
        public Vector3 GetOnSphere()
        {
            Vector3 result = GetInsideSphere();

            // to make unit
            float length = result.Length();
            if (length == 0F)
            {
                // there is some chance to get (0,0,0)
                // so return some other value
                return Vector3(1, 0, 0);
            }

            // inverted length
            length = 1.0f / length;

            // normalize
            for (int i = 0; i < 3; i++)
            {
                result[i] *= length;
            }

            return new Vector3((float)result);
        }
        public Vector3 GetOnSphere(float r)
        {
            return GetOnSphere() * r;
        }
        public Vector3 GetInsideSphere()
        {
            Vector3 result = new Vector3();

            for (int i = 0; i < 3; i++)
            {
                result[i] = Random.GetFloat(-1.0f, 1.0f);
            }

            return new Vector3(result);
        }
        public Vector3 GetInsideSphere(float r)
        {
            return GetInsideSphere() * r;
        }
        public Vector3 GetInsideBox(in Vector3 extent)
        {
            Vector3 result = new Vector3();

            for (int i = 0; i < 3; i++)
            {
                result[i] = extent[i] * GetFloat(-1.0f, 1.0f);
            }

            return new Vector3(result);
        }
    }

    //Helper class added by C++ to C# Converter:

    //----------------------------------------------------------------------------------------
    //	Copyright © 2006 - 2023 Tangible Software Solutions, Inc.
    //	This class can be used by anyone provided that the copyright notice remains intact.
    //
    //	This class provides the ability to replicate the behavior of the C/C++ functions for 
    //	generating random numbers, using the .NET Framework System.Random class.
    //	'rand' converts to the parameterless overload of NextNumber
    //	'random' converts to the single-parameter overload of NextNumber
    //	'randomize' converts to the parameterless overload of Seed
    //	'srand' converts to the single-parameter overload of Seed
    //----------------------------------------------------------------------------------------
    internal static class RandomNumbers
    {
        private static System.Random r;

        public static int NextNumber()
        {
            if (r == null)
                Seed();

            return r.Next();
        }

        public static int NextNumber(int ceiling)
        {
            if (r == null)
                Seed();

            return r.Next(ceiling);
        }

        public static void Seed()
        {
            r = new System.Random();
        }

        public static void Seed(int seed)
        {
            r = new System.Random(seed);
        }

    }
}
