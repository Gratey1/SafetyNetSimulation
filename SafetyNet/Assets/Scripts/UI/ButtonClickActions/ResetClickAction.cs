public class ResetClickAction : BaseClickAction
{
    protected override void OnButtonClicked()
    {
        GameManager.Instance.ResetGame();
    }
}
