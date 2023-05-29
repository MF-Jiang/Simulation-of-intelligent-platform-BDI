/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:55:09 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:55:09 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back
{

    /// <summary>
    /// An interface for a strategy that deletes beliefs from a belief base based on a list of sensed formulas.
    /// </summary>
    public interface DeleteBeliefsStrategy
    {

        /// <summary>
        /// Deletes beliefs from a belief base based on a list of sensed formulas.
        /// </summary>
        /// <param name="beliefs">The belief base to delete beliefs from.</param>
        /// <param name="senseList">The list of sensed formulas to use for deleting beliefs.</param>
        public void DeleteBeliefs(BeliefBase beliefs, List<Formula> senseList);
    }
}
