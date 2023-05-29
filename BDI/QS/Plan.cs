/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 14:01:26 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 14:01:26 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back
{
        /// <summary>
        /// Represents a plan to move an agent left
        /// </summary>
        public class MoveLeftPlan : Plan
        {
            double moveDistance;
            Term agent;
            Term positionGoal;
            Term positionNow; 
            Term positionNowX;
            Term positionAfterMoveX;
            Term positionAfterMove;

            /// <summary>
            /// Initializes a new instance of the <see cref="MoveLeftPlan"/> class.
            /// </summary>
            /// <param name="moveDistance">The distance to move the agent</param>
            /// <param name="parameters">A list of parameters for the plan, including the agent and goal position</param>
            public MoveLeftPlan(double moveDistance, List<Term> parameters) : base("MoveLeftPlan", parameters)
            {
                if (parameters.Count == 2)
                {
                    this.moveDistance = moveDistance;
                    agent = parameters[0];
                    positionGoal = parameters[1];
                    positionNow = new Term("p1"); 
                    positionNowX = new Term("x1");
                    positionAfterMoveX = new Term("x2");
                    positionAfterMove = new Term("p2");

                    Term positionGoalX = new Term("goalX", ((Position)positionGoal.GetValue()).GetX());
                    preConditions = new List<Formula>() { new Formula("At", new List<Term>() { agent, positionNow }),
                new LessThan(new List<Term>() { positionGoalX, positionNowX })};
                }
                else
                {
                    Console.WriteLine("parameters' length is wrong");
                }
                actions = new List<Action>() { new MoveLeftAction(moveDistance, parameters) };
            }


            /// <summary>
            /// Updates the data associated with the plan
            /// </summary>
            public override void UpdateData()
            {
                if (positionNow.GetValue() != null)
                {
                    positionNowX.SetValue(((Position)positionNow.GetValue()).GetX());
                    double afterMoveX = (double)positionNowX.GetValue() - moveDistance;
                    double goalX = ((Position)positionGoal.GetValue()).GetX();
                    if (afterMoveX < goalX) afterMoveX = goalX;
                    positionAfterMoveX.SetValue(afterMoveX);
                    Position position = (Position)positionNow.GetValue();
                    position.SetX(afterMoveX);
                    positionAfterMove.SetValue(position);
                }
            }
        }

        /// <summary>
        /// Represents a plan for moving an agent to the right by a specified distance.
        /// </summary>
        public class MoveRightPlan : Plan
        {
            double moveDistance;
            Term agent;
            Term positionGoal;
            Term positionNow; 
            Term positionNowX;
            Term positionAfterMoveX;
            Term positionAfterMove;

            /// <summary>
            /// Initializes a new instance of the <see cref="MoveRightPlan"/> class.
            /// </summary>
            /// <param name="moveDistance">The distance to move the agent to the right.</param>
            /// <param name="parameters">A list of terms that includes the agent to move and the goal position to move to.</param>
            public MoveRightPlan(double moveDistance, List<Term> parameters) : base("MoveRightPlan", parameters)
            {
                if (parameters.Count == 2)
                {
                    this.moveDistance = moveDistance;
                    agent = parameters[0];
                    positionGoal = parameters[1];
                    positionNow = new Term("p1"); 
                    positionNowX = new Term("x1");
                    positionAfterMoveX = new Term("x2");
                    positionAfterMove = new Term("p2");

                    Term positionGoalX = new Term("goalX", ((Position)positionGoal.GetValue()).GetX());
                    preConditions = new List<Formula>() { new Formula("At", new List<Term>() { agent, positionNow }),
                new BiggerThan(new List<Term>() { positionGoalX, positionNowX })};
                }
                else
                {
                    Console.WriteLine("parameters' length is wrong");
                }
                actions = new List<Action>() { new MoveRightAction(moveDistance, parameters) };
            }

            /// <summary>
            /// Updates the data of the plan.
            /// </summary>
            public override void UpdateData()
            {
                if (positionNow.GetValue() != null)
                {
                    positionNowX.SetValue(((Position)positionNow.GetValue()).GetX());
                    double afterMoveX = (double)positionNowX.GetValue() + moveDistance;
                    double goalX = ((Position)positionGoal.GetValue()).GetX();
                    if (afterMoveX > goalX) afterMoveX = goalX;
                    positionAfterMoveX.SetValue(afterMoveX);
                    Position position = (Position)positionNow.GetValue();
                    position.SetX(afterMoveX);
                    positionAfterMove.SetValue(position);
                  
                }
            }
        }

    public class GivePlan : Plan
    {
        Term agent;
        Term cus;
        Term pos;
        public GivePlan(List<Term> parameters) : base("Give", parameters)
        {
            agent = parameters[0];
            cus = parameters[1];
            pos = parameters[2];

            preConditions.Add(new Formula("At", new List<Term>() { agent, pos }));
            preConditions.Add(new Formula("At", new List<Term>() { cus, pos }));
            actions = new List<Action>() { new Give(parameters) };
        }
    }
}