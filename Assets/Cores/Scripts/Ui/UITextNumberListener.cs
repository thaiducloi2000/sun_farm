using EventBus;
using TMPro;
using UnityEngine;

public struct UITextNumberData : IEventUIData
{
    public int OldNumber;
    public int NewNumber;
} 
public class UITextNumberListener : UIListener<UITextNumberData>
{
    [SerializeField] private TextMeshProUGUI m_text;

    public void UITextNumberListenerCallBack(UITextNumberData data)
    {
        m_text.SetText(data.NewNumber.ToString());
    }
}
