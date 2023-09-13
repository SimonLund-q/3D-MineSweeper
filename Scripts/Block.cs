using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public int count;
    public TMP_Text bombs;
    public bool isSelected = false;
    public Generation gen;

    public Renderer ren;

    private float currentTime = 0.1f;
    private bool hasSpawned = false;
    // Update is called once per frame
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
            if (currentTime >= 0f){
                currentTime = currentTime -= Time.deltaTime;
            }
            
            if (currentTime <= 0f && count == 0){
                    if (hasSpawned == false){
                        Destroy(gameObject, 1);
                        gen.remainingBlocks = gen.remainingBlocks -1;
                        hasSpawned = true;
                    }
                    
            }
            if (currentTime <= 0f && count >= 1)
            {
                    if (hasSpawned == false)
                    {
                         bombs.text = count.ToString();
                         Instantiate(bombs, transform.position, Quaternion.identity);
                         hasSpawned = true;
                    }
                
            }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bomb")
        {
            count = count + 1;            
        }
        
    }
     void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Main");
        gen = g.GetComponent<Generation>();
        isSelected = false;
    }
   
}
