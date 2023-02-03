using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Clockable : MonoBehaviour
{
    protected void Start()
    {
        Debug.Log("parent clockable start");
        Debug.Log("this : " + this);
        Debug.Log(Clock.Instance);
        Clock.Instance.Register(this);

    }

    public abstract void Action();
}

public class Clock : MonoBehaviour
{
    public int Timer { get; private set; }
    public static Clock Instance { get; private set; }
    void Awake() 
    {
        Timer = 0;
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        {
            Debug.Log("Destroying");
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
            Debug.Log("instance set" + Instance);
        } 
    }

    List<Clockable> objects;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("parent clock start");
        objects = new List<Clockable>();
        StartCoroutine(Tick());
    }

    public void Register(Clockable obj)
    {
        objects.Add(obj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Tick()
    {
        while (true)
        {
            Timer++;
            objects.ForEach(delegate(Clockable e)
            {
                e.Action();
            });
            yield return new WaitForSeconds(0.5f);
        }
    }
}
