using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs
{
	public float bump;
	public float follow;
	public float acceleration;
	public float fantome;
	float maxCumulableTimer = 100;
	public Vector3 bumpDir = Vector3.zero;
	public Buffs()
	{
		bump = 0;
		follow = 0;
		acceleration = 0;
		fantome = 0;
	}

	public void LoweringDebuffs(float timeToReduce)
	{
		bump = Mathf.Clamp(bump - timeToReduce, 0, 1);
		follow = Mathf.Clamp(follow - timeToReduce, 0, maxCumulableTimer);
		acceleration = Mathf.Clamp(acceleration - timeToReduce, 0, maxCumulableTimer);
		fantome = Mathf.Clamp(fantome - timeToReduce, 0, maxCumulableTimer);
	}
}

public class playerBuffs : MonoBehaviour {

public AnimationCurve animationCurve;
public Buffs buffActifs;
switchManager switchManager;
playerMovement pm;
Rigidbody rb;
playerShader playerShader;
	
	void Awake () {
		buffActifs = new Buffs();
		pm = GetComponent<playerMovement>();
		rb = GetComponent<Rigidbody>();
		switchManager = GameObject.Find("Master").GetComponent<switchManager>();
		playerShader = GetComponent<playerShader>();
	}
	
	
	void FixedUpdate () 
	{
		BuffManager();
		buffActifs.LoweringDebuffs(Time.deltaTime);
	}

	void BuffManager()
	{
		if (buffActifs.bump > 0)
		{
			float yy = animationCurve.Evaluate(Mathf.Clamp(1-buffActifs.bump - Time.deltaTime, 0,1));
			float y = animationCurve.Evaluate(Mathf.Clamp(1-buffActifs.bump, 0, 1));
			Vector3 v = buffActifs.bumpDir;
			v.y = (y - yy) * 25;
			v.z *= 1 - buffActifs.bump;
			if (gameObject == switchManager.actualPlayer)
			{
				rb.velocity = v * 16;
			}
			else
			{
				rb.velocity = v * 8;
			}
		}

		if (buffActifs.follow > 0 && switchManager.actualPlayer == gameObject)
		{
			buffActifs.follow = 0;
		}

		if (buffActifs.acceleration > 0)
		{
            if (pm.speed == pm.baseSpeed)
            {
                pm.speed = pm.baseSpeed * 2.5f;
            }
		}
		else
		{
            if (pm.speed != pm.baseSpeed)
            {
                pm.speed = pm.baseSpeed;
            }
		}

		if (buffActifs.fantome > 0)
		{
			if (playerShader.index == 0)
			{
				playerShader.PutDissolve();
			}
			
		}
		else if (buffActifs.fantome == 0 && playerShader.index != 0)
		{
			playerShader.PutBaseSkin();
		}
	}
}
