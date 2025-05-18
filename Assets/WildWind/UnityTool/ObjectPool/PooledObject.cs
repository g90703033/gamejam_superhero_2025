using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WildWind.ObjectPool
{
	public class PooledObject : MonoBehaviour
	{
		public ObjectPoolSystem pool;
		
		public bool InvokeReset = false;
		public UnityEvent resetEvent;
		
		public void BackToPool()
		{
			pool.BackToPool(gameObject);
			
			if(InvokeReset)
			{
				ResetObject();
			}
		}
		
		public void ResetObject()
		{
			resetEvent.Invoke();
		}
	}
}