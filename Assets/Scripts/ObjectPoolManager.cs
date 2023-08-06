using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ObjectPoolManager : MonoBehaviour
{

    #region Inspector Variables

    //Your prefab to be pooled to memory.
    [SerializeField] private GameObject _prefab;

    //Total count of the objects to be generated in the pool.
    [SerializeField] private int _objectPoolCount;

    //If checked true, all objects will stay under this gameobject in hierarchy.
    [SerializeField] private bool _parentToThisObject = true;

    #endregion

    #region Private Variables
    //A queue to hold/store all pooled objects.
    private Queue<GameObject> _objectPoolQueue = new Queue<GameObject>();
    #endregion

    #region Singleton
    public static ObjectPoolManager Instance;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        //Creating a singleton.
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        //Creating object pool.
        InitializeObjectPool();
    }
    #endregion

    #region Private Methods
    //Object Pool Logic
    private void InitializeObjectPool()
    {
        //Iterating till the total count.
        for (int i = 0; i < _objectPoolCount; i++)
        {
            GameObject tempGO = Instantiate(_prefab);

            //Enqueuing the generated object to the memory.
            _objectPoolQueue.Enqueue(tempGO);

            if (_parentToThisObject)
                tempGO.transform.SetParent(this.transform);

            //Setting name for the object.
            tempGO.SetActive(false);
        }
    }
    #endregion

    #region Public Methods
    //This method returns the object from the memory.
    public GameObject GetObject()
    {
        if (_objectPoolQueue.Count > 0)
        {
            //Dequeue an object from the queue and allow to be used in the game.
            GameObject obj = _objectPoolQueue.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            //Generate a new object and return if none of the objects are available in the memory.
            //This is an edge case.
            GameObject obj = Instantiate(_prefab);
            return obj;
        }
    }

    //Use this method to return an object back to the memory after the use.
    public void ReturnObject(GameObject obj)
    {
        if (obj != null)
        {
            _objectPoolQueue.Enqueue(obj);
            obj.SetActive(false);
        }
    }
    #endregion
}
