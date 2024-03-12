using Scripts.View;

namespace Scripts.Controllers
{
    public class ScreenController
    {
        private readonly InventoryService _service;
        private readonly ScreenView _view;

        private InventoryGridController _currentController;
        
        public ScreenController(InventoryService service, ScreenView view)
        {
            _service = service;
            _view = view;
        }

        public void OpenInventory(string ownerId)
        {
            var inventory = _service.GetInventory(ownerId);
            var inventoryView = _view.InventoryView;
            _currentController = new InventoryGridController(inventory, inventoryView);
        }
    }
}