using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Infinite : MonoBehaviour
{
    private GameObject mainCam;
    private int[] width = {165, 160, 185};
    [SerializeField] private GameObject deathParticles;
    private List<Transform> props1 = new List<Transform>(); 
    private List<Transform> props2 = new List<Transform>();
    private List<Transform> props3 = new List<Transform>();
    [SerializeField] private GameObject sky1;
    [SerializeField] private GameObject sky2;
    [SerializeField] private GameObject sky3;
    private List<GameObject> Sky = new List<GameObject>();


    private void Start()
    {
        // to find the props in plane1 
        foreach (Transform child in GameObject.Find("Plane1").transform)
        {
            if(child.gameObject.GetComponent<Collectable>() != null)
            {
                child.gameObject.SetActive(false);
                props1.Add(child);
            }
        } 
        foreach (Transform child in GameObject.Find("Plane2").transform)
        {
            if(child.gameObject.GetComponent<Collectable>() != null)
            {
                child.gameObject.SetActive(false);
                props2.Add(child);
            }
        } 
        foreach (Transform child in GameObject.Find("Plane3").transform)
        {
            if(child.gameObject.GetComponent<Collectable>() != null)
            {
                child.gameObject.SetActive(false);
                props3.Add(child);
            }
        }

        for (int i = props1.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = props1[i];
            props1[i] = props1[randomIndex];
            props1[randomIndex] = temp;
        }

        for (int i = props2.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = props2[i];
            props2[i] = props2[randomIndex];
            props2[randomIndex] = temp;
        }

        for (int i = props3.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = props3[i];
            props3[i] = props3[randomIndex];
            props3[randomIndex] = temp;
        }

        // active first 10 objects
        for (int i = 0; i < 10; i++)
        {
            props1[i].gameObject.SetActive(true);
            props2[i].gameObject.SetActive(true);
            props3[i].gameObject.SetActive(true);
        }

        Sky.Add(sky1);
        Sky.Add(sky2);
        Sky.Add(sky3);

        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        Vector3 floor_position = transform.position;
        Vector3 Plane1 = GameObject.Find("Plane1").transform.position;
        Vector3 Plane2 = GameObject.Find("Plane2").transform.position;
        Vector3 Plane3 = GameObject.Find("Plane3").transform.position;
        int current = 0;
        if(floor_position.x == Plane1.x)
        {
            current = 1;
        }
        else if(floor_position.x == Plane2.x)
        {
            current = 2;
        }
        else if(floor_position.x == Plane3.x)
        {
            current = 3;
        }
    
        if(mainCam.transform.position.x <= floor_position.x + 50 &&
           mainCam.transform.position.x >= floor_position.x + 20)
        {
            if(Plane1.x <= floor_position.x && Plane2.x <= floor_position.x 
            && Plane3.x <= floor_position.x)
            {
                int randomNumber = Random.Range(1, 4);
                while(randomNumber == current)
                {
                    randomNumber = Random.Range(1, 4);
                }
                //Debug.Log(randomNumber);
                if(randomNumber == 1)
                {
                    Plane1.x = floor_position.x + width[current - 1];
                    Sky[0].transform.position = new Vector3(Sky[current - 1].transform.position.x + 33, Sky[current - 1].transform.position.y, Sky[current - 1].transform.position.z);
                    for (int i = props1.Count - 1; i > 0; i--)
                    {
                        int randomIndex = Random.Range(0, i + 1);
                        Transform temp = props1[i];
                        props1[i] = props1[randomIndex];
                        props1[randomIndex] = temp;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        props1[i].gameObject.SetActive(true);
                    }


                    // foreach (Transform child in GameObject.Find("Plane1").transform)
                    // {
                    //     child.gameObject.SetActive(true);
                    //     if(child.gameObject.name.Equals("BreakableWall"))
                    //     {
                    //         GameObject newObject = Instantiate(deathParticles, child.gameObject.transform);
                    //         newObject.transform.position = GetChildPositionByName(child, "Graphic");
                    //         child.GetComponentInChildren<Breakable>().deathParticles = newObject;
                    //         child.GetComponentInChildren<Breakable>().health = 1;
                    //         newObject.SetActive(false);
                    //     }
                    // }   

                }
                else if(randomNumber == 2)
                {
                    Plane2.x = floor_position.x + width[current - 1];
                    Sky[1].transform.position = new Vector3(Sky[current - 1].transform.position.x + 33, Sky[current - 1].transform.position.y, Sky[current - 1].transform.position.z);
                    for (int i = props2.Count - 1; i > 0; i--)
                    {
                        int randomIndex = Random.Range(0, i + 1);
                        Transform temp = props2[i];
                        props2[i] = props2[randomIndex];
                        props2[randomIndex] = temp;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        props2[i].gameObject.SetActive(true);
                    }

                    // foreach (Transform child in GameObject.Find("Plane2").transform)
                    // {
                    //     child.gameObject.SetActive(true);
                    //     if(child.gameObject.name.Equals("BreakableWall"))
                    //     {
                    //         GameObject newObject = Instantiate(deathParticles, child.gameObject.transform);
                    //         newObject.transform.position = GetChildPositionByName(child, "Graphic");
                    //         child.GetComponentInChildren<Breakable>().deathParticles = newObject;
                    //         child.GetComponentInChildren<Breakable>().health = 1;
                    //         newObject.SetActive(false);
                    //     }
                    // }     

                }
                else
                {
                    Sky[2].transform.position = new Vector3(Sky[current - 1].transform.position.x + 33, Sky[current - 1].transform.position.y, Sky[current - 1].transform.position.z);
                    Plane3.x = floor_position.x + width[current - 1]; 
                    for (int i = props3.Count - 1; i > 0; i--)
                    {
                        int randomIndex = Random.Range(0, i + 1);
                        Transform temp = props3[i];
                        props3[i] = props3[randomIndex];
                        props3[randomIndex] = temp;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        props3[i].gameObject.SetActive(true);
                    } 

                    // foreach (Transform child in GameObject.Find("Plane3").transform)
                    // {
                    //     child.gameObject.SetActive(true);
                    //     if(child.gameObject.name.Equals("BreakableWall"))
                    //     {
                    //         GameObject newObject = Instantiate(deathParticles, child.gameObject.transform);
                    //         newObject.transform.position = GetChildPositionByName(child, "Graphic");
                    //         child.GetComponentInChildren<Breakable>().deathParticles = newObject;
                    //         child.GetComponentInChildren<Breakable>().health = 1;
                    //         newObject.SetActive(false);
                    //     }
                    // }   
                    
                }
            }
        }

        GameObject.Find("Plane1").transform.position = Plane1;
        GameObject.Find("Plane2").transform.position = Plane2;
        GameObject.Find("Plane3").transform.position = Plane3;  

    }

    public Vector3 GetChildPositionByName(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child.position;
            }
        }

        return new Vector3(0, 0, 0);
    }
}
