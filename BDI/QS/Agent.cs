using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Back
{
    public class QS : Agent
    {
        public QS(string name) : base(name)
        {
            addGoalsStrategy = new AddGoal();
        }
    }
}