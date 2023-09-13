using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Generation : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public int depth = 10;
    public static int lives;

    public GameObject[] Cube;
    public static int score;
    public int bombCount;
    public TMP_Text congratulations;
    public TMP_Text Bomb;
    public TMP_Text livesText;
    public TMP_Text timeTaken;
    
    public Block block;
    public Bomba bomb;
    public bool hasSelected = false;
    private bool hasFinished = false;

    public int remainingBlocks;
    public float time = 0f;
    public float countDown = 2f;
    public float gameSoundLoop;

    public AudioSource sfx;
    public AudioSource sfxx;
    public AudioClip sfx1;
    public AudioClip sfx2;

    void Start()
    {
        lives = 3;
         width = Main_Menu.width;
         height = Main_Menu.height;
         depth = Main_Menu.depth;

         score = width * height * depth;

        transform.position = new Vector3(width/2 - 0.5f, height/2 - 0.5f, depth/2 - 0.5f);
        for (int z = 0; z <depth;z++)
        {
            for (int y = 0; y <height;y++)
            {
                for(int x = 0; x < width; x++)
                {
                     int i = Random.Range(0, 9);
                     Instantiate(Cube[i], new Vector3(x, y, z), Quaternion.identity);
                     remainingBlocks = remainingBlocks + 1;
                }
            }
        }
    }
    public void Update()
    {
        gameSoundLoop = gameSoundLoop - Time.deltaTime;
        if (gameSoundLoop <= 0f){
            sfxx.clip = sfx2;
            sfxx.Play();
            gameSoundLoop = 101f;
        }
        
        if (hasFinished == false){
            time = time + Time.deltaTime;
        }
        timeTaken.text = "Time: " + time.ToString("0") + "s";
        if (remainingBlocks == 0){
            hasFinished = true;
            congratulations.text = "Congratulations! You did it in " + time.ToString("0") +"s";
            countDown = countDown - Time.deltaTime;
            if(countDown <= 0f){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

            }
            
        } 
            if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    CurrentClickedGameObject(raycastHit.transform.gameObject);
                }
            }
        }
        else if (Input.GetMouseButtonDown(2))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    CurrentSelectedObject(raycastHit.transform.gameObject);
                }
            }
        }
        void CurrentSelectedObject(GameObject gameObject){
            if (gameObject.tag == "Block")
            {
                block = gameObject.GetComponent<Block>();
                    if (block.isSelected == false && hasSelected == true){
                        hasSelected = false;
                    }else if (block.isSelected == true && hasSelected == false){
                        hasSelected = true;
                    }
                
                    if (hasSelected == false){
                        block = gameObject.GetComponent<Block>();
                        block.isSelected = true;
                        hasSelected = true;
                        bombCount = bombCount - 1;
                    }
                    else if (hasSelected == true){
                        block = gameObject.GetComponent<Block>();
                        block.isSelected = false;
                        hasSelected = false;
                        bombCount = bombCount + 1;
                    }   
            }
            if (gameObject.tag == "Bomb"){
                bomb = gameObject.GetComponent<Bomba>();
                    if (bomb.isSelected == false && hasSelected == true){
                        hasSelected = false;
                    }else if (bomb.isSelected == true && hasSelected == false){
                        hasSelected = true;
                    }
                
                    if (hasSelected == false){
                        bomb = gameObject.GetComponent<Bomba>();
                        bomb.isSelected = true;
                        hasSelected = true;
                        bombCount = bombCount - 1;
                    }
                    else if (hasSelected == true){
                        bomb = gameObject.GetComponent<Bomba>();
                        bomb.isSelected = false;
                        hasSelected = false;
                        bombCount = bombCount + 1;
                    }
                    Debug.Log("Boom");
            } 
            

        }
        void CurrentClickedGameObject(GameObject gameObject)
        {
            if(gameObject.tag == "Block")
            {
                block = gameObject.GetComponent<Block>();
                Destroy(gameObject);
                remainingBlocks = remainingBlocks - 1;
                if (block.isSelected == true){
                    bombCount ++;
                }
            }
            if(gameObject.tag == "Bomb"){
            lives = lives - 1;
            sfx.clip = sfx1;
            sfx.Play();
            if (lives <= 0){
                lives--;
                Debug.Log ("Game Over. Score:");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
            }
        }
        Bomb.text = "Remaining Bombs: " + bombCount.ToString();
        livesText.text = "Remaining Lives: " + lives.ToString();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bomb")
        {
            remainingBlocks = remainingBlocks - 1; 
            bombCount = bombCount + 1;
        }
        
    }
}

