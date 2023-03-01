using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAGE.Base
{
    internal class EventParam<D>
{
        // Stores all subscribed functions
        // Must be allocated before adding
        // Will be deleted on event destruction
        private DynamicArray<IDelegateParam<D>> subscribers = new DynamicArray<IDelegateParam<D>>();

        // Empty constructor
        public EventParam()
        {
        }

        // Default destructor
        ~EventParam()
        {
            Delete();
        }

        // Allocate memory for arrays
        public void Init()
        {
            subscribers.Init(8);
        }

        // Create delegate for function in object and subscribe it to this event
        //C++ TO C# CONVERTER WARNING: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
        //ORIGINAL LINE: template <class T>
        private delegate void objectFunctionDelegate(D data);

        public void Subscribe<T>(T @object, objectFunctionDelegate objectFunction)
        {
            IDelegateParam<D> newDelegate = new DelegateParam<T, D>(@object, objectFunction);
            subscribers.Push(newDelegate);
        }

        // Calls all subscribers
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: inline void operator ()(const D * data) const
        public void functorMethod(D data)
        {
            int count = subscribers.GetSize();
            for (int i = 0; i < count; i++)
            {
                subscribers[i].Invoke(data);
            }
        }

        // Get all subscribers in this event
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: inline const DynamicArray<IDelegateParam<D>*>& GetSubscribers() const
        public DynamicArray<IDelegateParam<D>> GetSubscribers()
        {
            return new DynamicArray<IDelegateParam<D>>(subscribers);
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
