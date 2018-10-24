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

    //Position parent of Instatiate object
    private GameObject pare;

    // Use this for initialization
    void Start()
    {
        pare = GameObject.Find("MoveDown");
    }

    //Add items to list and move down scroll
    public void AddItems()
    {

        //Scroll down 
        StartCoroutine(ScrollDown());

        //Instantiate new object
        GameObject clone = Instantiate(newItem) as GameObject;

        //Set position new object down from parent position
        clone.transform.SetParent(newItem.transform.parent);
        clone.transform.position = newItem.transform.position;
        clone.transform.position = new Vector2(clone.transform.position.x, clone.transform.position.y - heigh);

        //Add new object to the list and select it like prefab item
        newItem = clone;
        collection.Add(newItem);
    }

    //Remove items from the list and move up scroll
    public void RemoveItems()
    { 
        //Limit to 1 list objects
        if (collection.Count != 1)
        {
            //Start coroutine moving up objects
            StartCoroutine(ScrollUp());

            //Remove objects from list 
            collection.Remove(newItem);

            //Remove objects from the scene
            Destroy(newItem);

            //Add last object into prefab item
            int last = collection.Count;
            newItem = collection[last-1];

        }
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
