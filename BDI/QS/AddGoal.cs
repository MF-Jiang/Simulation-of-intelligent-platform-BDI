/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 14:01:15 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 14:01:15 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back
{
    /// <summary>
    /// Implements the AddGoalsStrategy interface to add UnloadGoals to the agent's goal list based on its beliefs about the Carry predicate.
    /// </summary>
    public class AddGoal : AddGoalsStrategy
    {
        /// <summary>
        /// Adds UnloadGoals to the agent's goal list for each container being carried by the agent, if not already present.
        /// </summary>
        /// <param name="agent">The agent for which goals need to be added.</param>
        public void AddGoals(Agent agent)
        {
            BeliefBase beliefs = agent.GetBeliefs();
            List<Goal> goals = agent.GetGoals();
            List<Formula> formulas = beliefs.SearchFormula("At");

            foreach (Formula formula in formulas)
            {
                if (formula.GetParameters().Count > 1)
                {
                    if (formula.GetParameters()[0].GetValue() is Custom)
                    {

                        goals.Add(new Deliberate(new List<Term>() { new Term("self", agent), formula.GetParameters()[0], formula.GetParameters()[1] }));

                    }
                }
            }
        }
    }
}
