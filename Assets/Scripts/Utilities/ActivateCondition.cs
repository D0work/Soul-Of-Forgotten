using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ActivateCondition : MonoBehaviour
{
    public enum FieldType { Bool, Int, Float, String }
    public enum CompareMode { Equal, NotEqual, Greater, GreaterOrEqual, Less, LessOrEqual }
    public enum CombineMode { All, Any }

    [Serializable]
    public class Condition
    {
        public Component targetComponent;
        public string fieldName;
        public FieldType fieldType;
        public CompareMode compareMode;
        public bool boolValue;
        public int intValue;
        public float floatValue;
        public string stringValue;

        public bool Evaluate()
        {
            if (targetComponent == null || string.IsNullOrEmpty(fieldName))
                return false;

            var type = targetComponent.GetType();
            object rawValue = null;
            var prop = type.GetProperty(fieldName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (prop != null)
            {
                rawValue = prop.GetValue(targetComponent);
            }
            else
            {
                var field = type.GetField(fieldName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (field != null)
                    rawValue = field.GetValue(targetComponent);
                else
                {
                    Debug.LogWarning($"Field/Property '{fieldName}' not found on {type.Name}.");
                    return false;
                }
            }

            try
            {
                switch (fieldType)
                {
                    case FieldType.Bool:
                        bool bVal = Convert.ToBoolean(rawValue);
                        return compareMode == CompareMode.Equal
                            ? bVal == boolValue
                            : bVal != boolValue;

                    case FieldType.Int:
                        int iVal = Convert.ToInt32(rawValue);
                        switch (compareMode)
                        {
                            case CompareMode.Equal: return iVal == intValue;
                            case CompareMode.NotEqual: return iVal != intValue;
                            case CompareMode.Greater: return iVal > intValue;
                            case CompareMode.GreaterOrEqual: return iVal >= intValue;
                            case CompareMode.Less: return iVal < intValue;
                            case CompareMode.LessOrEqual: return iVal <= intValue;
                        }
                        break;

                    case FieldType.Float:
                        float fVal = Convert.ToSingle(rawValue);
                        switch (compareMode)
                        {
                            case CompareMode.Equal: return Mathf.Approximately(fVal, floatValue);
                            case CompareMode.NotEqual: return !Mathf.Approximately(fVal, floatValue);
                            case CompareMode.Greater: return fVal > floatValue;
                            case CompareMode.GreaterOrEqual: return fVal >= floatValue;
                            case CompareMode.Less: return fVal < floatValue;
                            case CompareMode.LessOrEqual: return fVal <= floatValue;
                        }
                        break;

                    case FieldType.String:
                        string sVal = rawValue as string ?? string.Empty;
                        return compareMode == CompareMode.Equal
                            ? sVal == stringValue
                            : sVal != stringValue;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"ActivateCondition Error: {ex.Message}");
            }

            return false;
        }
    }

    [Header("Conditions")]
    public CombineMode combineMode = CombineMode.All;
    public bool evaluateOnStart = false;
    public bool evaluateOnUpdate = true;
    public bool evaluateOnlyOnce = false;
    public List<Condition> conditions = new List<Condition>();

    [Header("Events")]
    public UnityEvent onConditionsMet;
    private bool _hasTriggered = false;

    private void Start()
    {
        if (evaluateOnStart)
            CheckConditions();
    }

    private void Update()
    {
        if (evaluateOnUpdate)
            CheckConditions();
    }

    public void CheckConditions()
    {
        if (evaluateOnlyOnce && _hasTriggered)
            return;

        bool result = (combineMode == CombineMode.All)
            ? conditions.All(c => c.Evaluate())
            : conditions.Any(c => c.Evaluate());

        if (result)
        {
            onConditionsMet.Invoke();
            _hasTriggered = true;
        }
    }

    public void ResetTrigger()
    {
        _hasTriggered = false;
    }
}
