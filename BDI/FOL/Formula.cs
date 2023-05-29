/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 14:01:55 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 14:01:55 
 */

//Predicate(Term1, Term2, Term3...)
using System.Collections.Generic;

namespace Back
{
    public class Formula
    {
        //The predicate of the formula
        /// <summary>
        /// The predicate of the formula.
        /// </summary>
        protected string predicate;

        //The terms of the formula
        /// <summary>
        /// The terms of the formula.
        /// </summary>
        protected List<Term> parameters;

        /// <summary>
        /// Gets the predicate of the formula.
        /// </summary>
        /// <returns>The predicate of the formula.</returns>
        public virtual string GetPredicate()
        {
            return predicate;
        }

        /// <summary>
        /// Gets the list of terms of the formula.
        /// </summary>
        /// <returns>The list of terms of the formula.</returns>
        public List<Term> GetParameters()
        {
            return parameters;
        }

        /// <summary>
        /// Sets the list of terms of the formula.
        /// </summary>
        /// <param name="parameters">The list of terms to be set.</param>
        public void SetParameters(List<Term> parameters)
        {
            this.parameters = parameters;
        }

        /// <summary>
        /// Returns a string representation of the formula.
        /// </summary>
        /// <returns>A string representation of the formula.</returns>
        public override string ToString()
        {
            string res = predicate + "(";
            for (int i = 0; i < parameters.Count - 1; i++)
            {
                res += parameters[i].GetValue() + ", ";
            }

            if (parameters.Count == 0)
            {
                res += ")";
            }
            else if (parameters.Count >= 1)
            {
                res += parameters[parameters.Count - 1].GetValue() + ")";
            }

            return res;
        }

        /// <summary>
        /// Initializes a new instance of the Formula class with the specified predicate and list of terms.
        /// </summary>
        /// <param name="predicate">The predicate of the formula.</param>
        /// <param name="parameters">The list of terms of the formula.</param>
        public Formula(Formula formula)
        {
            predicate = formula.predicate;
            parameters = formula.parameters;
        }

        /// <summary>
        /// Initializes a new instance of the Formula class with the specified formula.
        /// </summary>
        /// <param name="formula">The formula to be set.</param>
        public Formula(string predicate, List<Term> parameters)
        {
            this.predicate = predicate;
            this.parameters = parameters;
        }

        /// <summary>
        /// Checks whether the formula is ground (contains no variables).
        /// </summary>
        /// <returns>True if the formula is ground, false otherwise.</returns>
        public bool IsGround()
        {
            if (parameters.Count == 0) return true;
            foreach (Term term in parameters)
            {
                if (!term.IsGround()) return false;
            }
            return true;
        }

        /// <summary>
        /// Evaluates the formula.
        /// </summary>
        /// <returns>True if the formula evaluates to true, false otherwise.</returns>
        public virtual bool Evaluate()
        {
            return IsGround();
        }

        /// <summary>
        /// Creates a copy of the formula with all variables replaced by their values.
        /// </summary>
        /// <returns>A copy of the formula with all variables replaced by their values.</returns>
        public virtual Formula PassByValue()
        {
            List<Term> paras = new List<Term>();
            foreach (Term term in parameters)
            {
                paras.Add(new Term(term.GetName(), term.GetValue()));
            }
            return new Formula(predicate, paras);
        }

        /// <summary>
        /// Determines whether this Formula is equal to another Formula.
        /// Two Formulas are considered equal if they have the same predicate and parameters.
        /// </summary>
        /// <param name="obj">The other object to compare with.</param>
        /// <returns>True if the two Formulas are equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Formula))
            {
                return false;
            }
            var other = (Formula)obj;
            if (!other.GetPredicate().Equals(predicate)) return false;
            List<Term> inputParameters = other.GetParameters();
            if (inputParameters.Count != parameters.Count) return false;
            for (int i = 0; i < parameters.Count; i++)
            {
                if (!parameters[i].Equals(inputParameters[i])) return false;
            }
            return true;
        }

        /// <summary>
        /// Gets a hash code for this Formula.
        /// </summary>
        /// <returns>A hash code for this Formula.</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + predicate.GetHashCode();
            foreach (var p in parameters)
            {
                hash = hash * 23 + p.GetHashCode();
            }
            return hash;
        }
    }

    ///<summary>
    ///This class represents a negation formula, which negates the evaluation of its internal formula.
    ///</summary>
    public class Negation : Formula
    {
        private Formula formula;
        private bool formulaEvaluate = true;

        ///<summary>
        ///Constructor for the Negation class. Takes a formula object and negates its evaluation.
        ///</summary>
        ///<param name="formula">The formula to be negated</param>
        public Negation(Formula formula) : base(formula)
        {
            this.formula = formula;
        }

        ///<summary>
        ///Returns a string representation of the negation formula, with a "!" prefix.
        ///</summary>
        public override string ToString()
        {
            return "!" + formula.ToString();
        }

        ///<summary>
        ///Getter method for the formula object within the negation.
        ///</summary>
        public Formula GetFormula() { return formula; }

        ///<summary>
        ///Setter method for the formula object within the negation.
        ///</summary>
        ///<param name="formula">The new formula to replace the current formula in the negation.</param>
        public void SetFormula(Formula formula) { this.formula = formula; }

        ///<summary>
        ///Evaluates the negation formula by negating the evaluation of its internal formula.
        ///If the internal formula has already been evaluated to false, the negation returns true.
        ///</summary>
        public override bool Evaluate()
        {
            if (!formulaEvaluate) return true;
            return !formula.Evaluate();
        }

        ///<summary>
        ///Returns a copy of the negation formula with the same formula object as the original formula,
        ///but with a new reference.
        ///</summary>
        public override Formula PassByValue()
        {
            Formula formulaTemp = formula.PassByValue();
            return new Negation(formulaTemp);
        }

        ///<summary>
        ///Setter method for the formulaEvaluate boolean, which tracks whether the internal formula has
        ///already been evaluated to false.
        ///</summary>
        ///<param name="input">The new boolean value to set for formulaEvaluate.</param>
        public void SetFormulaEvaluate(bool input)
        {
            formulaEvaluate = input;
        }

        ///<summary>
        ///Getter method for the formulaEvaluate boolean, which tracks whether the internal formula has
        ///already been evaluated to false.
        ///</summary>
        public bool GetFormulaEvaluate()
        {
            return formulaEvaluate;
        }

        ///<summary>
        ///Compares two negation formulas for equality by checking whether their internal formulas and
        ///formulaEvaluate boolean values are equal.
        ///</summary>
        ///<param name="obj">The object to compare to this negation formula.</param>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Negation)) return false;
            var other = (Negation)obj;
            return formula.Equals(other.formula) && formulaEvaluate == other.formulaEvaluate;
        }

        ///<summary>
        ///Returns a hash code for the negation formula, based on its internal formula and formulaEvaluate
        ///values.
        ///</summary>
        public override int GetHashCode()
        {
            return 17 * 23 + base.GetHashCode();
        }
    }
}