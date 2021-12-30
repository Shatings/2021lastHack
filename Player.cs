using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    public Collider2D colle;
    public Rigidbody2D rigy;

    private Vector3 vector;

    private bool jump;
    private int jumpcount;
    
   
   

    public Transform checky;
    
    public Transform respown;

    public string type;
  

    public GameM gmem;
    public GameObject esekey;
    public Animator ani;


    public int hp;
    public GameObject hit;
    public float checktime;

    void Start()
    {
        type = "Player";
        colle = GetComponent<Collider2D>();
        rigy = GetComponent<Rigidbody2D>();
        gmem = FindObjectOfType<GameM>();
        hp = 100;
        ani=GetComponent<Animator>();
        hit.SetActive(false);
        
    }   
    public void Move()
    {
        if (gmem.waitmove)
        {
            return;
        }
        if (Input.GetAxisRaw("Horizontal")!=0) 
        {
            vector = Vector3.zero;
            vector = (Input.GetAxisRaw("Horizontal") > 0) ? Vector3.right : Vector3.left;
            transform.localScale = new Vector3((vector == Vector3.right) ? 1 : -1, transform.localScale.y);
            ani.SetBool("Run", true);

            transform.position += vector * speed * Time.deltaTime;
        }
        else
        {
            ani.SetBool("Run", false);
        }
    }
    public void Check(bool _chekc)
    {
        hit.SetActive(_chekc);
    }
    public void testJump()
    {
        if (!jump || jumpcount == 0)
        {
            return;
        }
        Debug.Log("점성");
        rigy.velocity = Vector2.zero;
        Vector2 jumpvector = new Vector2(0, gmem.jumppower);

        rigy.AddForce(jumpvector, ForceMode2D.Impulse);

        jump = false;
        jumpcount--;
    }
    public bool CheckY(GameObject y, Transform _transform,string _type)
    {
       
        if (y.transform.position.y < checky.position.y)
        {
            Debug.Log("성공");
            Debug.Log(_type);
            y.transform.position=_transform.position;
            if (_type.Equals("Player"))
            {
                hp = 0;
            }
            return true;
        }
        
            return false;
       
    }
    // Update is called once per frame
    void Update()
    {
       
        Move();
        
        if (Input.GetKeyDown(KeyCode.Space) && jumpcount > 0)
        {
            Debug.Log("점");
            jump = true;
            

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Canves();
        }

        if (hp <= 0)
        {
            gmem.endgame = true;
        }   
        
        
    }
    public void Canves()
    {
        esekey.SetActive(!esekey.activeSelf);
    }
    
    private void FixedUpdate()
    {
        if (hit.activeSelf)
        {
            checktime+=Time.deltaTime;
            if (checktime > 0.2f)
            {
                Check(false);
                checktime = 0;
            }
        }
        testJump();
        CheckY(this.gameObject, respown,type);
    }
  

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
           
            jumpcount=2;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (!gmem.endgame)
        {
            if (collision.gameObject.tag == "Emy")
            {
                SoundM.instanse.SoundEff(0);
                Check(true);
                hp -= 10;
                collision.gameObject.transform.position = FindObjectOfType<GameM>().emysrespwn.position;
                collision.gameObject.SetActive(false);

            }
            if (collision.gameObject.tag == "Goal")
            {
                hp += 10;
                SoundM.instanse.SoundEff(1);
                collision.gameObject.SetActive(false);
            }
        }
    }

}
