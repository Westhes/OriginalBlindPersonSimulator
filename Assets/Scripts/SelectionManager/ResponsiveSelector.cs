using System.Collections.Generic;
using UnityEngine;

public class ResponsiveSelector : MonoBehaviour, ISelector
{
    [SerializeField] private List<Selectable> selectables;
    //Default threshold for selecting each selectable in the list
    [SerializeField] private float threshold = 0.97f;
    [SerializeField] private float selectionRange = 5f;

    private Transform _selection;

    public void Check(Ray ray)
    {
        _selection = null;
        
        //Calculate for all objects in the selectables list
        for (int i = 0; i < selectables.Count; i++)
        {
            Vector3 selectableCenter = selectables[i].GetComponent<Renderer>().bounds.center;

            Vector3 viewDirection = ray.direction;
            Vector3 distanceToSelectable = selectableCenter - ray.origin;
            float lookPercentage = Vector3.Dot(viewDirection.normalized, distanceToSelectable.normalized);


            float distanceBetween = Vector3.Distance(selectableCenter, ray.origin);

            //Display corresponding lookpercentage on the Canvas Text Objects
            selectables[i].LookPercentage = lookPercentage;

            //Do selection if player is looking towards objects and that are close 
            if (lookPercentage > threshold && distanceBetween < selectionRange)
            {
                //closest = lookPercentage;
                _selection = selectables[i].transform;
            }
        }
    }

    public Transform GetSelection()
    {
        return _selection;
    }
}