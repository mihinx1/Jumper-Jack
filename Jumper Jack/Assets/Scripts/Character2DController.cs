
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;


public class Character2DController : MonoBehaviour
{
	[Header("Layers")]
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private LayerMask wallLayer;

	[Header("Movement")]
	public float MovementSpeed = 1;
    bool facingRight = true;
	private float movement;
	private int extraJumps;
	public int extraJumpsValue;
	public float JumpForce = 1;
	private float wallJumpCooldown;

	private Rigidbody2D _rigidbody;
	private Animator anim;
	private BoxCollider2D boxCollider;
	
	[Header("Attack")]
	public Transform attackPoint;
	public float attackRange = 0.5f;
	public LayerMask enemyLayers;
	private GameObject ozljeda;
	[SerializeField] string[] tags;

	private void Start()
    {
        extraJumps = extraJumpsValue;
        _rigidbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		boxCollider = GetComponent<BoxCollider2D>();
	}



	private void Update()
    {
		WallJump();
		Attack();
		Movement();
		Flip();
		ExtraJump();

        //animator parametri
		anim.SetBool("run", movement != 0);
		anim.SetBool("grounded", isGrounded());
	}


    void ExtraJump()
    {
		//visestruki skok
		if (isGrounded() == true)
		{
			extraJumps = extraJumpsValue;
		}

		if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
		{
			_rigidbody.velocity = Vector2.up * JumpForce;
			extraJumps--;
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded() == true)
		{
			_rigidbody.velocity = Vector2.up * JumpForce;
		}

		if (Input.GetKey(KeyCode.Space) && isGrounded())
		{
			Jump();
		}

	}

	

	void Flip()
    {
		//okretnanje igraca u smjeru kretanja
		if (movement < 0 && facingRight)
		{
			flip();
		}
		else if (movement > 0 && !facingRight)
		{
			flip();
		}
	}

	void flip() //okret igraca
    {
        facingRight = !facingRight; 
        transform.Rotate(0f, 180f, 0f);
    }


	private void Jump() //skok
    {
		if (isGrounded())
		{
			_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce);
			anim.SetTrigger("jump");
		}
		else if (onWall() && !isGrounded()) //penjanje po zidu
		{
			if (movement == 0)
			{
				_rigidbody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
			}
			else
			{
				_rigidbody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
			}
		}
	}

	

	
	

    void WallJump()
    {
		//skok od zida
		if (wallJumpCooldown > 0.2f)
		{
			_rigidbody.velocity = new Vector2(movement * MovementSpeed, _rigidbody.velocity.y);

			if (onWall() && !isGrounded())
			{
				
				_rigidbody.gravityScale = 0;
				_rigidbody.velocity = Vector2.zero;
			}
			else
				_rigidbody.gravityScale = 1;

			if (Input.GetKey(KeyCode.Space))
				Jump();
		}
		else
			wallJumpCooldown += Time.deltaTime;
	}

	private bool isGrounded() //provjera podloge
    {
		RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
		return raycastHit.collider!=null;
    }


	private bool onWall() //provjera zida
	{
		RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
		return raycastHit.collider != null;
	}

	void Movement()
    {
		//horizontalno gibanje
		movement = Input.GetAxis("Horizontal");
		transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
	}

	




void Attack()
    {
		//klikom misa pozivamo funkciju attack
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			attack();
		}
	}



	void attack()
	{

		//animacija napada
        if (!isGrounded())
        {
			anim.SetTrigger("jumpAttack");
        }
        else
        {
			anim.SetTrigger("attack");
		}

        //radijus u kojem mač oštećuje
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);



        

		//damage
		foreach (Collider2D enemy1 in hitEnemies)
		{
			for (int i = 0; i < tags.Length; i++)
			{
                if (enemy1.CompareTag(tags[i]))
                {
					ozljeda = GameObject.FindWithTag(tags[i]);
					ozljeda.GetComponent<Health>().TakeDamage(1);

				}
			}
		}
	}
}


