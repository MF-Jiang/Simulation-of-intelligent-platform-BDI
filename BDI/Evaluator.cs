/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:50:34 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:50:34 
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
    /// Represents a class for evaluating formulas based on a belief base.
    /// </summary>
    public class Evaluator
    {
        /// <summary>
        /// Sets the conditions for the evaluation based on a list of formulas and a belief base.
        /// </summary>
        /// <param name="conditions">A list of formulas representing the conditions to be evaluated.</param>
        /// <param name="beliefs">The belief base used for evaluating the formulas.</param>
        /// <returns>True if the conditions can be evaluated, false otherwise.</returns>
        public bool SetConditions(List<Formula> conditions, BeliefBase beliefs)
        {
            Hashtable termTable = new Hashtable();
            foreach (Formula formula in conditions)
            {
                Formula temp = formula.PassByValue();
                if (!Traverse(temp, termTable, beliefs))
                {
                    if (temp is Negation)
                    {
                        ((Negation)formula).SetFormulaEvaluate(((Negation)temp).GetFormulaEvaluate());
                        continue;
                    }
                    return false;
                }
                if (temp is Negation)
                {
                    List<Term> terms = ((Negation)temp).GetFormula().GetParameters();
                    List<Term> paras = ((Negation)formula).GetFormula().GetParameters();
                    for (int i = 0; i < paras.Count; i++)
                    {
                        Term term = terms[i];
                        if (term.IsGround())
                        {
                            paras[i].SetValue(term.GetValue());
                        }
                    }
                }
                else
                {
                    List<Term> terms = temp.GetParameters();
                    List<Term> paras = formula.GetParameters();
                    for (int i = 0; i < paras.Count; i++)
                    {
                        Term term = terms[i];
                        if (term.IsGround())
                        {
                            paras[i].SetValue(term.GetValue());
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Traverses a formula and a belief base to find the proper formula and parameters for evaluation.
        /// </summary>
        /// <param name="formula">The formula to be evaluated.</param>
        /// <param name="termTable">A hashtable containing values for the formula parameters.</param>
        /// <param name="beliefs">The belief base used for evaluating the formula.</param>
        /// <returns>True if the formula can be evaluated, false otherwise.</returns>
        bool Traverse(Formula formula, Hashtable termTable, BeliefBase beliefs)
        {
            Hashtable tempTable = new Hashtable();
            foreach (DictionaryEntry entry in termTable)
            {
                tempTable.Add(entry.Key, entry.Value);
            }
            if (formula is Negation)
            {
                bool judge = Traverse((((Negation)formula).GetFormula()), tempTable, beliefs);
                ((Negation)formula).SetFormulaEvaluate(judge);
                return judge;
            }
            else
            {
                if (formula is Calculate)
                {
                    foreach (Term term in formula.GetParameters())
                    {
                        if (term.GetValue() is PredicateFormulas)
                        {
                            string predicate = ((PredicateFormulas)term.GetValue()).GetPredicate();
                            List<Formula> formulas = beliefs.SearchFormula(predicate);
                            ((PredicateFormulas)term.GetValue()).SetFormulas(formulas);
                        }
                    }
                    return true;
                }
                List<Formula> list = beliefs.SearchFormula(formula.GetPredicate()); // formula with the same name in beliefbase
                
                if (list.Count == 0) return false;
                List<Term> parameters = formula.GetParameters(); // get the parameters of formula

                // To find the proper formula in the list
                foreach (Formula formula1 in list)
                {
                    bool evaluate = true; // true if there is no conflict with the value
                    List<Term> parametersBelief = formula1.GetParameters(); // get the parameters of the beliefs formula
                    if (parametersBelief.Count != parameters.Count) continue;
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        Term term = parameters[i];
                        Term termBelief = parametersBelief[i];
                        if (term.GetValue() != null)
                        {
                            if (tempTable.ContainsKey(term.GetName()))
                            {
                                if (term.GetValue() is ObjectType)
                                {
                                    if (tempTable[term.GetName()].Equals(termBelief.GetValue()))
                                    {
                                        term.SetValue(termBelief.GetValue());
                                        continue;
                                    }
                                    else
                                    {
                                        evaluate = false;
                                        break;
                                    }
                                }
                                if (tempTable[term.GetName()] != term.GetValue())
                                {
                                    evaluate = false;
                                    break;
                                }
                                else if (!term.GetValue().Equals(termBelief.GetValue()))
                                {
                                    evaluate = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (term.GetValue() is ObjectType)
                                {
                                    if (((ObjectType)(term.GetValue())).GetObjectType() == termBelief.GetValue().GetType())
                                    {
                                        bool contains = false;
                                        foreach (DictionaryEntry kvp in termTable)
                                        {
                                            if (kvp.Value.Equals(termBelief.GetValue()))
                                            {
                                                if (!kvp.Key.Equals(term.GetName()))
                                                {
                                                    contains = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (contains)
                                        {
                                            evaluate = false;
                                            break;
                                        }
                                        term.SetValue(termBelief.GetValue());
                                        tempTable.Add(term.GetName(), term.GetValue());
                                    }
                                    else
                                    {
                                        evaluate = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (!term.GetValue().Equals(termBelief.GetValue()))
                                    {
                                        evaluate = false;
                                        break;
                                    }
                                    else
                                    {
                                        bool contains = false;
                                        foreach (DictionaryEntry kvp in termTable)
                                        {
                                            if (kvp.Value.Equals(term.GetValue()))
                                            {
                                                if (kvp.Key.Equals(term.GetName()))
                                                {
                                                    contains = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (contains)
                                        {
                                            evaluate = false;
                                            break;
                                        }
                                        tempTable.Add(term.GetName(), term.GetValue());
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (tempTable.ContainsKey(term.GetName()))
                            {
                                if (tempTable[term.GetName()].Equals(termBelief.GetValue()))
                                {
                                    term.SetValue(termBelief.GetValue());
                                }
                                else
                                {
                                    evaluate = false;
                                    break;
                                }
                            }
                            else
                            {
                                bool contains = false;
                                foreach (DictionaryEntry kvp in termTable)
                                {
                                    if (kvp.Value.Equals(termBelief.GetValue()))
                                    {
                                        if (!kvp.Key.Equals(term.GetName()))
                                        {
                                            contains = true;
                                            break;
                                        }
                                    }
                                }
                                if (contains)
                                {
                                    evaluate = false;
                                    break;
                                }
                                term.SetValue(termBelief.GetValue());
                                tempTable.Add(term.GetName(), term.GetValue());
                            }
                        }
                    }
                    if (evaluate)
                    {
                        foreach (DictionaryEntry entry in tempTable)
                        {
                            if (termTable.ContainsKey(entry.Key)) continue;
                            termTable.Add(entry.Key, entry.Value);
                        }
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Evaluates a list of conditions based on their formulas.
        /// </summary>
        /// <param name="consitions">The list of formulas representing the conditions to be evaluated.</param>
        /// <returns>True if all the conditions evaluate to true, false otherwise.</returns>
        public bool EvaluateConditions(List<Formula> consitions)
        {
            foreach (Formula formula in consitions)
            {
                if (!formula.Evaluate()) return false;
            }
            return true;
        }
    }
}
