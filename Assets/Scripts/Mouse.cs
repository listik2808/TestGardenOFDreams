using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class Mouse : MonoBehaviour
    {
        [SerializeField] private Image _mouseItemIcon;

        private InventoryCell _enteredCell;
        private InventoryCell _draggingCell;
        private bool _isDraggingItem =false;
        private bool _isMoving = true;

        public InventoryCell EnteredCell => _enteredCell;
        public InventoryCell DraggingCell => _draggingCell;

        private void Update()
        {
            if (_isDraggingItem && DraggingCell.CellItem)
            {
                _mouseItemIcon.sprite = DraggingCell.CellItem.Icon;
                _mouseItemIcon.color = Color.white;
                _mouseItemIcon.rectTransform.position = Input.mousePosition;
            }
            else if(_isMoving && _isDraggingItem== false)
            {
                _mouseItemIcon.color = Color.clear;
                DiactivateImage();
            }
        }

        public void DraggingActivate(InventoryCell inventoryCell)
        {
            _isDraggingItem = true;
            _draggingCell = inventoryCell;
            _isMoving = true;
        }

        public void DraggingDeactivate()
        {
            _isDraggingItem = false;
            _draggingCell = null;
        }
        public void ActivateEnteredCell(InventoryCell inventoryCell)
        {
            _enteredCell = inventoryCell;
        }
        
        public void DiactivateEnteredCell()
        {
            _enteredCell = null;
        }

        public void Reset()
        {
            DraggingDeactivate();
            DiactivateEnteredCell();
        }

        private void DiactivateImage()
        {
            _isMoving = false;
        }
    }
}
