using UnityEngine;
using System;

using TMPro;
using Zenject;
using UniRx;

namespace TinyWar
{
    public interface IDevelopmentPointsAmountDataSource
    {
        IObservable<int> DevelopmentPointsChangeObservable { get; }
    }

    public class DevelopmentPointsAmountUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IDevelopmentPointsAmountDataSource _dataSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI _amountText = null;

        private DisposableList _disposables = new DisposableList();

        #endregion

        #region UNITY

        private void Awake()
        {
            _dataSource.DevelopmentPointsChangeObservable.Subscribe(UpdateAmount).AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        #endregion

        #region METHODS

        private void UpdateAmount(int newAmount)
        {
            _amountText.text = newAmount.ToString();
        }

        #endregion
    }
}
