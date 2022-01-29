using UnityEngine;

public class MouseScreenRayProvider : MonoBehaviour, IRayProvider
{
    public Ray CreateRay()
    {
        //return Camera.main.ScreenPointToRay(Input.mousePosition);
        return new Ray(Camera.main.transform.position + Camera.main.transform.forward * 0.01f, Camera.main.transform.forward * 0.01f);
    }
}