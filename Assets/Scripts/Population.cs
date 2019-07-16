using UnityEngine;
using UnityEngine.UI;

public class Population : MonoBehaviour {

    public GameObject[] dots;
    public GameObject blackDot;
    public int gen = 1;
    public int bestDot = 0;
    public int minStep = 1000;
    public float totalFitness = 0;
    public Text step;
    public Text generation;


	// Use this for initialization
	void Start () {
        dots = new GameObject[500];
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i] = Instantiate(blackDot, new Vector3(0, 0, 0), Quaternion.identity);
        }
        StartMove();
	}
	
	// Update is called once per frame
	void Update () {
        generation.text = "Gen:" + gen;
        
        if (isAllDotsDead())
        {
            FindBest();
            step.text = "Step:" + dots[bestDot].GetComponent<Dot>().finalStep;
            CreateNewGen();
            calcTotalFitness();
            StartAgain();
            bestDot = 0;
            gen++;
        }
        
	}

    void calcTotalFitness()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            totalFitness += dots[i].GetComponent<Dot>().fitness;
        }
        totalFitness = 0;
    }

    void StartAgain()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].GetComponent<Dot>().StartAgain();
        }

    }
    public void StartMove()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].GetComponent<Dot>().isStart = true;
        }
    }

    private bool isAllDotsDead()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            if (!dots[i].GetComponent<Dot>().isDead)
                return false;
        }
        return true;
    }

    private void FindBest()
    {
        bool reached = false;
        int temp = 0;
        for (int i = 0; i < dots.Length; i++)
        {
            if (dots[i].GetComponent<Dot>().reachedGoal && dots[i].GetComponent<Dot>().finalStep <= dots[temp].GetComponent<Dot>().finalStep)
            {
                temp = i;
                reached = true;
            }
        }
        if (!reached)
        {
            for (int i = 0; i < dots.Length; i++)
            {
                if(dots[i].GetComponent<Dot>().fitness > dots[temp].GetComponent<Dot>().fitness)
                {
                    temp = i;
                }
            }
        }
        bestDot = temp;
    }

    void CreateNewGen()
    {
        Vector3[] bestDirects = dots[bestDot].GetComponent<Dot>().directions;

        for (int i = 0; i < bestDirects.Length; i++)
        {
            dots[0].GetComponent<Dot>().directions[i] = bestDirects[i];
        }


        for (int i = 1; i < dots.Length; i++)
        {

            dots[i].GetComponent<Dot>().newDirections(bestDirects);
        }

    }

}
