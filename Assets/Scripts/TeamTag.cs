
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamTag : MonoBehaviour
{
	static public List<TeamTag> goodguyList = new List<TeamTag>();
	static public List<TeamTag> badguyList = new List<TeamTag>();
	
	public bool goodguy = false;
    // Start is called before the first frame update
    void Start()
    {
        if(goodguy)
		{
			goodguyList.Add(this);
		}
		else
		{
			badguyList.Add(this);
		}
    }
	
	void OnDestroy()
	{
		if(goodguy)
		{
			goodguyList.Remove(this);
		}
		else
		{
			badguyList.Remove(this);
		}
	}

}
