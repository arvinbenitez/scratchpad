using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules
{
    class Rule: IRule, IRuleOperator
    {
        private LinkedRule LinkedRule { get; set; }

        public IRule Equals(int value)
        {
            throw new NotImplementedException();
        }

        public IRule In(int[] values)
        {
            throw new NotImplementedException();
        }

        public IRuleOperator WithColumn(RuleColumnEnum columnName)
        {
            throw new NotImplementedException();
        }

        public IRule And()
        {
            throw new NotImplementedException();
        }

        public IRule And(IRule rule)
        {
            throw new NotImplementedException();
        }

        public IRule Or()
        {
            throw new NotImplementedException();
        }

        public IRule Or(IRule rule)
        {
            throw new NotImplementedException();
        }

        public IRule Parse(string expression)
        {
            throw new NotImplementedException();
        }

        public bool Evaluate(Node node)
        {
            throw new NotImplementedException();
        }
    }

    class LinkedRule
    {
        Rule Rule { get; set; }
        RuleOperator Operator { get; set; }
    }
}
