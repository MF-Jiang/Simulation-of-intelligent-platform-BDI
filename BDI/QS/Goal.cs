/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 14:01:22 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 14:01:22 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back
{
        /// <summary>
        /// Represents a goal to unload a container from a vessel.
        /// </summary>
        public class Deliberate : Goal
        {
            Term agent;
            Term custom;
            Term pos_self;
            Term pos_cus;

            /// <summary>
            /// Initializes an instance of the UnloadGoal class with the specified parameters.
            /// </summary>
            /// <param name="parameters">A list of terms representing the parameters of the goal.</param>
            public Deliberate(List<Term> parameters) : base("Deliberate", parameters)
            {
                agent = new Term("agent1", parameters[0].GetValue());
                custom = new Term("cus_1", parameters[1].GetValue());
                pos_cus = new Term("p1", parameters[2].GetValue());

                postConditions = new List<Formula>() { new Formula("At", new List<Term>() { agent, pos_cus }) };
            }

            /// <summary>
            /// Gets a list of plans that satisfy this goal.
            /// </summary>
            /// <returns>A list of Plan objects.</returns>
            public override List<Plan> GetPlans()
            {
                return new List<Plan>() { new MoveLeftPlan(150, new List<Term>() { agent, pos_cus, }), new MoveRightPlan(150, new List<Term>() { agent, pos_cus }), new GivePlan(new List<Term>() { agent, custom, pos_cus }) };
            }
        }
    }