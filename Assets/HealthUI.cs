using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image ballHealth;
    public Sprite fullBall; //full amount of sprites
    public Sprite emptyBall; //empty yarn sprite 

    private List<Image> balls = new List<Image>();
    public void setMaxBalls(int maxBalls)
    {
        foreach(Image ball in balls)
        {
            Destroy(ball.gameObject);
        }

        balls.Clear();

        for (int i=0; i < maxBalls; i++)
        {
            Image newBall = Instantiate(ballHealth, transform);
            newBall.sprite = fullBall;
            newBall.color = Color.white;
            balls.Add(newBall);
        }

    }

public void UpdateBalls(int currentBalls)
    {
        for(int i = 0; i < balls.Count; i++)
        {
            if(i < currentBalls)
            {
                balls[i].sprite = fullBall;
                balls[i].color = Color.white;

            }
            else
            {
                balls[i].sprite = emptyBall;
                balls[i].color = Color.black;

            }
        }
    }

}
