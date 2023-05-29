/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:56:09 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:56:09 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Back
{

    /// <summary>
    /// Selects a random plan from a list of plans
    /// </summary>
    public class SelectRandomPlan : PlanSelector
    {

        /// <summary>
        /// Selects a random plan from a list of plans
        /// </summary>
        /// <param name="plans">List of plans to select from</param>
        /// <param name="beliefBase">Current belief base of the agent</param>
        /// <returns>A randomly selected plan from the list</returns>
        public Plan SelectPlan(List<Plan> plans, BeliefBase beliefBase)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, plans.Count - 1);
            return plans[randomNumber];
        }
    }
}
