/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:55:56 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:55:56 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Back
{

    /// <summary>
    /// An interface for selecting a plan from a list of plans based on the current belief base.
    /// </summary>
    public interface PlanSelector
    {

        /// <summary>
        /// Selects a plan from a list of plans given a belief base.
        /// </summary>
        /// <param name="plans">The list of plans to select from.</param>
        /// <param name="beliefBase">The belief base used for selecting the plan.</param>
        /// <returns>The selected plan.</returns>
        Plan SelectPlan(List<Plan> plans, BeliefBase beliefBase);
    }
}
