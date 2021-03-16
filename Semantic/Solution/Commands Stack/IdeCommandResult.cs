namespace Semantic.Solution
{
    public class IdeCommandResult
    {
        public IdeCommandResult(bool isSuccessed)
        {
            Successed = isSuccessed;
            if (isSuccessed)
            {
                Message = Strings.ActionSuccessfullyCompleted;
            }
        }

        public bool Successed { get; private set; }
        public string Message { get; internal set; }
        public string Result { get; internal set; }
    }
}