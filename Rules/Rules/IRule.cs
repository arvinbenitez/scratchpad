using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules
{
    interface IRule
    {
        IRuleOperator WithColumn(RuleColumnEnum columnName);
        IRule And();
        IRule And(IRule rule); 
        IRule Or();
        IRule Or(IRule rule);

        IRule Parse(string expression);

        bool Evaluate(Node node);
    }


    interface IRuleOperator
    {
        IRule Equals(int value);
        IRule In(int[] values);
    }

    class Node
    {

    }

    enum RuleColumnEnum
    {
        Client,
    }

    enum RuleOperator
    {
        Equals,
        In
    }


    // rule.WithColumn(RuleColumnEnum.Client).Equals(1245).Or().
}
