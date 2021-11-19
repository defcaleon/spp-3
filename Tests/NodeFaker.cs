using Bogus;
using Browser;

namespace Tests
{
    public class NodeFaker : Faker<Node>
    {
        public NodeFaker()
        {
            RuleFor(n => n.Text, f => f.Lorem.Word());
        }
    }
}