using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException
{
    // Sealed makes it so this class cannot be inherited from, making it teh final stage in the inheritance hierarchy.
    // the standard is to have "Attribute" at the end of the class name, which can be ignored
    // when adding it to anything: in this example, it would just be used as [Tag]
    /// <summary>
    /// This can be added to any string to make it render as the tag dropdown in the inspector.
    /// </summary>
    public sealed class TagAttribute : PropertyAttribute
    {
        
    }
}