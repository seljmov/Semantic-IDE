using System.Collections.Generic;

namespace Semantic.Solution
{
    public interface IActionValidator
    {
        Dictionary<string, int> AnalyzeProject(Project standardSolution);
        void AnalyzeDictionary(Dictionary<string, int> dictionary);
        void AnalyzeNewOperators(Dictionary<string, int> dictionary);
        bool IsNewOperators(Dictionary<string, int> newDictionary, Dictionary<string, int> oldDictionary);
        void UpdateDictionary();
        bool IsValidatorStarted();
    }
}