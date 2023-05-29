/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:50:08 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:50:08 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Xml;
using System.ComponentModel;

namespace Back
{
    /// <summary>
    /// Represents the environment for the simulation.
    /// </summary>
    public class Environment
    {
        Env environment = Env.GetInstance();

        /// <summary>
        /// Initializes the environment and returns a list of initial agent positions.
        /// </summary>
        /// <returns>A list of "PairAgentPosition" objects representing the initial agent positions.</returns>

        public List<PairAgentPosition> InitEnv()
        {
            environment.InitEnv();

            List<PairAgentPosition> list = environment.OutputUnityPosition();
            foreach (PairAgentPosition obj in list)
            {

            }

            return list;
        }

        /// <summary>
        /// Starts the simulation and returns a list of current agent positions.
        /// </summary>
        /// <returns>A list of "PairAgentPosition" objects representing the current agent positions.</returns>
        public List<PairAgentPosition> StartEnv()
        {
            environment.StartEnv();

            List<PairAgentPosition> list = environment.OutputUnityPosition();
            foreach (PairAgentPosition obj in list)
            {

            }

            return list;
        }
    }

    public class Custom
    {
        public Custom(int value)
        {
            this.value = value;
        }
        public int value;
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Custom))
            {
                return false;
            }
            var pos_new = (Custom)obj;
            if (pos_new.value == value) return true;
            return false;
        }

        /// <summary>
        /// Returns a hash code for the current position.
        /// </summary>
        /// <returns>A hash code for the current position.</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash + hash * 23 + value.GetHashCode();
            return hash;
        }
    }

    /// <summary>
    /// class for define environment
    /// </summary>
    public class Env
    {

        //FIELDS!!!!!!!//////////////////////////////////////////////////////////////////////////////////////////
        //Variable in Enviornment

        //all state and information in env
        /// <summary>
        /// Belief base of the environment.
        /// </summary>
        private BeliefBase envBelief;

        //all Agents Info <agentID, agentObj> in env
        /// <summary>
        /// List of all agents present in the environment.
        /// </summary>
        private List<Agent> agentList = new List<Agent>();

        /// <summary>
        /// Evaluator object used to evaluate the formulas of agents.
        /// </summary>
        private Evaluator evaluator = new Evaluator();

        /// <summary>
        /// List of actions performed by each agent in a round.
        /// </summary>
        private List<PairAgentAction> eachRoundAction;

        /// <summary>
        /// Returns the list of agents present in the environment.
        /// </summary>
        public List<Agent> GetAgentsinEnv() { return agentList; }

        /// <summary>
        /// Returns the belief base of the environment.
        /// </summary>
        /// <returns>The belief base of the environment.</returns>
        public BeliefBase GetBeliefinEnv() { return envBelief; }

        //Singleton Env!!!!!!!//////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Singleton instance of the Env class.
        /// </summary>
        private static Env environment = new Env();

        /// <summary>
        /// Private constructor for the singleton class.
        /// </summary>
        private Env() { }

        /// <summary>
        /// Returns the singleton instance of the Env class.
        /// </summary>
        /// <returns>The singleton instance of the Env class.</returns>
        public static Env GetInstance()
        {
            return environment;
        }

        //INITIAL!!!!!!!//////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Initializes the environment with initial values.
        /// </summary>
        public void InitEnv()
        {
            //initial the beliefs base
            envBelief = new BeliefBase();

            QS qs1 = new QS("qs1");
            agentList.Add(qs1);
            /* //create vessel agent
             Vessel vessel = new Vessel("vessel");
             agentList.Add(vessel);

             //create QCrane agent
             QCrane qcrane0 = new QCrane("qcrane0");
             agentList.Add(qcrane0);
             QCrane qcrane1 = new QCrane("qcrane1");
             agentList.Add(qcrane1);

             //create AGVS agent
             AGV agvehicle0 = new AGV("agvehicle0");
             agentList.Add(agvehicle0);
             AGV agvehicle1 = new AGV("agvehicle1");
             agentList.Add(agvehicle1);

             //create StarategyAgent
             StrategyAgent strategyAgent = new StrategyAgent("startegyAgent");
             agentList.Add(strategyAgent);

             //create YCraneU
             YCrane ycrane0 = new YCrane("ycrane0");
             agentList.Add(ycrane0);
             YCrane ycrane1 = new YCrane("ycrane1");
             agentList.Add(ycrane1);*/


            //Constructing Formula-----------------------------------------------------------------------------

            Term pos_now = new Term("tkdl", new Position(10, 10, 10));
            Formula at0 = new Formula("At", new List<Term> { new Term("qishou", qs1), pos_now });
            envBelief.AddBelief(at0);

            Term pos_cus = new Term("unnc", new Position(20, 10, 10));
            Formula at1 = new Formula("At", new List<Term> { new Term("s1", new Custom(10)) , pos_cus});
            envBelief.AddBelief(at1);
            /*//create beliefs: IsAvailable(berth)
            Term berthVar = new Term("BerthVar", new Berth(0, new Position(0, 0, 250), 400, 100));
            Formula isAvailable = new Formula("IsAvailable", new List<Term> { berthVar });
            envBelief.AddBelief(isAvailable);

            //create beliefs: At(agent, pos)
            Term vesVar = new Term("VesVar", new Position(500, 0, 250));
            Term agentVesssel = new Term("vessel", vessel);
            Formula atVessel = new Formula("At", new List<Term> { agentVesssel, vesVar });
            envBelief.AddBelief(atVessel);

            //At(Qccrane0, pos)
            Term qcPos0 = new Term("qcPos0", new Position(50, 0, 170));
            Term agentQcrane0 = new Term("qcrane0", qcrane0);
            Formula atQcrane0 = new Formula("At", new List<Term> { agentQcrane0, qcPos0 });
            envBelief.AddBelief(atQcrane0);

            //At(Qccrane1, pos)
            Term qcPos1 = new Term("qcPos1", new Position(-50, 0, 170));
            Term agentQcrane1 = new Term("qcrane1", qcrane1);
            Formula atQcrane1 = new Formula("At", new List<Term> { agentQcrane1, qcPos1 });
            envBelief.AddBelief(atQcrane1);

            //At(AGV0, pos)
            Term agvPos0 = new Term("agvPos0", new Position(0, 0, 150));
            Term agentAGV0 = new Term("agvehicle0", agvehicle0);
            Formula atAGV0 = new Formula("At", new List<Term> { agentAGV0, agvPos0 });
            envBelief.AddBelief(atAGV0);

            //At(AGV1, pos)
            Term agvPos1 = new Term("agvPos1", new Position(0, 0, 140));
            Term agentAGV1 = new Term("agvehicle1", agvehicle1);
            Formula atAGV1 = new Formula("At", new List<Term> { agentAGV1, agvPos1 });
            envBelief.AddBelief(atAGV1);

            //At(Yccrane0, pos)
            Term ycPos0 = new Term("ycPos0", new Position(50, 0, 0));
            Term agentYcrane0 = new Term("ycrane0", ycrane0);
            Formula atYcrane0 = new Formula("At", new List<Term> { agentYcrane0, ycPos0 });
            envBelief.AddBelief(atYcrane0);

            //At(Yccrane1, pos)
            Term ycPos1 = new Term("ycPos1", new Position(-50, 0, 0));
            Term agentYcrane1 = new Term("ycrane1", ycrane1);
            Formula atYcrane1 = new Formula("At", new List<Term> { agentYcrane1, ycPos1 });
            envBelief.AddBelief(atYcrane1);*/

            //---------------------------------------------------------------------------------------

            //create beliefs: Size(agent, len, wid)
            /*double len = 107.56, wid = 16.50;
            Term vesLen = new Term("VesLen", len);
            Term vesWid = new Term("VesWid", wid);
            Formula sizeVessel = new Formula("Size", new List<Term> { agentVesssel, vesLen, vesWid });
            envBelief.AddBelief(sizeVessel);

            //create waiting and leaving area
            Term waiting1 = new Term("waiting_pos1", new Position(500, 0, 250));
            Term waiting2 = new Term("waiting_pos2", new Position(300, 0, 250));
            Term leaving1 = new Term("leaving_pos1", new Position(-300, 0, 250));
            Formula waitArea = new Formula("WaitArea", new List<Term> { waiting1, waiting2 });
            Formula exitArea = new Formula("ExitArea", new List<Term> { leaving1 });
            envBelief.AddBelief(waitArea);
            envBelief.AddBelief(exitArea);

            //create container of one vessel: Carry(vessel,container1,...)
            List<Term> termList = new List<Term>();
            //add agent term
            termList.Add(agentVesssel);*/

            //for (int i = 0; i < 5; i++)
            //{
            //    Container newcontainer = new Container("Container_" + (i + 1));
            //    Position positon = new Position(500 + i, 0, 250);

            //    //create new formula at(ContainerU, position)
            //    Term newC = new Term("Container_" + (i + 1), newcontainer);
            //    Term newP = new Term("ContainerU" + (i + 1) + "pos", positon);
            //    Formula atContainer = new Formula("At", new List<Term> { newC, newP });
            //    //add into beliefbase
            //    envBelief.AddBelief(atContainer);

            //    //add into "Carry" formula termlist
            //    termList.Add(newC);
            //}

            //Formula carryContainer = new Formula("Carry", termList);
            //envBelief.AddBelief(carryContainer);

            ////create empty carry for two AGVS: Carry(AGVS)
            //Formula carryAGV0 = new Formula("Carry", new List<Term>() { agentAGV0 });
            //envBelief.AddBelief(carryAGV0);
            //Formula carryAGV1 = new Formula("Carry", new List<Term>() { agentAGV1 });
            //envBelief.AddBelief(carryAGV1);

            ////create empty WaitAllocate(container...)
            //Formula waitAllocate = new Formula("WaitAllocate", new List<Term>() { });
            //envBelief.AddBelief(waitAllocate);

            ////create WaitQueue(agv0, agv1, p3...)
            //Term AGVWaitingQ_3 = new Term("waitingS_pos3", new Position(0, 0, 130));
            //Formula waitQueue = new Formula("WaitQueue", new List<Term> { agentAGV0, agentAGV1, AGVWaitingQ_3 });
            //envBelief.AddBelief(waitQueue);

            ////create WaitYCrane(stack,c1...)
            //Stack stack0 = new Stack("smallStack", 55, 25, new Position(50, 0, 0), new Position(50, 0, 15), "");
            //Stack stack1 = new Stack("bigStack", 55, 45, new Position(-50, 0, 0), new Position(-50, 0, 22), "");
            //Formula waitYCrane0 = new Formula("WaitYCrane", new List<Term> { new Term("smallStack", stack0) });
            //Formula waitYCrane1 = new Formula("WaitYCrane", new List<Term> { new Term("bigStack", stack1) });
            //envBelief.AddBelief(waitYCrane0);
            //envBelief.AddBelief(waitYCrane1);

            ////create empty carry for two YC: Carry(YC)
            //Formula carryYC0 = new Formula("Carry", new List<Term>() { agentYcrane0 });
            //envBelief.AddBelief(carryYC0);
            //Formula carryYC1 = new Formula("Carry", new List<Term>() { agentYcrane1 });
            //envBelief.AddBelief(carryYC1);
        }


        //SENSE!!!!!!!!!!!//////////////////////////////////////////////////////////////////////////////////////////

        //Agent sense Info
        /// <summary>
        /// Returns the sensing rules of an agent by calling different rule methods based on the type of the agent.
        /// </summary>
        /// <param name="agent">The agent to sense.</param>
        /// <returns>The sensing rules list of the agent.</returns>
        public List<Formula> SenseRule(Agent agent)
        {
            if (agent is QS) return QSRule(agent);
            else throw new Exception("WRONG Agent Type in sense information!");

        }

        /// <summary>
        /// Returns the sensing rules of a vessel.
        /// </summary>
        /// <param name="agent">The vessel agent to sense.</param>
        /// <returns>The sensing rules list of the vessel, at(agent, pos), size(agent,wid,len), Limit(pos, pos), all isAvailable(berth).</returns>
        
        private List<Formula> QSRule(Agent agent)
        {
            List<Formula> senseList = new List<Formula>();
            senseList = envBelief.SearchFormula("At");
            return senseList;
        }

        /// <summary>
        /// Returns the sensing rules of a quay crane.
        /// </summary>
        /// <param name="agent">The quay crane agent to sense.</param>
        /// <returns>The sensing rules list of the quay crane, all At(QCrane,pos) At(Conatiner,pos), carry(VesselU,container1....).</returns>


        /// <summary>
        /// Returns the sensing rules of an AGV.
        /// </summary>
        /// <param name="agent">The AGV agent to sense.</param>
        /// <returns>All at formulas within a fixed range, his own Carry(AGVS,container1...)</returns>

        //RUN!!!!!!!!//////////////////////////////////////////////////////////////////////////////////////////

        // start() in environment
        // run all agents in the Env
        /// <summary>
        /// Starts the environment and runs all agents in the environment.
        /// </summary>
        public void StartEnv()
        {
            Console.WriteLine("\n//////////The belief in environment///////////");
            envBelief.PrintBeliefs();

            Console.WriteLine("\n//////////The agents in environment///////////");
            foreach (Agent agent in agentList)
            {
                Console.WriteLine(agent.GetName());
            }

            //calculate the total num of agent
            int agentNum = agentList.Count;

            //random choose the agent to run
            while (true)
            {
                //generate random List between (0,agentNum-1)
                //store List as the order for running agent
                if (agentNum == 0)
                {
                    //UnityEngine.Debug.Log("!!No AGENT in the Environment!!");
                    Console.WriteLine("!!No AGENT in the Environment!!");
                    break;
                }

                List<int> orderList = RandomArr(0, agentNum);

                //store the return agent_action objs
                List<PairAgentAction> actionList = new List<PairAgentAction>();

                //start running for a round
                for (int i = 0; i < agentNum; i++)
                {
                    //according to the order search the agent in dictionary
                    Agent tempA = agentList[orderList[i]];

                    //run Agent
                    //get return actions list
                    Action actionReceive = tempA.Run();

                    if (actionReceive == null)
                    {
                        //UnityEngine.Debug.Log("!!This Agent NO ACTION!!");
                        Console.WriteLine("!!This Agent NO ACTION!!");
                        continue;
                    }
                    Console.WriteLine("Env received: " + actionReceive.ToString());

                    PairAgentAction newPair = new PairAgentAction(tempA, actionReceive);
                    actionList.Add(newPair);
                }
                //update receive var in Env
                UpdateEnv(actionList);
                break;
            }
        }

        //return the random arr as the order of run all agents
        /// <summary>
        /// Returns a random list as the order of running all the agents.
        /// </summary>
        /// <param name="minValue">The minimum value of the range.</param>
        /// <param name="maxValue">The maximum value of the range.</param>
        /// <returns>the random arr as the order of run all agents</returns>
        public List<int> RandomArr(int minValue, int maxValue)
        {
            System.Random rnd = new System.Random();
            List<int> list = new List<int>();
            int k = 0;
            do
            {
                k = rnd.Next(minValue, maxValue);
                if (!list.Contains(k))
                    list.Add(k);
            } while (list.Count < maxValue);
            return list;
        }

        /// <summary>
        /// Output Unity position of all agents.
        /// </summary>
        /// <returns>List of pairs of agent name and position.</returns>

        public List<PairAgentPosition> OutputUnityPosition()
        {
            List<PairAgentPosition> list = new List<PairAgentPosition>();

            List<Formula> agentFormulas = envBelief.SearchFormula("At");
            foreach (Formula formula in agentFormulas)
            {
                List<Term> termList = formula.GetParameters();
                string agentName = null;
                float x = 0;
                float y = 0;
                float z = 0;
                foreach (Term term in termList)
                {
                    System.Object value = term.GetValue();
                    if (value is Agent)
                    {
                        agentName = ((Agent)value).GetName();
                    }
                    else if (value is Position)
                    {
                        Position agentPos = (Position)value;
                        x = (float)agentPos.GetX();
                        y = (float)agentPos.GetY();
                        z = (float)agentPos.GetZ();
                    }
                }

                Action action = null;

                if (eachRoundAction != null)
                {
                    foreach (PairAgentAction actionPair in eachRoundAction)
                    {
                        if (actionPair.GetAgent().GetName().Equals(agentName))
                        {
                            action = actionPair.GetAction();
                        }
                    }
                }

                list.Add(new PairAgentPosition(agentName, "", action, x, y, z));
            }

            return list;
        }


        //UPDATE!!!!!!!//////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update the belief base of the environment.
        /// </summary>
        /// <param name="agent_Actions">List of pairs of agents and their actions.</param>

        private void UpdateEnv(List<PairAgentAction> agent_Actions)
        {
            eachRoundAction = new List<PairAgentAction>();
            //if preCondition is in Env_var
            //update postcondition
            foreach (PairAgentAction pairObj in agent_Actions)
            {
                Action tmpAction = pairObj.GetAction();

                PairBoolAction checkPreConditions = EvaluateAction(tmpAction, envBelief);
                Console.WriteLine("\nEnv Action received: " + checkPreConditions.GetAction().ToString() + "  PreChecking: " + checkPreConditions.GetBool());

                if (checkPreConditions.GetBool())
                {
                    EnvUpdatePostCond(checkPreConditions.GetAction().GetPostConditions());
                    eachRoundAction.Add(pairObj);
                }
            }
        }
        //Update the postcondition///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Updates the belief base of the environment with the given postconditions.
        /// </summary>
        /// <param name="postCond">The postconditions to update.</param>
        private void EnvUpdatePostCond(List<Formula> postCond)
        {
            //if (postCond.Count == 0) UnityEngine.Debug.Log("!!!!!!!!!!!!!!!!!!!!NoPOSTcondition!!!!");

            foreach (Formula postF in postCond)
            {
                Console.WriteLine("This is the postcond of this action received in Env:  " + postF.ToString());

                if (postF is Negation)
                {
                    EnvUpdateNeg(((Negation)postF).GetFormula());
                }
                else if (postF is Effect)
                {
                    Console.WriteLine("This is effect");
                    Effect effect = (Effect)postF;
                    effect.Act();
                }
                else EnvUpdatePos(postF);

                EnvUpdateWaitingQueue();
            }
        }

        //Determine the preconditions///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Evaluates the preconditions of an action and returns a boolean-action pair.
        /// </summary>
        /// <param name="action">The action to be evaluated.</param>
        /// <param name="belief">The belief base of the environment.</param>
        /// <returns>A boolean-action pair indicating whether the preconditions are met.</returns>
        private PairBoolAction EvaluateAction(Action action, BeliefBase belief)
        {

            if (!evaluator.SetConditions(action.GetPreConditions(), belief)) return new PairBoolAction(false, action);
            action.UpdateData();
            bool booleanV = evaluator.EvaluateConditions(action.GetPreConditions());

            return new PairBoolAction(booleanV, action);
        }



        //update NEGATIVE formula
        /// <summary>
        /// Updates the belief base of the environment with a negative formula.
        /// </summary>
        /// <param name="formula_in">The negative formula to be updated.</param>
        private void EnvUpdateNeg(Formula formula_in)
        {
            List<Formula> samePreList = envBelief.SearchFormula(formula_in.GetPredicate());

            if (samePreList.Count == 0) return;

                foreach (Formula formula in samePreList)
                {
                if (CompareAllTermFormula(formula, formula_in))
                {
                    Console.WriteLine("This shopuld be removed 1111111111111: " + formula);
                    envBelief.RemoveBelief(formula);
                }
                }
            

        }

        //update NOT NEGATIVE
        /// <summary>
        /// Update the NOT NEGATIVE formula in the environment's belief base.
        /// </summary>
        /// <param name="formula_in">The formula to be updated.</param>
        private void EnvUpdatePos(Formula formula_in)
        {
            List<Formula> samePreList = envBelief.SearchFormula(formula_in.GetPredicate());

            if (formula_in.GetParameters()[0].GetValue() is Agent)
            {
                Agent agentIn = (Agent)formula_in.GetParameters()[0].GetValue();
                Formula sameAgentF = SearchAgentFormula(samePreList, agentIn.GetName());

                if (sameAgentF != null)
                {

                    int termIndex = 0;
                    foreach (Term term in sameAgentF.GetParameters())
                    {
                        if (term.GetValue() is Agent)
                        {
                            termIndex++;
                            continue;
                        }

                        if (formula_in.GetParameters() != null && formula_in.GetParameters().Count >= termIndex)
                        {
                            object tmp = formula_in.GetParameters()[termIndex].GetValue();
                        
                            if (tmp != null)
                            {
                                term.SetValue(tmp);
                            }
                        }

                        termIndex++;
                    }

                    if (formula_in.GetParameters().Count >= sameAgentF.GetParameters().Count)
                    {
                        sameAgentF.SetParameters(formula_in.GetParameters());
                    }

                }
                else
                {
                    envBelief.AddBelief(formula_in);
                }

            }

            else
            {
                foreach (Formula formula in samePreList)
                {
                    
                    if (formula.GetParameters().Count == 0)
                    {
                        
                        formula.SetParameters(formula_in.GetParameters());
                        return;
                    }
                   
                }
                
                envBelief.AddBelief(formula_in);
            }

        }

        /// <summary>
        /// Update the waiting queue positions in the environment's belief base.
        /// </summary>
        private void EnvUpdateWaitingQueue()
        {
            List<Formula> Qlist = envBelief.SearchFormula("WaitQueue");
            List<Formula> Atlist = envBelief.SearchFormula("At");
            List<Formula> Carrylist = envBelief.SearchFormula("Carry");

            Position[] WaitingQueuePosList = { new Position(0, 0, 150), new Position(0, 0, 140), new Position(0, 0, 130) };

            foreach (Formula formula in Qlist)
            {
                int index = 0;
                foreach (Term term in formula.GetParameters())
                {
                    if (term.GetValue() is Agent)
                    {

                        Formula thisAgentAt = SearchAgentFormula(Atlist, ((Agent)term.GetValue()).GetName());
                        Position thisAgentPos = GetPositionFormula(thisAgentAt);
                        if (!thisAgentPos.Equals(WaitingQueuePosList[index]))
                        {

                            Term newPterm = new Term("WaitQueuePos" + index, WaitingQueuePosList[index]);
                            formula.GetParameters()[index] = newPterm;

                            return;
                        }
                    }
                    index++;
                }
            }
        }


        //extra  method/////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare all terms between two formulas to check if they are identical.
        /// </summary>
        /// <param name="A">The first formula to be compared.</param>
        /// <param name="B">The second formula to be compared.</param>
        /// <returns>Returns true if all terms in formula A are identical to those in formula B, false otherwise.</returns>
        private bool CompareAllTermFormula(Formula A, Formula B)
        {
            /*Console.WriteLine("This is formula A: !!! " + A);
            Console.WriteLine("This is formula B: !!! " + B);*/
            List<Term> AParas = A.GetParameters();
            List<Term> BParas = B.GetParameters();
            int flag = 0;

            if (AParas.Count != BParas.Count) return false;

            foreach (Term Aterm in AParas)
            {
                flag = 0;
                foreach (Term Bterm in BParas)
                {
                    if (CompareTerm(Aterm, Bterm))
                    {
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0) return false;
            }
            return true;
        }


        /// <summary>
        /// Compare the value of both vars for position, and if so, compare the x and z values of pos
        /// </summary>
        /// <param name="A">The first term to be compared.</param>
        /// <param name="B">The second term to be compared.</param>
        /// <returns>Returns true if both terms have the same value, false otherwise.</returns>
        private bool CompareTerm(Term A, Term B)
        {
            object tmpObj = A.GetValue();
            object inObj = B.GetValue();

            return tmpObj.Equals(inObj);

            if (tmpObj == null || inObj == null) return false;

            if (tmpObj is Position && inObj is Position) return ((Position)tmpObj).Equals((Position)inObj);


            if (tmpObj is Container && inObj is Container) return ((Container)tmpObj).Equals((Container)inObj);

            if (tmpObj is Stack && inObj is Stack) return ((Stack)tmpObj).Equals((Stack)inObj);



            if (tmpObj is Agent && inObj is Agent)
            {
                Agent tmpP = (Agent)tmpObj;
                Agent inP = (Agent)inObj;

                if (tmpP.GetName().Equals(inP.GetName()))
                {
                    return true;
                }
                else return false;
            }
            return false;
        }

        /// <summary>
        /// Find the position term in a formula.
        /// </summary>
        /// <param name="formula">The formula to be searched.</param>
        /// <returns>Returns the position term in the given formula.</returns>
        private Position GetPositionFormula(Formula formula)
        {
            List<Term> list = formula.GetParameters();
            foreach (Term term in list)
            {
                object obj = term.GetValue();

                if (obj != null && obj is Position)
                {
                    return (Position)obj;
                }
            }
            return new Position();
        }

        /// <summary>
        /// Searches for a formula in the List of formulas with the same predicate as the input formula,
        /// and having a parameter with the specified agentName or containerName.
        /// </summary>
        /// <param name="list">The list of formulas to search in</param>
        /// <param name="findName">The name of the agent or container to search for</param>
        /// <returns>The first formula found with the specified agent or container name, or null if not found</returns>
        private Formula SearchAgentFormula(List<Formula> list, string findName)
        {
            foreach (Formula formula in list)
            {
                List<Term> termlist = formula.GetParameters();
                foreach (Term term in termlist)
                {
                    object obj = term.GetValue();

                    if (obj != null && obj is Agent)
                    {
                        if (((Agent)obj).GetName().Equals(findName)) return formula;
                    }
                }
            }
            return null;
        }

    }


    //PAIR!!!!!!!!!//////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// A pair consisting of an agent and an action for the agent to perform.
    /// </summary>
    class PairAgentAction
    {
        private Agent agent;
        private Action action;

        /// <summary>
        /// Constructor for creating a PairAgentAction object.
        /// </summary>
        /// <param name="agent1">The agent for this pair.</param>
        /// <param name="action1">The action for the agent to perform.</param>
        public PairAgentAction(Agent agent1, Action action1)
        {
            this.agent = agent1;
            this.action = action1;
        }

        /// <summary>
        /// Returns the action for the agent to perform.
        /// </summary>
        /// <returns>The action for the agent to perform.</returns>
        public Action GetAction()
        {
            return this.action;
        }

        /// <summary>
        /// Returns the agent in this pair.
        /// </summary>
        /// <returns>The agent in this pair.</returns>
        public Agent GetAgent()
        {
            return this.agent;
        }

        /// <summary>
        /// Sets the action for the agent to perform.
        /// </summary>
        /// <param name="action_in">The action for the agent to perform.</param>
        public void SetAction(Action action_in)
        {
            this.action = action_in;
        }

        /// <summary>
        /// Sets the agent in this pair.
        /// </summary>
        /// <param name="agent_in">The agent in this pair.</param>
        public void SetAgent(Agent agent_in)
        {
            this.agent = agent_in;
        }
    }

    /// <summary>
    /// A pair consisting of a boolean value and an action.
    /// </summary>
    class PairBoolAction
    {
        private Action action;
        private bool boolean;

        /// <summary>
        /// Constructor for creating a PairBoolAction object.
        /// </summary>
        /// <param name="boolean">The boolean value for this pair.</param>
        /// <param name="action">The action for this pair.</param>
        public PairBoolAction(bool boolean, Action action)
        {
            this.action = action;
            this.boolean = boolean;
        }

        /// <summary>
        /// Returns the boolean value in this pair.
        /// </summary>
        /// <returns>The boolean value in this pair.</returns>
        public bool GetBool()
        {
            return boolean;
        }

        /// <summary>
        /// Returns the action in this pair.
        /// </summary>
        /// <returns>The action in this pair.</returns>
        public Action GetAction()
        {
            return action;
        }
    }

    /// <summary>
    /// A struct representing a pair of agent position.
    /// </summary>
    public struct PairAgentPosition
    {
        private string agentName;
        private string agentType;
        private Action action;
        private float posX;
        private float posY;
        private float posZ;

        /// <summary>
        /// Constructs a new PairAgentPosition.
        /// </summary>
        /// <param name="name">The name of the agent.</param>
        /// <param name="type">The type of the agent.</param>
        /// <param name="action">The action of the agent.</param>
        /// <param name="posX">The X position of the agent.</param>
        /// <param name="posY">The Y position of the agent.</param>
        /// <param name="posZ">The Z position of the agent.</param>
        public PairAgentPosition(string name, string type, Action action, float posX, float posY, float posZ)
        {
            this.agentName = name;
            this.agentType = type;
            this.action = action;
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;
        }
        /// <summary>
        /// Returns the name of the agent.
        /// </summary>
        /// <returns>The name of the agent.</returns>
        public string GetAgentName() { return agentName; }

        /// <summary>
        /// Returns the type of the agent.
        /// </summary>
        /// <returns>The type of the agent.</returns>
        public string GetAgentType() { return agentType; }

        /// <summary>
        /// Returns the action of the agent.
        /// </summary>
        /// <returns>The action of the agent.</returns>
        public Action GetAction() { return action; }


        /// <summary>
        /// Returns the X position of the agent.
        /// </summary>
        /// <returns>The X position of the agent.</returns>
        public float GetPosX() { return posX; }

        /// <summary>
        /// Returns the Y position of the agent.
        /// </summary>
        /// <returns>The Y position of the agent.</returns>
        public float GetPosY() { return posY; }

        /// <summary>
        /// Returns the Z position of the agent.
        /// </summary>
        /// <returns>The Z position of the agent.</returns>
        public float GetPosZ() { return posZ; }


        /// <summary>
        /// Returns a string representation of this PairAgentPosition.
        /// </summary>
        /// <returns>A string representation of this PairAgentPosition.</returns>
        public string printString()
        {
            return agentName + " X: " + posX + " Y: " + posY + " Z: " + posZ + "action: " + action;
        }

    }

}
