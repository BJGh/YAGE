using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAGE.Base;

namespace YAGE.Base
{
    public class Event
    {
        // Stores all subscribed functions
        // Must be allocated before adding
        // Will be deleted on event destruction
        private DynamicArray<IDelegate> subscribers = new DynamicArray<IDelegate>();

        // Empty constructor
        public Event()
        {
        }

        // Default destructor
        ~Event()
        {
            Delete();
        }

        // Allocate memory for arrays
        public void Init()
        {
            subscribers.Init(8);
        }

        // Create delegate for static function and subscribe it to this event
        private delegate void staticFunctionDelegate();

        //C++ TO C# CONVERTER TASK: The += operator cannot be overloaded in C#:
        public static void operator +(staticFunctionDelegate staticFunction)
        {
            Subscribe(staticFunction);
        }

        // Create delegate for static function and subscribe it to this event
        public void Subscribe(staticFunctionDelegate staticFunction)
        {
            IDelegate newDelegate = new DelegateStatic(staticFunction);
            subscribers.Push(newDelegate);
        }

        // Create delegate for function in object and subscribe it to this event
        //C++ TO C# CONVERTER WARNING: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
        //ORIGINAL LINE: template <class T>
        private delegate void objectFunctionDelegate();

        public void Subscribe<T>(T @object, objectFunctionDelegate objectFunction)
        {
            IDelegate newDelegate = new Delegate<T>(@object, objectFunction);
            subscribers.Push(newDelegate);
        }

        // Calls all subscribers
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: inline void operator ()() const
        public void functorMethod()
        {
            int count = subscribers.GetSize();
            for (int i = 0; i < count; i++)
            {
                subscribers[i].Invoke();
            }
        }

        // Get all subscribers in this event
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: inline const DynamicArray<IDelegate*>& GetSubscribers() const
        public DynamicArray<IDelegate> GetSubscribers()
        {
            return new DynamicArray<IDelegate>(subscribers);
        }


        // Clear subscribers
        public void Clear()
        {
            for (int i = 0; i < subscribers.GetSize(); i++)
            {
                subscribers[i] = null;
            }

            subscribers.Clear();
        }

        // Deallocate
        public void Delete()
        {
            // clear data
            Clear();
            // delete array
            subscribers.Delete();
        }

    }
}