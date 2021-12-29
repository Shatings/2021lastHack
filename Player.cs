using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    public Collider2D colle;
    public Rigidbody2D rigy;

    private Vector3 vector;

    private bool jump;
    private int jumpcount;
    
   
    private int itemindex;

    public Transform checky;
    
    public Transform respown;
  

    public GameM gmem;
    public GameObject esekey;


    public int hp;

    void Start()
    {
        colle = GetComponent<Collider2D>();
        rigy = GetComponent<Rigidbody2D>();
        gmem = FindObjectOfType<GameM>();
        hp = 100;
        
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
            transform.position += vector * speed * Time.deltaTime;
        }
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
    public bool CheckY(GameObject y, Transform _transform)
    {
        if (y.transform.position.y < checky.position.y)
        {
            Debug.Log("성공");
            y.transform.position=_transform.position;
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
        
        testJump();
        CheckY(this.gameObject, respown);
    }
  

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
           
            jumpcount=1;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (!gmem.endgame)
        {
            if (collision.gameObject.tag == "Emy")
            {
                SoundM.instanse.SoundEff(0);
                hp -= 10;
                collision.gameObject.transform.position = FindObjectOfType<GameM>().emysrespwn.position;
                collision.gameObject.SetActive(false);

            }
            if (collision.gameObject.tag == "Goal")
            {
                hp += 10;
                collision.gameObject.SetActive(false);
            }
        }
    }

}
