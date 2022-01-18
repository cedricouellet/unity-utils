using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LevelManager : PersistentSingleton<LevelManager>
{
    [Header("Animation")]
    [SerializeField]
    private Animator _fadeAnimator;

    [SerializeField]
    private string _fadeOutTrigger, _fadeInTrigger;

    [Header("Duration")]
    [SerializeField]
    private float _transitionDuration;

    /// <summary>
    /// Load a scene with a transition
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void LoadScene(string sceneName)
    {
        _fadeAnimator.SetTrigger(_fadeOutTrigger);

        StartCoroutine(WaitFor(_transitionDuration));

        SceneManager.LoadSceneAsync(sceneName);

        _fadeAnimator.SetTrigger(_fadeInTrigger);
    }

    /// <summary>
    /// Yields a coroutine for the given duration.
    /// </summary>
    /// <param name="duration">The duration for which to delay</param>
    /// <returns>The coroutine for the given duration</returns>
    private IEnumerator WaitFor(float duration)
    {
        yield return duration.GetWait();
    }
}
