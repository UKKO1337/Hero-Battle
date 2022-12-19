using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codebase.Logic;
using Codebase.Player;
using UnityEngine;

namespace Codebase.BattleManager
{
    public class PlayerTurn : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _playerUnits;

        public List<GameObject> PlayerUnits => _playerUnits;
        public event Action EveryoneIsDead;

        private int _currentUnit;

        private void Start()
        { 
            ChangeActiveUnit();
            
            foreach (GameObject unit in _playerUnits) 
                unit.GetComponent<Death>().DeathHappend += ClearDeadUnit;
        }

        public void ChangeActiveUnit()
        {
            if (PlayerUnits.Count > _currentUnit)
            {
                if (CurrentUnitIsDead()) 
                    _currentUnit++;
                
                SpawnAction();
            }
                
            else
                ResetActiveUnit();
        }

        private bool CurrentUnitIsDead() => 
            PlayerUnits[_currentUnit] == null && _currentUnit != PlayerUnits.Count;

        private void SpawnAction() => 
            PlayerUnits[_currentUnit++].GetComponent<ActionSpawner>().SpawnNewAction();

        public void ResetActiveUnit() => 
            _currentUnit = 0;

        private void ClearDeadUnit() => 
            StartCoroutine(CheckForDeadUnits());

        private IEnumerator CheckForDeadUnits()
        {
            yield return null;
            
            for(var i = _playerUnits.Count - 1; i > -1; i--)
            {
                if (_playerUnits[i] == null)
                    _playerUnits.RemoveAt(i);
            }

            if (_playerUnits.Count == 0) 
                EveryoneIsDead?.Invoke();
        }

        private void OnDestroy()
        {
          foreach (GameObject unit in _playerUnits.Where(unit => unit != null))
            unit.GetComponent<Death>().DeathHappend -= ClearDeadUnit;
        }
    }
}
