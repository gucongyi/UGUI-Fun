using UnityEngine;
using System.Collections;

namespace UnityEngine.UI
{
    [System.Serializable]
    public class LoopScrollPrefabSource
    {
        public GameObject templateGo;
        public string poolName;
        public int poolSize = 5;

        private bool inited = false;
        public virtual GameObject GetObject()
        {
            //var go = GameObject.Instantiate(templateGo);
            //return go;
            if (!inited)
            {
                SG.ResourceManager.Instance.InitPool(poolName, templateGo, poolSize);
                inited = true;
            }
           
            var obj = SG.ResourceManager.Instance.GetObjectFromPool(poolName, templateGo);
            //Debug.LogError($"取一个，还剩下{SG.ResourceManager.Instance.PoolActiveCount(poolName)}");
            return obj;
        }

        public virtual void ReturnObject(Transform go)
        {
           
            go.SendMessage("ScrollCellReturn", SendMessageOptions.DontRequireReceiver);
            SG.ResourceManager.Instance.ReturnObjectToPool(go.gameObject);
            //Debug.LogError($"存一个，还剩下{SG.ResourceManager.Instance.PoolActiveCount(poolName)}");
        }
    }
}
