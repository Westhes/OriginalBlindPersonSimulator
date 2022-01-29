using UnityEngine;

internal class GrabDoorSelectionResponse : MonoBehaviour, ISelectionResponse
{
    private bool grabbable;
    private Transform selectableDoor;
    private bool mousePressed;
    [SerializeField] public float openDoorSpeed = 0.01f;

    public void OnDeselect(Transform selection)
    {
            grabbable = false;
        
    }

    public void OnSelect(Transform selection)
    {
            selectableDoor = selection;
            grabbable = true;
        
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePressed = true;
                      
            //Old rotation quaternion calculation
            //float newRotationY = selectableDoor.rotation.x + (Input.GetAxis("Mouse X") * openDoorSpeed);
            //Quaternion newRotation = new Quaternion (selectableDoor.rotation.x, newRotationY, selectableDoor.rotation.z, selectableDoor.rotation.w);
            //selectableDoor.rotation = newRotation;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mousePressed = false;
        }


        if (mousePressed && grabbable)
        {
            float force = -Input.GetAxis("Mouse X") * openDoorSpeed;

            if (selectableDoor != null)
            {
                if (Input.GetAxis("Mouse X") > 0)
                {
                    selectableDoor.GetComponent<Rigidbody>().AddForce(Vector3.up * openDoorSpeed, ForceMode.VelocityChange);
                }
                else if(Input.GetAxis("Mouse X") < 0)
                {
                    selectableDoor.GetComponent<Rigidbody>().AddForce(Vector3.down * openDoorSpeed, ForceMode.VelocityChange);
                }
            }
        }
    }
}