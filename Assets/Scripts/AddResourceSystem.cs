using DG.Tweening;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

namespace Source.Scripts.Systems.Game
{
    public class AddResourceSystem : Singleton<AddResourceSystem>
    {
        [SerializeField] private GameObject prefab;

        private Camera _cam;
        private UIManager _uiManager;

        public void Start()
        {
            _uiManager = FindObjectOfType<UIManager>();
            _cam = Camera.main;
        }

        public void AnimateMoney(Vector3 position)
        {
            var coinSpawnPos = _cam.WorldToScreenPoint(position);
            var flyPos = _uiManager.OrangeIcon.position;
            
            var resource = Instantiate(prefab, coinSpawnPos, Quaternion.identity, _uiManager.ResultUi.transform);
            var coinAnim = DOTween.Sequence();

            coinAnim.Append(resource.transform.DOMove(new Vector3(coinSpawnPos.x + Random.Range(-150f, 150f), coinSpawnPos.y + Random.Range(-150f, 150f), 0), 0.3f));
            coinAnim.Join(resource.transform.DOScale(1.5f, 0.3f));
            coinAnim.Append(resource.transform.DOMove(flyPos, Random.Range(0.3f, 0.5f)));
            coinAnim.Join(resource.transform.DOScale(1f, 0.15f));
            coinAnim.OnComplete(() =>
            {
                YandexGame.savesData.oranges++;
                _uiManager.UpdateOrangeScore();
                Destroy(resource.gameObject);
            });
        }
    }
}