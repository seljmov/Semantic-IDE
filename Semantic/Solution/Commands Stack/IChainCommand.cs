namespace Semantic.Solution
{
    public interface IChainCommand
    {
        bool IsInChain(IdeCommand ideCommand);
    }
}