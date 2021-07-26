namespace ArchitectureTest
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using FluentAssertions;
   using NetArchTest.Rules.Policies;

   internal static class AssertPolicyResults
   {
      internal static void ShouldBeValid(this PolicyResults policyResults)
      {
         policyResults.Results.ForEach(r => r.ShouldBeValid());
      }

      private static void ShouldBeValid(this PolicyResult policyResult)
      {
         string failingTypes = string.Empty;
         if (policyResult.FailingTypes?.Any() == true)
         {
            failingTypes = $" (affected types: {string.Join(",", policyResult.FailingTypes.Select(t => t.Name))})";
         }

         policyResult.IsSuccessful.Should().BeTrue($"{policyResult.Description}{failingTypes}");
      }

      private static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
      {
         foreach (T item in source)
         {
            action(item);
         }
      }
   }
}