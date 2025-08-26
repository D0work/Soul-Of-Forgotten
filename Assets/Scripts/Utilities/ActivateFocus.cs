using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFocus : MonoBehaviour
{
    public FocusZones focusZones;
    public void ActivateObjectFocus()
    {
        focusZones.SetFocus(this.transform);
    }
}
