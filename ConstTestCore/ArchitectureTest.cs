using ConstTest;

namespace ArchitectureTest
{
    using NetArchTest.Rules;
    using NetArchTest.Rules.Policies;
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for system level tests.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AssemblyLevelTest
    {
        [TestMethod]
        public void DependencyCheck()
        {
            Policy.Define("ConstTest policies", "Dependency conditions for the ConstTest namespace.")
                .For(Types.InAssembly(typeof(ClassToTest).Assembly))
                .Add(
                    types => types
                        .That().ResideInNamespace("ConstTest")
                        .Should().OnlyHaveDependenciesOn("ConstTest", "System"),
                    "Dependency Contract",
                    "The assembly must not have any dependencies to other assemblies.")
                .Evaluate()
                .ShouldBeValid();
        }
    }
}