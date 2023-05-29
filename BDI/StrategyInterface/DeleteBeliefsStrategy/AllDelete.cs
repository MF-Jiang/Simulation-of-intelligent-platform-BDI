/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:55:04 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:55:04 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Back
{

    /// <summary>
    /// A strategy for deleting all beliefs that do not match with any sensed formula.
    /// </summary>
    public class AllDelete : DeleteBeliefsStrategy
    {

        /// <summary>
        /// Deletes beliefs from beliefbase that do not match with any sensed formula.
        /// </summary>
        /// <param name="beliefs">The BeliefBase object to delete beliefs from.</param>
        /// <param name="senseList">The list of formulas that were sensed.</param>
        public void DeleteBeliefs(BeliefBase beliefs, List<Formula> senseList)
        {
            List<Formula> temp = new List<Formula>();
            foreach (Formula formula in beliefs.GetBeliefs())
            {
                bool contains = false;
                foreach (Formula sense in senseList)
                {
                    if (sense is null) continue;
                    if (sense.Equals(formula))
                    {
                        contains = true;
                        break;
                    }
                }
                if (!contains) temp.Add(formula);
            }
            foreach (Formula formula in temp)
            {
                beliefs.RemoveBelief(formula);
            }
        }
    }
}
