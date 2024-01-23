using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CircleDiagram
{
    public class Diagram : MonoBehaviour
    {
        [Header("Visual")]
        [SerializeField] private LineRenderer _renderer;

        [SerializeField] private float _width;
        [SerializeField] private Vector3 _centerOffset;
        
        [SerializeField] private float _radius = 0.5f;
        [SerializeField] private int _pointsCount = 24;
        [Range(0 , 360)]
        [SerializeField] private float _degrees = 360f;
        
        [Range(0 , 4)]
        [SerializeField] private int _loopCount = 2;

        [SerializeField] private bool _addPointsOnEdges = false;
        [SerializeField] private float _smoothDegreesLenght = 0.2f;
        [SerializeField] private Vector3[] _mainCirclePoints;

        [Header("Parts")]
        [SerializeField] private DiagramPart _partPrefab;
        
        [SerializeField] private DiagramPartSO[] _partsData;
        [SerializeField] private List<DiagramPart> _parts;

        public void  GenerateCircleEditor()
        {
            if (_renderer == null || _pointsCount < 0)
                return;

            GenerateCircle();
        }
        
        public void UpdatePartsEditor()
        {
            if (_renderer == null || _pointsCount < 0)
                return;

            UpdateParts();
        }

        private void GenerateCircle()
        {
            _renderer.startWidth = _width;
            _renderer.endWidth = _width;
            
            CalculateMainCirclePoints();
            _renderer.positionCount = _mainCirclePoints.Length;
            _renderer.SetPositions(_mainCirclePoints);
            UpdateParts();
        }

        private void CalculateMainCirclePoints()
        {
            _mainCirclePoints = CalculateCirclePoints(_pointsCount, _degrees, _radius, _addPointsOnEdges);
        }

        private Vector3[] CalculateCirclePoints(int pointsCount , float totalDegrees , float radius = 1 , bool addPointOnEdges = false , int edgeEXTRA = 0)
        {
            // int extraPoints = addPointOnEdges ? 2 : 0;
            // Vector3[] points = new Vector3[pointsCount + _loopCount + extraPoints + edgeEXTRA];
            Vector3[] points = new Vector3[pointsCount + _loopCount + edgeEXTRA];
            
            float degreesPerPoint = totalDegrees / (pointsCount -1) ;
            float currentDegrees = 0;


            for (int i = 0 ; i < pointsCount + _loopCount + edgeEXTRA; i++)
            {
                // Last Point 
                if (i == pointsCount + _loopCount - 1)
                {
                    currentDegrees -= degreesPerPoint;
                    float degreesBetween = (totalDegrees - currentDegrees) / (edgeEXTRA + 1);
                        
                    for (int j = 0; j < edgeEXTRA + 1; j++)
                    {
                        currentDegrees += degreesBetween;
                        
                        points[i + j] = new Vector3(
                            Mathf.Sin(Mathf.Deg2Rad * currentDegrees) * radius,
                            Mathf.Cos(Mathf.Deg2Rad * currentDegrees) * radius,
                            0) + _centerOffset;
                    }
                    
                    break;
                }
                
                points[i] = new Vector3(
                    Mathf.Sin(Mathf.Deg2Rad * currentDegrees) * radius,
                    Mathf.Cos(Mathf.Deg2Rad * currentDegrees) * radius,
                    0) + _centerOffset;

                currentDegrees += degreesPerPoint;
            }

            return points;
        }

        private void UpdateParts()
        {
            Debug.Log("Update Parts");
            DeleteAllParts();

            for (int i = 0; i < _partsData.Length; i++)
            {
                DiagramPart part = SpawnPart();
                part.Init(GetPointsForPart(_partsData[i]));
            }
        }

        private DiagramPart SpawnPart()
        {
            DiagramPart part = Instantiate(_partPrefab, transform);
            _parts.Add(part);

            return part;
        }

        private void DeleteAllParts()
        {
            for (int i = _parts.Count - 1; i >= 0; i--)
            {
                DiagramPart part = _parts[i];
                
                _parts.RemoveAt(i);
                part.Destroy();
            }
        }

        private Vector3[] GetPointsForPart(DiagramPartSO partData)
        {
            int pointsCount = Mathf.RoundToInt(partData.degrees / (_degrees / _pointsCount));
            float radius = _radius;
            radius += (partData.width - _width) / 2;
                
            Vector3[] points = CalculateCirclePoints(pointsCount , partData.degrees , radius , _addPointsOnEdges , 12);
            
            Debug.Log(points);
            return points;
        }

        private void DrawMainCircle()
        {
            _renderer.positionCount = _pointsCount + _loopCount + 2;
            _renderer.SetPositions(_mainCirclePoints);
        }
    }
}