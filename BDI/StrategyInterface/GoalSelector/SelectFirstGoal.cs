/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:55:45 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:55:45 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Back
{

    /// <summary>
    /// Selects the first goal in the list of goals.
    /// </summary>
    public class SelectFirstGoal : GoalSelector
    {

        /// <summary>
        /// Selects the first goal in the list of goals.
        /// </summary>
        /// <param name="goals">List of goals to select from.</param>
        /// <param name="beliefBase">Belief base of the agent.</param>
        /// <returns>The selected goal.</returns>
        public Goal SelectGoal(List<Goal> goals, BeliefBase beliefBase)
        {
            if (goals.Count == 0) return null;
            return goals[0];
        }
    }
}
