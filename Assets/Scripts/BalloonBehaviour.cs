using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBehaviour : MonoBehaviour
{

    public GameObject clone;
    public GameObject prefab_clone;
    public Menubehaviour menuBeh;

    private Camera cam;
    public Transform objtransfm;
    public Animator anim;
    private Rigidbody2D _rb;

    public float velocityY;
    public float velocityshiftX;
    public float timeShift;
    public Color balloon_color;

    void Start()
    {
        cam = Camera.main;
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 point = cam.WorldToViewportPoint(transform.position);
        if (point.y > 1.2f)
        {
            GameObject.Find("ObjectManager").GetComponent<Menubehaviour>().gameOver();
            Destroy(gameObject);
            if (clone != null) { Destroy(clone); }
        }

        if (clone != null)
        {
            if (point.x > 0.1 && point.x < 0.9) { Destroy(clone); }
        }
        else
        {
            if (point.x < 0.1)
            {
                Vector2 clonePos = new Vector2((float)Screen.width / Screen.height * 6f, objtransfm.position.y);
                CreateClone(clonePos);
            }
            else if (point.x > 0.9)
            {
                Vector2 clonePos = new Vector2(-(float)Screen.width / Screen.height * 6f, objtransfm.position.y);
                CreateClone(clonePos);
            }
        }
    }

    public void Generate(float velY, float timeSh, float velshX, Color color, float speed)
    {
        objtransfm = transform;
        _rb = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        balloon_color = color;
        SetNewColor(balloon_color);
        velocityY = velY * speed;
        timeShift = timeSh;
        velocityshiftX = velshX;
        StartCoroutine(change_dirctn(timeSh, velshX));
    }

    public void killTheBalloon()
    {
        anim.Play("Balloon_popd");
        if (clone != null) { clone.GetComponent<BalloonCloneBeh>().anim.Play("Balloon_popd"); }
    }

    public void destroyBalloon()
    {
        Destroy(gameObject);
    }

    private void SetNewColor(Color newColor)
    {
        objtransfm.Find("tail").Find("tail_ins").GetComponent<SpriteRenderer>().color = newColor;
        objtransfm.Find("body").GetComponent<SpriteRenderer>().color = newColor;
    }

    public void CreateClone(Vector2 clonePos)
    {
        clone = Instantiate(prefab_clone, clonePos, Quaternion.identity, transform.parent) as GameObject;
        BalloonCloneBeh cloneBeh = clone.GetComponent<BalloonCloneBeh>();
        cloneBeh.Generate(balloon_color, _rb.velocity);
        cloneBeh.parent = gameObject;
    }

    IEnumerator change_dirctn(float time, float changePosx)
    {
        float maxShift = Mathf.Abs(changePosx * 10);
        float currShift = 0f;
        while (true)
        {
           
            yield return new WaitForSeconds(time);
            if (Mathf.Abs(currShift) >= maxShift) { changePosx = - changePosx; }
            currShift = currShift + changePosx;
            _rb.velocity = new Vector2(currShift, velocityY);
            if (clone != null)
            {
                clone.GetComponent<Rigidbody2D>().velocity = _rb.velocity;
            }
        }
    }
}
