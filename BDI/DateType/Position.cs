/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 14:02:15 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 14:02:15 
 */
using System.Collections;

namespace Back
{
    /// <summary>
    /// Represents a 3D position with x, y and z coordinates.
    /// </summary>
    public struct Position
    {
        private double x, y, z;

        /// <summary>
        /// Initializes a new instance of the <see cref="Position"/> struct with the specified x, y and z coordinates.
        /// </summary>
        /// <param name="x">The x coordinate of the position.</param>
        /// <param name="y">The y coordinate of the position.</param>
        /// <param name="z">The z coordinate of the position.</param>
        public Position(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Sets the x coordinate of the position.
        /// </summary>
        /// <param name="x">The new value of the x coordinate.</param>
        public void SetX(double x)
        {
            this.x = x;
        }

        /// <summary>
        /// Sets the y coordinate of the position.
        /// </summary>
        /// <param name="y">The new value of the y coordinate.</param>
        public void SetY(double y)
        {
            this.y = y;
        }

        /// <summary>
        /// Sets the z coordinate of the position.
        /// </summary>
        /// <param name="z">The new value of the z coordinate.</param>
        public void SetZ(double z)
        {
            this.z = z;
        }

        /// <summary>
        /// Gets the x coordinate of the position.
        /// </summary>
        /// <returns>The value of the x coordinate.</returns>
        public double GetX()
        {
            return this.x;
        }

        /// <summary>
        /// Gets the y coordinate of the position.
        /// </summary>
        /// <returns>The value of the y coordinate.</returns>
        public double GetY()
        {
            return this.y;
        }

        /// <summary>
        /// Gets the z coordinate of the position.
        /// </summary>
        /// <returns>The value of the z coordinate.</returns>
        public double GetZ()
        {
            return this.z;
        }

        /// <summary>
        /// Returns a string that represents the current position.
        /// </summary>
        /// <returns>A string that represents the current position.</returns>
        public override string ToString()
        {
            string res = "(";
            res += x;
            res += ", ";
            res += y;
            res += ", ";
            res += z;
            res += ")";
            return res;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current position.
        /// </summary>
        /// <param name="obj">The object to compare with the current position.</param>
        /// <returns>True if the specified object is equal to the current position; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Position))
            {
                return false;
            }
            var pos_new = (Position)obj;
            if (pos_new.GetX() == x && pos_new.GetY() == y && pos_new.GetZ() == z) return true;
            return false;
        }

        /// <summary>
        /// Returns a hash code for the current position.
        /// </summary>
        /// <returns>A hash code for the current position.</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash + hash * 23 + x.GetHashCode();
            hash = hash + hash * 23 + y.GetHashCode();
            hash = hash + hash * 23 + z.GetHashCode();
            return hash;
        }
    }
}
