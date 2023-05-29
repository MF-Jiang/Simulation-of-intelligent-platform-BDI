/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 14:02:09 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 14:02:09 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back
{
    /// <summary>
    /// Represents a type of object.
    /// </summary>
    public class ObjectType
    {
        private Type type;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectType"/> class with the specified type.
        /// </summary>
        /// <param name="type">The type of the object.</param>
        public ObjectType(Type type)
        {
            this.type = type;
        }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <returns>The type of the object.</returns>
        public Type GetObjectType() { return type; }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ObjectType"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ObjectType"/>.</param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ObjectType"/>; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is ObjectType)) return false;
            var other = (ObjectType)obj;
            return type.Equals(other.type);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="ObjectType"/>.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return type.GetHashCode();
        }
    }
}
