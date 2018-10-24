using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeIcon : MonoBehaviour {

    public Sprite oldIcon;
    public Sprite newIcon;

    public void Change (bool icon_new)
    {
        if (!icon_new)
            GetComponent<SpriteRenderer>().sprite = newIcon;
        else
            GetComponent<SpriteRenderer>().sprite = oldIcon;
    }
}
