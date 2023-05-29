/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 14:02:01 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 14:02:01 
 */

namespace Back
{
    /// <summary>
    /// Represents a term, which has a name and an optional value.
    /// </summary>
    public class Term
    {
        protected string name; 
        protected object value = null; 

        /// <summary>
        /// Initializes a new instance of the <see cref="Term"/> class with a name and a value.
        /// </summary>
        /// <param name="name">The name of the term.</param>
        /// <param name="value">The value of the term.</param>
        public Term(string name, object value)
        {
            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Term"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the term.</param>
        public Term(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Gets the name of the term.
        /// </summary>
        /// <returns>The name of the term.</returns>
        public string GetName() 
        {
            return name;
        }

        /// <summary>
        /// Gets the value of the term.
        /// </summary>
        /// <returns>The value of the term.</returns>
        public object GetValue()
        {
            if (!(value == null)) return value;
            return null;
        }

        /// <summary>
        /// Sets the value of the term.
        /// </summary>
        /// <param name="value">The new value of the term.</param>
        public virtual void SetValue(object value)
        {
            this.value = value;
        }

        /// <summary>
        /// Determines whether the term is ground, meaning it has a value.
        /// </summary>
        /// <returns>True if the term has a value; otherwise, false.</returns>
        public bool IsGround()
        {
            return value != null;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Term))
            {
                return false;
            }
            var other = (Term)obj;
            //if(!name.Equals(other.name)) return false;
            if (value != null)
            {
                if (other.value == null) return false;
                return value.Equals(other.value);
            }
            else
            {
                return other.value == null;
            }
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + name.GetHashCode();
            hash = hash * 23 + value.GetHashCode();
            return hash;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            if (value == null) return name;
            else return value.ToString();
        }
    }
}