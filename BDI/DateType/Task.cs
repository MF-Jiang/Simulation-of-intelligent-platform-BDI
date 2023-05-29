/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 14:02:24 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 14:02:24 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back
{
    /// <summary>
    /// Class representing a task to be performed by an agent.
    /// </summary>
    public class Task
    {
        private Term agent;
        private Term container;
        private Term start;
        private Term stack;

        /// <summary>
        /// Constructs a task object with the given parameters.
        /// </summary>
        /// <param name="agent">The term representing the agent performing the task.</param>
        /// <param name="container">The term representing the container involved in the task.</param>
        /// <param name="start">The term representing the starting position of the task.</param>
        /// <param name="end">The term representing the ending position of the task.</param>
        public Task(Term agent, Term container, Term start, Term end)
        {
            this.agent = agent;
            this.container = container;
            this.start = start;
            this.stack = end;
        }

        /// <summary>
        /// Gets the term representing the agent performing the task.
        /// </summary>
        /// <returns>The term representing the agent performing the task.</returns>
        public Term GetAgent()
        {
            return agent;
        }

        /// <summary>
        /// Gets the term representing the container involved in the task.
        /// </summary>
        /// <returns>The term representing the container involved in the task.</returns>
        public Term GetContainer()
        {
            return container;
        }

        /// <summary>
        /// Gets the term representing the starting position of the task.
        /// </summary>
        /// <returns>The term representing the starting position of the task.</returns>
        public Term GetStart()
        {
            return start;
        }

        /// <summary>
        /// Gets the term representing the ending position of the task.
        /// </summary>
        /// <returns>The term representing the ending position of the task.</returns>
        public Term GetStack()
        {
            return stack;
        }

        /// <summary>
        /// Determines whether this task is equal to the given object.
        /// </summary>
        /// <param name="obj">The object to compare with this task.</param>
        /// <returns>True if the object is a task and has the same agent, container, start, and end as this task, false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!(obj is Task)) return false;
            var other = (Task)obj;
            return other.agent.Equals(agent) && other.container.Equals(container) && other.start.Equals(start) && other.stack.Equals(stack);
        }

        /// <summary>
        /// Returns a hash code for this task.
        /// </summary>
        /// <returns>A hash code for this task.</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash + hash * 23 + agent.GetHashCode();
            hash = hash + hash * 23 + container.GetHashCode();
            hash = hash + hash * 23 + start.GetHashCode();
            hash = hash + hash * 23 + stack.GetHashCode();
            return hash;
        }

        /// <summary>
        /// Returns a string representation of this task.
        /// </summary>
        /// <returns>A string representation of this task.</returns>
        public override string ToString()
        {
            return "(" + agent.ToString() + ", " + container.ToString() + ", " + start.ToString() + ", " + stack.ToString() + ")";
        }
    }
}
