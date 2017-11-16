using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUiElement
{

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
}
