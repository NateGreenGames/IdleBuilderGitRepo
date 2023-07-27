using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditWidgetPassthrough : MonoBehaviour
{
   public void CloseCredits()
    {
        GameManager.gm.canvasMan.CloseCreditsWidget();
        GameManager.gm.canvasMan.PlayClickEffect();
    }
}
