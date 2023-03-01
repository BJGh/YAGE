using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using glfw;

namespace YAGE.Base
{
    internal class Time
    {
        private static float lastFrame = 0F;
        private static float deltaTime = 0F;
        private static float fixedDeltaTime = 1.0f / 60.0f;
        private static float fdtAccum = 0F;

        public static void Init()
        {
            lastFrame = GetTime();
        }

        // Must be called only on frame change
        public static void Calculate()
        {
            float currentFrame = GetTime();
            deltaTime = currentFrame - lastFrame;
            lastFrame = currentFrame;

            fdtAccum += deltaTime;
        }

        public static bool ToFixedUpdate()
        {
            if (fdtAccum >= fixedDeltaTime)
            {
                fdtAccum -= fixedDeltaTime;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Current time
        public static float GetTime()
        {
            return (float)glfwGetTime();
        }

        // Delta time (frame update)
        public static float GetDeltaTime()
        {
            return deltaTime;
        }

        // Fixed delta time (physics update)
        public static float GetFixedDeltaTime()
        {
            return fixedDeltaTime;
        }

    }
}
