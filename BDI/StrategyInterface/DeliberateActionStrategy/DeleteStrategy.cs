/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:55:27 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:55:27 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Back
{

    /// <summary>
    /// Defines a strategy for deleting an action from the stack of intentions.
    /// </summary>
    public class DeleteStrategy : DeliberateActionStrategy
    {

        /// <summary>
        /// Implements the deliberate action method by popping the top action from the stack of intentions.
        /// </summary>
        /// <param name="intentions">The stack of intentions.</param>
        /// <returns>The top action from the stack of intentions.</returns>
        public Action DeliberateAction(Stack<Action> intentions)
        {
            return intentions.Pop();
        }
    }
}
