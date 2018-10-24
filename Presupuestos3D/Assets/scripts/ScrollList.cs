using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollList : MonoBehaviour {

    //Coroutine variables
    public float distance;
    private Vector2 initialPosition;
    private Vector2 objectivePosition;
    public float translationTime;

    //Distance to move items up o down
    public float heigh;

    //Gamobject instantiate prefab and list. 
    //It's necesary add prefab into newItem and into collection firts item
    public GameObject newItem;
    public List<GameObject> collection;
    public int value;

    //Position parent of Instatiate object
    private GameObject pare;

    public void PareObject(GameObject MoveDownObject)
    {
        pare = MoveDownObject;
    }

    public void Item (GameObject itemfromscence)
    {
        newItem = itemfromscence;
    }
 
    public void TypeOfItem (string text)
    {
        if (text == "calendario")
            value = 0;
        if (text == "gastos")
            value = 1;
        if (text == "entrega")
            value = 2;
        if (text == "mobiliario")
            value = 3;
    }


    //Add items to list and move down scroll
    public void AddItems(bool add)
    {
        if (add)
        {
        //Scroll down 
        StartCoroutine(ScrollDown());

        //Instantiate new object
        GameObject clone = Instantiate(newItem) as GameObject;

        //Set position new object down from parent position
        clone.transform.SetParent(pare.transform);
        clone.transform.position = newItem.transform.position;
        clone.transform.position = new Vector2(clone.transform.position.x, clone.transform.position.y - heigh);

        //Add new object to the list and select it like prefab item
        //newItem = clone;
        collection.Add(newItem);

        }
        else
        {
            Debug.Log("nada");

            //Limit to 1 list objects
            if (collection.Count != 0)
            {
                //Start coroutine moving up objects
                StartCoroutine(ScrollUp());

                //Remove objects from list 
                collection.Remove(newItem);

                //Remove objects from the scene
                Destroy(newItem);

                //Add last object into prefab item
                int last = collection.Count;
                newItem = collection[last - 1];

            }

        }
    }

    //Remove items from the list and move up scroll
    public void RemoveItems()
    { 
    }

    //Coroutine to down scroll and all of elements

    private IEnumerator ScrollDown()
    {
        // Initialize
        float actualTime = 0;
        initialPosition = pare.GetComponent<RectTransform>().transform.position;
        objectivePosition = pare.GetComponent<RectTransform>().transform.position;
        objectivePosition = new Vector2(objectivePosition.x, objectivePosition.y + distance);

        // Loop
        while (actualTime < translationTime)
        {
            pare.transform.position = Vector3.Lerp(initialPosition, objectivePosition, actualTime / translationTime);
            actualTime += Time.deltaTime;
            yield return null;
        }


        // Closure
        pare.transform.position = objectivePosition;

    }


    //Coroutine to up scroll and all of elements

        private IEnumerator ScrollUp()
        {
            // Initialize
            float actualTime = 0;
            initialPosition = pare.GetComponent<RectTransform>().transform.position;
            objectivePosition = pare.GetComponent<RectTransform>().transform.position;
            objectivePosition = new Vector2(objectivePosition.x, objectivePosition.y - distance);

            // Loop
            while (actualTime < translationTime)
            {
                pare.transform.position = Vector3.Lerp(initialPosition, objectivePosition, actualTime / translationTime);
                actualTime += Time.deltaTime;
                yield return null;
            }


            // Closure
            pare.transform.position = objectivePosition;
        }
    }
