using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPage : MonoBehaviour {

    //Coroutine variables
    private float distance;
    private Vector2 initialPosition;
    private Vector2 objectivePosition;
    public float translationTime;
    private Vector2 initialPositionprevious;
    private Vector2 objectivePositionprevious;

    //List gameObjects
    private GameObject prefab;
    private GameObject previousGameobject;
    public GameObject[] fases;


    public void TransitionNext (int pag)
    {
        //Select element previous 
        if (pag > 1)
            // return element to the main menu fases[1]
            previousGameobject = fases[1];
        else
            previousGameobject = fases[pag - 1];

        //Select prefab element from list gameobjects
        prefab = fases[pag];

        //Start coroutine moving left objects
        StartCoroutine(LateralScrollLeft());

    }


    public void TransitionBack(int pag)
    {
        //Select element previous 
        previousGameobject = prefab;

        //Select prefab element from list gameobjects
        prefab = fases[pag];

        Debug.Log(prefab.name + "prefab");
        Debug.Log(previousGameobject.name + "previous");
        //Start coroutine moving right objects
        StartCoroutine(LateralScrollRight());
    }

        private IEnumerator LateralScrollLeft()
        {
            // Initialize
            float actualTime = 0;
            distance = prefab.GetComponent<RectTransform>().sizeDelta.x;

            //prefab positions
            initialPosition = prefab.GetComponent<RectTransform>().transform.position;
            objectivePosition = prefab.GetComponent<RectTransform>().transform.position;
            objectivePosition = new Vector3(objectivePosition.x - distance, objectivePosition.y);

        //previousGameobject positions
        initialPositionprevious = previousGameobject.GetComponent<RectTransform>().transform.position;
        objectivePositionprevious = previousGameobject.GetComponent<RectTransform>().transform.position;
        objectivePositionprevious = new Vector3(objectivePositionprevious.x - distance, objectivePositionprevious.y);


        // Loop
        while (actualTime < translationTime)
            {
                previousGameobject.transform.position = Vector3.Lerp(initialPositionprevious, objectivePositionprevious, actualTime / translationTime);
                prefab.transform.position = Vector3.Lerp(initialPosition, objectivePosition, actualTime / translationTime);
                actualTime += Time.deltaTime;
                yield return null;
            }


             // Closure
             prefab.transform.position = objectivePosition;
             previousGameobject.transform.position = objectivePositionprevious;
    }

    private IEnumerator LateralScrollRight()
    {
        // Initialize
        float actualTime = 0;
        distance = prefab.GetComponent<RectTransform>().sizeDelta.x;

        //prefab positions
        initialPosition = prefab.GetComponent<RectTransform>().transform.position;
        objectivePosition = prefab.GetComponent<RectTransform>().transform.position;
        objectivePosition = new Vector3(objectivePosition.x + distance, objectivePosition.y);

        //previousGameobject positions
        initialPositionprevious = previousGameobject.GetComponent<RectTransform>().transform.position;
        objectivePositionprevious = previousGameobject.GetComponent<RectTransform>().transform.position;
        objectivePositionprevious = new Vector3(objectivePositionprevious.x + distance, objectivePositionprevious.y);

        // Loop
        while (actualTime < translationTime)
        {
            previousGameobject.transform.position = Vector3.Lerp(initialPositionprevious, objectivePositionprevious, actualTime / translationTime);
            prefab.transform.position = Vector3.Lerp(initialPosition, objectivePosition, actualTime / translationTime);
            actualTime += Time.deltaTime;
            yield return null;
        }

        // Closure
        previousGameobject.transform.position = objectivePositionprevious;
        prefab.transform.position = objectivePosition;
    }
}
