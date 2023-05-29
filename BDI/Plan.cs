/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:51:57 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:51:57 
 */
using System.Collections.Generic;
using System.Numerics;


namespace Back
{
    /// <summary>
    /// Represents a plan in the system.
    /// </summary>
    public class Plan
    {
        private string name;
        protected List<Formula> preConditions;
        protected List<Action> actions;
        protected List<Term> parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="Plan"/> class with the given name and parameters.
        /// </summary>
        /// <param name="name">The name of the plan.</param>
        /// <param name="parameters">The parameters of the plan.</param>
        public Plan(string name, List<Term> parameters)
        {
            this.name = name;
            this.parameters = parameters;
            preConditions = new List<Formula>();
            actions = new List<Action>();
        }

        /// <summary>
        /// Gets the name of the plan.
        /// </summary>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Gets the preconditions of the plan.
        /// </summary>
        public List<Formula> GetPreConditions()
        {
            if (preConditions == null) return new List<Formula> { };
            return preConditions;
        }

        /// <summary>
        /// Gets the actions of the plan.
        /// </summary>
        public List<Action> GetActions()
        {
            return actions;
        }

        /// <summary>
        /// Gets the parameters of the plan.
        /// </summary>
        public List<Term> GetParameters()
        {
            return parameters;
        }

        /// <summary>
        /// Updates the data of the plan.
        /// </summary>
        public virtual void UpdateData()
        {

        }

        /// <summary>
        /// Returns a string representation of the plan.
        /// </summary>
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
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the objects are equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Plan))
            {
                return false;
            }

            var other = (Plan)obj;
            if (other.name != name) return false;
            if (preConditions.Count != other.preConditions.Count) return false;
            else
            {
                for (int i = 0; i < preConditions.Count; i++)
                {
                    if (!preConditions[i].Equals(other.preConditions[i])) return false;
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
            if (actions.Count != other.actions.Count) return false;
            else
            {
                for (int i = 0; i < actions.Count; i++)
                {
                    if (!actions[i].Equals(other.actions[i])) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a hash code for this plan.
        /// </summary>
        /// <returns>An integer hash code.</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + name.GetHashCode();
            foreach (var item in preConditions)
            {
                hash = hash * 23 + item.GetHashCode();
            }
            foreach (var item in actions)
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


