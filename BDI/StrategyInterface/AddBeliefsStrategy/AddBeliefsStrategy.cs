/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:54:24 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:54:24 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back
{
    /// <summary>
    /// Interface for classes that add beliefs to a belief base.
    /// </summary>
    public interface AddBeliefsStrategy
    {
        /// <summary>
        /// Adds beliefs to the given belief base.
        /// </summary>
        /// <param name="beliefs">The belief base to which beliefs will be added.</param>
        /// <param name="senseList">A list of formulas representing sensed information.</param>
        /// <param name="ubs">The update beliefs strategy to use.</param>
        public void AddBeliefs(BeliefBase beliefs, List<Formula> senseList, UpdateBeliefsStrategy ubs);
    }
}
