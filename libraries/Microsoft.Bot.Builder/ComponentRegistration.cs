﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Microsoft.Bot.Builder
{
    /// <summary>
    /// ComponentRegistration is a signature class for discovering assets from components. 
    /// </summary>
    /// <remarks>
    /// To make your components available to the system you derive from ComponentRegistration and implement appropriate 
    /// interfaces which register functionality.  These components then are consumed in appropriate places by the 
    /// systems that need them. For example, to add declarative types to the system you simply add class that 
    /// implements IComponentDeclarativeTypes.
    /// <code>
    /// public class MyComponentRegistration : IComponentDeclarativeTypes
    /// {
    ///     public IEnumerable&lt;DeclarativeType&gt;()
    ///     {  
    ///          yield return new DeclarativeType&lt;MyType&gt;("Contoso.MyType");
    ///          ...
    ///     }
    /// }
    /// startup.cs:
    ///      ComponentRegistration.Add(new DeclarativeComponentRegistration()); 
    ///      ComponentRegistration.Add(new MyComponentRegistration());
    /// </code>
    /// </remarks>
#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable (we can't change this without breaking binary compat)
    public class ComponentRegistration
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
    {
        private static readonly ConcurrentDictionary<Type, ComponentRegistration> _components = new ConcurrentDictionary<Type, ComponentRegistration>();

        /// <summary>
        /// Gets list of all ComponentRegistration objects registered.
        /// </summary>
        /// <value>
        /// A numeration of ComponentRegistration objects.
        /// </value>
        public static IEnumerable<object> Components => _components.Values;

        /// <summary>
        /// Add a component which implements registration methods.
        /// </summary>
        /// <remarks>Only one instance per type is allowed for components.</remarks>
        /// <param name="componentRegistration">componentRegistration.</param>
        public static void Add(ComponentRegistration componentRegistration)
        {
            _components[componentRegistration.GetType()] = componentRegistration;
        }
    }
}
