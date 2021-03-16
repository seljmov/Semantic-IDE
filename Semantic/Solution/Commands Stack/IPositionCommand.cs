namespace Semantic.Solution
{
    public interface IPositionCommand
    {
        int PositionOffset { get; }
        PositionMode PositionMode { get; }
    }
}