using Providers;
using TMPro;
using UnityEngine;
using VContainer;

namespace MainMenu.UI
{
    public class MenuUISystem : MonoBehaviour
    {
        [Inject] private UpdateProvider updateProvider;
        [Inject] private MenuSceneLoader MenuSceneLoader;
        
        [Header("Texts")] [Space]
        [SerializeField] private TMP_Text gameCounterText;

        private float countTime = 3;
        
        public void Init()
        {
            updateProvider.StartCounterUpdate += StartCounter;
        }

        private void StartCounter()
        {
            if (countTime <= 0) return;
            
            countTime -= Time.deltaTime;
            gameCounterText.text = ((int)countTime).ToString();

            if (countTime <= 0)
            {
                TimeCounterFinished();
            }
            
        }

        private void TimeCounterFinished()
        {
            countTime = 0;
            updateProvider.StartCounterUpdate -= StartCounter;
            
            MenuSceneLoader.LoadGameSceneAsync();
            gameObject.SetActive(false);
        }
        
    }
}
