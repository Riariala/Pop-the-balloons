using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class TouchBalloon : MonoBehaviour
{
    public Text scoreText;
    public int score;
    private int bonus;
    private float lastTime;

    void Start()
    {
        bonus = 0;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 pos = Input.mousePosition;
            Vector2 ray = Camera.main.ScreenToWorldPoint(pos);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.tag);
                    switch (hit.collider.tag)
                    {
                        case "balloon":
                            hit.collider.tag = "killed";
                            hit.collider.GetComponent<BalloonBehaviour>().killTheBalloon();
                            ScoreUp();
                            break;
                        case "balloon_clone":
                            hit.collider.tag = "killed";
                            hit.collider.GetComponent<BalloonCloneBeh>().killTheBalloon();
                            ScoreUp();
                            break;
                        case "killed":
                            break;
                        default:
                            lastTime = 0;
                            bonus = 0;
                            break;
                    }
                }
                else
                {
                    lastTime = 0;
                    bonus = 0;
                }
            }
        }
    }

    public void ScoreUp()
    {
        if (lastTime != 0)
        {
            float changeTime = Time.realtimeSinceStartup - lastTime;
            if (changeTime < 1f)
            {
                bonus++;
            }
            else { bonus = 0; }
            Debug.Log(bonus);
        }
        score += bonus + 1;
        scoreText.text = score.ToString();
        lastTime = Time.realtimeSinceStartup;
    }
}
