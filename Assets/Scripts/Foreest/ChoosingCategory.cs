using UnityEngine;
using UnityEngine.UI;

public class ChoosingCategory : MonoBehaviour
{
    public Button thisButton;

    public Button meatButton;
    public Button veggiesButton;
    public Button grainsButton;
    public Button noneButton;

    public void chooseButton()
    {
        if (thisButton.image.color != new Color(1, 1, 1))
        {
            thisButton.image.color = new Color(1, 1, 1);
        }
        else
        {
            thisButton.image.color = new Color(0.158f, 0.800f, 0.128f);
            noneButton.image.color = new Color(1, 1, 1);
        }
    }

    public void NONEButton()
    {
        if (thisButton.image.color != new Color(1, 1, 1))
        {
            thisButton.image.color = new Color(1, 1, 1);
        }
        else
        {
            thisButton.image.color = new Color(0.158f, 0.800f, 0.128f);
            meatButton.image.color = new Color(1, 1, 1);
            veggiesButton.image.color = new Color(1, 1, 1);
            grainsButton.image.color = new Color(1, 1, 1);
        }
    }
}
