using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonCloneBeh : MonoBehaviour
{
    public GameObject parent;
    private Camera cam;
    private Rigidbody2D _rb;
    public Transform objtransfm;
    public Animator anim;
    public Color balloon_color;

    void Start()
    {
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        Vector2 point = cam.WorldToViewportPoint(transform.position);
        if (point.x > 0.1 && point.x < 0.9) 
        {
            parent.transform.position = objtransfm.position;
            Destroy(gameObject); 
        }
    }

    public void Generate(Color color, Vector2 veloc)
    {
        objtransfm = transform;
        _rb = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        balloon_color = color;
        SetNewColor(balloon_color);
        _rb.velocity = veloc;
    }

    private void SetNewColor(Color newColor)
    {
        objtransfm.Find("tail").Find("tail_ins").GetComponent<SpriteRenderer>().color = newColor;
        objtransfm.Find("body").GetComponent<SpriteRenderer>().color = newColor;
    }

    public void killTheBalloon()
    {
        anim.Play("Balloon_popd");
        if (parent != null)
        {
            parent.GetComponent<BalloonBehaviour>().anim.Play("Balloon_popd");
        }
    }

    public void destroyBalloon()
    {
        Destroy(gameObject);
    }
}
