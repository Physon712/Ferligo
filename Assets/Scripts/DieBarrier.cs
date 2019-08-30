using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBarrier : MonoBehaviour
{
   void OnTriggerEnter(Collider other){
	   if(other.transform.name == "Player")
	   {
			other.gameObject.GetComponent<Inventory>().GetHurt(999999f,999999f);
	   }
   }
}
