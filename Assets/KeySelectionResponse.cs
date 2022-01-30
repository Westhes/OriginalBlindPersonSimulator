using UnityEngine;

internal class KeySelectionResponse : MonoBehaviour, ISelectionResponse
{
    private bool mousePressed;
    private bool grabbable;
    private bool playOnce = false;

    [SerializeField] public GameObject LockedGate;
    private Transform Key;
    [SerializeField] public AudioSource audioData;

    public void OnDeselect(Transform selection)
    {
        grabbable = false;

    }

    public void OnSelect(Transform selection)
    {
        grabbable = true;
        Key = selection;
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


        if (mousePressed && grabbable && !playOnce)
        {
            LockedGate.GetComponent<LockedDoor>().UnlockDoor();
            Key.gameObject.SetActive(false);
            audioData = GetComponent<AudioSource>();
            audioData.Play(0);
            grabbable = false;
            playOnce = true;
        }
    }
}