using System;
using MyMentalHealth.Models;
using System.Diagnostics.CodeAnalysis;

namespace MyMentalHealth.Comparer
{
    public class CompareIssues : IEqualityComparer<MentalHealthIssues>
    {

        public bool Equals(MentalHealthIssues? x, MentalHealthIssues? y)
        {
            if (y == null) return false;
            if (x.Id == y.Id)
            {
                return true;
            }
            return false;
        }

     
        public int GetHashCode([DisallowNull] MentalHealthIssues obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}

