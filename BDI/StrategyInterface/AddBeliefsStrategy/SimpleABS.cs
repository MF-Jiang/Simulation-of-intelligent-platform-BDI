/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:54:30 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:54:30 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Back
{
    /// <summary>
    /// This class is an implementation of the AddBeliefsStrategy interface, which provides a way to add beliefs to a BeliefBase object based on sensory input.
    /// </summary>
    public class SimpleABS : AddBeliefsStrategy
    {

        /// <summary>
        /// This method adds beliefs to the belief base based on sensory input.
        /// </summary>
        /// <param name="beliefs">The BeliefBase object to which the beliefs are added.</param>
        /// <param name="senseList">The sensory input in the form of a List of Formula objects.</param>
        /// <param name="ubs">The UpdateBeliefsStrategy object used to update the belief base.</param>
        public void AddBeliefs(BeliefBase beliefs, List<Formula> senseList, UpdateBeliefsStrategy ubs)
        {
            foreach (Formula senseFormula in senseList)
            {
                if (senseFormula == null) continue;
                if (senseFormula is Negation)
                {
                    Formula temp = ((Negation)senseFormula).GetFormula();
                    if (beliefs.ExistSameBelief(temp)) beliefs.RemoveBelief(temp);
                }
                else if (senseFormula.GetParameters().Count == 0) continue;
                if ((!beliefs.ContainsPredicate(senseFormula.GetPredicate())) && (senseFormula.IsGround()))
                {
                    Formula temp = senseFormula.PassByValue();
                    beliefs.AddBelief(temp);
                }
                else if (!beliefs.ExistSameBelief(senseFormula))
                {
                    ubs.UpdateBeliefs(beliefs, senseFormula);
                }
            }
        }
    }
}
