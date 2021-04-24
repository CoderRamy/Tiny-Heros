using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

public class BotController : MonoBehaviour
{

	private Animator animator;

	public float walkspeed = 5;
	private float horizontal;
	private float vertical;
	private float rotationDegreePerSecond = 1000;
	private bool isAttacking = false;

	public GameObject gamecam;
	public Vector2 camPosition;
	private bool dead;


	public GameObject[] characters;
	public int currentChar = 0;

	NavMeshAgent agent;
	GameObject[] Players;

	public float botDestance = 2f;
	public float attackRate = 3f;
	public float forgetDestance = 6f;
	public float detectDestance = 3f;

	public GameObject[] waypoints;

	public bool playerPicking = false;

	[SerializeField]
	List<Transform> Targets;

	[SerializeField]
	Transform pickedTarget;

	void Start()
	{
		Players = GameObject.FindGameObjectsWithTag("Player");
		agent = GetComponent<NavMeshAgent>();

		
		//agent.speed = _agentSpeed;
		//agent.isStopped = false;

		setCharacter(0);
	}

 

	void FixedUpdate()
	{
        //if (animator && !dead)
        //{
        //	//walk
        //	horizontal = Input.GetAxis("Horizontal");
        //	vertical = Input.GetAxis("Vertical");

        //	Vector3 stickDirection = new Vector3(horizontal, 0, vertical);
        //	float speedOut;

        //	if (stickDirection.sqrMagnitude > 1) stickDirection.Normalize();

        //	if (!isAttacking)
        //		speedOut = stickDirection.sqrMagnitude;
        //	else
        //		speedOut = 0;

        //	if (stickDirection != Vector3.zero && !isAttacking)
        //		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(stickDirection, Vector3.up), rotationDegreePerSecond * Time.deltaTime);
        //	GetComponent<Rigidbody>().velocity = transform.forward * speedOut * walkspeed + new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);

        //	animator.SetFloat("Speed", speedOut);

        //}


        //if (!dead)
        //{
        //	foreach (var player in Players)
        //	{
        //		if (Vector3.Distance(transform.position, player.transform.position) > 3)
        //		{
        //			transform.LookAt(player.transform);
        //			agent.destination = player.transform.position;
        //		}
        //	}
        //}

	
		  
		}

	IEnumerator PickPlayer()
    {
		Targets.Clear();
		playerPicking = true;
		yield return new WaitForSeconds(3);
		Debug.Log("pick player");

		foreach (var player in Players)
		{
			if (Vector3.Distance(transform.position, player.transform.position) < detectDestance)
            {
                if (!Targets.Contains(player.transform))
                {
					Targets.Add(player.transform);
				}
				
			}
		}

		if(Targets.Count > 0 && pickedTarget == null)
        {
			int targetNumber = Random.Range(0, Targets.Count);
			pickedTarget = Targets[targetNumber]; //set target
		}

		playerPicking = false;
    }



	void Update()
	{
		

		if (!dead)
		{
			if (!playerPicking)
			{
				StartCoroutine(PickPlayer());
			}

			if (Vector3.Distance(transform.position, pickedTarget.transform.position) > (botDestance + forgetDestance))
			{
				pickedTarget = null;
				agent.destination = transform.position;
				animator.SetFloat("Speed", 0);
				StartCoroutine(PickPlayer());
				return;
			}

			if (Vector3.Distance(transform.position, pickedTarget.transform.position) > botDestance)
            {
                agent.destination = pickedTarget.transform.position;
                animator.SetFloat("Speed", agent.speed);
            }
            else
            {
                agent.destination = transform.position;
                animator.SetFloat("Speed", 0);
                //attack code
                if (!isAttacking)
                {
                    isAttacking = true;
                    animator.SetTrigger("Attack");
                    StartCoroutine(stopAttack(attackRate));
                    activateTrails(true);
                }

                animator.SetBool("isAttacking", isAttacking);
            }



            // move camera
            if (gamecam)
				gamecam.transform.position = transform.position + new Vector3(0, camPosition.x, -camPosition.y);

			// attack

	

			//switch character

			if (Input.GetKeyDown("left"))
			{
				setCharacter(-1);
				isAttacking = true;
				StartCoroutine(stopAttack(1f));
			}

			if (Input.GetKeyDown("right"))
			{
				setCharacter(1);
				isAttacking = true;
				StartCoroutine(stopAttack(1f));
			}

			// death
			if (Input.GetKeyDown("m"))
				StartCoroutine(selfdestruct());
		}

	}

	public IEnumerator stopAttack(float lenght)
	{
		yield return new WaitForSeconds(lenght); // attack lenght
		isAttacking = false;
		activateTrails(false);
	}

	public IEnumerator selfdestruct()
	{
		animator.SetTrigger("isDead");
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		dead = true;

		yield return new WaitForSeconds(1.3f);
		GameObject.FindWithTag("GameController").GetComponent<gameContoller>().resetLevel();
	}

	public void setCharacter(int i)
	{
	
		currentChar += i;

		if (currentChar > characters.Length - 1)
			currentChar = 0;
		if (currentChar < 0)
			currentChar = characters.Length - 1;

		foreach (GameObject child in characters)
		{
			if (child == characters[currentChar])
				child.SetActive(true);
			else
			{
				child.SetActive(false);

				if (child.GetComponent<triggerProjectile>())
					child.GetComponent<triggerProjectile>().clearProjectiles();
			}
		}

		animator = GetComponentInChildren<Animator>();
	}

	public void activateTrails(bool state)
	{
		var tails = GetComponentsInChildren<TrailRenderer>();
		foreach (TrailRenderer tt in tails)
		{
			tt.enabled = state;
		}
	}
}
