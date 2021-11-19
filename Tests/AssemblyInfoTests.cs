using System;
using System.Linq;
using Bogus;
using Browser;
using Xunit;

namespace Tests
{
    public class AssemblyInfoTests
    {
        [Fact]
        public void Correct_Created_Type()
        {
            var faker = new Faker();
            var assemblyInfo = new AssemblyInfo(faker.Lorem.Word());
            var namespaceName = faker.Lorem.Word();
            var typeName = faker.Lorem.Word();
            assemblyInfo.AddType(namespaceName, typeName);

            Assert.Equal(namespaceName, assemblyInfo.Namespaces.Values.Single().Text);
            Assert.Equal(typeName, assemblyInfo.Namespaces.Values.Single().Nodes.Single().Text);
        }

        [Fact]
        public void Correct_Created_Type_In_Different_Namespaces()
        {
            var faker = new Faker();
            var assemblyInfo = new AssemblyInfo(faker.Lorem.Word());
            var firstNamespace = Guid.NewGuid().ToString();
            var secondNamespace = Guid.NewGuid().ToString();
            assemblyInfo.AddType(firstNamespace, faker.Lorem.Word());
            assemblyInfo.AddType(secondNamespace, faker.Lorem.Word());

            Assert.Equal(2, assemblyInfo.Namespaces.Count);
        }

        [Fact]
        public void Correct_Created_Two_In_One_Namespace()
        {
            var faker = new Faker();
            var assemblyInfo = new AssemblyInfo(faker.Lorem.Word());
            var namespaceName = faker.Lorem.Word();
            assemblyInfo.AddType(namespaceName, faker.Lorem.Word());
            assemblyInfo.AddType(namespaceName, faker.Lorem.Word());

            Assert.Single(assemblyInfo.Namespaces);
            Assert.Equal(2, assemblyInfo.Namespaces.Values.Single().Nodes.Count);
        }

        [Fact]
        public void Correct_Recursive_Adding()
        {
            const int itemTypesCount = 4;
            var faker = new Faker();
            var assemblyInfo = new AssemblyInfo(faker.Lorem.Word());
            var namespaceName = faker.Lorem.Word();
            assemblyInfo.AddType(namespaceName, faker.Lorem.Word());

            var type = assemblyInfo.Namespaces.Values.Single().Nodes.Single();
            var newItemType = (ItemType) new Randomizer().Int(0,3);
            var newItem = new NodeFaker().Generate();
            assemblyInfo.AddItemToLastAddedType(
                namespaceName,
                newItem,
                newItemType);

            Assert.Equal(itemTypesCount, type.Nodes.Count);
            Assert.Equal(newItem, type.Nodes[(int)newItemType].Nodes.Single());
        }
    }
}