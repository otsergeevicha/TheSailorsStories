using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;
        
        private void OnEnable()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnDisable()
        {
            _saveLoadService.SaveProgress();
            
            Debug.Log("Progress Saved");
            gameObject.SetActive(false);
        }
    }
}