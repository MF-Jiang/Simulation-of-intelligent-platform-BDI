/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:51:48 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:51:48 
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace Back
{
    /// <summary>
    /// Represents an abstract class for defining goals in the system.
    /// </summary>
    public abstract class Goal
    {
        private string name;
        protected List<Formula> postConditions;
        protected List<Term> parameters;

        /// <summary>
        /// Creates a new instance of a Goal object with the given name and parameters.
        /// </summary>
        /// <param name="name">The name of the goal.</param>
        /// <param name="parameters">The parameters associated with the goal.</param>
        public Goal(string name, List<Term> parameters)
        {
            this.name = name;
            this.parameters = parameters;
            postConditions = new List<Formula>();
        }

        /// <summary>
        /// Gets the plans associated with the goal.
        /// </summary>
        /// <returns>The list of plans associated with the goal.</returns>
        public abstract List<Plan> GetPlans();

        /// <summary>
        /// Gets the post-conditions associated with the goal.
        /// </summary>
        /// <returns>The list of post-conditions associated with the goal.</returns>
        public List<Formula> GetPostConditions()
        {
            return postConditions;
        }

        /// <summary>
        /// Gets the name of the goal.
        /// </summary>
        /// <returns>The name of the goal.</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Gets a string representation of the goal object.
        /// </summary>
        /// <returns>A string representation of the goal object.</returns>
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
        /// Gets the parameters associated with the goal.
        /// </summary>
        /// <returns>The list of parameters associated with the goal.</returns>
        public List<Term> GetParameters()
        {
            return parameters;
        }

        /// <summary>
        /// Checks whether two Goal objects are equal.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Goal))
            {
                return false;
            }

            var other = (Goal)obj;
            if (!other.name.Equals(name)) return false;
            List<Term> inputParameters = other.GetParameters();
            if (inputParameters.Count != parameters.Count) return false;
            for (int i = 0; i < parameters.Count; i++)
            {
                if (!inputParameters[i].GetName().Equals(parameters[i].GetName())) return false;
                if (!inputParameters[i].GetValue().Equals(parameters[i].GetValue())) return false;
            }
            return true;
        }

        /// <summary>
        /// Returns the hash code of the goal object.
        /// </summary>
        /// <returns>The hash code of the goal object.</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + name.GetHashCode();
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