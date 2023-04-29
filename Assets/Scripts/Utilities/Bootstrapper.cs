using System.Threading.Tasks;
using UnityEngine;

public static class Bootstrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Boot()
    {
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Managers")));
    }
}