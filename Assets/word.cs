using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class word : MonoBehaviour
{
    public Vector3 velocity;
    public float speed;
    public string text;
    public float scalar = 0.1f;
    Vector3 movePlace = new Vector3(-1, 0, 0);
    float life = 7f;
    float currentLife = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void makeModel(Color c)
    {
        float bounds = 3f;
        List<GameObject> letters = new List<GameObject>();
        for(int i= 0; i < text.Length; i++)
        {
            if (!"ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(text[i]))
            {
                continue;
            }
            int charLetter = text[i]-64;
            string toLoad = "alphabet_0";
            if (charLetter < 10)
            {
                toLoad += "0";
            }
             toLoad += charLetter.ToString();
           // Debug.Log(toLoad);
            GameObject newLetter = Instantiate(Resources.Load<GameObject>(toLoad));
            newLetter.transform.position = transform.position + ((movePlace * (scalar * bounds )) * i);
            newLetter.transform.parent = transform;
            newLetter.GetComponent<MeshRenderer>().material.color = c;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime * speed;
        currentLife += Time.deltaTime;
        if(currentLife >= life)
        {
            Destroy(gameObject);//fuck object pooling
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            healthScript.instance.removeHealth();
            Destroy(gameObject);
        }
        //Debug.Log("COLLIDE");
    }

}
