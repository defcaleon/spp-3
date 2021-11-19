using System.Linq;
using Bogus;
using Browser;
using Xunit;

namespace Tests
{
    public class NodeTests
    {
        [Fact]
        public void Correct_Created_Node()
        {
            var text = new Faker().Lorem.Word();
            var node = new Node(text);

            Assert.Equal(text, node.Text);
        }

        [Fact]
        public void Correct_Created_Node_With_Parents()
        {
            var faker = new Faker();

            var parentText = faker.Lorem.Word();
            var parentNode = new Node(parentText);

            var childText = faker.Lorem.Word();
            var childNode = new Node(childText);
            parentNode.AddNode(childNode);


            Assert.Single(parentNode.Nodes);
            Assert.Equal(childNode, parentNode.Nodes.Single());
        }
    }
}