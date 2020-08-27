using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    static PoolManager instance;
    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PoolManager>();
            }

            return instance;
        }

    }



    //풀링사용예
    private void Start()
    {

        for (int i = 0; i < 1; i++)
        {
            GameObject go =  Instantiate("공격Prefab/ATK3test");
            Destroy(go);
        }
    }

    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>();


        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            
            return go.GetComponent<Poolable>();

        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.isUsing = false;

            _poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0 )
            {
                poolable = _poolStack.Pop();
            }else
            {
                poolable = Create();
            }
            poolable.gameObject.SetActive(true);
            poolable.transform.parent = parent;
            poolable.isUsing = true;
            return poolable;
        }

    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();


    public void Init()
    {
        
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = transform;

        _pool.Add(original.name, pool);

    }


    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if(_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);


        return _pool[original.name].Pop(parent);
    }


    // Resources.Load<T>(path);
    public GameObject GetOriginal(string name)
    {

        if (_pool.ContainsKey(name) == false)
        {
            return null;
        }
           

         return _pool[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        _pool.Clear();
    }


   
    public T Load<T>(string path) where T : Object
    {
        if(typeof(T) ==  typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = GetOriginal(name);
            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null )
    {
        GameObject original = Load<GameObject>(path);
        if(original == null)
        {
            Debug.Log($"Failed to load prefab :  {path}");
            return null;
        }

        if (original.GetComponent<Poolable>() != null)
            return Pop(original, parent).gameObject;

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;

        return go;

    }


    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Poolable poolable = go.GetComponent<Poolable>();
        if(poolable != null)
        {
            Push(poolable);
            return;
        }

        Object.Destroy(go);
    }

    public void Destroy(GameObject go , float time)
    {
        if (go == null)
            return;

        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            StartCoroutine(DelPush(poolable,time));
            return;
        }

        Object.Destroy(go);
    }

    IEnumerator DelPush(Poolable poolable , float time)
    {
        yield return new WaitForSeconds(time);
        Push(poolable);
    }



}
