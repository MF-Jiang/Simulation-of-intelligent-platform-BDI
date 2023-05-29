/*
 * @Author: GRP Team-16 
 * @Date: 2023-04-05 13:52:05 
 * @Last Modified by:   GRP Team-16 
 * @Last Modified time: 2023-04-05 13:52:05 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;

namespace Back
{
    /// <summary>
    /// The main program entry point for the project.
    /// </summary>
    public class ProgramScript
    {
        /// <summary>
        /// The main function of this project
        /// </summary>
        public static void Main()
        {
            Environment environment = new Environment();
            environment.InitEnv();

            for (int i = 1; i < 50; i++)
            {
                Console.WriteLine("\nRound " + i + ": ");
                environment.StartEnv();
            }
        }
    }
}
