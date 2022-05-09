using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalloon : MonoBehaviour
{
    public GameObject baloon_prefab;
    public Transform baloon_parent;
    public GameObject obj;

    public float speed;
    private List<Color> colorBank;

    void Start()
    {
        speed = 0.5f;
        colorBank = new List<Color>();
        Generatecolors();
        StartCoroutine(spawn_new());
    }

    IEnumerator spawn_new()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(Random.Range(0.15f/ speed, 2f/ speed));
            speed += 0.003f;
        }
    }

    public void Spawn()
    {
        Vector2 newpos = new Vector2(Random.Range(-2f, 2f), -8f);
        obj = Instantiate(baloon_prefab, newpos, Quaternion.identity, baloon_parent) as GameObject;
        float velY = Random.Range(0.5f, 1.5f);
        float timeSh = Random.Range(0.05f, 0.3f);
        float velshX = Random.Range(-0.15f, 0.15f);
        obj.GetComponent<BalloonBehaviour>().Generate(velY, timeSh, velshX, colorBank[Random.Range(0, colorBank.Count)]/255, speed);
    }

    public void Generatecolors()
    {
        colorBank.Add(new Color(251, 229, 172, 255));
        colorBank.Add(new Color(254, 179, 96, 255));
        colorBank.Add(new Color(148, 175, 196, 255));
        colorBank.Add(new Color(247, 195, 218, 255));
        colorBank.Add(new Color(247, 195, 218, 255));
        colorBank.Add(new Color(255, 233, 173, 255));
        colorBank.Add(new Color(229, 146, 162, 255));
        colorBank.Add(new Color(217, 182, 224, 255));
        colorBank.Add(new Color(130, 116, 201, 255));
        colorBank.Add(new Color(186, 217, 150, 255));
        colorBank.Add(new Color(128, 209, 199, 255));
        colorBank.Add(new Color(247, 209, 199, 255));
        colorBank.Add(new Color(173, 199, 229, 255));
        colorBank.Add(new Color(182, 247, 150, 255));
    }

}
