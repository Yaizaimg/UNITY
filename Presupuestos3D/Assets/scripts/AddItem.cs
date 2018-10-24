using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddItem : MonoBehaviour {

    public GameObject newItem;
    public float heigh;
    public List<GameObject> list;
    public GameObject last;
    public bool add;

    public void AddItemsBotton()
    {

        if (last.GetComponent<Toggle>().isOn == true)
        {
            if (last!=null)
            {
                GameObject clonelast = Instantiate(newItem) as GameObject;
                //Set position new object down from item references
                clonelast.transform.parent = last.transform.parent;
                clonelast.transform.position = new Vector2(last.transform.position.x, last.transform.position.y - heigh);
                list.Add(clonelast);
                last = clonelast;
                last.GetComponent<Toggle>().isOn = false;
            }
            else
            {
                //Instantiate new object
                GameObject clone = Instantiate(newItem) as GameObject;
                //Set position new object down from item references
                clone.transform.parent = newItem.transform.parent;
                clone.transform.position = new Vector2(newItem.transform.position.x, newItem.transform.position.y - heigh);
                list.Add(clone);
                last = clone;
                last.GetComponent<Toggle>().isOn = false;
            }
        }
        else
        {
            list.Remove(list[list.Count]);
        }
    }
}
