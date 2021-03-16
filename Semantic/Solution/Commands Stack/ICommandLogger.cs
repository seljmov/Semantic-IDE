namespace Semantic.Solution
{
    public interface ICommandLogger
    {
        void CommandBegin(IdeCommand command);
        void CommandEnd(IdeCommandResult result);
        void ShowNextCommandInTutor(IdeCommand command);
        void StopEducationMode();
    }
}