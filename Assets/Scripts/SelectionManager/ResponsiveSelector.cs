using System.Collections.Generic;
using UnityEngine;

public class ResponsiveSelector : MonoBehaviour, ISelector
{
    [SerializeField] private List<Selectable> selectables;
    //Default threshold for selecting each selectable in the list
    [SerializeField] private float threshold = 0.97f;
    
    private Transform _selection;

    public void Check(Ray ray)
    {
        _selection = null;

        float closest = 0f;
        
        //Calculate for all objects in the selectables list
        for (int i = 0; i < selectables.Count; i++)
        {
            Vector3 viewDirection = ray.direction;
            Vector3 distanceToSelectable = selectables[i].transform.position - ray.origin;
        
            float lookPercentage = Vector3.Dot(viewDirection.normalized, distanceToSelectable.normalized);

            //Display corresponding lookpercentage on the Canvas Text Objects
            selectables[i].LookPercentage = lookPercentage;

            //Do selection for objects that is close and player is looking nearby
            if (lookPercentage > threshold && lookPercentage > closest)
            {
                closest = lookPercentage;
                _selection = selectables[i].transform;
            }
        }
    }

    public Transform GetSelection()
    {
        return _selection;
    }
}