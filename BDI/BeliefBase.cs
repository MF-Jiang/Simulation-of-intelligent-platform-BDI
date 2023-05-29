/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:51:29 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:51:29 
 */
using System;
using System.Collections.Generic;

namespace Back
{
    // <summary>
    /// BeliefBase class is used to store the agent's set of beliefs, which can add, remove, and replace beliefs, as well as search and print them.
    /// </summary>
    public class BeliefBase
    {
        /// <summary>
        /// The list of beliefs.
        /// </summary>
        public List<Formula> beliefs { get; private set; }

        /// <summary>
        /// Constructor for the BeliefBase class.
        /// </summary>
        public BeliefBase()
        {
            beliefs = new List<Formula>();
        }

        /// <summary>
        /// Adds a belief to the list of beliefs.
        /// </summary>
        /// <param name="belief">The belief to add.</param>
        public void AddBelief(Formula belief)
        {
            beliefs.Add(belief);
        }

        /// <summary>
        /// Removes a belief from the list of beliefs.
        /// </summary>
        /// <param name="belief">The belief to remove.</param>
        public void RemoveBelief(Formula belief)
        {
            beliefs.Remove(belief);
        }

        /// <summary>
        /// Replaces an old belief with a new belief.
        /// </summary>
        /// <param name="oldBelief">The old belief to replace.</param>
        /// <param name="newBelief">The new belief to replace the old belief with.</param>
        public void ReplaceBelief(Formula oldBelief, Formula newBelief)
        {
            beliefs.Remove(oldBelief);
            beliefs.Add(newBelief);
        }

        /// <summary>
        /// Determines if a belief with the given predicate exists in the list of beliefs.
        /// </summary>
        /// <param name="predicate">The predicate to search for.</param>
        /// <returns>True if a belief with the given predicate exists in the list of beliefs, false otherwise.</returns>
        public bool ContainsPredicate(string predicate)
        {
            foreach (Formula belief in beliefs)
            {
                if (belief.GetPredicate().Equals(predicate)) return true;
            }
            return false;
        }

        /// <summary>
        /// Determines if a belief with the same formula as the given senseFormula exists in the list of beliefs.
        /// </summary>
        /// <param name="senseFormula">The formula to search for.</param>
        /// <returns>True if a belief with the same formula as the given senseFormula exists in the list of beliefs, false otherwise.</returns>
        public bool ExistSameBelief(Formula senseFormula)
        {
            return beliefs.Contains(senseFormula);
        }

        /// <summary>
        /// Searches the list of beliefs for all beliefs with the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to search for.</param>
        /// <returns>A list of all beliefs with the given predicate.</returns>
        public List<Formula> SearchFormula(string predicate)
        {
            List<Formula> formulaList = new List<Formula>();
            foreach (Formula belief in beliefs)
            {
                if (belief.GetPredicate() == predicate) formulaList.Add(belief);
            }
            return formulaList;
        }

        /// <summary>
        /// Gets the list of beliefs.
        /// </summary>
        /// <returns>The list of beliefs.</returns>
        public List<Formula> GetBeliefs()
        {
            return beliefs;
        }

        /// <summary>
        /// Prints all the beliefs in the list of beliefs.
        /// </summary>
        public void PrintBeliefs()
        {
            foreach (Formula formula in beliefs)
            {
                Console.WriteLine(formula.ToString());
            }
        }
    }
}