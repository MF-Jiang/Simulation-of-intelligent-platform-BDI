/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:55:40 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:55:40 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Back
{

    /// <summary>
    /// An interface for selecting a goal from a list of goals based on the agent's belief base.
    /// </summary>
    public interface GoalSelector
    {

        /// <summary>
        /// Selects a goal from the list of goals based on the agent's belief base.
        /// </summary>
        /// <param name="goals">A list of candidate goals</param>
        /// <param name="beliefBase">The agent's belief base</param>
        /// <returns>The selected goal</returns>
        Goal SelectGoal(List<Goal> goals, BeliefBase beliefBase);
    }
}
