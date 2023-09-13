using UnityEngine;

public class Bomba : MonoBehaviour
{
    public bool isSelected;
    public Renderer ren;
    void Start()
    {

    }

    void Update()
    {
        if(isSelected == true){
                ren = GetComponent<Renderer>();
                ren.material.color = Color.red;
            }
            if (isSelected == false){
                ren = GetComponent<Renderer>();
                ren.material.color = Color.white;
            }
    }
}
