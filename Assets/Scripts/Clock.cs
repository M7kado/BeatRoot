using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Clockable : MonoBehaviour
{
    protected void Start()
    {
        Debug.Log(this + " registering...");
        Clock.Instance.Register(this);
    }

    public abstract void Action();
}

public class Clock : MonoBehaviour
{

    [SerializeField] private float bpm = 15f;
    public bool playerCanMove { get; private set; }
    public PlayerActions playerAction { get; set; } = PlayerActions.NONE;

    public static Clock Instance { get; private set; }

    public int Timer { get; private set; }

    List<Clockable> objects;

    void Awake() 
    {
        Timer = 0;
        if (Instance != null && Instance != this) 
        {
            Debug.Log("Destroying");
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
            Debug.Log("instance set" + Instance);
            objects = new List<Clockable>();
            StartCoroutine(Tick());
        } 
    }

    public void Register(Clockable obj)
    {
        objects.Add(obj);
    }

    private IEnumerator Tick()
    {
        GetComponent<AudioSource>().Play();

        while (true)
        {
            Timer++;
            objects.ForEach(delegate(Clockable e)
            {
                e.Action();
            });
            playerCanMove = true;
            yield return new WaitForSeconds((60f / bpm) * 0.25f);
            playerCanMove = false;
            playerAction = PlayerActions.NONE;
            yield return new WaitForSeconds((60f / bpm) * 0.5f);
            playerCanMove = true;
            yield return new WaitForSeconds((60f / bpm) * 0.25f);
        }
    }

}
