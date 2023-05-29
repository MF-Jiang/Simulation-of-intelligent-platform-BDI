/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 14:01:47 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 14:01:47 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back
{
    /// <summary>
    /// Represents a logical effect in a planning domain.
    /// </summary>
    public class Effect : Formula
    {
        /// <summary>
        /// Initializes a new instance of the Effect class with the specified predicate and parameters.
        /// </summary>
        /// <param name="predicate">The predicate of the effect.</param>
        /// <param name="parameters">The list of parameters for the effect.</param>
        public Effect(string predicate, List<Term> parameters) : base(predicate, parameters)
        {

        }

        /// <summary>
        /// Returns a copy of this Effect object.
        /// </summary>
        /// <returns>A copy of this Effect object.</returns>
        public override Formula PassByValue()
        {
            List<Term> paras = new List<Term>();
            foreach (Term term in parameters)
            {
                paras.Add(new Term(term.GetName(), term.GetValue()));
            }
            return new Effect(predicate, paras);
        }


        /// <summary>
        /// Performs the action associated with this Effect object.
        /// </summary>
        public virtual void Act()
        {

        }
    }

    /// <summary>
    /// Represents an Effect object that adds a Goal object to an Agent object's list of goals.
    /// </summary>
    public class AddAgentGoal : Effect
    {
        Agent agent;
        Goal goal;

        /// <summary>
        /// Constructor for AddAgentGoal effect. 
        /// The parameters are a list of Terms: [Agent, Goal]
        /// </summary>
        /// <param name="parameters">List of terms representing the parameters for the effect</param>
        public AddAgentGoal(List<Term> parameters) : base("AddAgentGoal", parameters)
        {
            if (parameters.Count == 2)
            {
                agent = (Agent)parameters[0].GetValue();
                goal = (Goal)parameters[1].GetValue();
            }
            else
            {
                throw new Exception("Parameters in a wrong length");
            }
        }

        /// <summary>
        /// Adds the Goal object associated with this AddAgentGoal object to the Agent object's list of goals.
        /// </summary>
        public override void Act()
        {
            if (agent.AddGoal(goal))
            {
                Console.WriteLine(agent + " + " + goal);
                Console.WriteLine("This is the length of goals: " + agent.GetGoals().Count);
            }
        }
        
        /// <summary>
        /// Evaluates whether the Agent object's list of goals contains the Goal object associated with this AddAgentGoal object.
        /// </summary>
        /// <returns>True if the Agent object's list of goals contains the Goal object associated with this AddAgentGoal object, false otherwise.</returns>
        public override bool Evaluate()
        {
            return agent.GetGoals().Contains(goal);
        }
    }
}


