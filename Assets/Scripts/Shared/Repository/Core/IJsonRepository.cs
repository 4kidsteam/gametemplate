using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJsonRepository<T>
{
    public bool InitIfNotExisted(object value);

    public T Get();

    public void Save(object ob);

    public bool IsExisted();

    public void Clear();
}
