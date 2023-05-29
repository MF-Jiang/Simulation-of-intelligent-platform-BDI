/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:55:16 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:55:16 
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
    /// A strategy for deleting goals that have been completed.
    /// </summary>
    public class DeleteFinishedGoal : DeleteGoalStrategy
    {
        private Evaluator evaluator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteFinishedGoal"/> class.
        /// </summary>
        public DeleteFinishedGoal()
        {
            evaluator = new Evaluator();
        }

        /// <summary>
        /// Deletes completed goals from the list of current goals.
        /// </summary>
        /// <param name="beliefs">The agent's belief base.</param>
        /// <param name="goals">The agent's current goals.</param>
        /// <param name="goalsDoing">The stack of goals the agent is currently working on.</param>
        /// <param name="intentions">The stack of intended actions.</param>
        public void DeleteGoal(BeliefBase beliefs, List<Goal> goals, Stack<Goal> goalsDoing, Stack<Action> intentions)
        {
            List<Goal> goalsRemoved = new List<Goal>();
            foreach (Goal goal in goals)
            {
                if (EvaluateGoal(goal, beliefs))
                {
                    goalsRemoved.Add(goal);
                    if (goalsDoing.Count > 0)
                    {
                        if (goalsDoing.Peek().Equals(goal))
                        {
                            goalsDoing.Pop();
                            intentions.Clear();
                        }
                    }
                }
            }
            foreach (Goal goal in goalsRemoved)
            {
                goals.Remove(goal);
            }
        }


        /// <summary>
        /// Evaluates if a goal has been completed based on its post-conditions.
        /// </summary>
        /// <param name="goal">The goal to be evaluated.</param>
        /// <param name="beliefs">The belief base of the agent.</param>
        /// <returns>True if the goal has been completed, false otherwise.</returns>
        protected bool EvaluateGoal(Goal goal, BeliefBase beliefs)
        {
            if (!evaluator.SetConditions(goal.GetPostConditions(), beliefs)) return false;
            return evaluator.EvaluateConditions(goal.GetPostConditions());
        }
    }
}
