/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 14:02:20 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 14:02:20 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back
{
    /// <summary>
    /// Represents a set of formulas that share a common predicate.
    /// </summary>
    public class PredicateFormulas
    {
        /// <summary>
        /// The predicate that is common to all formulas in the set.
        /// </summary>
        private string predicate;

        /// <summary>
        /// The list of formulas in the set.
        /// </summary>
        private List<Formula> formulas;

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateFormulas"/> class
        /// with the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate that is common to all formulas in the set.</param>
        public PredicateFormulas(string predicate)
        {
            this.predicate = predicate;
            formulas = new List<Formula>();
        }

        /// <summary>
        /// Gets the predicate that is common to all formulas in the set.
        /// </summary>
        /// <returns>The predicate.</returns>
        public string GetPredicate()
        {
            return predicate;
        }

        /// <summary>
        /// Gets the list of formulas in the set.
        /// </summary>
        /// <returns>The list of formulas.</returns>
        public List<Formula> GetFormulas()
        {
            return formulas;
        }

        /// <summary>
        /// Sets the list of formulas in the set.
        /// </summary>
        /// <param name="formulas">The list of formulas.</param>
        public void SetFormulas(List<Formula> formulas)
        {
            this.formulas = formulas;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is PredicateFormulas)) return false;
            var other = (PredicateFormulas)obj;
            if (other.predicate != predicate) return false;
            if (formulas.Count != other.formulas.Count) return false;
            else
            {
                for (int i = 0; i < formulas.Count; i++)
                {
                    if (!formulas[i].Equals(other.formulas[i])) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns the hash code for the current object.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + predicate.GetHashCode();
            foreach (Formula formula in formulas)
            {
                hash = hash * 23 + formula.GetHashCode();
            }
            return hash;
        }
    }
}
