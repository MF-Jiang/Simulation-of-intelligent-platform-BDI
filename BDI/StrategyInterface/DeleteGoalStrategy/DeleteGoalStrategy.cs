/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:55:21 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:55:21 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Back
{

    /// <summary>
    /// An interface for strategies to delete goals from an agent's goal list.
    /// </summary>
    public interface DeleteGoalStrategy
    {

        /// <summary>
        /// Deletes goals that have been achieved or are no longer relevant.
        /// </summary>
        /// <param name="beliefs">The agent's belief base.</param>
        /// <param name="goals">The agent's current list of goals.</param>
        /// <param name="goalsDoing">The stack of goals the agent is currently working on.</param>
        /// <param name="intentions">The stack of intentions to achieve the goals.</param>
        public void DeleteGoal(BeliefBase beliefs, List<Goal> goals, Stack<Goal> goalsDoing, Stack<Action> intentions);
    }
}
