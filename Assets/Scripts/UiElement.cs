using UnityEngine.UI;

public interface IUiElement
{
    void OnGUI();
}

public class UiTextElement: IUiElement {

    public UiTextElement(Text text, string label)
    {
        textElement = text;
    }

    private Text textElement;
    private string label;

    public void UpdateLabel(string changed)
    {
        textElement.text = label + changed;
    }

    public void OnGUI()
    {
        throw new System.NotImplementedException();
    }
}
