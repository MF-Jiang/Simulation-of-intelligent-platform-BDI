/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:50:55 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:50:55 
 */

using System;
using System.Collections.Generic;
using System.Numerics;


namespace Back
{
    /// <summary>
    /// An abstract class representing an action.
    /// </summary>
    public abstract class Action
    {
        private string name;
        protected List<Formula> preConditions;
        protected List<Formula> postConditions;
        protected List<Term> parameters;

        /// <summary>
        /// Constructs a new Action with the given name and parameters.
        /// </summary>
        /// <param name="name">The name of the Action.</param>
        /// <param name="parameters">The parameters of the Action.</param>
        public Action(string name, List<Term> parameters)
        {
            this.name = name;
            this.parameters = parameters;
            preConditions = new List<Formula>();
            postConditions = new List<Formula>();
        }

        /// <summary>
        /// Gets the name of the Action.
        /// </summary>
        /// <returns>The name of the Action.</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Gets the pre-conditions of the Action.
        /// </summary>
        /// <returns>The pre-conditions of the Action.</returns>
        public List<Formula> GetPreConditions()
        {
            return preConditions;
        }

        /// <summary>
        /// Gets the post-conditions of the Action.
        /// </summary>
        /// <returns>The post-conditions of the Action.</returns>
        public List<Formula> GetPostConditions()
        {
            return postConditions;
        }

        /// <summary>
        /// Gets the parameters of the Action.
        /// </summary>
        /// <returns>The parameters of the Action.</returns>
        public List<Term> GetParameters()
        {
            return parameters;
        }

        /// <summary>
        /// Returns a string representation of the Action.
        /// </summary>
        /// <returns>A string representation of the Action.</returns>
        public override string ToString()
        {
            string res = name + "(";
            for (int i = 0; i < parameters.Count - 1; i++)
            {
                res += parameters[i].GetValue();
                res += ", ";
            }
            res += parameters[parameters.Count - 1].GetValue();
            res += ")";
            return res;
        }

        /// <summary>
        /// Updates the pre-conditions and post-conditions of the Action based on the current state of the system.
        /// </summary>
        public virtual void UpdateData() { }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Action))
            {
                return false;
            }

            var other = (Action)obj;
            if (other.name != name) return false;
            if (preConditions.Count != other.preConditions.Count) return false;
            else
            {
                for (int i = 0; i < preConditions.Count; i++)
                {
                    if (!preConditions[i].Equals(other.preConditions[i])) return false;
                }
            }
            if (postConditions.Count != other.postConditions.Count) return false;
            else
            {
                for (int i = 0; i < postConditions.Count; i++)
                {
                    if (!postConditions[i].Equals(other.postConditions[i])) return false;
                }
            }
            if (parameters.Count != other.parameters.Count) return false;
            else
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    if (!parameters[i].Equals(other.parameters[i])) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a hash code for this Action object.
        /// </summary>
        /// <returns>A hash code for this Action object.</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash + hash * 23 + name.GetHashCode();
            foreach (var item in preConditions)
            {
                hash = hash * 23 + item.GetHashCode();
            }
            foreach (var item in postConditions)
            {
                hash = hash * 23 + item.GetHashCode();
            }
            foreach (var item in parameters)
            {
                hash = hash * 23 + item.GetHashCode();
            }
            return hash;
        }
    }

}