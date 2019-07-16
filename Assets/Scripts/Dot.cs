using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour {

    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acc;
    public Vector3[] directions;
    public bool isStart = false;
    public bool isDead = false;
    public float fitness = 0;
    public bool reachedGoal = false;
    public bool isBest = false;
    public int initialStep = 0;
    public int finalStep = 1000;

    // Use this for initialization
    void Start () {
        createDirections();
        position = new Vector3(0, -4f, 0);
        transform.position = position;
    }
	
    void createDirections()
    {
        directions = new Vector3[1000];
        FillDirections();
    }

    void FillDirections()
    {
        for (int i = 0; i < directions.Length; i++)
        {
            float randomAngle = Random.Range(-Mathf.PI, Mathf.PI);
            directions[i] = new Vector3(Mathf.Cos(randomAngle) / 300, Mathf.Sin(randomAngle) / 300, 0);
        }
    }

    public void StartAgain()
    {
        position = new Vector3(0, -4f, 0);
        isDead = false;
        fitness = 0;
        reachedGoal = false;
        isBest = false;
        finalStep = 1000;
        initialStep = 0;
        isStart = true;
        velocity = new Vector3(0, 0, 0);
        transform.position = position;
    }

    public void Show()
    {
        transform.position = position;
    }

    public void Move()
    {
        if (directions.Length > initialStep)
        {
            acc = directions[initialStep];
            initialStep++;
        }
        else
            isDead = true;
        velocity = new Vector3(velocity.x + acc.x, velocity.y + acc.y, 0);
        position = new Vector3(position.x + velocity.x, position.y + velocity.y, 0);

    }

    // Update is called once per frame
    void Update()
    {
       if (transform.position.x < 1.1f && (transform.position.y < -0.9f && transform.position.y > -1.1f)
           || transform.position.x > -0.8f && (transform.position.y < 1.6f && transform.position.y > 1.4f))
            isDead = true;

        if (isStart && !isDead)
        {
            Move();
            if (transform.position.x <= -2.65 || transform.position.x >= 2.65 || transform.position.y <= -4.75 || transform.position.y >= 4.75)
                isDead = true;
            Show();
        }
        if (Mathf.Abs(transform.position.x - 0) <= 0.1f && Mathf.Abs(transform.position.y - 4.5f) <= 0.1f)
        {
            reachedGoal = true;
            isDead = true;
            finalStep = initialStep;
        }
        if (isDead)
        {
            isStart = false;
            CalculateFitness();
        }
    }
    public void CalculateFitness()
    {

       float distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - 0, 2) + Mathf.Pow(transform.position.y - 4.5f, 2));
       fitness = 1 / (distance * distance);

    }

    public void newDirections(Vector3[] news)
    {
        int mutateRatio = 100;
        for (int i = 0; i < news.Length; i++)
        {
            if (mutateRatio > Random.Range(0, 1000))
            {
                float randomAngle = Random.Range(-Mathf.PI, Mathf.PI);
                directions[i] = new Vector3(Mathf.Cos(randomAngle) / 300, Mathf.Sin(randomAngle) / 300, 0);
            }
            else
                directions[i] = news[i];
        }
    }

}
