/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:56:02 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:56:02 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Back
{

    /// <summary>
    /// Selects the first plan in a list of plans.
    /// </summary>
    public class SelectFirstPlan : PlanSelector
    {

        /// <summary>
        /// Selects the first plan in a list of plans.
        /// </summary>
        /// <param name="plans">A list of plans to choose from.</param>
        /// <param name="beliefBase">The current belief base.</param>
        /// <returns>The first plan in the list of plans.</returns>
        public Plan SelectPlan(List<Plan> plans, BeliefBase beliefBase)
        {
            return plans[0];
        }
    }
}
