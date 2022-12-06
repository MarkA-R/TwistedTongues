using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wordEmitter : MonoBehaviour
{
    public bool emit = true;
    public string text;
    public Transform origin;
    public float secondsBetweenWords = 1;
    public float wordSpeed = 5;
    GameObject player;
    public Transform destination;
    string endingChars = ".,!? ";
    float timeBetweenWords = 0;
    public float precision = 5;
    public Gradient colourChange;
    public bool repeat = false;
    string repeatString = "";
    List<string> words = new List<string>();
    int wordIndex = 0;
    // Start is called before the first frame update
    /*
     * for (int i = 0; i < text.TrimEnd().Length; i++)
        {
            if (text.Length == 0 || text.TrimEnd().Length == 0)
            {
                if (repeat)
                {
                    text = repeatString;
                    break;
                }
            }
     */
    void Start()
    {
        player = GameManager.instance.player;   
        if(destination == null)
        {
            destination = player.transform;
        }
        if (repeat)
        {
            repeatString = text;
        }
        string prevWord = "";
        for(int c = 0; c < text.Length; c++)
        {
            if((' ').Equals(text[c]))
            {
                //Debug.Log(prevWord);
                words.Add(prevWord);
                prevWord = "";
                continue;

            }
            prevWord += text[c];

        }
        words.Add(prevWord);
    }

    // Update is called once per frame
    void Update()
    {
        if (!emit || text.Length < 0)
        {
            return;
        }
        timeBetweenWords += Time.deltaTime;
        if(timeBetweenWords < secondsBetweenWords)
        {
            return;
        }
        timeBetweenWords = 0;
        wordIndex++;
        if(wordIndex>= words.Count && repeat)
        {
            wordIndex = 0;
        }

        string outputWord = words[wordIndex];
        

        GameObject newWord = new GameObject();
        //newWord.AddComponent<TextMesh>();
        //newWord.GetComponent<TextMesh>().text = outputWord;
        
        //newWord.GetComponent<TextMesh>().color = colourChange.Evaluate(gradientPercentage);
        newWord.AddComponent<BoxCollider>();
        newWord.GetComponent<BoxCollider>().size = new Vector3(0.3f, 0.3f, 0.3f);
        newWord.GetComponent<BoxCollider>().isTrigger = true;
        newWord.transform.position = origin.position;
        Vector3 velocity = Vector3.zero;
        Vector3 straightPath = destination.position - origin.transform.position;

        velocity = (destination.position + new Vector3(Random.Range(-precision, precision), Random.Range(-precision, precision), Random.Range(-precision, precision))) - origin.position;
        velocity.Normalize();
        straightPath.Normalize();
        newWord.transform.forward = (velocity - straightPath).normalized; // Vector3.Cross, Vector3.up);
        Quaternion newRot = Quaternion.LookRotation(newWord.transform.forward);
        //newRot.x = 0;
        //newRot.z = 0;
        newWord.transform.rotation = newRot;
        newWord.AddComponent<word>();
        newWord.GetComponent<word>().velocity = velocity;
        newWord.GetComponent<word>().speed = wordSpeed;
        newWord.GetComponent<word>().text = outputWord;
        newWord.GetComponent<word>().makeModel(colourChange.Evaluate((float)wordIndex /(float)words.Count));
        



    }

    
}


/*
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wordEmitter : MonoBehaviour
{
    public bool emit = true;
    public string text;
    public Transform origin;
    public float secondsBetweenWords = 1;
    public float wordSpeed = 5;
    GameObject player;
    public Transform destination;
    string endingChars = ".,!? ";
    float timeBetweenWords = 0;
    public float precision = 5;
    public Gradient colourChange;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;   
        if(destination == null)
        {
            destination = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!emit || text.Length < 0)
        {
            return;
        }
        timeBetweenWords += Time.deltaTime;
        if(timeBetweenWords < secondsBetweenWords)
        {
            return;
        }
        timeBetweenWords = 0;
        bool isEnding = false;
        for (int i = 0; i < text.Length; i++)
        {
            
            for (int u = 0; u < endingChars.Length; u++)
            {
                if (endingChars[u].Equals(text[i]))
            {
                isEnding = true;
                    
                    break;
            }

                

            }
            if(i == text.Length - 1)
            {
                isEnding = true;
               
            }
            i++;
            if (isEnding)
            {
                string outputWord = text.Substring(0, i);
                text = text.Substring(i, (text.Length - i));

                GameObject newWord = new GameObject();
                newWord.AddComponent<TextMesh>();
                newWord.GetComponent<TextMesh>().text = outputWord;
                float gradientPercentage = (float)i / (float)text.Length;
                newWord.GetComponent<TextMesh>().color = colourChange.Evaluate(gradientPercentage);
                newWord.AddComponent<BoxCollider>();
                newWord.GetComponent<BoxCollider>().size = new Vector3(0.3f, 0.3f, 0.3f);
                newWord.transform.position = origin.position;
                Vector3 velocity = Vector3.zero;
                Vector3 straightPath = destination.position - origin.transform.position;
                
                velocity = (destination.position + new Vector3(Random.Range(-precision, precision), Random.Range(-precision, precision), Random.Range(-precision, precision))) - origin.position;
                velocity.Normalize();
                straightPath.Normalize();
                newWord.transform.forward = (velocity - straightPath).normalized; // Vector3.Cross, Vector3.up);
                Quaternion newRot = Quaternion.LookRotation(newWord.transform.forward);
                //newRot.x = 0;
                //newRot.z = 0;
                newWord.transform.rotation = newRot;
                newWord.AddComponent<word>();
                newWord.GetComponent<word>().velocity = velocity;
                newWord.GetComponent<word>().speed = wordSpeed;

                break;
            }
        }
    }
}
*/
