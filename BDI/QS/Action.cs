/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 14:01:10 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 14:01:10 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back
{

        // <summary>
        /// Represents an action of moving left.
        /// </summary>
        public class MoveLeftAction : Action
        {
            double moveDistance;
            Term agent;
            Term positionGoal;
            Term positionNow; 
            Term positionNowX;
            Term positionAfterMoveX;
            Term positionAfterMove;

            // <summary>
            /// Initializes a new instance of the MoveLeftAction class with the specified move distance and parameters.
            /// </summary>
            /// <param name="moveDistance">The distance to move the agent.</param>
            /// <param name="parameters">The parameters for the action.</param>
            public MoveLeftAction(double moveDistance, List<Term> parameters) : base("MoveLeftAction", parameters)
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
                postConditions = new List<Formula>() { new Formula("At", new List<Term>() { agent, positionAfterMove }), new Negation(new Formula("At", new List<Term>() { agent, positionNow })) };

            }

            /// <summary>
            /// Updates the data after the action is executed.
            /// </summary>
            public override void UpdateData()
            {
                if (positionNow.GetValue() != null)
                {
                    Position now = (Position)positionNow.GetValue();
                    double x = now.GetX();
                    positionNowX.SetValue(x);

                    positionNowX.SetValue(x);
                    double afterMoveX = x - moveDistance;
                    double goalX = ((Position)positionGoal.GetValue()).GetX();
                    if (afterMoveX < goalX) afterMoveX = goalX;
                    positionAfterMoveX.SetValue(afterMoveX);

                    now.SetX(afterMoveX);
                    positionAfterMove.SetValue(now);
                }
            }
        }

    public class Give : Action
    {
        Term agent;
        Term cus;
        Term pos;
        public Give(List<Term> parameters) : base("Give", parameters)
        {
            agent = parameters[0];
            cus = parameters[1];
            pos = parameters[2];

            preConditions.Add(new Formula("At", new List<Term>() { agent, pos }));
            preConditions.Add(new Formula("At", new List<Term>() { cus, pos }));

            postConditions.Add(new Negation(new Formula("At", new List<Term>() { cus, pos })));
        }
    }



        /// <summary>
        /// Represents an action of moving right.
        /// </summary>
        public class MoveRightAction : Action
        {
            double moveDistance;
            Term agent;
            Term positionGoal;
            Term positionNow; 
            Term positionNowX;
            Term positionAfterMoveX;
            Term positionAfterMove;
            List<Term> positions;
            PredicateFormulas at;

            /// <summary>
            /// Initializes a new instance of the MoveRightAction class with the specified move distance and parameters.
            /// </summary>
            /// <param name="moveDistance">The distance to move the agent.</param>
            /// <param name="parameters">The parameters for the action.</param>
            public MoveRightAction(double moveDistance, List<Term> parameters) : base("MoveRightAction", parameters)
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
                postConditions = new List<Formula>() { new Formula("At", new List<Term>() { agent, positionAfterMove }), new Negation(new Formula("At", new List<Term>() { agent, positionNow })) };

            }

            /// <summary>
            /// Updates the data for the MoveRightAction instance.
            /// </summary>
            public override void UpdateData()
            {
                if (positionNow.GetValue() != null)
                {
                    Position now = (Position)positionNow.GetValue();
                    double x = now.GetX();
                    positionNowX.SetValue(x);

                    double afterMoveX = x + moveDistance;
                    double goalX = ((Position)positionGoal.GetValue()).GetX();
                    if (afterMoveX > goalX) afterMoveX = goalX;
                    positionAfterMoveX.SetValue(afterMoveX);

                    now.SetX(afterMoveX);
                    positionAfterMove.SetValue(now);
                }
            }
        }
    }