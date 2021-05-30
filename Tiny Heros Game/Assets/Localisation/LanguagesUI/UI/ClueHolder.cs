using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localisation.Creator
{

    public class ClueHolder : MonoBehaviour
    {
        public List<ClueItem> _clues;
        public ClueItem cluePrefab;

        // Use this for initialization
        void Start()
        {
            _clues = new List<ClueItem>();
            cluePrefab.gameObject.SetActive(false);
        }

        public void AddClue()
        {
            ClueItem ix = Instantiate(cluePrefab, cluePrefab.transform.parent) as ClueItem;
            ix.transform.localScale = Vector3.one;
            ix.gameObject.SetActive(true);

            _clues.Add(ix);
        }

        public void AddClue(string hintId, string hintData)
        {
            ClueItem ix = Instantiate(cluePrefab, cluePrefab.transform.parent) as ClueItem;
            ix.transform.localScale = Vector3.one;
            ix.gameObject.SetActive(true);

            //Add the clue data
            ix.SetData(hintId, hintData);

            ix.gameObject.name = _clues.Count.ToString();

            _clues.Add(ix);
        }

        public void RemoveClue()
        {
            if (_clues.Count == 0)
            {
                return;
            }

            ClueItem hh = _clues[_clues.Count - 1];

            _clues.RemoveAt(_clues.Count - 1);

            Destroy(hh.gameObject);
        }

        public void Clear()
        {
            for (int i = 0; i < _clues.Count; i++)
            {
                Destroy(_clues[i].gameObject);
            }
            _clues.Clear();
        }

        public void PopulateData(Dictionary<string, string> mClues)
        {
            foreach (var item in mClues)
            {
                AddClue(item.Key, item.Value);
            }
        }
    }
}
