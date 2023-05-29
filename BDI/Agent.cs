/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:51:05 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:51:05 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Back
{
    /// <summary>
    /// An abstract class representing an agent in the environment.
    /// </summary>
    public abstract class Agent
    {
        protected PlanSelector planSelector;
        protected GoalSelector goalSelector;
        protected UpdateBeliefsStrategy updateBeliefsStrategy;
        protected AddBeliefsStrategy addBeliefsStrategy;
        protected DeleteBeliefsStrategy deleteBeliefsStrategy;
        protected DeleteGoalStrategy deleteGoalStrategy;
        protected AddGoalsStrategy addGoalsStrategy;
        protected string name;
        protected BeliefBase beliefs;
        protected List<Goal> goals;
        public Env env = Env.GetInstance();
        protected Stack<Action> intentions;
        protected Stack<Goal> goalsDoing;
        protected Evaluator evaluator;

        /// <summary>
        /// Constructor for the agent class.
        /// </summary>
        /// <param name="name">The name of the agent.</param>
        public Agent(string name)
        {
            this.name = name;
            goals = new List<Goal>();
            beliefs = new BeliefBase();
            intentions = new Stack<Action>();
            goalsDoing = new Stack<Goal>();
            planSelector = new SelectRandomPlan();
            goalSelector = new SelectFirstGoal();
            updateBeliefsStrategy = new ObjectNotSame();
            addGoalsStrategy = new AddNothing();
            deleteBeliefsStrategy = new AllDelete();
            addBeliefsStrategy = new SimpleABS();
            evaluator = new Evaluator();
            deleteGoalStrategy = new DeleteFinishedGoal();
        }

        /// <summary>
        /// Adds a new goal to the agent's list of goals.
        /// </summary>
        /// <param name="goal">The goal to be added.</param>
        /// <returns>True if the goal is added successfully, false otherwise.</returns>
        public bool AddGoal(Goal goal)
        {
            goals.Add(goal);
            if (goals.Contains(goal)) return true;
            else return false;
        }

        /// <summary>
        /// Gets the name of the agent.
        /// </summary>
        /// <returns>The name of the agent.</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Performs sensing in the environment and updates the agent's beliefs accordingly.
        /// </summary>
        public void Sense()
        {
            List<Formula> senseList = env.SenseRule(this);
            AddBeliefs(senseList);
            DeleteBeliefs(senseList);
        }

        /// <summary>
        /// sense from environment
        /// </summary>
        /// <param name="senseList">The list of beliefs to be added.</param>
        void AddBeliefs(List<Formula> senseList)
        {
            addBeliefsStrategy.AddBeliefs(beliefs, senseList, updateBeliefsStrategy);
        }

        /// <summary>
        /// Gets a string representation of the agent.
        /// </summary>
        /// <returns>A string representation of the agent.</returns>
        public override string ToString()
        {
            return name;
        }

        /// <summary>
        /// Deletes a list of beliefs from the agent's BeliefBase.
        /// </summary>
        /// <param name="senseList">The list of beliefs to be deleted.</param>
        void DeleteBeliefs(List<Formula> senseList)
        {
            deleteBeliefsStrategy.DeleteBeliefs(beliefs, senseList);
        }

        /// <summary>
        /// Updates the agent's beliefs based on a search formula.
        /// </summary>
        /// <param name="searchFormula">The search formula to update the agent's beliefs.</param>
        public void UpdateBeliefs(Formula searchFormula)
        {
            updateBeliefsStrategy.UpdateBeliefs(beliefs, searchFormula);
        }

        /// <summary>
        /// Add all the agent's goals using the agent's AddGoalsStrategy.
        /// </summary>
        void AddGoals()
        {
            addGoalsStrategy.AddGoals(this);
        }

        /// <summary>
        /// Choose the next action to take based on the agent's goals and intentions.
        /// </summary>
        Action Deliberate()
        {
            if (goalsDoing.Count == 0)
            {
                if (goals.Count == 0) return null;
                Goal goal = GoalSelection();
                Console.WriteLine("This is the goal to do: " + goal);
                goalsDoing.Push(goal);
            }
            if (intentions.Count == 0)
            {
                if (goalsDoing.Count == 0) return null;
                else if (goalsDoing.Count > 0)
                {
                    Goal goal = goalsDoing.Peek();
                    List<Plan> goalPlans = goal.GetPlans();
                    if (goalPlans.Count > 0)
                    {
                        List<Plan> validPlans = GetValidPlans(goalPlans);
                        Console.WriteLine("This is the valid plans: " + validPlans.Count);
                        if (validPlans.Count > 0)
                        {
                            Plan planToDo = PlanSelection(validPlans);
                            Console.WriteLine("This is the plan to do: " + planToDo);
                            AddActions(planToDo);
                        }
                    }
                }
            }
            if (intentions.Count != 0)
            {
                if (intentions.Peek() is Action)
                {
                    return (Action)intentions.Pop();
                }
            }
            return null;
        }

        /// <summary>
        /// Adding Action Lists to the stack
        /// </summary>
        /// <param name="plan">The plan whose actions to add.</param>
        void AddActions(Plan plan)
        {
            List<Action> actions = plan.GetActions();
            for (int i = actions.Count - 1; i >= 0; i--)
            {
                intentions.Push(actions[i]);
            }
        }

        /// <summary>
        /// Selects a goal for the agent to pursue based on its beliefs and goals.
        /// </summary>
        /// <returns>The selected goal.</returns>
        public Goal GoalSelection()
        {
            return goalSelector.SelectGoal(goals, beliefs);
        }

        /// <summary>
        /// Filters a list of plans to only include the ones that are valid based on the agent's beliefs.
        /// </summary>
        /// <param name="PlanList">The list of plans to filter.</param>
        /// <returns>The list of valid plans.</returns>
        List<Plan> GetValidPlans(List<Plan> PlanList)
        {

            List<Plan> result = new List<Plan>();
            foreach (Plan plan in PlanList)
            {
                if (EvaluatePlan(plan)) result.Add(plan);
            }
            return result;
        }

        /// <summary>
        /// Determines if a plan is valid based on the agent's beliefs.
        /// </summary>
        /// <param name="plan">The plan to evaluate.</param>
        /// <returns>True if the plan is valid, false otherwise.</returns>
        bool EvaluatePlan(Plan plan)
        {
            if (!evaluator.SetConditions(plan.GetPreConditions(), beliefs)) return false;
            plan.UpdateData();
            return EvaluateConditions(plan.GetPreConditions());
        }

        /// <summary>
        /// Evaluates a list of conditions against the agent's beliefs.
        /// </summary>
        /// <param name="conditions">The list of conditions to evaluate.</param>
        /// <returns>True if all conditions are true, false otherwise.</returns>
        bool EvaluateConditions(List<Formula> consitions)
        {
            foreach (Formula formula in consitions)
            {
                if (!formula.Evaluate()) return false;
            }
            return true;
        }

        /// <summary>
        /// Selects a plan from a list of valid plans based on the agent's beliefs.
        /// </summary>
        /// <param name="plans">The list of valid plans to select from.</param>
        /// <returns>The selected plan.</returns>
        Plan PlanSelection(List<Plan> plans)
        {
            return planSelector.SelectPlan(plans, beliefs);
        }

        /// <summary>
        /// Updates the agent's list of goals based on its beliefs.
        /// </summary>
        
        void UpdateGoals()
        {
            DeleteGoals();
            AddGoals();
        }

        /// <summary>
        /// Executes the agent's behavior by sensing its environment, updating its beliefs and goals,
        /// deliberating on a plan, and executing the selected action.
        /// </summary>
        /// <returns>The action to execute, or null if no action can be executed.</returns>
        public Action Run()
        {
            Console.WriteLine("\nThis is the agent: " + name);
            Sense();
            beliefs.PrintBeliefs();
            UpdateGoals();

            Console.WriteLine("This is the goal list: ");
            foreach (Goal goal in goals)
            {
                Console.WriteLine(goal);
            }
            Action action = Deliberate();
            if (action != null)
            {
                Console.WriteLine(name + " returns action: " + action);
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++");
            }
            return action;
        }

        /// <summary>
        /// Sets the goal selector strategy for the agent.
        /// </summary>
        /// <param name="goalSelector">The goal selector strategy to set.</param>
        public void SetGoalSelector(GoalSelector goalSelector)
        {
            this.goalSelector = goalSelector;
        }

        /// <summary>
        /// Sets the plan selector strategy for the agent.
        /// </summary>
        /// <param name="planSelector">The plan selector strategy to set.</param>
        public void SetPlanSelector(PlanSelector planSelector)
        {
            this.planSelector = planSelector;
        }

        /// <summary>
        /// Sets the update beliefs strategy for the agent.
        /// </summary>
        /// <param name="updateBeliefsStrategy">The update beliefs strategy to set.</param>
        public void SetUpdateBeliefsStrategy(UpdateBeliefsStrategy updateBeliefsStrategy)
        {
            this.updateBeliefsStrategy = updateBeliefsStrategy;
        }

        /// <summary>
        /// Sets the add beliefs strategy for the agent.
        /// </summary>
        /// <param name="addBeliefsStrategy">The add beliefs strategy to set.</param>
        public void SetAddBeliefsStrategy(AddBeliefsStrategy addBeliefsStrategy)
        {
            this.addBeliefsStrategy = addBeliefsStrategy;
        }

        /// <summary>
        /// Sets the delete beliefs strategy for the agent.
        /// </summary>
        /// <param name="deleteBeliefsStrategy">The delete beliefs strategy to set.</param>
        public void SetDeleteBeliefsStrategy(DeleteBeliefsStrategy deleteBeliefsStrategy)
        {
            this.deleteBeliefsStrategy = deleteBeliefsStrategy;
        }

        /// <summary>
        /// Sets the delete goal strategy for the agent.
        /// </summary>
        /// <param name="deleteGoalStrategy">The delete goal strategy to set.</param>
        public void SetDeleteGoalStrategy(DeleteGoalStrategy deleteGoalStrategy)
        {
            this.deleteGoalStrategy = deleteGoalStrategy;
        }

        /// <summary>
        /// Adds an add goals strategy for the agent.
        /// </summary>
        /// <param name="addGoalsStrategy">The add goals strategy to add.</param>
        public void AddAddGoalsStrategy(AddGoalsStrategy addGoalsStrategy)
        {
            this.addGoalsStrategy = addGoalsStrategy;
        }

        /// <summary>
        /// Returns the add goals strategy for the agent.
        /// </summary>
        /// <returns>The add goals strategy for the agent.</returns>
        public AddGoalsStrategy GetGoalsStrategy()
        {
            return this.addGoalsStrategy;
        }

        /// <summary>
        /// Deletes goals based on the agent's delete goal strategy.
        /// </summary>
        void DeleteGoals()
        {
            deleteGoalStrategy.DeleteGoal(beliefs, goals, goalsDoing, intentions);
        }

        /// <summary>
        /// Determines if this agent is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>True if the agent is equal to the object, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Agent)) return false;
            var other = (Agent)obj;
            return name.Equals(other.name);
        }

        /// <summary>
        /// Generates a hash code for this agent.
        /// </summary>
        /// <returns>A hash code for this agent.</returns>
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        /// <summary>
        /// Returns the agent's belief base.
        /// </summary>
        /// <returns>The agent's belief base.</returns>
        public BeliefBase GetBeliefs()
        {
            return beliefs;
        }

        /// <summary>
        /// Returns the agent's list of goals.
        /// </summary>
        /// <returns>The agent's list of goals.</returns>
        public List<Goal> GetGoals()
        {
            return goals;
        }

        /// <summary>
        /// Returns the agent's current location.
        /// </summary>
        /// <returns>The agent's current location.</returns>
        public Position GetSelfLocation()
        {
            List<Formula> lists = beliefs.SearchFormula("At");
            if (lists.Count > 0)
            {
                foreach (Formula formula in lists)
                {
                    List<Term> terms = formula.GetParameters();
                    if (terms.Count == 2)
                    {
                        object o = terms[0].GetValue();
                        if (o != null)
                        {
                            if (o.Equals(this))
                            {
                                object p = terms[1].GetValue();
                                if (p != null)
                                {
                                    if (p is Position) return (Position)p;
                                }
                                else throw new Exception("Null value");
                            }
                        }
                        else throw new Exception("Null value");
                    }
                    else throw new Exception("Wrong length");
                }
                return new Position(int.MinValue, int.MinValue, int.MinValue);
            }
            else throw new Exception("Wrong length");
        }
    }

}
