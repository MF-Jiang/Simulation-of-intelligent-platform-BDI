/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 14:01:42 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 14:01:42 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back
{
    /// <summary>
    /// Represents a formula that calculates a value based on a predicate and a list of parameters.
    /// </summary>
    public class Calculate : Formula
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Calculate"/> class with the specified predicate and list of parameters.
        /// </summary>
        /// <param name="predicate">The predicate used in the calculation.</param>
        /// <param name="parameters">The list of parameters used in the calculation.</param>
        public Calculate(string predicate, List<Term> parameters) : base(predicate, parameters)
        {
        }

        /// <summary>
        /// Creates a new instance of the Calculate class with the same predicate and parameter values, but with all parameters passed by value.
        /// </summary>
        /// <returns>A new instance of the Calculate class with all parameters passed by value.</returns>
        public override Formula PassByValue()
        {
            List<Term> paras = new List<Term>();
            foreach (Term term in parameters)
            {
                if (term == null) throw new Exception("Null value");
                paras.Add(new Term(term.GetName(), term.GetValue()));
            }
            return new Calculate(predicate, paras);
        }
    }

    /// <summary>
    /// Represents a formula that evaluates whether the first parameter is less than all subsequent parameters.
    /// </summary>
    public class LessThan : Calculate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LessThan"/> class with the specified list of parameters.
        /// </summary>
        /// <param name="parameters">The list of parameters used in the calculation.</param>
        public LessThan(List<Term> parameters) : base("LessThan", parameters)
        {
        }

        /// <summary>
        /// Evaluates whether the first parameter is less than all subsequent parameters.
        /// </summary>
        /// <returns><c>true</c> if the first parameter is less than all subsequent parameters; otherwise, <c>false</c>.</returns>
        public override bool Evaluate()
        {
            if (parameters.Count < 2) return true;
            else
            {
                for (int i = 1; i < parameters.Count; i++)
                {
                    if ((double)parameters[i - 1].GetValue() >= (double)parameters[i].GetValue()) return false;
                }
                return true;
            }

        }
    }

    /// <summary>
    /// Represents a formula that evaluates whether the first parameter is greater than all subsequent parameters.
    /// </summary>
    public class BiggerThan : Calculate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BiggerThan"/> class with the specified list of parameters.
        /// </summary>
        /// <param name="parameters">The list of parameters used in the calculation.</param>
        public BiggerThan(List<Term> parameters) : base("BiggerThan", parameters)
        {
        }

        /// <summary>
        /// Evaluates whether the first parameter is greater than all subsequent parameters.
        /// </summary>
        /// <returns><c>true</c> if the first parameter is greater than all subsequent parameters; otherwise, <c>false</c>.</returns>
        public override bool Evaluate()
        {
            if (parameters.Count < 2) return true;
            else
            {
                for (int i = 1; i < parameters.Count; i++)
                {
                    if ((double)parameters[i - 1].GetValue() <= (double)parameters[i].GetValue()) return false;
                }
                return true;
            }
        }
    }

    /// <summary>
    /// Represents a formula that evaluates whether all parameters are equal.
    /// </summary>
    public class EqualTo : Calculate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualTo"/> class with the specified list of parameters.
        /// </summary>
        /// <param name="parameters">The list of parameters used in the calculation.</param>
        public EqualTo(List<Term> parameters) : base("EqualTo", parameters)
        {
        }

        /// <summary>
        /// Evaluates whether all parameters are equal.
        /// </summary>
        /// <returns><c>true</c> if all parameters are equal; otherwise, <c>false</c>.</returns>
        public override bool Evaluate()
        {
            if (parameters.Count < 2) return true;
            else
            {
                for (int i = 1; i < parameters.Count; i++)
                {
                    if ((double)parameters[i - 1].GetValue() != (double)parameters[i].GetValue()) return false;
                }
                return true;
            }
        }
    }


    /// <summary>
    /// The ContainsObject class is used to determine whether there is an object between two points, including the two points.
    /// </summary>
    public class ContainsObject : Calculate
    {
        /// <summary>
        /// Constructor for the ContainsObject class.
        /// </summary>
        /// <param name="parameters">List of parameters for the ContainsObject function.</param>
        public ContainsObject(List<Term> parameters) : base("ContainsObject", parameters)
        {
        }


        /// <summary>
        /// Determines if there is an object between two points (inclusive) by calculating if a third point is on the same line as the first two points.
        /// </summary>
        /// <returns>True if there is an object between the first two points (inclusive), false otherwise.</returns>
        public override bool Evaluate()
        {
            if (parameters.Count < 3) return false;
            else
            {
                Position p1 = (Position)parameters[0].GetValue();
                Position p2 = (Position)parameters[1].GetValue();
                if (p1.Equals(p2))
                {
                    for (int i = 2; i < parameters.Count; i++)
                    {
                        Position temp = (Position)parameters[i].GetValue();
                        if (temp.Equals(p1)) return true;
                    }
                    return false;
                }
                else
                {
                    int digit = 4;
                    double x1 = p1.GetX(), y1 = p1.GetY(), z1 = p1.GetZ();
                    double x2 = p2.GetX(), y2 = p2.GetY(), z2 = p2.GetZ();
                    for (int i = 2; i < parameters.Count; i++)
                    {
                        Position temp = (Position)parameters[i].GetValue();
                        double x3 = temp.GetX(), y3 = temp.GetY(), z3 = temp.GetZ();
                        if (x2 == x1)
                        {
                            if (x3 != x1) continue;
                            if (y2 == y1)
                            {
                                if (y3 != y2) continue;
                                double bigZ = Math.Max(z1, z2), smallZ = Math.Min(z1, z2);
                                if (z3 > bigZ || z3 < smallZ) continue;
                                else return true;
                            }
                            else
                            {
                                if (z1 == z2)
                                {
                                    if (z3 != z2) continue;
                                    double bigY = Math.Max(y1, y2), smallY = Math.Min(y1, y2);
                                    if (y3 > bigY || y3 < smallY) continue;
                                    else return true;
                                }

                                else
                                {
                                    double y = Math.Round((y3 - y1) / (y2 - y1), digit), z = Math.Round((z3 - z1) / (z2 - z1), digit);
                                    if (z != y) continue;
                                    else
                                    {
                                        double bigY = Math.Max(y1, y2), smallY = Math.Min(y1, y2);
                                        if (y3 > bigY || y3 < smallY) continue;
                                        double bigZ = Math.Max(z1, z2), smallZ = Math.Min(z1, z2);
                                        if (z3 > bigZ || z3 < smallZ) continue;
                                        else return true;
                                    }
                                }
                            }
                        }
                      
                        else if (y2 == y1)
                        {
                            if (y3 != y1) continue;
                           
                            if (z1 == z2)
                            {
                                if (z3 != z2) continue;
                                double bigX = Math.Max(x1, x2), smallX = Math.Min(x1, x2);
                                if (x3 > bigX || x3 < smallX) continue;
                                else return true;
                            }
                      
                            else
                            {
                                double x = Math.Round((x3 - x1) / (x2 - x1), digit), z = Math.Round((z3 - z1) / (z2 - z1), digit);
                                if (z != x) continue;
                                else
                                {
                                    double bigX = Math.Max(x1, x2), smallX = Math.Min(x1, x2);
                                    if (x3 > bigX || x3 < smallX) continue;
                                    double bigZ = Math.Max(z1, z2), smallZ = Math.Min(z1, z2);
                                    if (z3 > bigZ || z3 < smallZ) continue;
                                    else return true;
                                }
                            }

                        }
                     
                        else if (z2 == z1)
                        {
                            if (z3 != z1) continue;
                            else
                            {
                                double x = Math.Round((x3 - x1) / (x2 - x1), digit), y = Math.Round((y3 - y1) / (y2 - y1), digit);
                                if (y != x) continue;
                                else
                                {
                                    double bigX = Math.Max(x1, x2), smallX = Math.Min(x1, x2);
                                    if (x3 > bigX || x3 < smallX) continue;
                                    double bigY = Math.Max(y1, y2), smallY = Math.Min(y1, y2);
                                    if (y3 > bigY || y3 < smallY) continue;
                                    else return true;
                                }
                            }
                        }
                       
                        else
                        {
                            double x = Math.Round((x3 - x1) / (x2 - x1), digit), y = Math.Round((y3 - y1) / (y2 - y1), digit), z = Math.Round((z3 - z1) / (z2 - z1), digit);
                            if (x == z && x == y)
                            {
                                double bigX = Math.Max(x1, x2), smallX = Math.Min(x1, x2);
                                if (x3 > bigX || x3 < smallX) continue;
                                double bigY = Math.Max(y1, y2), smallY = Math.Min(y1, y2);
                                if (y3 > bigY || y3 < smallY) continue;
                                double bigZ = Math.Max(z1, z2), smallZ = Math.Min(z1, z2);
                                if (z3 > bigZ || z3 < smallZ) continue;
                                return true;
                            }
                            continue;
                        }
                    }
                    return false;
                }
            }
        }
    }

    /// <summary>
    /// Determines if there is an object between two points (inclusive) on the xz-plane by checking if a third point lies on the line between the first two points.
    /// </summary>
    public class ContainsObjectXZ : Calculate
    {
        /// <summary>
        /// Initializes a new instance of the ContainsObjectXZ class with the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters used to evaluate whether there is an object between two points on the XZ plane.</param>
        public ContainsObjectXZ(List<Term> parameters) : base("ContainsObjectXZ", parameters)
        {
        }

        /// <summary>
        /// Determines if there is an object between two points (inclusive) on the xz-plane by checking if a third point lies on the line between the first two points.
        /// </summary>
        /// <returns>True if there is an object between the first two points (inclusive) on the xz-plane, false otherwise.</returns>
        public override bool Evaluate()
        {
            if (parameters.Count < 3) return false;
            else
            {
                Position p1 = (Position)parameters[0].GetValue();
                Position p2 = (Position)parameters[1].GetValue();
                if (p1.Equals(p2))
                {
                    for (int i = 2; i < parameters.Count; i++)
                    {
                        Position temp = (Position)parameters[i].GetValue();
                        if (temp.Equals(p1)) return true;
                    }
                    return false;
                }
                else
                {
                    if (p1.GetX() == p2.GetX())
                    {
                        double x = p1.GetX();
                        for (int i = 2; i < parameters.Count; i++)
                        {
                            Position temp = (Position)parameters[i].GetValue();
                            double x3 = temp.GetX();
                            if (x3 == x)
                            {
                                double z1 = p1.GetZ();
                                double z2 = p2.GetZ();
                                double z3 = temp.GetZ();
                                if ((z3 - z1) * (z3 - z2) <= 0) return true;
                            }
                        }
                    }
                    else if (p1.GetZ() == p2.GetZ())
                    {
                        double z = p1.GetZ();
                        for (int i = 2; i < parameters.Count; i++)
                        {
                            Position temp = (Position)parameters[i].GetValue();
                            double z3 = temp.GetX();
                            if (z3 == z)
                            {
                                double x1 = p1.GetX();
                                double x2 = p2.GetX();
                                double x3 = temp.GetX();
                                if ((x3 - x1) * (x3 - x2) <= 0) return true;
                            }
                        }
                    }
                    return false;
                }
            }
        }
    }
}