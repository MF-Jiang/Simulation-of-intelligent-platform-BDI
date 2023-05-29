/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:55:33 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:55:33 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Back
{

    /// <summary>
    /// Interface for strategies that determine the next action to be performed by an agent
    /// </summary>
    public interface DeliberateActionStrategy
    {

        /// <summary>
        /// Determines the next action to be performed by an agent
        /// </summary>
        /// <param name="intentions">The stack of intentions of the agent</param>
        /// <returns>The next action to be performed</returns>
        public Action DeliberateAction(Stack<Action> intentions);
    }
}
