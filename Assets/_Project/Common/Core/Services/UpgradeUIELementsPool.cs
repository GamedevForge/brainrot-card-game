namespace Project.Core.Services
{
    public class UpgradeUIELementsPool
    {
        private readonly UnityEngine.Pool.ObjectPool<UpgradeUIElementsCreateData> _cardPool;
        private readonly UpgradeUIElementsFactory _factory;

        public UpgradeUIELementsPool(UpgradeUIElementsFactory factory, int poolMaxSize)
        {
            _factory = factory;
            _cardPool = new UnityEngine.Pool.ObjectPool<UpgradeUIElementsCreateData>(
                OnCreate,
                OnGet,
                OnRelease,
                OnDestroy,
                true,
                poolMaxSize);
        }

        private void OnGet(UpgradeUIElementsCreateData createdData) =>
            createdData.UIELementGameObject.SetActive(true);

        private void OnRelease(UpgradeUIElementsCreateData createdData) =>
            createdData.UIELementGameObject.SetActive(false);

        private void OnDestroy(UpgradeUIElementsCreateData createdData)
        {

        }

        private UpgradeUIElementsCreateData OnCreate() =>
            _factory.Create();

        public UpgradeUIElementsCreateData Get() =>
            _cardPool.Get();

        public void Release(UpgradeUIElementsCreateData createdData) =>
            _cardPool.Release(createdData);
    }
}