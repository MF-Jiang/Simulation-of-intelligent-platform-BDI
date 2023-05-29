/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:54:55 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:54:55 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Back
{

    /// <summary>
    /// Interface for adding goals to an agent
    /// </summary>
    public class AddNothing : AddGoalsStrategy
    {

        /// <summary>
        /// Adds goals to the given agent
        /// </summary>
        /// <param name="agent">The agent to add goals to</param>
        public void AddGoals(Agent agent)
        {

        }
    }
}
