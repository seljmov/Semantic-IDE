namespace Semantic.Solution
{
    public interface INavigation
    {
        SemanticItem GetUpItem();
        SemanticItem GetDownItem();
        SemanticItem GetLeftItem();
        SemanticItem GetRightItem();

        void MoveUpFast();
        void MoveUp();

        void MoveDownFast();
        void MoveDown();

        void MoveLeft(bool fast = false);
        void MoveRight(bool fast = false);

        void Tab();
        void ShiftTab();
    }
}