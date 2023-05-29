/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:55:50 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:55:50 
 */
using System;
using System.Collections.Generic;
namespace Back
{

    /// <summary>
    /// Selects a random goal from the list of goals.
    /// </summary>
    public class SelectRandomGoal : GoalSelector
    {

        /// <summary>
        /// Selects a goal from a list of goals based on some criteria.
        /// </summary>
        /// <param name="goals">The list of goals to select from.</param>
        /// <param name="beliefBase">The belief base of the agent.</param>
        /// <returns>The selected goal.</returns>
        public Goal SelectGoal(List<Goal> goals, BeliefBase beliefBase)
        {
            if (goals.Count == 0) return null;
            Random random = new Random();
            int randomNumber = random.Next(0, goals.Count - 1);
            return goals[randomNumber];
        }
    }
}
