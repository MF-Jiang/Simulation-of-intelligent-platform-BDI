/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:56:28 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:56:28 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Back
{

    /// <summary>
    /// An interface representing the update beliefs strategy used by agents to update their belief base.
    /// </summary>
    public interface UpdateBeliefsStrategy
    {

        /// <summary>
        /// Updates the belief base according to the search formula.
        /// </summary>
        /// <param name="beliefs">The belief base to update.</param>
        /// <param name="searchFormula">The search formula used to update the belief base.</param>
        public void UpdateBeliefs(BeliefBase beliefs, Formula searchFormula);
    }
}
