using DG.Tweening;
using UnityEngine;
using VContainer;

namespace Sheeps
{
    public class Sheep : MonoBehaviour
    {
        [Inject] private Transform playerTransform;

        private void Start()
        {
            transform.DOMove(playerTransform.position, 3f);
        }
    }
}
