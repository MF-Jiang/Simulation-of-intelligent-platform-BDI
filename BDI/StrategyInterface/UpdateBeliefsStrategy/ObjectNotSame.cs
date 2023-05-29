/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:56:21 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:56:21 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Back
{

    /// <summary>
    /// A strategy for updating beliefs where the object references of the terms are not the same.
    /// </summary>
    public class ObjectNotSame : UpdateBeliefsStrategy
    {

        /// <summary>
        /// Updates the beliefs with the given search formula.
        /// </summary>
        /// <param name="beliefs">The belief base to update.</param>
        /// <param name="searchFormula">The search formula to update with.</param>
        public void UpdateBeliefs(BeliefBase beliefs, Formula searchFormula)
        {
            List<Formula> list = beliefs.SearchFormula(searchFormula.GetPredicate());
            bool contains = false;
            foreach (Formula formula in list)
            {
                if (formula.GetParameters()[0].GetValue() == searchFormula.GetParameters()[0].GetValue())
                {
                    beliefs.ReplaceBelief(formula, searchFormula);
                    contains = true;
                    break;
                }
            }
            if (!contains) beliefs.AddBelief(searchFormula);
        }
    }
}
